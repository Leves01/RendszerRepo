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
                .HasAnnotation("ProductVersion", "7.0.4")
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

                    b.Property<int>("partId")
                        .HasColumnType("int");

                    b.Property<int>("row")
                        .HasColumnType("int");

                    b.HasKey("storageId");

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
#pragma warning restore 612, 618
        }
    }
}
