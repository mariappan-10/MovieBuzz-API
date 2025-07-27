
using Core.Entities;
using Core.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace Infrastructure.DatabaseContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        //private Guid userId = Guid.Parse("4d2d6e4c-5d3a-4d47-a91b-3f51ecdfb9e5"); // Example user ID
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<WatchListData> WatchListDatas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<ApplicationUser>()
        //        .HasMany(u => u.Watchlist)
        //        .WithOne()
        //        .HasForeignKey(w => w.ApplicationUserId)
        //        .OnDelete(DeleteBehavior.Cascade);

        //    var dummyUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        //    var user = new ApplicationUser
        //    {
        //        Id = dummyUserId,
        //        UserName = "dummy@example.com",
        //        NormalizedUserName = "DUMMY@EXAMPLE.COM",
        //        Email = "dummy@example.com",
        //        NormalizedEmail = "DUMMY@EXAMPLE.COM",
        //        EmailConfirmed = true,
        //        SecurityStamp = "STATIC_SECURITY_STAMP",
        //        RefreshToken = null,
        //        RefreshTokenExpirationDateTime = DateTime.Parse("2000-01-01T00:00:00Z"),
        //        PasswordHash = "AQAAAAIAAYagAAAAEAAAAAHARDCFVYgV9..." // static hashed password
        //    };

        //    modelBuilder.Entity<ApplicationUser>().HasData(user);

        //    modelBuilder.Entity<WatchListData>().HasData(
        //        new WatchListData
        //        {
        //            Id = 1,
        //            imdbId = "tt0111161",
        //            ApplicationUserId = dummyUserId
        //        }
        //    );
        }
    }

}
