using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyCT.Core.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.DbContext
{
    public class MyCTDbContext : IdentityDbContext<CTUser, CTRole, int, CTUserClaim, CTUserRole, CTUserLogin, CTRoleClaim, CTUserToken>
    {
        public MyCTDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder.Entity<CTUser>().ToTable("users");
            builder.Entity<CTRole>().ToTable("roles");
            builder.Entity<CTUserRole>().ToTable("user_roles").HasKey(x => x.Id);
            builder.Entity<CTUserLogin>().ToTable("user_logins").HasKey(x => x.Id);
            builder.Entity<CTUserClaim>().ToTable("user_claims");
            builder.Entity<CTUserToken>().ToTable("user_tokens").HasKey(x => x.Id);
            builder.Entity<CTRoleClaim>().ToTable("role_claims");
            builder.Entity<Shop>().ToTable("shops");
            builder.Entity<Category>().ToTable("categories");
            builder.Entity<SubCategory>().ToTable("subcategories");
            builder.Entity<State>().ToTable("states");
            builder.Entity<City>().ToTable("cities");
            builder.Entity<Status>().ToTable("status");
            builder.Entity<LatLong>().Property(x => x.Latitude).HasPrecision(12, 10);
            builder.Entity<LatLong>().Property(x => x.Longitude).HasPrecision(12, 10);
            builder.Entity<LatLong>().ToTable("latlongs");

           
        }
    }
}
