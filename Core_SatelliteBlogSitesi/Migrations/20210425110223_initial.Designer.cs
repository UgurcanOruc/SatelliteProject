// <auto-generated />
using System;
using Core_SatelliteBlogSitesi.Models.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Core_SatelliteBlogSitesi.Migrations
{
    [DbContext(typeof(ProjectContext))]
    [Migration("20210425110223_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Core_SatelliteBlogSitesi.Models.DATA.Article", b =>
                {
                    b.Property<int>("ArticleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ArticleImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Head")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("HitRate")
                        .HasColumnType("int");

                    b.Property<short>("ReadTime")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ArticleID");

                    b.HasIndex("UserID");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("Core_SatelliteBlogSitesi.Models.DATA.ArticleCategory", b =>
                {
                    b.Property<int>("ArticleID")
                        .HasColumnType("int");

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.HasKey("ArticleID", "CategoryID");

                    b.HasIndex("CategoryID");

                    b.ToTable("ArticleCategories");
                });

            modelBuilder.Entity("Core_SatelliteBlogSitesi.Models.DATA.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("HitRate")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Core_SatelliteBlogSitesi.Models.DATA.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Bio")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("HitRate")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("URL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Core_SatelliteBlogSitesi.Models.DATA.UserCategory", b =>
                {
                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.HasKey("UserID", "CategoryID");

                    b.HasIndex("CategoryID");

                    b.ToTable("UserCategories");
                });

            modelBuilder.Entity("Core_SatelliteBlogSitesi.Models.DATA.Article", b =>
                {
                    b.HasOne("Core_SatelliteBlogSitesi.Models.DATA.User", "User")
                        .WithMany("Articles")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core_SatelliteBlogSitesi.Models.DATA.ArticleCategory", b =>
                {
                    b.HasOne("Core_SatelliteBlogSitesi.Models.DATA.Article", "Article")
                        .WithMany("ArticleCategories")
                        .HasForeignKey("ArticleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core_SatelliteBlogSitesi.Models.DATA.Category", "Category")
                        .WithMany("ArticleCategories")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Article");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Core_SatelliteBlogSitesi.Models.DATA.UserCategory", b =>
                {
                    b.HasOne("Core_SatelliteBlogSitesi.Models.DATA.Category", "Category")
                        .WithMany("UserCategories")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core_SatelliteBlogSitesi.Models.DATA.User", "User")
                        .WithMany("UserCategories")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core_SatelliteBlogSitesi.Models.DATA.Article", b =>
                {
                    b.Navigation("ArticleCategories");
                });

            modelBuilder.Entity("Core_SatelliteBlogSitesi.Models.DATA.Category", b =>
                {
                    b.Navigation("ArticleCategories");

                    b.Navigation("UserCategories");
                });

            modelBuilder.Entity("Core_SatelliteBlogSitesi.Models.DATA.User", b =>
                {
                    b.Navigation("Articles");

                    b.Navigation("UserCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
