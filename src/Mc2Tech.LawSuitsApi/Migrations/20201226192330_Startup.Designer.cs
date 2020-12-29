﻿// <auto-generated />
using System;
using Mc2Tech.LawSuitsApi.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Mc2Tech.LawSuitsApi.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20201226192330_Startup")]
    partial class Startup
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Mc2Tech.LawSuitsApi.Model.DALEntity.LawSuitEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ClientPhysicalFolder")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<Guid?>("CreationUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime>("DistributedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ExternalReference")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsSystemDefined")
                        .HasColumnType("bit");

                    b.Property<bool>("JusticeSecret")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<Guid?>("ModifiedUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ParentLawSuitId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SituationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("UnifiedProcessNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasAlternateKey("ExternalReference");

                    b.HasIndex("ParentLawSuitId");

                    b.HasIndex("UnifiedProcessNumber")
                        .IsUnique();

                    b.ToTable("LawSuits");
                });

            modelBuilder.Entity("Mc2Tech.LawSuitsApi.Model.DALEntity.LawSuitEntity", b =>
                {
                    b.HasOne("Mc2Tech.LawSuitsApi.Model.DALEntity.LawSuitEntity", "ParentLawSuit")
                        .WithMany("ChildLawSuits")
                        .HasForeignKey("ParentLawSuitId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("ParentLawSuit");
                });

            modelBuilder.Entity("Mc2Tech.LawSuitsApi.Model.DALEntity.LawSuitEntity", b =>
                {
                    b.Navigation("ChildLawSuits");
                });
#pragma warning restore 612, 618
        }
    }
}
