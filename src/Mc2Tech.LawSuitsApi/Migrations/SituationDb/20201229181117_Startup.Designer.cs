﻿// <auto-generated />
using System;
using Mc2Tech.LawSuitsApi.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Mc2Tech.LawSuitsApi.Migrations.SituationDb
{
    [DbContext(typeof(SituationDbContext))]
    [Migration("20201229181117_Startup")]
    partial class Startup
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Mc2Tech.LawSuitsApi.Model.DALEntity.SituationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<Guid?>("CreationUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<Guid>("ExternalReference")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsClosed")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSystemDefined")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<Guid?>("ModifiedUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.HasKey("Id");

                    b.HasAlternateKey("ExternalReference");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Situations");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2349b7e9-f966-4159-8b8b-08d8a4840d3f"),
                            CreationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ExternalReference = new Guid("00000000-0000-0000-0000-000000000000"),
                            IsClosed = true,
                            IsSystemDefined = false,
                            ModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Finalizado",
                            Status = 0
                        },
                        new
                        {
                            Id = new Guid("fea5977d-d634-475c-0a87-08d8a484dd71"),
                            CreationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ExternalReference = new Guid("00000000-0000-0000-0000-000000000000"),
                            IsClosed = false,
                            IsSystemDefined = false,
                            ModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Arquivado",
                            Status = 0
                        },
                        new
                        {
                            Id = new Guid("12cfd7d7-e71a-43c9-07e2-08d8a4850f73"),
                            CreationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ExternalReference = new Guid("00000000-0000-0000-0000-000000000000"),
                            IsClosed = false,
                            IsSystemDefined = false,
                            ModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Em Recurso",
                            Status = 0
                        },
                        new
                        {
                            Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                            CreationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ExternalReference = new Guid("00000000-0000-0000-0000-000000000000"),
                            IsClosed = false,
                            IsSystemDefined = false,
                            ModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Em Andamento",
                            Status = 0
                        },
                        new
                        {
                            Id = new Guid("e0cc0cac-c0fb-4403-8e58-d48d279d50b0"),
                            CreationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ExternalReference = new Guid("00000000-0000-0000-0000-000000000000"),
                            IsClosed = true,
                            IsSystemDefined = false,
                            ModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Desmembrado",
                            Status = 0
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
