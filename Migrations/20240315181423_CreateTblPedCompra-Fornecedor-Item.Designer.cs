﻿// <auto-generated />
using System;
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
    [Migration("20240315181423_CreateTblPedCompra-Fornecedor-Item")]
    partial class CreateTblPedCompraFornecedorItem
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

            modelBuilder.Entity("ManyMindsApi.Models.Fornecedor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("Fornecedor");
                });

            modelBuilder.Entity("ManyMindsApi.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("PedidoCompraId")
                        .HasColumnType("int");

                    b.Property<int>("Produtocodigo")
                        .HasColumnType("int");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PedidoCompraId");

                    b.HasIndex("Produtocodigo");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("ManyMindsApi.Models.PedidoCompra", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FornecedorId")
                        .HasColumnType("int");

                    b.Property<string>("Obs")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<bool>("status")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("FornecedorId");

                    b.ToTable("pedidosCompra");
                });

            modelBuilder.Entity("ManyMindsApi.Models.Item", b =>
                {
                    b.HasOne("ManyMindsApi.Models.PedidoCompra", null)
                        .WithMany("Itens")
                        .HasForeignKey("PedidoCompraId");

                    b.HasOne("ManyMinds.Models.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("Produtocodigo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("ManyMindsApi.Models.PedidoCompra", b =>
                {
                    b.HasOne("ManyMindsApi.Models.Fornecedor", "Fornecedor")
                        .WithMany()
                        .HasForeignKey("FornecedorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Fornecedor");
                });

            modelBuilder.Entity("ManyMindsApi.Models.PedidoCompra", b =>
                {
                    b.Navigation("Itens");
                });
#pragma warning restore 612, 618
        }
    }
}