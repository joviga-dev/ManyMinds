﻿// <auto-generated />
using ManyMinds.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ManyMindsApi.Migrations
{
    [DbContext(typeof(ManyMindsApiContext))]
    [Migration("20240315162335_alter table produto add column status")]
    partial class altertableprodutoaddcolumnstatus
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("ManyMinds.Models.Produto", b =>
                {
                    b.Property<int>("codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("codigo"));

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<bool>("status")
                        .HasColumnType("tinyint(1)");

                    b.Property<decimal>("valorUnitario")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("codigo");

                    b.ToTable("produtos");
                });
#pragma warning restore 612, 618
        }
    }
}
