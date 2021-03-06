﻿using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SyncJob.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using Volo.Abp;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.Security.Claims;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Hangfire;
using Hangfire.MySql.Core;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.BackgroundJobs.Hangfire;
using Volo.Abp.AspNetCore.Mvc;
using Hangfire.RecurringJobExtensions;
using Hangfire.Console;
using Hangfire.Samples;
using Volo.Abp.EntityFrameworkCore.SqlServer;

using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using SyncJob.Localization;
using Permissions;
using Volo.Abp.Auditing;

namespace SyncJob.Host
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(ApplicationModule),
        typeof(EFCoreModule),
        typeof(HttpApiModule),
        typeof(AbpPermissionManagementEntityFrameworkCoreModule),
        typeof(AbpSettingManagementEntityFrameworkCoreModule),
        typeof(AbpAuditLoggingEntityFrameworkCoreModule),
        typeof(AbpEntityFrameworkCoreSqlServerModule),
        typeof(AbpEntityFrameworkCoreMySQLModule)
        //, typeof(AbpBackgroundJobsModule)
        , typeof(AbpBackgroundJobsHangfireModule)
        )]
    public class AppModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.BuildConfiguration();

            //自定义注入
            context.Services.AddSingleton(configuration);

           


            Configure<DbConnectionOptions>(options =>
            {
                options.ConnectionStrings.Default = configuration.GetConnectionString("TargetDb");
                //options.ConnectionStrings.Add("SourceDb", configuration.GetConnectionString("SourceDb"));
                //options.ConnectionStrings.Add("TargetDb", configuration.GetConnectionString("TargetDb"));
            });

           
            Configure<AbpDbContextOptions>(options =>
            {
                //options.UseMySQL();
                options.UseSqlServer();
                options.UseMySQL<SourceDbContext>();

                options.UseMySQL<TargetDbContext>();
                //options.UseSqlServer<TargetDbContext>();



            });
           
           Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(ApplicationModule).Assembly);
            
            });

            if (hostingEnvironment.IsDevelopment())
            {
                Configure<VirtualFileSystemOptions>(options =>
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<DomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}SyncJob.Domain", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<ApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}SyncJob.Application", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<ApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}SyncJob.Application.Contracts", Path.DirectorySeparatorChar)));
                });
            }

            context.Services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new Info { Title = "SyncJob API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                    options.CustomSchemaIds(type => type.FullName);
                });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Languages.Add(new LanguageInfo("en", "en", "English"));
                //...add other languages
            });




            //context.Services.AddDistributedSqlServerCache(options =>
            //{
            //    options.ConnectionString = configuration.GetConnectionString("SqlServerCache");
            //    options.SchemaName = "dbo";
            //    options.TableName = "TestCache";
            //});

             
            context.Services.AddAuthentication("Bearer")
               .AddIdentityServerAuthentication(options =>
               {
                   options.Authority = configuration["AuthServer:Authority"];
                   options.ApiName = configuration["AuthServer:ApiName"];
                   options.RequireHttpsMetadata = false;
                    //TODO: Should create an extension method for that (may require to create a new ABP package depending on the IdentityServer4.AccessTokenValidation)
                   options.InboundJwtClaimTypeMap["sub"] = AbpClaimTypes.UserId;
                   options.InboundJwtClaimTypeMap["role"] = AbpClaimTypes.Role;
                   options.InboundJwtClaimTypeMap["email"] = AbpClaimTypes.Email;
                   options.InboundJwtClaimTypeMap["email_verified"] = AbpClaimTypes.EmailVerified;
                   options.InboundJwtClaimTypeMap["phone_number"] = AbpClaimTypes.PhoneNumber;
                   options.InboundJwtClaimTypeMap["phone_number_verified"] = AbpClaimTypes.PhoneNumberVerified;
                   options.InboundJwtClaimTypeMap["name"] = AbpClaimTypes.UserName;
               });


            context.Services.AddHangfire(config =>
            {
                //config.UseSqlServerStorage(Configuration.GetConnectionString("Default"));
                config.UseStorage(new MySqlStorage(configuration.GetConnectionString("Hangfire"), new MySqlStorageOptions() { TablePrefix = "Hangfire" }));
                config.UseConsole();
                config.UseRecurringJob("recurringjob.json");
                //config.UseRecurringJob(typeof(RecurringJobService));
                //config.UseDefaultActivator();
            });

 

            Configure<BackgroundJobOptions>(options =>
            {
                options.IsJobExecutionEnabled = false; //Disables job execution\
                
               
            });
            Configure<BackgroundJobWorkerOptions>(options =>
            {
                options.DefaultTimeout = 864000; //10 days (as seconds)
            });


            //context.Services.AddDistributedRedisCache(options =>
            //{
            //    options.Configuration = configuration["Redis:Configuration"];
            //});

            //Configure<AbpAuditingOptions>(options =>
            //{
            //    options.IsEnabledForGetRequests = true;
            //    options.ApplicationName = "IdentityService";
            //});

            //var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
            //context.Services.AddDataProtection()
            //    .PersistKeysToStackExchangeRedis(redis, "MsDemo-DataProtection-Keys");
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            #region 添加权限
            var authorizationService = context.ServiceProvider.GetRequiredService<IAuthorizationService>();
            var l = context.ServiceProvider.GetRequiredService<IStringLocalizer<Resource>>();


            //var rootMenuItem = new ApplicationMenuItem("ProductManagement", l["Menu:ProductManagement"]);

            if ( authorizationService.IsGranted(UserPermissions.GroupName))
            {
                //rootMenuItem.AddItem(new ApplicationMenuItem("Products", l["Menu:Products"], "/ProductManagement/Products"));
            }

            //context.Menu.AddItem(rootMenuItem);
            #endregion

            var app = context.GetApplicationBuilder();


            app.UseVirtualFiles();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Support APP API");
            });

            app.UseAuthentication();
            app.UseAbpRequestLocalization();
            app.UseAuditing();

         
            //启动Hangfire服务
            app.UseHangfireServer(new BackgroundJobServerOptions
            {
                WorkerCount = 2
            });
            //启动hangfire面板
            //app.UseHangfireDashboard();
            //backgroundJobs.Enqueue(() => Console.WriteLine("Hello world from Hangfire!"));
            BackgroundJob.Enqueue(() => Job.CancellExecute(JobCancellationToken.Null));
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                //Authorization = new[] {
                //    new AuthorizationFilter() 
                //    //new AbpHangfireAuthorizationFilter()
                //    //,new AbpHangfireAuthorizationFilter("HangFireDashboardPermissionName")
                //}
            });

            app.UseMvcWithDefaultRoute();
        }

        
    }
}
