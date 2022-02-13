﻿// <auto-generated />
using System;
using Basket.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Basket.Migrations
{
    [DbContext(typeof(MyContext))]
    partial class MyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Basket.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<bool>("IsFinally");

                    b.Property<int>("Sum");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("OrderId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Basket.Models.OrderDetails", b =>
                {
                    b.Property<int>("OrderDetailsId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count");

                    b.Property<int>("OrderId");

                    b.Property<int>("Price");

                    b.Property<int>("ProductId");

                    b.HasKey("OrderDetailsId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("Basket.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("ImageName")
                        .IsRequired();

                    b.Property<int>("Price");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Basket.Models.OrderDetails", b =>
                {
                    b.HasOne("Basket.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Basket.Models.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}