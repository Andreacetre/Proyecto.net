﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TeoSoft.Data;

#nullable disable

namespace TeoSoft.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241208235815_ModeloLoginYRegistroUUUU")]
    partial class ModeloLoginYRegistroUUUU
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TeoSoft.Models.AutenticacionModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Contrasena")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CorreoElectronico")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CorreoElectronico")
                        .IsUnique();

                    b.ToTable("Autenticaciones");
                });

            modelBuilder.Entity("TeoSoft.Models.CategoriaProducto", b =>
                {
                    b.Property<int>("IdCatProduc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCatProduc"));

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCatProduc");

                    b.ToTable("CategoriasProductos");
                });

            modelBuilder.Entity("TeoSoft.Models.Cliente", b =>
                {
                    b.Property<int>("ClienteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClienteId"));

                    b.Property<string>("Correo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Direccion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Documento")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClienteId");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("TeoSoft.Models.DetallePedido", b =>
                {
                    b.Property<int>("IdDetPedido")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDetPedido"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<int>("IdPedido")
                        .HasColumnType("int");

                    b.Property<int>("IdProducto")
                        .HasColumnType("int");

                    b.Property<decimal>("PrecioUnitario")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("IdDetPedido");

                    b.HasIndex("IdPedido");

                    b.HasIndex("IdProducto");

                    b.ToTable("DetallesPedidos");
                });

            modelBuilder.Entity("TeoSoft.Models.DetalleVenta", b =>
                {
                    b.Property<int>("DetalleVentaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DetalleVentaId"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductoId")
                        .HasColumnType("int");

                    b.Property<decimal>("SubTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("VentaId")
                        .HasColumnType("int");

                    b.HasKey("DetalleVentaId");

                    b.HasIndex("ProductoId");

                    b.HasIndex("VentaId");

                    b.ToTable("DetallesVentas");
                });

            modelBuilder.Entity("TeoSoft.Models.Devolucion", b =>
                {
                    b.Property<int>("IdDevolucion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDevolucion"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<string>("EstadoDeDevolucion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaDeDevolucion")
                        .HasColumnType("datetime2");

                    b.Property<string>("MotivoDeDevolucion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductoId")
                        .HasColumnType("int");

                    b.Property<int>("VentaId")
                        .HasColumnType("int");

                    b.HasKey("IdDevolucion");

                    b.HasIndex("ProductoId");

                    b.HasIndex("VentaId");

                    b.ToTable("Devoluciones");
                });

            modelBuilder.Entity("TeoSoft.Models.Pedido", b =>
                {
                    b.Property<int>("IdPedido")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPedido"));

                    b.Property<string>("DireccionEnvio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EstadoPedido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaDelPedido")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdCliente")
                        .HasColumnType("int");

                    b.Property<int>("IdProducto")
                        .HasColumnType("int");

                    b.Property<string>("MetodoPago")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("MontoTotal")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("IdPedido");

                    b.HasIndex("IdCliente");

                    b.HasIndex("IdProducto");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("TeoSoft.Models.Producto", b =>
                {
                    b.Property<int>("ProductoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductoId"));

                    b.Property<int>("CategoriaProductoId")
                        .HasColumnType("int");

                    b.Property<string>("CodigoDeBarra")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FechaVencimiento")
                        .HasColumnType("datetime2");

                    b.Property<double>("IVA")
                        .HasColumnType("float");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("SinVencimiento")
                        .HasColumnType("bit");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("ProductoId");

                    b.HasIndex("CategoriaProductoId");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("TeoSoft.Models.Venta", b =>
                {
                    b.Property<int>("VentaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VentaId"));

                    b.Property<int>("ClienteId")
                        .HasColumnType("int");

                    b.Property<string>("Estado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdProducto")
                        .HasColumnType("int");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("VentaId");

                    b.HasIndex("ClienteId");

                    b.HasIndex("IdProducto");

                    b.ToTable("Ventas");
                });

            modelBuilder.Entity("TeoSoft.Models.DetallePedido", b =>
                {
                    b.HasOne("TeoSoft.Models.Pedido", "Pedido")
                        .WithMany("DetallesPedido")
                        .HasForeignKey("IdPedido")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeoSoft.Models.Producto", "Producto")
                        .WithMany("DetallePedidos")
                        .HasForeignKey("IdProducto")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Pedido");

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("TeoSoft.Models.DetalleVenta", b =>
                {
                    b.HasOne("TeoSoft.Models.Producto", "Producto")
                        .WithMany("DetalleVentas")
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TeoSoft.Models.Venta", "Venta")
                        .WithMany("DetalleVenta")
                        .HasForeignKey("VentaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producto");

                    b.Navigation("Venta");
                });

            modelBuilder.Entity("TeoSoft.Models.Devolucion", b =>
                {
                    b.HasOne("TeoSoft.Models.Producto", "Producto")
                        .WithMany("Devoluciones")
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TeoSoft.Models.Venta", "Venta")
                        .WithMany("Devoluciones")
                        .HasForeignKey("VentaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Producto");

                    b.Navigation("Venta");
                });

            modelBuilder.Entity("TeoSoft.Models.Pedido", b =>
                {
                    b.HasOne("TeoSoft.Models.Cliente", "Cliente")
                        .WithMany("Pedidos")
                        .HasForeignKey("IdCliente")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TeoSoft.Models.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("IdProducto")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("TeoSoft.Models.Producto", b =>
                {
                    b.HasOne("TeoSoft.Models.CategoriaProducto", "CategoriaProducto")
                        .WithMany("Productos")
                        .HasForeignKey("CategoriaProductoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CategoriaProducto");
                });

            modelBuilder.Entity("TeoSoft.Models.Venta", b =>
                {
                    b.HasOne("TeoSoft.Models.Cliente", "Cliente")
                        .WithMany("Ventas")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TeoSoft.Models.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("IdProducto")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("TeoSoft.Models.CategoriaProducto", b =>
                {
                    b.Navigation("Productos");
                });

            modelBuilder.Entity("TeoSoft.Models.Cliente", b =>
                {
                    b.Navigation("Pedidos");

                    b.Navigation("Ventas");
                });

            modelBuilder.Entity("TeoSoft.Models.Pedido", b =>
                {
                    b.Navigation("DetallesPedido");
                });

            modelBuilder.Entity("TeoSoft.Models.Producto", b =>
                {
                    b.Navigation("DetallePedidos");

                    b.Navigation("DetalleVentas");

                    b.Navigation("Devoluciones");
                });

            modelBuilder.Entity("TeoSoft.Models.Venta", b =>
                {
                    b.Navigation("DetalleVenta");

                    b.Navigation("Devoluciones");
                });
#pragma warning restore 612, 618
        }
    }
}
