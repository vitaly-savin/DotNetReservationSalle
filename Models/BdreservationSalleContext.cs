using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace backend.Models;

public partial class BdreservationSalleContext : IdentityDbContext<ApplicationUser>
{
    public BdreservationSalleContext(DbContextOptions<BdreservationSalleContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Administrateur> Administrateurs { get; set; }

    public virtual DbSet<EtatInvitation> EtatInvitations { get; set; }

    public virtual DbSet<EtatReservation> EtatReservations { get; set; }

    public virtual DbSet<Invitation> Invitations { get; set; }

    public virtual DbSet<Membre> Membres { get; set; }

    public virtual DbSet<Personne> Personnes { get; set; }

    public virtual DbSet<Plainte> Plaintes { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<SalleLaboratoire> SalleLaboratoires { get; set; }

    public virtual DbSet<TypeActivite> TypeActivites { get; set; }

    //public virtual DbSet<SalleLaboratoireTypeActivite> SalleLaboratoireTypeActivites { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Server=tgvd.database.windows.net;database=BDReservationSalle;uid=tgvdAdmin;pwd=Toscane2000**");


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administrateur>(entity =>
        {
            entity.HasKey(e => e.Courriel);

            entity.ToTable("Administrateur");

            entity.Property(e => e.Courriel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("courriel");
            entity.Property(e => e.Matricule)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("matricule");

            entity.HasOne(d => d.CourrielNavigation).WithOne(p => p.Administrateur)
                .HasForeignKey<Administrateur>(d => d.Courriel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Administrateur_Personne");
        });

        modelBuilder.Entity<EtatInvitation>(entity =>
        {
            entity.HasKey(e => e.IdEtatInvitation);

            entity.ToTable("etatInvitation");

            entity.Property(e => e.IdEtatInvitation).HasColumnName("idEtatInvitation");
            entity.Property(e => e.NomEtatInvitation)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nomEtatInvitation");
        });

        modelBuilder.Entity<EtatReservation>(entity =>
        {
            entity.HasKey(e => e.IdEtatReservation);

            entity.ToTable("etatReservation");

            entity.Property(e => e.IdEtatReservation).HasColumnName("idEtatReservation");
            entity.Property(e => e.NomEtatReservation)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nomEtatReservation");
        });

        modelBuilder.Entity<Invitation>(entity =>
        {
            entity.HasKey(e => new { e.NoReservation, e.MembreCourriel });

            entity.ToTable("Invitation");

            entity.Property(e => e.NoReservation).HasColumnName("noReservation");
            entity.Property(e => e.MembreCourriel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("membreCourriel");
            entity.Property(e => e.DateReponse)
                .HasColumnType("date")
                .HasColumnName("dateReponse");
            entity.Property(e => e.IdEtatInvitation).HasColumnName("idEtatInvitation");

            entity.HasOne(d => d.IdEtatInvitationNavigation).WithMany(p => p.Invitations)
                .HasForeignKey(d => d.IdEtatInvitation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invitation_etatInvitation");

            entity.HasOne(d => d.MembreCourrielNavigation).WithMany(p => p.Invitations)
                .HasForeignKey(d => d.MembreCourriel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invitation_Membre");

            entity.HasOne(d => d.NoReservationNavigation).WithMany(p => p.Invitations)
                .HasForeignKey(d => d.NoReservation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invitation_Reservation");
        });

        modelBuilder.Entity<Membre>(entity =>
        {
            entity.HasKey(e => e.Courriel);

            entity.ToTable("Membre");

            entity.Property(e => e.Courriel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("courriel");
            entity.Property(e => e.Adresse)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("adresse");
            entity.Property(e => e.CodePostal)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("codePostal");
            entity.Property(e => e.EstActif)
                .HasDefaultValueSql("((0))")
                .HasColumnName("estActif");
            entity.Property(e => e.EtatModifierParAdministrateurCourriel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("etatModifierParAdministrateurCourriel");
            entity.Property(e => e.Province)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("province");
            entity.Property(e => e.Telephone)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("telephone");

            entity.HasOne(d => d.CourrielNavigation).WithOne(p => p.Membre)
                .HasForeignKey<Membre>(d => d.Courriel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Membre_Personne");

            entity.HasOne(d => d.EtatModifierParAdministrateurCourrielNavigation).WithMany(p => p.Membres)
                .HasForeignKey(d => d.EtatModifierParAdministrateurCourriel)
                .HasConstraintName("FK_Membre_Administrateur");
        });

        modelBuilder.Entity<Personne>(entity =>
        {
            entity.HasKey(e => e.Courriel);

            entity.ToTable("Personne");

            entity.Property(e => e.Courriel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("courriel");
            entity.Property(e => e.Nom)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nom");
            entity.Property(e => e.Prenom)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("prenom");
        });

        modelBuilder.Entity<Plainte>(entity =>
        {
            entity.HasKey(e => new { e.NoReservation, e.MembreCourriel });

            entity.ToTable("Plainte");

            entity.Property(e => e.NoReservation).HasColumnName("noReservation");
            entity.Property(e => e.MembreCourriel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("membreCourriel");
            entity.Property(e => e.AdministrateurCourriel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("administrateurCourriel");
            entity.Property(e => e.DatePlainte)
                .HasColumnType("date")
                .HasColumnName("datePlainte");
            entity.Property(e => e.Description)
                .IsUnicode(false)
                .HasColumnName("description");

            entity.HasOne(d => d.AdministrateurCourrielNavigation).WithMany(p => p.Plaintes)
                .HasForeignKey(d => d.AdministrateurCourriel)
                .HasConstraintName("FK_Plainte_Administrateur");

            entity.HasOne(d => d.MembreCourrielNavigation).WithMany(p => p.Plaintes)
                .HasForeignKey(d => d.MembreCourriel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Plainte_Membre");

            entity.HasOne(d => d.NoReservationNavigation).WithMany(p => p.Plaintes)
                .HasForeignKey(d => d.NoReservation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Plainte_Reservation");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.NoReservation);
            entity.ToTable("Reservation");
            entity.Property(e => e.NoReservation)
               // .ValueGeneratedNever()
                .HasColumnName("noReservation");
            entity.Property(e => e.CreerParMembreCourriel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("creerParMembreCourriel");
            entity.Property(e => e.DateHeureDebut)
                .HasColumnType("datetime")
                .HasColumnName("dateHeureDebut");
            entity.Property(e => e.DateHeureFin)
                .HasColumnType("datetime")
                .HasColumnName("dateHeureFin");
            entity.Property(e => e.IdEtatReservation).HasColumnName("idEtatReservation");
            entity.Property(e => e.NoSalle).HasColumnName("noSalle");
            entity.Property(e => e.NomActivite)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nomActivite");
            entity.Property(e => e.TraiterLe)
                .HasColumnType("date")
                .HasColumnName("traiterLe");
            entity.Property(e => e.TraiterParAdministrateurCourriel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("traiterParAdministrateurCourriel");

            entity.HasOne(d => d.CreerParMembreCourrielNavigation).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.CreerParMembreCourriel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reservation_Membre");

            entity.HasOne(d => d.IdEtatReservationNavigation).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.IdEtatReservation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reservation_etatReservation");

            entity.HasOne(d => d.NoSalleNavigation).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.NoSalle)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reservation_SalleLaboratoire");

            entity.HasOne(d => d.NomActiviteNavigation).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.NomActivite)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reservation_TypeActivite");

            entity.HasOne(d => d.TraiterParAdministrateurCourrielNavigation).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.TraiterParAdministrateurCourriel)
                .HasConstraintName("FK_Reservation_Administrateur");
        });

        modelBuilder.Entity<SalleLaboratoire>(entity =>
        {
            entity.HasKey(e => e.NoSalle);

            entity.ToTable("SalleLaboratoire");

            entity.Property(e => e.NoSalle)
                .ValueGeneratedNever()
                .HasColumnName("noSalle");
            entity.Property(e => e.Capacite).HasColumnName("capacite");
            entity.Property(e => e.CreerParAdministrateurCourriel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("creerParAdministrateurCourriel");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.EstActif)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("estActif");

            entity.HasOne(d => d.CreerParAdministrateurCourrielNavigation).WithMany(p => p.SalleLaboratoires)
                .HasForeignKey(d => d.CreerParAdministrateurCourriel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SalleLaboratoire_Administrateur");
            
            entity.HasMany(d => d.NomActivites).WithMany(p => p.NoSalles)
                .UsingEntity<Dictionary<string, object>>(
                    "SalleLaboratoireTypeActivite",
                    r => r.HasOne<TypeActivite>().WithMany()
                        .HasForeignKey("NomActivite")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_SalleLaboratoire_TypeActivite_TypeActivite"),
                    l => l.HasOne<SalleLaboratoire>().WithMany()
                        .HasForeignKey("NoSalle")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_SalleLaboratoire_TypeActivite_SalleLaboratoire"),
                    j =>
                    {
                        j.HasKey("NoSalle", "NomActivite");
                        j.ToTable("SalleLaboratoire_TypeActivite");
                    });
            
        });

        modelBuilder.Entity<TypeActivite>(entity =>
        {
            entity.HasKey(e => e.NomActivite);

            entity.ToTable("TypeActivite");

            entity.Property(e => e.NomActivite)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nomActivite");
            entity.Property(e => e.CreerParAdministrateurCourriel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("creerParAdministrateurCourriel");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.EstActif)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("estActif");

            entity.HasOne(d => d.CreerParAdministrateurCourrielNavigation).WithMany(p => p.TypeActivites)
                .HasForeignKey(d => d.CreerParAdministrateurCourriel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TypeActivite_Administrateur");
        });
  /*      
        modelBuilder.Entity<SalleLaboratoire_TypeActivite>()
                .HasKey(s => new { s.NoSalle, s.NomActivite });

        modelBuilder.Entity<SalleLaboratoire_TypeActivite>()
                .ToTable("SalleLaboratoire_TypeActivite");

        modelBuilder.Entity<SalleLaboratoire_TypeActivite>()
            .HasOne(s => s.SalleLaboratoire)
            .WithMany(sa => sa.SalleLaboratoire_TypeActivites)
            .HasForeignKey(s => s.NoSalle);

        modelBuilder.Entity<SalleLaboratoire_TypeActivite>()
            .HasOne(t => t.TypeActivite)
            .WithMany(sa => sa.SalleLaboratoire_TypeActivites)
            .HasForeignKey(t => t.NomActivite);
     */   
        base.OnModelCreating(modelBuilder);
        OnModelCreatingPartial(modelBuilder);
        
            
    }
    
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
