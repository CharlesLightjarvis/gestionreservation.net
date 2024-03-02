using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestionReservation.Data
{
    public partial class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public virtual DbSet<Client> Clients { get; set; }

        public virtual DbSet<CompagnieAerienne> CompagnieAeriennes { get; set; }

        public virtual DbSet<Paiement> Paiements { get; set; }

        public virtual DbSet<Reservation> Reservations { get; set; }

        public virtual DbSet<SiegeDisponible> SiegeDisponibles { get; set; }

        public virtual DbSet<Tarif> Tarifs { get; set; }

        public virtual DbSet<Vol> Vols { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseSqlServer("Data Source=DESKTOP-EDRSJR0\\MSSQLSERVER1;Initial Catalog=GestionReservation;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey }); // Clé composite
                // Autres configurations si nécessaires
            });


            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.IdClient).HasName("PK__Client__27D6F2128E3D5509");

                entity.ToTable("Client");

                entity.Property(e => e.IdClient).HasColumnName("ID_client");
                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.InformationsPaiement)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Informations_paiement");
                entity.Property(e => e.MotDePasse)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Mot_de_passe");
                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Prenom)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CompagnieAerienne>(entity =>
            {
                entity.HasKey(e => e.IdCompagnie).HasName("PK__Compagni__5499BE82C00D9E16");

                entity.ToTable("CompagnieAerienne");

                entity.Property(e => e.IdCompagnie).HasColumnName("ID_compagnie");
                entity.Property(e => e.FlotteDAvions)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Flotte_d_avions");
                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Paiement>(entity =>
            {
                entity.HasKey(e => e.IdPaiement).HasName("PK__Paiement__264F802F3BC55E16");

                entity.ToTable("Paiement");

                entity.Property(e => e.IdPaiement).HasColumnName("ID_paiement");
                entity.Property(e => e.CodePaiement)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Code_paiement");
                entity.Property(e => e.DatePaiement)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_paiement");
                entity.Property(e => e.IdReservation).HasColumnName("ID_reservation");
                entity.Property(e => e.ModeDePaiement)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("mode_de_paiement");
                entity.Property(e => e.Montant).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.IdReservationNavigation).WithMany(p => p.Paiements)
                    .HasForeignKey(d => d.IdReservation)
                    .HasConstraintName("FK__Paiement__ID_res__3B75D760");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => e.IdReservation).HasName("PK__Reservat__AAC2FDAFFA4CC3E5");

                entity.ToTable("Reservation");

                entity.Property(e => e.IdReservation).HasColumnName("ID_reservation");
                entity.Property(e => e.CodeReservation)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Code_reservation");
                entity.Property(e => e.DateReservation)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_reservation");
                entity.Property(e => e.IdClient).HasColumnName("ID_client");
                entity.Property(e => e.IdCompagnie).HasColumnName("ID_compagnie");
                entity.Property(e => e.IdVol).HasColumnName("ID_vol");
                entity.Property(e => e.Statut)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.IdClient)
                    .HasConstraintName("FK__Reservati__ID_cl__36B12243");

                entity.HasOne(d => d.IdCompagnieNavigation).WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.IdCompagnie)
                    .HasConstraintName("FK__Reservati__ID_co__38996AB5");

                entity.HasOne(d => d.IdVolNavigation).WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.IdVol)
                    .HasConstraintName("FK__Reservati__ID_vo__37A5467C");
            });

            modelBuilder.Entity<SiegeDisponible>(entity =>
            {
                entity.HasKey(e => e.IdSiege).HasName("PK__SiegeDis__D3670AB4FB8A8DE0");

                entity.ToTable("SiegeDisponible");

                entity.Property(e => e.IdSiege).HasColumnName("ID_siege");
                entity.Property(e => e.Classe)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.CodeSiege)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Code_siege");
                entity.Property(e => e.IdVol).HasColumnName("ID_vol");
                entity.Property(e => e.Prix).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.IdVolNavigation).WithMany(p => p.SiegeDisponibles)
                    .HasForeignKey(d => d.IdVol)
                    .HasConstraintName("FK__SiegeDisp__ID_vo__30F848ED");
            });

            modelBuilder.Entity<Tarif>(entity =>
            {
                entity.HasKey(e => e.IdTarif).HasName("PK__Tarif__545957E02C6A4B4B");

                entity.ToTable("Tarif");

                entity.Property(e => e.IdTarif).HasColumnName("ID_tarif");
                entity.Property(e => e.Classe)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.IdVol).HasColumnName("ID_vol");
                entity.Property(e => e.PolitiqueAnnulation)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Politique_annulation");
                entity.Property(e => e.PrixBase)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("Prix_base");

                entity.HasOne(d => d.IdVolNavigation).WithMany(p => p.Tarifs)
                    .HasForeignKey(d => d.IdVol)
                    .HasConstraintName("FK__Tarif__ID_vol__33D4B598");
            });

            modelBuilder.Entity<Vol>(entity =>
            {
                entity.HasKey(e => e.IdVol).HasName("PK__Vol__1F3B949C9FD24271");

                entity.ToTable("Vol");

                entity.Property(e => e.IdVol).HasColumnName("ID_vol");
                entity.Property(e => e.AvionUtilise)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Avion_utilise");
                entity.Property(e => e.DateArrivee)
                    .HasColumnType("date")
                    .HasColumnName("Date_arrivee");
                entity.Property(e => e.DateDepart)
                    .HasColumnType("date")
                    .HasColumnName("Date_depart");
                entity.Property(e => e.Depart)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.Destination)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.HeureArrivee).HasColumnName("Heure_arrivee");
                entity.Property(e => e.HeureDepart).HasColumnName("Heure_depart");
                entity.Property(e => e.IdCompagnie).HasColumnName("ID_compagnie");
                entity.Property(e => e.NumeroVol)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Numero_vol");

                entity.HasOne(d => d.IdCompagnieNavigation).WithMany(p => p.Vols)
                    .HasForeignKey(d => d.IdCompagnie)
                    .HasConstraintName("FK__Vol__ID_compagni__29572725");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}