using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WannaWhat.Core.Models;

namespace WannaWhat.Data
{

    public class WannaWhatDbContext : IdentityDbContext<WannaWhatUser>
    {
        public WannaWhatDbContext(DbContextOptions<WannaWhatDbContext> options)
            : base(options)
        {

        }

        public DbSet<UserInfo> UserInfo { get; set; }

        public DbSet<UserPersonality> UserPersonalities { get; set; }

        public DbSet<UserInterest> UserInterests { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Set UserInterest composite PK
            builder.Entity<UserInterest>()
                   .HasKey(ui =>  new { ui.UserId, ui.InterestId } );


            // Set UserPersonality composite PK
            builder.Entity<UserPersonality>()
                   .HasKey(up => new { up.UserId, up.PersonalityId });


            base.OnModelCreating(builder);
        }
    }
}
