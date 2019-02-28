using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DM.Services.Core.Implementation;
using DM.Services.DataAccess.BusinessObjects.Common;
using DM.Services.DataAccess.MongoIntegration;
using MongoDB.Driver;

namespace DM.Services.Common.Repositories
{
    public class UnreadCountersRepository : MongoCollectionRepository<UnreadCounter>, IUnreadCountersRepository
    {
        private readonly IDateTimeProvider dateTimeProvider;

        public UnreadCountersRepository(DmMongoClient client,
            IDateTimeProvider dateTimeProvider) : base(client)
        {
            this.dateTimeProvider = dateTimeProvider;
        }

        public Task Create(Guid entityId, Guid parentId, UnreadEntryType entryType)
        {
            return Collection.InsertOneAsync(new UnreadCounter
            {
                UserId = Guid.Empty,
                EntityId = entityId,
                ParentId = parentId,
                EntryType = entryType,
                LastRead = dateTimeProvider.Now,
                Counter = 0
            });
        }

        public Task Increment(Guid entityId, UnreadEntryType entryType)
        {
            return Collection.UpdateManyAsync(
                Filter.Eq(c => c.EntityId, entityId) &
                Filter.Eq(c => c.EntryType, entryType),
                Update.Inc(c => c.Counter, 1));
        }

        public Task Decrement(Guid entityId, UnreadEntryType entryType, DateTime createDate)
        {
            return Collection.UpdateManyAsync(
                Filter.Eq(c => c.EntityId, entityId) &
                Filter.Eq(c => c.EntryType, entryType) &
                Filter.Lt(c => c.LastRead, createDate),
                Update.Inc(c => c.Counter, -1));
        }

        public async Task<IDictionary<Guid, int>> SelectByParents(
            Guid userId, UnreadEntryType entryType, params Guid[] parentIds)
        {
            var userIds = new[] {userId, Guid.Empty}.Distinct();
            var counters = (await Collection.Aggregate()
                    .Match(
                        Filter.In(c => c.UserId, userIds) &
                        Filter.In(c => c.ParentId, parentIds) &
                        Filter.Eq(c => c.EntryType, entryType))
                    .Group(c => c.EntityId,
                        g => new UnreadCounter
                        {
                            EntityId = g.First().EntityId,
                            ParentId = g.First().ParentId,
                            Counter = g.Min(c => c.Counter)
                        })
                    .Group(c => c.ParentId,
                        g => new UnreadCounter
                        {
                            EntityId = g.First().ParentId,
                            Counter = g.Sum(c => c.Counter > 0 ? 1 : 0)
                        })
                    .ToListAsync())
                .ToDictionary(c => c.EntityId, c => c.Counter);
            return parentIds.ToDictionary(id => id, id => counters.TryGetValue(id, out var counter) ? counter : 0);
        }

        public async Task<IDictionary<Guid, int>> SelectByEntities(
            Guid userId, UnreadEntryType entryType, params Guid[] entityIds)
        {
            var userIds = new[] {userId, Guid.Empty}.Distinct();
            var counters = (await Collection.Aggregate()
                    .Match(
                        Filter.In(c => c.UserId, userIds) &
                        Filter.In(c => c.EntityId, entityIds) &
                        Filter.Eq(c => c.EntryType, entryType))
                    .Group(c => c.EntityId,
                        g => new UnreadCounter
                        {
                            EntityId = g.First().EntityId,
                            Counter = g.Min(c => c.Counter)
                        })
                    .ToListAsync())
                .ToDictionary(c => c.EntityId, c => c.Counter);
            return entityIds.ToDictionary(id => id, id => counters.TryGetValue(id, out var counter) ? counter : 0);
        }

        public async Task Flush(Guid userId, UnreadEntryType entryType, Guid entityId)
        {
            var counter = await Collection.Find(
                    Filter.Eq(c => c.EntityId, entityId) &
                    Filter.Eq(c => c.EntryType, entryType))
                .FirstOrDefaultAsync();

            await Collection
                .ReplaceOneAsync(
                    Filter.Eq(c => c.UserId, userId) &
                    Filter.Eq(c => c.EntityId, entityId) &
                    Filter.Eq(c => c.EntryType, entryType),
                    new UnreadCounter
                    {
                        UserId = userId,
                        EntityId = entityId,
                        ParentId = counter.ParentId,
                        EntryType = entryType,
                        LastRead = dateTimeProvider.Now,
                        Counter = 0
                    },
                    new UpdateOptions {IsUpsert = true});
        }

        public async Task FlushAll(Guid userId, UnreadEntryType entryType, Guid parentId)
        {
            var entityIds = await Collection.Distinct(c => c.EntityId,
                    Filter.Eq(c => c.ParentId, parentId) &
                    Filter.Eq(c => c.EntryType, entryType))
                .ToListAsync();
            var rightNow = dateTimeProvider.Now;

            await Collection.BulkWriteAsync(entityIds
                .Select(id => new ReplaceOneModel<UnreadCounter>(
                        Filter.Eq(c => c.UserId, userId) &
                        Filter.Eq(c => c.EntityId, id) &
                        Filter.Eq(c => c.EntryType, entryType),
                        new UnreadCounter
                        {
                            UserId = userId,
                            EntityId = id,
                            ParentId = parentId,
                            EntryType = entryType,
                            LastRead = rightNow,
                            Counter = 0
                        })
                    {IsUpsert = true}));
        }
    }
}