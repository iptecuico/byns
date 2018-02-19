﻿// <auto-generated />
using Codetecuico.Byns.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Codetecuico.Byns.Data.Migrations
{
    [DbContext(typeof(BynsDbContext))]
    [Migration("20180219045821_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Codetecuico.Byns.Domain.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Category")
                        .HasMaxLength(50);

                    b.Property<string>("Condition")
                        .HasMaxLength(50);

                    b.Property<int?>("CreatedBy");

                    b.Property<string>("Currency")
                        .HasMaxLength(10);

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<DateTime>("DatePosted");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("Image");

                    b.Property<bool>("IsSold");

                    b.Property<int?>("ModifiedBy");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<Guid>("OrganizationId");

                    b.Property<double>("Price");

                    b.Property<string>("Remarks")
                        .HasMaxLength(1500);

                    b.Property<int>("StarCount");

                    b.Property<string>("Status")
                        .HasMaxLength(20);

                    b.Property<int>("StockCount");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("UserId");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("Codetecuico.Byns.Domain.Organization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEW Guid()");

                    b.Property<int?>("CreatedBy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<int?>("ModifiedBy");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Organization");
                });

            modelBuilder.Entity("Codetecuico.Byns.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CreatedBy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<DateTime>("DateRegistered");

                    b.Property<string>("Email")
                        .HasMaxLength(50);

                    b.Property<string>("ExternalId");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50);

                    b.Property<string>("Image");

                    b.Property<string>("LastName")
                        .HasMaxLength(50);

                    b.Property<string>("Location");

                    b.Property<string>("MobileNumber")
                        .HasMaxLength(20);

                    b.Property<int?>("ModifiedBy");

                    b.Property<Guid>("OrganizationId");

                    b.Property<string>("PersonalWebsite")
                        .HasMaxLength(50);

                    b.Property<int>("UserRoleId");

                    b.Property<string>("Username")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("UserRoleId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Codetecuico.Byns.Domain.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("Codetecuico.Byns.Domain.Item", b =>
                {
                    b.HasOne("Codetecuico.Byns.Domain.Organization", "Organization")
                        .WithMany("Items")
                        .HasForeignKey("OrganizationId");

                    b.HasOne("Codetecuico.Byns.Domain.User", "User")
                        .WithMany("Items")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Codetecuico.Byns.Domain.User", b =>
                {
                    b.HasOne("Codetecuico.Byns.Domain.Organization", "Organization")
                        .WithMany("Users")
                        .HasForeignKey("OrganizationId");

                    b.HasOne("Codetecuico.Byns.Domain.UserRole", "UserRole")
                        .WithMany()
                        .HasForeignKey("UserRoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
