using Core_SatelliteBlogSitesi.Models.DATA;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_SatelliteBlogSitesi.Models.DAL
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {
            // Connection String
            // Startup.cs'in içerisinde tanımla.
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserCategory> UserCategories { get; set; }
        public DbSet<ArticleCategory> ArticleCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // User ve Category arasındaki çoka çok ilişki için oluşturulan UserCategory tablosunun bağlantıları.
            modelBuilder.Entity<UserCategory>()
         .HasKey(bc => new { bc.UserID, bc.CategoryID });
            modelBuilder.Entity<UserCategory>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.UserCategories)
                .HasForeignKey(bc => bc.UserID);
            modelBuilder.Entity<UserCategory>()
                .HasOne(bc => bc.Category)
                .WithMany(c => c.UserCategories)
                .HasForeignKey(bc => bc.CategoryID);

            // Article ve Category arasındaki çoka çok ilişki için oluşturulan ArticleCategory tablosunun bağlantıları.
            modelBuilder.Entity<ArticleCategory>()
         .HasKey(bc => new { bc.ArticleID, bc.CategoryID });
            modelBuilder.Entity<ArticleCategory>()
                .HasOne(bc => bc.Article)
                .WithMany(b => b.ArticleCategories)
                .HasForeignKey(bc => bc.ArticleID);
            modelBuilder.Entity<ArticleCategory>()
                .HasOne(bc => bc.Category)
                .WithMany(c => c.ArticleCategories)
                .HasForeignKey(bc => bc.CategoryID);
        }
    }
}
