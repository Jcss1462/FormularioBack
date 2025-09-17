using System;
using System.Collections.Generic;
using FormularioBack.Models;
using Microsoft.EntityFrameworkCore;

namespace FormularioBack.Context;

public partial class FormularioDbContext : DbContext
{
    public FormularioDbContext()
    {
    }

    public FormularioDbContext(DbContextOptions<FormularioDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Formulario> Formularios { get; set; }

    public virtual DbSet<FormularioHasPregunta> FormularioHasPreguntas { get; set; }

    public virtual DbSet<Opcione> Opciones { get; set; }

    public virtual DbSet<Pregunta> Preguntas { get; set; }

    public virtual DbSet<Respuesta> Respuestas { get; set; }

    public virtual DbSet<RespuestasPregunta> RespuestasPreguntas { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Formulario>(entity =>
        {
            entity.HasKey(e => e.IdFormulario).HasName("PK__Formular__090ED3C5DE3528A9");

            entity.Property(e => e.Nombre).HasMaxLength(200);
        });

        modelBuilder.Entity<FormularioHasPregunta>(entity =>
        {
            entity.HasKey(e => e.IdFhp).HasName("PK__Formular__0FE2BFE21037B912");

            entity.Property(e => e.IdFhp)
                .HasDefaultValueSql("(NEXT VALUE FOR [SeqFormularioHasPreguntas])")
                .HasColumnName("IdFHP");

            entity.HasOne(d => d.IdFormularioNavigation).WithMany(p => p.FormularioHasPregunta)
                .HasForeignKey(d => d.IdFormulario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FHP_Formulario");

            entity.HasOne(d => d.IdPreguntaNavigation).WithMany(p => p.FormularioHasPregunta)
                .HasForeignKey(d => d.IdPregunta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FHP_Pregunta");
        });

        modelBuilder.Entity<Opcione>(entity =>
        {
            entity.HasKey(e => e.IdOpcion).HasName("PK__Opciones__4F23885828FB1C6E");

            entity.Property(e => e.Texto).HasMaxLength(300);

            entity.HasOne(d => d.IdPreguntaNavigation).WithMany(p => p.Opciones)
                .HasForeignKey(d => d.IdPregunta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Opcion_Pregunta");
        });

        modelBuilder.Entity<Pregunta>(entity =>
        {
            entity.HasKey(e => e.IdPregunta).HasName("PK__Pregunta__754EC09E5A40220A");

            entity.Property(e => e.Pregunta1)
                .HasMaxLength(500)
                .HasColumnName("Pregunta");
        });

        modelBuilder.Entity<Respuesta>(entity =>
        {
            entity.HasKey(e => e.IdRespuesta).HasName("PK__Respuest__D3480198B3DC693F");

            entity.Property(e => e.FechaRespuesta)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdFormularioNavigation).WithMany(p => p.Respuesta)
                .HasForeignKey(d => d.IdFormulario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Respuesta_Formulario");
        });

        modelBuilder.Entity<RespuestasPregunta>(entity =>
        {
            entity.HasKey(e => e.IdRp).HasName("PK__Respuest__B7702AE111519048");

            entity.Property(e => e.IdRp)
                .HasDefaultValueSql("(NEXT VALUE FOR [SeqRespuestaPregunta])")
                .HasColumnName("IdRP");

            entity.HasOne(d => d.IdOpcionSeleccionadaNavigation).WithMany(p => p.RespuestasPregunta)
                .HasForeignKey(d => d.IdOpcionSeleccionada)
                .HasConstraintName("FK_RP_Opcion");

            entity.HasOne(d => d.IdPreguntaNavigation).WithMany(p => p.RespuestasPregunta)
                .HasForeignKey(d => d.IdPregunta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RP_Pregunta");

            entity.HasOne(d => d.IdRespuestaNavigation).WithMany(p => p.RespuestasPregunta)
                .HasForeignKey(d => d.IdRespuesta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RP_Respuesta");
        });
        modelBuilder.HasSequence("SeqFormularioHasPreguntas");
        modelBuilder.HasSequence("SeqRespuestaPregunta");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
