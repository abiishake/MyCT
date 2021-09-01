using BusinessLogicLayer.BOObjects;
using DataAccessLayer.DbContext;
using DataAccessLayer.Repositories;
using DataAccessLayer.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MyCT.Core.Model.Entities;
using MyCT.Interface.BOObjects;
using MyCT.Interface.Repositories;
using MyCT.Interface.ServiceLocator;
using MyCT.Interface.UnitOfWork;
using MyCT.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyCT
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MyCTDbContext>(option =>
            {
                option.UseMySQL(Configuration["ConnectionStrings:MyCTDbConnection"]);
            });



            BusinessLogicLayer.BOObjects.ShopBO bO = null; // #NeedToFix for adding a reference to assembly 

            services.AddScoped<IServiceLocator, ServiceLocator>();

            AddDependencies(services);

            services.AddIdentity<CTUser, CTRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            }).AddEntityFrameworkStores<MyCTDbContext>();

            services.AddAuthentication()
                .AddGoogle(opt =>
                {
                    opt.ClientId = Configuration["Google:ClientId"];
                    opt.ClientSecret = Configuration["Google:ClientSecret"];

                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
                {
                    opts.TokenValidationParameters.ValidateAudience = false;
                    opts.TokenValidationParameters.ValidateIssuer = false;
                    opts.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["BearerTokens:Key"]));
                });
            services.AddAuthorization();

            services.AddControllers();

            services.AddSwaggerGen();
        }

        private static void AddDependencies(IServiceCollection services)
        {
            Type iDisposable = typeof(IDisposable);

            HashSet<string> nameSpaces = new()
            {
                "DataAccessLayer.UnitOfWork",
                "DataAccessLayer.Repositories",
                "BusinessLogicLayer.BOObjects"
            };

            HashSet<string> assemblieNames = new();

            foreach (string ns in nameSpaces)
            {
                string assembly = ns.Split('.')[0];
                if (!assemblieNames.Contains(assembly))
                {
                    assemblieNames.Add(assembly);
                }
            }
           
            IEnumerable<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => assemblieNames.Contains(x.GetName().Name));

            foreach (Assembly assembly in assemblies)
            {
                Type[] types = assembly.GetExportedTypes().Where(x => nameSpaces.Contains(x.Namespace) && !x.IsInterface && !x.IsAbstract).ToArray();

                foreach (Type type in types)
                {
                    Type _interface = type.GetInterface("I" + type.Name);
                    ConstructorInfo[] constructors = type.GetConstructors();
                    if (_interface != null && constructors.Length > 0 && constructors.All(x => x.IsPublic))
                    {
                        if (iDisposable.IsAssignableFrom(type))
                        {
                            if (_interface.IsGenericType && !_interface.IsGenericTypeDefinition)
                            {
                                _interface = _interface.GetGenericTypeDefinition();
                            }

                            services.AddScoped(_interface, type);
                        }
                        else
                        {
                            if (_interface.IsGenericType && !_interface.IsGenericTypeDefinition)
                            {
                                _interface = _interface.GetGenericTypeDefinition();
                            }
                            services.AddTransient(_interface, type);
                        }
                    }
                }
            }


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                x.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
