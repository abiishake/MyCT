using BusinessLogicLayer.BOObjects;
using DataAccessLayer.DbContext;
using DataAccessLayer.Repositories;
using DataAccessLayer.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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
                opts.TokenValidationParameters.ValidIssuer = Configuration["BearerTokens:Issuer"];
            });
            services.AddAuthorization();

            services.AddControllers();

            services.AddSwaggerGen(x =>
            {
                //x.SwaggerDoc("MYct_V1", new OpenApiInfo()
                //{
                //    Title = "MyCT API",
                //    Description = "List Of Private APIs",
                //    Contact = new OpenApiContact()
                //    {
                //        Name = "Abhishek Pal",
                //        Url = new Uri("https://www.linkedin.com/in/abiishake/"),
                //        Email = "palabishek322@gmail.com"
                //    }
                //});

                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\ne.g.: \"Bearer 12345abcdef\""
                });

                x.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                   {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });

                
            });
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

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

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
