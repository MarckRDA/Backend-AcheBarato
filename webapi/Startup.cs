using Cronos;
using Domain.Common;
using Domain.Models.Products;
using Domain.Models.Users;
using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using Infra.Mapping;
using Infra.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using webapi.Services.BackgroundService;
using webapi.Services.MessagerBrokers;
using System;
using System.Reflection;
using System.IO;

namespace webapi {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddCors (options => {
                options.AddPolicy ("achebarato",
                    builder => {
                        builder
                            .AllowAnyHeader ()
                            .AllowAnyMethod ()
                            .AllowAnyOrigin ();
                    }
                );
            });

            services.AddScoped (typeof (IMongoRepository<>), typeof (MongoRepository<>));
            services.AddScoped<IProductRepository, ProductRepository> ();
            services.AddScoped<IProductServices, ProductServices> ();
            services.AddScoped<IUserRepository, UserRepository> ();
            services.AddScoped<IUserService, UserService> ();
            services.AddScoped<IMessagerBroker, MessagerBroker> ();
            services.AddScoped<IProductBackgroundTask, ProductBackgroundTask> ();

            var mongoUrlBuilder = new MongoUrlBuilder (Configuration.GetValue<string> ("MongoSettings:Connection"));
            var mongoClient = new MongoClient (mongoUrlBuilder.ToMongoUrl ());

            services.AddHangfire (configuration => configuration
                .SetDataCompatibilityLevel (CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer ()
                .UseRecommendedSerializerSettings ()
                .UseMongoStorage (mongoClient, Configuration.GetValue<string> ("MongoSettings:DatabaseName"), new MongoStorageOptions {
                    MigrationOptions = new MongoMigrationOptions {
                            MigrationStrategy = new MigrateMongoMigrationStrategy (),
                                BackupStrategy = new CollectionMongoBackupStrategy ()
                        },
                        Prefix = "hangfire.mongo",
                        CheckConnection = true
                }));

            services.AddHangfireServer (serverOptions => {
                serverOptions.ServerName = "Hangfire.Mongo server 1";
            });

            services.AddControllers ();

            ProductMap.Configure ();

            UserMap.Configure ();
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen ();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger ();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI (c => {
                c.SwaggerEndpoint ("/swagger/v1/swagger.json", "My API V1");
            });

            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }
            app.UseCors ("achebarato");
            app.UseHttpsRedirection ();

            app.UseRouting ();

            app.UseAuthorization ();

            app.UseHangfireDashboard ();

            app.UseHangfireServer ();

            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
            });

            InitProcess ();
        }

        private void InitProcess () {

            BackgroundJob.Enqueue<ProductBackgroundTask> (x => x.PushProductsInDB ());
            BackgroundJob.Enqueue<ProductBackgroundTask> (x => x.NotifyUserAboutAlarmPrice ());
            BackgroundJob.Enqueue<ProductBackgroundTask> (x => x.MonitorPriceProducts ());
        }

    }
}