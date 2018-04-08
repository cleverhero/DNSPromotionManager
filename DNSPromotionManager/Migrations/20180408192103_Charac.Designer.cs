﻿// <auto-generated />
using DNSPromotionManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace DNSPromotionManager.Migrations
{
    [DbContext(typeof(DNSContext))]
    [Migration("20180408192103_Charac")]
    partial class Charac
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DNSPromotionManager.Models.Branch", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.HasKey("Id");

                    b.ToTable("Branchs");
                });

            modelBuilder.Entity("DNSPromotionManager.Models.Characteristic", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.HasKey("Id");

                    b.ToTable("Characteristics");
                });

            modelBuilder.Entity("DNSPromotionManager.Models.CharacteristicValue", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<string>("CharacteristicId")
                        .IsRequired();

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.HasKey("Id");

                    b.HasIndex("CharacteristicId");

                    b.ToTable("CharacteristicValues");
                });

            modelBuilder.Entity("DNSPromotionManager.Models.Kind", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.HasKey("Id");

                    b.ToTable("Kinds");
                });

            modelBuilder.Entity("DNSPromotionManager.Models.Product", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<bool>("DelFlag");

                    b.Property<string>("KindId")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<string>("ParentId");

                    b.HasKey("Id");

                    b.HasIndex("KindId");

                    b.HasIndex("ParentId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("DNSPromotionManager.Models.Promotion", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<DateTime>("Begin");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<DateTime>("End");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.HasKey("Id");

                    b.ToTable("Promotions");
                });

            modelBuilder.Entity("DNSPromotionManager.Models.CharacteristicValue", b =>
                {
                    b.HasOne("DNSPromotionManager.Models.Characteristic", "Characteristic")
                        .WithMany()
                        .HasForeignKey("CharacteristicId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DNSPromotionManager.Models.Product", b =>
                {
                    b.HasOne("DNSPromotionManager.Models.Kind", "Kind")
                        .WithMany()
                        .HasForeignKey("KindId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DNSPromotionManager.Models.Product", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId");
                });
#pragma warning restore 612, 618
        }
    }
}