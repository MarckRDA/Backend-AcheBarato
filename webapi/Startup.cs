using System.Text;
using Domain.Common;
using Domain.Models.Products;
using Domain.Models.Users;
using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using Infra.Mapping;
using Infra.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using webapi.Crypts;
using webapi.Services.BackgroundService;
using webapi.Services.MessagerBrokers;
using webapi.Services.URIBuilder;

namespace webapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.

        public void ConfigureServices(IServiceCollection services)
        {
            var key = Encoding.ASCII.GetBytes(SecretString.Secret);

            services.AddCors(options =>
            {
                options.AddPolicy("achebarato",
                    builder =>
                    {
                        builder
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowAnyOrigin();
                    }
                );
            });

            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductServices, ProductServices>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMessagerBroker, MessagerBroker>();
            services.AddScoped<IProductBackgroundTask, ProductBackgroundTask>();


            var mongoUrlBuilder = new MongoUrlBuilder(Configuration.GetValue<string>("MongoSettings:Connection"));
            var mongoClient = new MongoClient(mongoUrlBuilder.ToMongoUrl());

            services.AddHangfire(configuration => configuration
               .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
               .UseSimpleAssemblyNameTypeSerializer()
               .UseRecommendedSerializerSettings()
               .UseMongoStorage(mongoClient, Configuration.GetValue<string>("MongoSettings:DatabaseName"), new MongoStorageOptions
               {
                   MigrationOptions = new MongoMigrationOptions
                   {
                       MigrationStrategy = new MigrateMongoMigrationStrategy(),
                       BackupStrategy = new CollectionMongoBackupStrategy()
                   },
                   Prefix = "hangfire.mongo",
                   CheckConnection = true
               }));


            services.AddHangfireServer(serverOptions =>
            {
                serverOptions.ServerName = "Hangfire.Mongo server 1";
            });


            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });


            services.AddMvc().AddJsonOptions(option =>
            {
                option.JsonSerializerOptions.IgnoreNullValues = true;
            });


            services.AddResponseCompression(options =>
           {
               options.Providers.Add<BrotliCompressionProvider>();
               options.EnableForHttps = true;
           });

            services.AddHttpContextAccessor();
            services.AddSingleton<IURIService>(o =>
            {
                var accessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new URIService(uri);
            });


            services.AddControllers();

            ProductMap.Configure();

            UserMap.Configure();
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("achebarato");
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseResponseCompression();

            app.UseHangfireDashboard();

            app.UseHangfireServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            InitProcess();
        }

        private void InitProcess()
        {

            BackgroundJob.Enqueue<ProductBackgroundTask>(x => x.PushProductsInDB());
            //RecurringJob.AddOrUpdate<ProductBackgroundTask> (x => x.NotifyUserAboutAlarmPrice (), Cron.MinuteInterval(5));
            //BackgroundJob.Enqueue<ProductBackgroundTask> (x => x.MonitorPriceProducts ());
        }

    }
}