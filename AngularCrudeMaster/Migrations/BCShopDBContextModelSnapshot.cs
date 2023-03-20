﻿// <auto-generated />
using System;
using AngularCrudeMaster.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AngularCrudeMaster.Migrations
{
    [DbContext(typeof(BCShopDBContext))]
    partial class BCShopDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AngularCrudeMaster.Context.BookCategory", b =>
                {
                    b.Property<string>("CategoryId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("BookCategory");
                });

            modelBuilder.Entity("AngularCrudeMaster.Context.BookItem", b =>
                {
                    b.Property<string>("BookId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("BookName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoryId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("BookId");

                    b.HasIndex("CategoryId");

                    b.ToTable("BookItem");
                });

            modelBuilder.Entity("AngularCrudeMaster.Context.BookItem", b =>
                {
                    b.HasOne("AngularCrudeMaster.Context.BookCategory", "BookCategory")
                        .WithMany("BookItem")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BookCategory");
                });

            modelBuilder.Entity("AngularCrudeMaster.Context.BookCategory", b =>
                {
                    b.Navigation("BookItem");
                });
#pragma warning restore 612, 618
        }
    }
}