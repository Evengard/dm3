﻿// <auto-generated />
using System;
using DM.Services.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DM.Services.DataAccess.Migrations
{
    /// <summary>
    /// 
    /// </summary>
    [DbContext(typeof(DmDbContext))]
    [Migration("20191016152222_ProfileMerge")]
    partial class ProfileMerge
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Administration.Ban", b =>
                {
                    b.Property<Guid>("BanId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessRestrictionPolicy");

                    b.Property<string>("Comment");

                    b.Property<DateTimeOffset>("EndDate");

                    b.Property<bool>("IsRemoved");

                    b.Property<bool>("IsVoluntary");

                    b.Property<Guid>("ModeratorId");

                    b.Property<DateTimeOffset>("StartDate");

                    b.Property<Guid>("UserId");

                    b.HasKey("BanId");

                    b.HasIndex("ModeratorId");

                    b.HasIndex("UserId");

                    b.ToTable("Bans");
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Administration.Report", b =>
                {
                    b.Property<Guid>("ReportId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Answer");

                    b.Property<Guid?>("AnswerAuthorId");

                    b.Property<string>("Comment");

                    b.Property<DateTimeOffset>("CreateDate");

                    b.Property<string>("ReportText");

                    b.Property<Guid>("TargetId");

                    b.Property<Guid>("UserId");

                    b.HasKey("ReportId");

                    b.HasIndex("AnswerAuthorId");

                    b.HasIndex("TargetId");

                    b.HasIndex("UserId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Administration.Warning", b =>
                {
                    b.Property<Guid>("WarningId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreateDate");

                    b.Property<Guid>("EntityId");

                    b.Property<bool>("IsRemoved");

                    b.Property<Guid>("ModeratorId");

                    b.Property<int>("Points");

                    b.Property<string>("Text");

                    b.Property<Guid>("UserId");

                    b.HasKey("WarningId");

                    b.HasIndex("EntityId");

                    b.HasIndex("ModeratorId");

                    b.HasIndex("UserId");

                    b.ToTable("Warnings");
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Common.Comment", b =>
                {
                    b.Property<Guid>("CommentId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreateDate");

                    b.Property<Guid>("EntityId");

                    b.Property<bool>("IsRemoved");

                    b.Property<DateTimeOffset?>("LastUpdateDate");

                    b.Property<string>("Text");

                    b.Property<Guid>("UserId");

                    b.HasKey("CommentId");

                    b.HasIndex("EntityId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Common.Like", b =>
                {
                    b.Property<Guid>("LikeId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("EntityId");

                    b.Property<Guid>("UserId");

                    b.HasKey("LikeId");

                    b.HasIndex("EntityId");

                    b.HasIndex("UserId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Common.Review", b =>
                {
                    b.Property<Guid>("ReviewId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreateDate");

                    b.Property<bool>("IsApproved");

                    b.Property<string>("Text");

                    b.Property<Guid>("UserId");

                    b.HasKey("ReviewId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Common.Tag", b =>
                {
                    b.Property<Guid>("TagId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("TagGroupId");

                    b.Property<string>("Title");

                    b.HasKey("TagId");

                    b.HasIndex("TagGroupId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Common.TagGroup", b =>
                {
                    b.Property<Guid>("TagGroupId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title");

                    b.HasKey("TagGroupId");

                    b.ToTable("TagGroups");
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Common.Upload", b =>
                {
                    b.Property<Guid>("UploadId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreateDate");

                    b.Property<Guid?>("EntityId");

                    b.Property<string>("FileName");

                    b.Property<bool>("IsRemoved");

                    b.Property<Guid>("UserId");

                    b.Property<string>("VirtualPath");

                    b.HasKey("UploadId");

                    b.HasIndex("EntityId");

                    b.HasIndex("UserId");

                    b.ToTable("Uploads");
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Fora.Forum", b =>
                {
                    b.Property<Guid>("ForumId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreateTopicPolicy");

                    b.Property<string>("Description");

                    b.Property<int>("Order");

                    b.Property<string>("Title");

                    b.Property<int>("ViewPolicy");

                    b.HasKey("ForumId");

                    b.ToTable("Fora");
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Fora.ForumModerator", b =>
                {
                    b.Property<Guid>("ForumModeratorId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ForumId");

                    b.Property<Guid>("UserId");

                    b.HasKey("ForumModeratorId");

                    b.HasIndex("ForumId");

                    b.HasIndex("UserId");

                    b.ToTable("ForumModerators");
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Fora.ForumTopic", b =>
                {
                    b.Property<Guid>("ForumTopicId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Attached");

                    b.Property<bool>("Closed");

                    b.Property<DateTimeOffset>("CreateDate");

                    b.Property<Guid>("ForumId");

                    b.Property<bool>("IsRemoved");

                    b.Property<Guid?>("LastCommentId");

                    b.Property<string>("Text");

                    b.Property<string>("Title");

                    b.Property<Guid>("UserId");

                    b.HasKey("ForumTopicId");

                    b.HasIndex("ForumId");

                    b.HasIndex("LastCommentId");

                    b.HasIndex("UserId");

                    b.ToTable("ForumTopics");
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Games.Characters.Attributes.CharacterAttribute", b =>
                {
                    b.Property<Guid>("CharacterAttributeId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AttributeId");

                    b.Property<Guid>("CharacterId");

                    b.Property<string>("Value");

                    b.HasKey("CharacterAttributeId");

                    b.HasIndex("CharacterId");

                    b.ToTable("CharacterAttributes");
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Games.Characters.Character", b =>
                {
                    b.Property<Guid>("CharacterId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessPolicy");

                    b.Property<int>("Alignment");

                    b.Property<string>("Appearance");

                    b.Property<string>("Class");

                    b.Property<DateTimeOffset>("CreateDate");

                    b.Property<Guid>("GameId");

                    b.Property<string>("Inventory");

                    b.Property<bool>("IsNpc");

                    b.Property<bool>("IsRemoved");

                    b.Property<DateTimeOffset?>("LastUpdateDate");

                    b.Property<string>("Name");

                    b.Property<string>("Race");

                    b.Property<string>("Skills");

                    b.Property<int>("Status");

                    b.Property<string>("Story");

                    b.Property<string>("Temper");

                    b.Property<Guid>("UserId");

                    b.HasKey("CharacterId");

                    b.HasIndex("GameId");

                    b.HasIndex("UserId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Games.Game", b =>
                {
                    b.Property<Guid>("GameId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AssistantId");

                    b.Property<Guid>("AttributeSchemaId");

                    b.Property<int>("CommentariesAccessMode");

                    b.Property<DateTimeOffset>("CreateDate");

                    b.Property<bool>("DisableAlignment");

                    b.Property<bool>("HideDiceResult");

                    b.Property<bool>("HideInventory");

                    b.Property<bool>("HideSkills");

                    b.Property<bool>("HideStory");

                    b.Property<bool>("HideTemper");

                    b.Property<string>("Info");

                    b.Property<bool>("IsRemoved");

                    b.Property<Guid>("MasterId");

                    b.Property<Guid?>("NannyId");

                    b.Property<string>("Notepad");

                    b.Property<DateTimeOffset?>("ReleaseDate");

                    b.Property<string>("SettingName");

                    b.Property<bool>("ShowPrivateMessages");

                    b.Property<int>("Status");

                    b.Property<string>("SystemName");

                    b.Property<string>("Title");

                    b.HasKey("GameId");

                    b.HasIndex("AssistantId");

                    b.HasIndex("MasterId");

                    b.HasIndex("NannyId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Games.Links.BlackListLink", b =>
                {
                    b.Property<Guid>("BlackListLinkId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("GameId");

                    b.Property<Guid>("UserId");

                    b.HasKey("BlackListLinkId");

                    b.HasIndex("GameId");

                    b.HasIndex("UserId");

                    b.ToTable("BlackListLinks");
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Games.Links.CharacterRoomLink", b =>
                {
                    b.Property<Guid>("CharacterRoomLinkId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CharacterId");

                    b.Property<Guid>("RoomId");

                    b.HasKey("CharacterRoomLinkId");

                    b.HasIndex("CharacterId");

                    b.HasIndex("RoomId");

                    b.ToTable("CharacterRoomLinks");
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Games.Links.GameTag", b =>
                {
                    b.Property<Guid>("GameTagId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("GameId");

                    b.Property<Guid>("TagId");

                    b.HasKey("GameTagId");

                    b.HasIndex("GameId");

                    b.HasIndex("TagId");

                    b.ToTable("GameTags");
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Games.Links.PostAnticipation", b =>
                {
                    b.Property<Guid>("PostAnticipationId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreateDate");

                    b.Property<Guid>("RoomId");

                    b.Property<Guid>("TargetId");

                    b.Property<Guid>("UserId");

                    b.HasKey("PostAnticipationId");

                    b.HasIndex("RoomId");

                    b.HasIndex("TargetId");

                    b.HasIndex("UserId");

                    b.ToTable("PostWaitNotifications");
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Games.Links.Reader", b =>
                {
                    b.Property<Guid>("ReaderId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("GameId");

                    b.Property<Guid>("UserId");

                    b.HasKey("ReaderId");

                    b.HasIndex("GameId");

                    b.HasIndex("UserId");

                    b.ToTable("Readers");
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Games.Posts.Post", b =>
                {
                    b.Property<Guid>("PostId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CharacterId");

                    b.Property<string>("Commentary");

                    b.Property<DateTimeOffset>("CreateDate");

                    b.Property<bool>("IsRemoved");

                    b.Property<DateTimeOffset?>("LastUpdateDate");

                    b.Property<string>("MasterMessage");

                    b.Property<Guid>("RoomId");

                    b.Property<string>("Text");

                    b.Property<Guid>("UserId");

                    b.HasKey("PostId");

                    b.HasIndex("CharacterId");

                    b.HasIndex("RoomId");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Games.Posts.Room", b =>
                {
                    b.Property<Guid>("RoomId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessType");

                    b.Property<Guid>("GameId");

                    b.Property<bool>("IsRemoved");

                    b.Property<Guid?>("NextRoomId");

                    b.Property<double>("OrderNumber");

                    b.Property<Guid?>("PreviousRoomId");

                    b.Property<string>("Title");

                    b.Property<int>("Type");

                    b.HasKey("RoomId");

                    b.HasIndex("GameId");

                    b.HasIndex("NextRoomId")
                        .IsUnique();

                    b.HasIndex("PreviousRoomId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Games.Rating.Vote", b =>
                {
                    b.Property<Guid>("VoteId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreateDate");

                    b.Property<Guid>("GameId");

                    b.Property<Guid>("PostId");

                    b.Property<short>("SignValue");

                    b.Property<Guid>("TargetUserId");

                    b.Property<int>("Type");

                    b.Property<Guid>("UserId");

                    b.HasKey("VoteId");

                    b.HasIndex("GameId");

                    b.HasIndex("PostId");

                    b.HasIndex("TargetUserId");

                    b.HasIndex("UserId");

                    b.ToTable("Votes");
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Messaging.Conversation", b =>
                {
                    b.Property<Guid>("ConversationId")
                        .ValueGeneratedOnAdd();

                    b.HasKey("ConversationId");

                    b.ToTable("Conversations");
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Messaging.Message", b =>
                {
                    b.Property<Guid>("MessageId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ConversationId");

                    b.Property<DateTimeOffset>("CreateDate");

                    b.Property<bool>("IsRemoved");

                    b.Property<string>("Text");

                    b.Property<Guid>("UserId");

                    b.HasKey("MessageId");

                    b.HasIndex("ConversationId");

                    b.HasIndex("UserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Messaging.UserConversationLink", b =>
                {
                    b.Property<Guid>("UserConversationLinkId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ConversationId");

                    b.Property<bool>("IsRemoved");

                    b.Property<Guid>("UserId");

                    b.HasKey("UserConversationLinkId");

                    b.HasIndex("ConversationId");

                    b.HasIndex("UserId");

                    b.ToTable("UserConversationLinks");
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Users.Token", b =>
                {
                    b.Property<Guid>("TokenId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreateDate");

                    b.Property<bool>("IsRemoved");

                    b.Property<int>("Type");

                    b.Property<Guid>("UserId");

                    b.HasKey("TokenId");

                    b.HasIndex("UserId");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Users.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessPolicy");

                    b.Property<bool>("Activated");

                    b.Property<bool>("CanMerge");

                    b.Property<string>("Email");

                    b.Property<string>("Icq");

                    b.Property<string>("Info");

                    b.Property<bool>("IsRemoved");

                    b.Property<DateTimeOffset?>("LastVisitDate");

                    b.Property<string>("Location");

                    b.Property<string>("Login");

                    b.Property<Guid?>("MergeRequested");

                    b.Property<string>("Name");

                    b.Property<string>("PasswordHash");

                    b.Property<int>("QualityRating");

                    b.Property<int>("QuantityRating");

                    b.Property<bool>("RatingDisabled");

                    b.Property<DateTimeOffset>("RegistrationDate");

                    b.Property<int>("Role");

                    b.Property<string>("Salt");

                    b.Property<bool>("ShowEmail");

                    b.Property<string>("Skype");

                    b.Property<string>("Status");

                    b.Property<string>("TimezoneId");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Administration.Ban", b =>
                {
                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Users.User", "Moderator")
                        .WithMany("BansGiven")
                        .HasForeignKey("ModeratorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Users.User", "User")
                        .WithMany("BansReceived")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Administration.Report", b =>
                {
                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Users.User", "AnswerAuthor")
                        .WithMany("ReportsAnswered")
                        .HasForeignKey("AnswerAuthorId");

                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Users.User", "Target")
                        .WithMany("ReportsTaken")
                        .HasForeignKey("TargetId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Users.User", "Author")
                        .WithMany("ReportsGiven")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Administration.Warning", b =>
                {
                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Common.Comment", "Comment")
                        .WithMany("Warnings")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Users.User", "Moderator")
                        .WithMany("WarningsGiven")
                        .HasForeignKey("ModeratorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Users.User", "User")
                        .WithMany("WarningsReceived")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Common.Comment", b =>
                {
                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Fora.ForumTopic", "Topic")
                        .WithMany("Comments")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Games.Game", "Game")
                        .WithMany("Comments")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Users.User", "Author")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Common.Like", b =>
                {
                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Common.Comment", "Comment")
                        .WithMany("Likes")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Fora.ForumTopic", "Topic")
                        .WithMany("Likes")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Common.Review", "Review")
                        .WithMany("Likes")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Users.User", "User")
                        .WithMany("Likes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Common.Review", b =>
                {
                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Users.User", "Author")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Common.Tag", b =>
                {
                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Common.TagGroup", "TagGroup")
                        .WithMany("Tags")
                        .HasForeignKey("TagGroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Common.Upload", b =>
                {
                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Games.Characters.Character", "Character")
                        .WithMany("Pictures")
                        .HasForeignKey("EntityId");

                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Games.Game", "Game")
                        .WithMany("Pictures")
                        .HasForeignKey("EntityId");

                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Games.Posts.Post", "Post")
                        .WithMany("Attachments")
                        .HasForeignKey("EntityId");

                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Users.User", "UserProfile")
                        .WithMany("ProfilePictures")
                        .HasForeignKey("EntityId");

                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Users.User", "Owner")
                        .WithMany("Uploads")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Fora.ForumModerator", b =>
                {
                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Fora.Forum", "Forum")
                        .WithMany("Moderators")
                        .HasForeignKey("ForumId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Users.User", "User")
                        .WithMany("ForumModerators")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Fora.ForumTopic", b =>
                {
                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Fora.Forum", "Forum")
                        .WithMany("Topics")
                        .HasForeignKey("ForumId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Common.Comment", "LastComment")
                        .WithMany()
                        .HasForeignKey("LastCommentId");

                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Users.User", "Author")
                        .WithMany("Topics")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Games.Characters.Attributes.CharacterAttribute", b =>
                {
                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Games.Characters.Character", "Character")
                        .WithMany("Attributes")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Games.Characters.Character", b =>
                {
                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Games.Game", "Game")
                        .WithMany("Characters")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Users.User", "Author")
                        .WithMany("Characters")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Games.Game", b =>
                {
                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Users.User", "Assistant")
                        .WithMany("GamesAsAssistant")
                        .HasForeignKey("AssistantId");

                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Users.User", "Master")
                        .WithMany("GamesAsMaster")
                        .HasForeignKey("MasterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Users.User", "Nanny")
                        .WithMany("GamesAsNanny")
                        .HasForeignKey("NannyId");
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Games.Links.BlackListLink", b =>
                {
                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Games.Game", "Game")
                        .WithMany("BlackList")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Users.User", "User")
                        .WithMany("GamesBlacklisted")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Games.Links.CharacterRoomLink", b =>
                {
                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Games.Characters.Character", "Character")
                        .WithMany("RoomLinks")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Games.Posts.Room", "Room")
                        .WithMany("CharacterLinks")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Games.Links.GameTag", b =>
                {
                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Games.Game", "Game")
                        .WithMany("GameTags")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Common.Tag", "Tag")
                        .WithMany("GameTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Games.Links.PostAnticipation", b =>
                {
                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Games.Posts.Room", "Room")
                        .WithMany("PostsAwaited")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Users.User", "Target")
                        .WithMany("PostsRequired")
                        .HasForeignKey("TargetId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Users.User", "User")
                        .WithMany("WaitsForPosts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Games.Links.Reader", b =>
                {
                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Games.Game", "Game")
                        .WithMany("Readers")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Users.User", "User")
                        .WithMany("GamesObserved")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Games.Posts.Post", b =>
                {
                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Games.Characters.Character", "Character")
                        .WithMany("Posts")
                        .HasForeignKey("CharacterId");

                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Games.Posts.Room", "Room")
                        .WithMany("Posts")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Users.User", "Author")
                        .WithMany("Posts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Games.Posts.Room", b =>
                {
                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Games.Game", "Game")
                        .WithMany("Rooms")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Games.Posts.Room", "NextRoom")
                        .WithOne()
                        .HasForeignKey("DM.Services.DataAccess.BusinessObjects.Games.Posts.Room", "NextRoomId");

                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Games.Posts.Room", "PreviousRoom")
                        .WithMany()
                        .HasForeignKey("PreviousRoomId");
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Games.Rating.Vote", b =>
                {
                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Games.Game", "Game")
                        .WithMany("Votes")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Games.Posts.Post", "Post")
                        .WithMany("Votes")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Users.User", "TargetUser")
                        .WithMany("VotesReceived")
                        .HasForeignKey("TargetUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Users.User", "VotedUser")
                        .WithMany("VotesGiven")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Messaging.Message", b =>
                {
                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Messaging.Conversation", "Conversation")
                        .WithMany("Messages")
                        .HasForeignKey("ConversationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Users.User", "Author")
                        .WithMany("Messages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Messaging.UserConversationLink", b =>
                {
                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Messaging.Conversation", "Conversation")
                        .WithMany("UserLinks")
                        .HasForeignKey("ConversationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Users.User", "User")
                        .WithMany("ConversationLinks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DM.Services.DataAccess.BusinessObjects.Users.Token", b =>
                {
                    b.HasOne("DM.Services.DataAccess.BusinessObjects.Users.User", "User")
                        .WithMany("Tokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
