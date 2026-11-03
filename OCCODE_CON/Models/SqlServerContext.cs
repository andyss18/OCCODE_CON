using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OCCODE_CON.Models;

public partial class SqlServerContext : DbContext
{
    public SqlServerContext()
    {
    }

    public SqlServerContext(DbContextOptions<SqlServerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Modulo> Modulos { get; set; }

    public virtual DbSet<Perfil> Perfils { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<RolModulo> RolModulos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:SqlServerConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Modulo>(entity =>
        {
            entity.HasKey(e => e.IdModulo).HasName("PK__Modulo__B2584DFC52700AF0");

            entity.ToTable("Modulo", "seguridad", tb => tb.HasComment("Representa la pantalla, vista o recurso dentro de la aplicación."));

            entity.Property(e => e.IdModulo).HasColumnName("id_modulo");
            entity.Property(e => e.IdModuloPadre)
                .HasComment("FK opcional para menús desplegables o submenús")
                .HasColumnName("id_modulo_padre");
            entity.Property(e => e.NombreModulo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_modulo");

            entity.HasOne(d => d.IdModuloPadreNavigation).WithMany(p => p.InverseIdModuloPadreNavigation)
                .HasForeignKey(d => d.IdModuloPadre)
                .HasConstraintName("fk_modulo_padre");
        });

        modelBuilder.Entity<Perfil>(entity =>
        {
            entity.HasKey(e => e.IdPerfil).HasName("PK__Perfil__1D1C8768590E40F2");

            entity.ToTable("Perfil", "seguridad", tb => tb.HasComment("Define el perfil o categoría general del usuario."));

            entity.Property(e => e.IdPerfil).HasColumnName("id_perfil");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.NombrePerfil)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("Ej. Administrativo, Cliente VIP, Soporte Técnico")
                .HasColumnName("nombre_perfil");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("PK__Persona__228148B005D07FEA");

            entity.ToTable("Persona", "seguridad", tb => tb.HasComment("Guarda la información física/personal de la persona."));

            entity.HasIndex(e => e.Dpi, "UQ__Persona__D876196525679414").IsUnique();

            entity.Property(e => e.IdPersona)
                .HasComment("Identificador único de la persona")
                .HasColumnName("id_persona");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Correo)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Dpi)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasComment("Documento Personal de Identificación único")
                .HasColumnName("dpi");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__6ABCB5E01EA12984");

            entity.ToTable("Rol", "seguridad", tb => tb.HasComment("Define el nivel o cargo de acceso."));

            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.NombreRol)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Ej. ADMIN, EDITOR, LECTOR")
                .HasColumnName("nombre_rol");
        });

        modelBuilder.Entity<RolModulo>(entity =>
        {
            entity.HasKey(e => new { e.IdRol, e.IdModulo }).HasName("PK__Rol_Modu__B199313F472D8074");

            entity.ToTable("Rol_Modulo", "seguridad");

            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.IdModulo).HasColumnName("id_modulo");
            entity.Property(e => e.PuedeCrear).HasColumnName("puede_crear");
            entity.Property(e => e.PuedeEditar).HasColumnName("puede_editar");
            entity.Property(e => e.PuedeEliminar).HasColumnName("puede_eliminar");
            entity.Property(e => e.PuedeLeer).HasColumnName("puede_leer");

            entity.HasOne(d => d.IdModuloNavigation).WithMany(p => p.RolModulos)
                .HasForeignKey(d => d.IdModulo)
                .HasConstraintName("fk_rm_modulo");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.RolModulos)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("fk_rm_rol");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__4E3E04ADB319C65F");

            entity.ToTable("Usuario", "seguridad", tb => tb.HasComment("Almacena las credenciales y accesos de los usuarios del sistema."));

            entity.HasIndex(e => e.IdPersona, "UQ__Usuario__228148B15B4AC2FE").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__Usuario__F3DBC57229086920").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Estado)
                .HasComment("1 Activo, 0 Inactivo")
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.IdPerfil).HasColumnName("id_perfil");
            entity.Property(e => e.IdPersona).HasColumnName("id_persona");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password_hash");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.IdPerfilNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdPerfil)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_usuario_perfil");

            entity.HasOne(d => d.IdPersonaNavigation).WithOne(p => p.Usuario)
                .HasForeignKey<Usuario>(d => d.IdPersona)
                .HasConstraintName("fk_usuario_persona");

            entity.HasMany(d => d.IdRols).WithMany(p => p.IdUsuarios)
                .UsingEntity<Dictionary<string, object>>(
                    "UsuarioRol",
                    r => r.HasOne<Rol>().WithMany()
                        .HasForeignKey("IdRol")
                        .HasConstraintName("fk_urole_rol"),
                    l => l.HasOne<Usuario>().WithMany()
                        .HasForeignKey("IdUsuario")
                        .HasConstraintName("fk_urole_usuario"),
                    j =>
                    {
                        j.HasKey("IdUsuario", "IdRol").HasName("PK__Usuario___5895CFF3D940D62F");
                        j.ToTable("Usuario_Rol", "seguridad");
                        j.IndexerProperty<int>("IdUsuario").HasColumnName("id_usuario");
                        j.IndexerProperty<int>("IdRol").HasColumnName("id_rol");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
