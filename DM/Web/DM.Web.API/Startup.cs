﻿using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using DM.Services.Core.Configuration;
using DM.Services.Core.Extensions;
using DM.Services.Core.Logging;
using DM.Services.DataAccess;
using DM.Services.DataAccess.MongoIntegration;
using DM.Services.MessageQueuing;
using DM.Services.Search;
using DM.Web.API.Middleware;
using DM.Web.API.Swagger;
using DM.Web.Core.Binders;
using DM.Web.Core.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DM.Web.API
{
    /// <summary>
    /// Application
    /// </summary>
    public class Startup
    {
        private IConfigurationRoot Configuration { get; set; }

        private static Assembly[] GetAssemblies()
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var referencedAssemblies = currentAssembly.GetReferencedAssemblies().Select(Assembly.Load).ToArray();
            return referencedAssemblies
                .Union(new[] {currentAssembly})
                .Union(referencedAssemblies.SelectMany(a => a.GetReferencedAssemblies().Select(Assembly.Load)))
                .Where(a => a.FullName.StartsWith("DM."))
                .Distinct()
                .ToArray();
        }

        /// <summary>
        /// Configure application services
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <returns>Service provider</returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            Configuration = ConfigurationFactory.Default;

            services
                .AddOptions()
                .Configure<ConnectionStrings>(
                    Configuration.GetSection(nameof(ConnectionStrings)).Bind)
                .Configure<IntegrationSettings>(
                    Configuration.GetSection(nameof(IntegrationSettings)).Bind)
                .Configure<EmailConfiguration>(
                    Configuration.GetSection(nameof(EmailConfiguration)).Bind)
                .AddDmLogging("DM.API");

            var assemblies = GetAssemblies();
            services
                .AddAutoMapper(config => config.AllowNullCollections = true, assemblies)
                .AddMemoryCache()
                .AddEntityFrameworkNpgsql()
                .AddDbContext<DmDbContext>(options => options
                    .UseNpgsql(Configuration.GetConnectionString(nameof(ConnectionStrings.Rdb))));

            services
                .AddSwaggerGen(c => c.ConfigureGen())
                .AddMvc(config => { config.ModelBinderProviders.Insert(0, new ReadableGuidBinderProvider()); })
                .AddJsonOptions(config =>
                {
                    config.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    config.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                    config.SerializerSettings.DateFormatString = "O";
                    config.SerializerSettings.Converters.Insert(0, new StringEnumConverter());
                    config.SerializerSettings.Converters.Insert(0, new ReadableGuidConverter());
                    config.SerializerSettings.Converters.Insert(0, new ReadableNullableGuidConverter());
                    config.SerializerSettings.Converters.Insert(0, new OptionalConverter());
                    config.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.IsClass)
                .AsSelf()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterTypes(typeof(DmMongoClient))
                .AsSelf()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterModuleOnce<MessageQueuingModule>();
            builder.RegisterModuleOnce<SearchEngineModule>();
            builder.Populate(services);

            var container = builder.Build();
            return new AutofacServiceProvider(container);
        }

        /// <summary>
        /// Configure application
        /// </summary>
        /// <param name="appBuilder"></param>
        public void Configure(IApplicationBuilder appBuilder)
        {
            appBuilder
                .UseSwagger(c => c.Configure())
                .UseSwaggerUI(c => c.ConfigureUi())
                .UseMiddleware<CorrelationMiddleware>()
                .UseMiddleware<ErrorHandlingMiddleware>()
                .UseMiddleware<AuthenticationMiddleware>()
                .UseCors(b => b
                    .WithExposedHeaders("X-Dm-Auth-Token")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin())
                .UseMvc();
        }
    }
}