﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WAMServer.Models;

#nullable disable

namespace server.Migrations
{
    [DbContext(typeof(WamDBContext))]
    partial class WamDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WAMServer.Models.ActionLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("actionTypeID")
                        .HasColumnType("integer");

                    b.Property<DateTime>("dateTimeStamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("userId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("actionTypeID");

                    b.HasIndex("userId");

                    b.ToTable("ActionLog");
                });

            modelBuilder.Entity("WAMServer.Models.ActionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("details")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("ActionType");
                });

            modelBuilder.Entity("WAMServer.Models.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HouseNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("Zip")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("WAMServer.Models.ControlPC", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ControlPCSecret")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("meetputBroID")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("secret")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("userId")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("ControlPC");
                });

            modelBuilder.Entity("WAMServer.Models.GroundWaterLog", b =>

                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");
                    b.Property<string>("controlPCID")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("date")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("level")
                        .HasColumnType("decimal");

                    b.HasKey("Id");

                    b.ToTable("GroundWaterLog");
                });

            modelBuilder.Entity("WAMServer.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AddressId")
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WAMServer.Models.UserSetting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("controlPCID")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("controlPCSecret")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("userId")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("UserSetting");
                });

            modelBuilder.Entity("WAMServer.Models.ActionLog", b =>
                {
                    b.HasOne("WAMServer.Models.ActionType", "ActionType")
                        .WithMany("ActionLogs")
                        .HasForeignKey("actionTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WAMServer.Models.User", "User")
                        .WithMany("ActionLogs")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ActionType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WAMServer.Models.User", b =>
                {
                    b.HasOne("WAMServer.Models.Address", "Address")
                        .WithOne("User")
                        .HasForeignKey("WAMServer.Models.User", "AddressId");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("WAMServer.Models.ActionType", b =>
                {
                    b.Navigation("ActionLogs");
                });

            modelBuilder.Entity("WAMServer.Models.Address", b =>
                {
                    b.Navigation("User");
                });

            modelBuilder.Entity("WAMServer.Models.User", b =>
                {
                    b.Navigation("ActionLogs");
                });
#pragma warning restore 612, 618
        }
    }
}
