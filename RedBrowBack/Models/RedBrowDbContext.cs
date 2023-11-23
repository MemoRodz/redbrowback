using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RedBrowBack.Models;

public partial class RedBrowDbContext : DbContext
{
    public RedBrowDbContext()
    {
    }

    public RedBrowDbContext(DbContextOptions<RedBrowDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PRIMARY");

            entity.ToTable("rol");

            entity.Property(e => e.IdRol)
                .HasColumnType("int(11)")
                .HasColumnName("idRol");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(30)
                .HasColumnName("descripcion");
            entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("fechaRegistro");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.IdRol, "idRol");

            entity.Property(e => e.IdUsuario)
                .HasColumnType("int(11)")
                .HasColumnName("idUsuario");
            entity.Property(e => e.Clave)
                .HasMaxLength(100)
                .HasColumnName("clave");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .HasColumnName("correo");
            entity.Property(e => e.Edad)
                .HasColumnType("int(11)")
                .HasColumnName("edad");
            entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdRol)
                .HasColumnType("int(11)")
                .HasColumnName("idRol");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");

            entity.HasOne(d => d.oRol).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("usuario_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
