using ArenaGestor.Domain;
using Microsoft.EntityFrameworkCore;

namespace ArenaGestor.DataAccess
{
    public class ArenaGestorContext : DbContext
    {
        public DbSet<Gender> Genders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<MusicalProtagonist> MusicalProtagonists { get; set; }
        public DbSet<Band> Bands { get; set; }
        public DbSet<Soloist> Soloists { get; set; }
        public DbSet<Concert> Concerts { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketStatus> TicketStatuses { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<ArtistBand> ArtistBands { get; set; }
        public DbSet<ConcertProtagonist> ConcertProtagonists { get; set; }
        public DbSet<Country> Countrys { get; set; }

        public ArenaGestorContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gender>().ToTable("Gender");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<Artist>().ToTable("Artist");
            modelBuilder.Entity<MusicalProtagonist>().ToTable("MusicalProtagonist");
            modelBuilder.Entity<Band>().ToTable("Band");
            modelBuilder.Entity<Soloist>().ToTable("Soloist");
            modelBuilder.Entity<Ticket>().ToTable("Ticket");
            modelBuilder.Entity<TicketStatus>().ToTable("TicketStatus");
            modelBuilder.Entity<Concert>().ToTable("Concert");
            modelBuilder.Entity<Session>().ToTable("Session");
            modelBuilder.Entity<UserRole>().ToTable("RoleUser");
            modelBuilder.Entity<ArtistBand>().ToTable("ArtistBand");
            modelBuilder.Entity<ConcertProtagonist>().ToTable("ConcertProtagonist");

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.HasIndex(e => e.Name).IsUnique();
            });
            modelBuilder.Entity<MusicalProtagonist>(entity =>
            {
                entity.HasIndex(e => e.Name).IsUnique();
            });
            modelBuilder.Entity<Artist>(entity =>
            {
                entity.HasIndex(e => e.Name).IsUnique();
            });
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
            });

            modelBuilder.Entity<TicketStatus>(entity =>
            {
                entity.HasIndex(e => e.Status).IsUnique();
            });
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasIndex(e => e.Name).IsUnique();
            });

            modelBuilder.Entity<TicketStatus>().HasData(
                new TicketStatus { TicketStatusId = TicketCode.Comprado, Status = TicketCode.Comprado.ToString() },
                new TicketStatus { TicketStatusId = TicketCode.Utilizado, Status = TicketCode.Utilizado.ToString() });

            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = RoleCode.Administrador, Name = RoleCode.Administrador.ToString() },
                new Role { RoleId = RoleCode.Vendedor, Name = RoleCode.Vendedor.ToString() },
                new Role { RoleId = RoleCode.Acomodador, Name = RoleCode.Acomodador.ToString() },
                new Role { RoleId = RoleCode.Espectador, Name = RoleCode.Espectador.ToString() },
                new Role { RoleId = RoleCode.Artista, Name = RoleCode.Artista.ToString() });

            modelBuilder.Entity<RoleArtist>().HasData(
                new RoleArtist { RoleArtistId = RoleArtistCode.Cantante, Name = RoleArtistCode.Cantante.ToString() },
                new RoleArtist { RoleArtistId = RoleArtistCode.Baterista, Name = RoleArtistCode.Baterista.ToString() },
                new RoleArtist { RoleArtistId = RoleArtistCode.Bajista, Name = RoleArtistCode.Bajista.ToString() },
                new RoleArtist { RoleArtistId = RoleArtistCode.Guitarrista, Name = RoleArtistCode.Guitarrista.ToString() },
                new RoleArtist { RoleArtistId = RoleArtistCode.Coro, Name = RoleArtistCode.Coro.ToString() });

            modelBuilder.Entity<Country>().HasData(
                new Country { CountryId = 1, Name = "Uruguay" },
                new Country { CountryId = 2, Name = "Argentina" });

            modelBuilder.Entity<Artist>()
                .HasMany(c => c.Soloists)
                .WithOne(e => e.Artist)
                .IsRequired();

            modelBuilder.Entity<ArtistBand>()
                .HasKey(ur => new { ur.ArtistId, ur.MusicalProtagonistId });

            modelBuilder.Entity<ArtistBand>()
                .HasOne(ab => ab.Artist)
                .WithMany(a => a.Bands)
                .HasForeignKey(b => b.ArtistId);

            modelBuilder.Entity<ArtistBand>()
                .HasOne(ab => ab.Band)
                .WithMany(b => b.Artists)
                .HasForeignKey(b => b.MusicalProtagonistId);

            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.RoleId, ur.UserId });

            modelBuilder.Entity<UserRole>()
                .HasOne(p => p.Role)
                .WithMany(r => r.Users);

            modelBuilder.Entity<UserRole>()
                .HasOne(p => p.User)
                .WithMany(r => r.Roles);

            modelBuilder.Entity<Gender>()
                .HasMany(x => x.MusicalProtagonists)
                .WithOne(x => x.Gender);

            modelBuilder.Entity<ConcertProtagonist>()
                 .HasKey(ur => new { ur.ConcertId, ur.MusicalProtagonistId });

            modelBuilder.Entity<ConcertProtagonist>()
                .HasOne(cp => cp.Concert)
                .WithMany(c => c.Protagonists)
                .HasForeignKey(b => b.ConcertId);

            modelBuilder.Entity<ConcertProtagonist>()
                .HasOne(cp => cp.Protagonist)
                .WithMany(p => p.Concerts)
                .HasForeignKey(c => c.MusicalProtagonistId);
        }
    }
}
