﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using RememberMe.Persistence;
using System;

namespace RememberMe.Migrations
{
    [DbContext(typeof(RememberMeDbContext))]
    partial class RememberMeDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-preview2-25794")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RememberMe.Models.Friend", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Friends");
                });

            modelBuilder.Entity("RememberMe.Models.ContactDetails", b =>
                {
                    b.Property<int?>("FriendId");

                    b.Property<string>("Email")
                        .HasColumnName("Email")
                        .HasMaxLength(255);

                    b.Property<string>("Phone")
                        .HasColumnName("Phone")
                        .HasMaxLength(255);

                    b.HasKey("FriendId");

                    b.ToTable("Friends");
                });

            modelBuilder.Entity("RememberMe.Models.ContactDetails", b =>
                {
                    b.HasOne("RememberMe.Models.Friend")
                        .WithOne("ContactDetails")
                        .HasForeignKey("RememberMe.Models.ContactDetails", "FriendId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
