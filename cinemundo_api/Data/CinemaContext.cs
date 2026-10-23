using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using cinemundo_api.Models;

namespace cinemundo_api.Data;

public partial class CinemaContext : DbContext
{
    public CinemaContext(DbContextOptions<CinemaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Atore> Atores { get; set; }

    public virtual DbSet<Diretore> Diretores { get; set; }

    public virtual DbSet<Elenco> Elencos { get; set; }

    public virtual DbSet<Filme> Filmes { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Atore>(entity =>
        {
            entity.HasKey(e => e.AtorId).HasName("PRIMARY");

            entity.ToTable("atores");

            entity.Property(e => e.AtorId)
                .HasColumnType("int(11)")
                .HasColumnName("ator_id");
            entity.Property(e => e.AtorNome)
                .HasMaxLength(100)
                .HasColumnName("ator_nome");
            entity.Property(e => e.AtorPais)
                .HasMaxLength(60)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("ator_pais");
        });

        modelBuilder.Entity<Diretore>(entity =>
        {
            entity.HasKey(e => e.DiretorId).HasName("PRIMARY");

            entity.ToTable("diretores");

            entity.Property(e => e.DiretorId)
                .HasColumnType("int(11)")
                .HasColumnName("diretor_id");
            entity.Property(e => e.DiretorNome)
                .HasMaxLength(100)
                .HasColumnName("diretor_nome");
            entity.Property(e => e.DiretorPais)
                .HasMaxLength(60)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("diretor_pais");
        });

        modelBuilder.Entity<Elenco>(entity =>
        {
            entity.HasKey(e => e.ElencoId).HasName("PRIMARY");

            entity.ToTable("elenco");

            entity.HasIndex(e => e.AtorId, "ator_id");

            entity.HasIndex(e => new { e.FilmeId, e.AtorId }, "filme_id").IsUnique();

            entity.Property(e => e.ElencoId)
                .HasColumnType("int(11)")
                .HasColumnName("elenco_id");
            entity.Property(e => e.AtorId)
                .HasColumnType("int(11)")
                .HasColumnName("ator_id");
            entity.Property(e => e.ElencoPersonagens)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("elenco_personagens");
            entity.Property(e => e.FilmeId)
                .HasColumnType("int(11)")
                .HasColumnName("filme_id");

            entity.HasOne(d => d.Ator).WithMany(p => p.Elencos)
                .HasForeignKey(d => d.AtorId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("elenco_ibfk_2");

            entity.HasOne(d => d.Filme).WithMany(p => p.Elencos)
                .HasForeignKey(d => d.FilmeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("elenco_ibfk_1");
        });

        modelBuilder.Entity<Filme>(entity =>
        {
            entity.HasKey(e => e.FilmeId).HasName("PRIMARY");

            entity.ToTable("filmes");

            entity.HasIndex(e => e.DiretorId, "diretor_id");

            entity.HasIndex(e => e.GeneroId, "genero_id");

            entity.Property(e => e.FilmeId)
                .HasColumnType("int(11)")
                .HasColumnName("filme_id");
            entity.Property(e => e.DiretorId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("diretor_id");
            entity.Property(e => e.FilmeAno)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("filme_ano");
            entity.Property(e => e.FilmeClassificacao)
                .HasMaxLength(20)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("filme_classificacao");
            entity.Property(e => e.FilmeDuracao)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("filme_duracao");
            entity.Property(e => e.FilmeTitulo)
                .HasMaxLength(150)
                .HasColumnName("filme_titulo");
            entity.Property(e => e.GeneroId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("genero_id");

            entity.HasOne(d => d.Diretor).WithMany(p => p.Filmes)
                .HasForeignKey(d => d.DiretorId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("filmes_ibfk_1");

            entity.HasOne(d => d.Genero).WithMany(p => p.Filmes)
                .HasForeignKey(d => d.GeneroId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("filmes_ibfk_2");
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.GeneroId).HasName("PRIMARY");

            entity.ToTable("generos");

            entity.Property(e => e.GeneroId)
                .HasColumnType("int(11)")
                .HasColumnName("genero_id");
            entity.Property(e => e.GeneroNome)
                .HasMaxLength(60)
                .HasColumnName("genero_nome");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
