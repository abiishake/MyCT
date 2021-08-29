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
using MyCT.Interface.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
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

            AddDependencies(services);

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(IRepositoryWithId<>), typeof(RepositoryWithId<>));
            services.AddTransient<IShopBO, ShopBO>();
            services.AddTransient<IShopRepository, ShopRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ICategoryBO, CategoryBO>();
            services.AddTransient<ISubCategoryRepository, SubCategoryRepository>();
            services.AddTransient<IStatusRepository, StatusRepository>();
            services.AddTransient<IStateRepository, StateRepository>();
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<ILatLongRepository, LatLongRepository>();

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
        }

        private void AddDependencies(IServiceCollection services)
        {
            List<string> nameSpaces = new List<string>();
            nameSpaces.Add("DataAccessLayer.UnitOfWork");
            nameSpaces.Add("DataAccessLayer.Repositories");
            nameSpaces.Add("BusinessLogicLayer.Objects");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
