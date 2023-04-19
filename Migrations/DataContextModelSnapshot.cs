﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RendszerRepo.Data;

#nullable disable

namespace RendszerRepo.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RendszerRepo.Models.Part", b =>
                {
                    b.Property<int>("partId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("partId"));

                    b.Property<int>("maxCount")
                        .HasColumnType("int");

                    b.Property<string>("partName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("price")
                        .HasColumnType("int");

                    b.HasKey("partId");

                    b.ToTable("Parts");
                });

            modelBuilder.Entity("RendszerRepo.Models.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProjectId"));

                    b.Property<int>("combinedPrice")
                        .HasColumnType("int");

                    b.Property<int>("partId")
                        .HasColumnType("int");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.HasKey("ProjectId");

                    b.HasIndex("partId");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("RendszerRepo.Models.Project_properties", b =>
                {
                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("assignedId")
                        .HasColumnType("int");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("ProjectProperties");
                });

            modelBuilder.Entity("RendszerRepo.Models.Storage", b =>
                {
                    b.Property<int>("storageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("storageId"));

                    b.Property<int>("column")
                        .HasColumnType("int");

                    b.Property<int>("countOfParts")
                        .HasColumnType("int");

                    b.Property<int>("drawer")
                        .HasColumnType("int");

                    b.Property<int>("max")
                        .HasColumnType("int");

                    b.Property<int>("partId")
                        .HasColumnType("int");

                    b.Property<int>("row")
                        .HasColumnType("int");

                    b.HasKey("storageId");

                    b.HasIndex("partId");

                    b.ToTable("Storages");
                });

            modelBuilder.Entity("RendszerRepo.Models.User", b =>
                {
                    b.Property<int>("userId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("userId"));

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userRole")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("userId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RendszerRepo.Models.Project", b =>
                {
                    b.HasOne("RendszerRepo.Models.Part", "Parts")
                        .WithMany()
                        .HasForeignKey("partId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parts");
                });

            modelBuilder.Entity("RendszerRepo.Models.Project_properties", b =>
                {
                    b.HasOne("RendszerRepo.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RendszerRepo.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RendszerRepo.Models.Storage", b =>
                {
                    b.HasOne("RendszerRepo.Models.Part", "Parts")
                        .WithMany()
                        .HasForeignKey("partId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parts");
                });
#pragma warning restore 612, 618
        }
    }
}
