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

        //public DbSet<UserPersonality> UserPersonalities { get; set; }

        //public DbSet<UserInterest> UserInterests { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<UserInterest>()
                   .HasKey(ui =>  new { ui.UserId, ui.InterestId } );

            builder.Entity<UserInterest>()
                   .HasOne<WannaWhatUser>(i => i.User)
                   .WithMany(u => u.UserInerests);

            builder.Entity<UserInterest>()
                   .HasOne<Interest>(ui => ui.Interest)
                   .WithMany(i => i.UserInterests);

            //builder.Entity<WannaWhatUser>()
            //       .HasOne(u => u.UserInfo)
            //       .WithOne(x => x.User)
            //       .HasForeignKey<UserInfo>(x => x.UserId);


            //builder.Entity<UserInterest>().HasKey(ui => new { ui.UserId, ui.InterestId });

            //builder.Entity<UserPersonality>().HasKey(ui => new { ui.UserId, ui.PersonalityId });

            //builder.Entity<UserInterest>()
            //    .HasOne<WannaWhatUser>(sc => sc.User)
            //    .WithMany(s => s.UserInerests)
            //    .HasForeignKey(sc => sc.UserId);


            //builder.Entity<UserInterest>()
            //    .HasOne<Interest>(sc => sc.Interest)
            //    .WithMany(s => s.UserInterests)
            //    .HasForeignKey(sc => sc.InterestId);

            //builder.Entity<UserPersonality>()
            //    .HasOne<WannaWhatUser>(sc => sc.User)
            //    .WithMany(s => s.UserPersonalities)
            //    .HasForeignKey(sc => sc.UserId);


            //builder.Entity<UserPersonality>()
            //    .HasOne<Personality>(sc => sc.Personality)
            //    .WithMany(s => s.UserPesonalities)
            //    .HasForeignKey(sc => sc.PersonalityId);

            base.OnModelCreating(builder);
        }
    }
}
