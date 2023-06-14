using Microsoft.EntityFrameworkCore;

namespace Models{
    public class AplikacijaContext : DbContext
    {
        public DbSet<Administrator> Administratori { get; set; }
        public DbSet<Greska> Greske { get; set; }
        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Stanica> Stanice { get; set; }
        public DbSet<Voznja> Voznje { get; set; }
        public DbSet<Prevoznik> Prevoznici {get;set;}

        public DbSet<RecenzijaStanice> RecenzijeStanice { get; set; }
        public DbSet<RecenzijaPrevoznika> RecenzijePrevoznika { get; set; }

        public DbSet<Zahtev> Zahtevi {get;set;}

        public DbSet<Karta> Karte { get; set; }
        public AplikacijaContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

    }
}