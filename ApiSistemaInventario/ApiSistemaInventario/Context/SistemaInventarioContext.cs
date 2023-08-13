using System;
using System.Collections.Generic;
using ApiSistemaInventario.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiSistemaInventario.Context;

public partial class SistemaInventarioContext : DbContext
{
    public SistemaInventarioContext()
    {
    }

    public SistemaInventarioContext(DbContextOptions<SistemaInventarioContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CategoriaProducto> CategoriaProductos { get; set; }

    public virtual DbSet<EntradaProducto> EntradaProductos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<SalidaProducto> SalidaProductos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoriaProducto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__categori__3213E83F7E541418");

            entity.ToTable("categoria_producto");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Tipo)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<EntradaProducto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__entrada___3213E83FD4B7972B");

            entity.ToTable("entrada_producto", tb => tb.HasTrigger("trg_AumentarStock"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Idproducto).HasColumnName("idproducto");
            entity.Property(e => e.StockE).HasColumnName("stockE");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.EntradaProductos)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_Usuario_Entradaproducto");

            entity.HasOne(d => d.IdproductoNavigation).WithMany(p => p.EntradaProductos)
                .HasForeignKey(d => d.Idproducto)
                .HasConstraintName("FK_Producto_Entradaproducto");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__producto__3213E83F0139A493");

            entity.ToTable("producto");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Idcategoria).HasColumnName("idcategoria");
            entity.Property(e => e.Marca)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("marca");
            entity.Property(e => e.Nombre)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasColumnType("smallmoney")
                .HasColumnName("precio");
            entity.Property(e => e.Stock).HasColumnName("stock");

            entity.HasOne(d => d.IdcategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.Idcategoria)
                .HasConstraintName("FK_CategoriaP_Producto");
        });

        modelBuilder.Entity<SalidaProducto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__salida_p__3213E83F4A199498");

            entity.ToTable("salida_producto", tb => tb.HasTrigger("trg_ReducirStock"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Idproducto).HasColumnName("idproducto");
            entity.Property(e => e.StockS).HasColumnName("stockS");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.SalidaProductos)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_Usuario_Salidaproducto");

            entity.HasOne(d => d.IdproductoNavigation).WithMany(p => p.SalidaProductos)
                .HasForeignKey(d => d.Idproducto)
                .HasConstraintName("FK_Producto_Salidaproducto");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__usuario__3213E83F742F2E73");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.Username, "UQ__usuario__F3DBC5727CD37ACF").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("((1))")
                .HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
