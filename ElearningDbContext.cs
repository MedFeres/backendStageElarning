using ElearningBackend.Models;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
namespace ElearningBackend
{
    public class ElearningDbContext : DbContext
    {
        public ElearningDbContext(DbContextOptions<ElearningDbContext> options) : base(options) { }

        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Formateur> Formateurs { get; set; }
        public DbSet<Admin> Admins { get; set; }

        public DbSet<Cours> Cours { get; set; }
        public DbSet<Contenu> Contenus { get; set; }
        public DbSet<Quiz> Quizs { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Resume> Resumes { get; set; }

        public DbSet<Paiement> Paiements { get; set; }
        public DbSet<Certificat> Certificats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contenu>()
                .HasDiscriminator<string>("TypeContenu")
                .HasValue<Quiz>("Quiz")
                .HasValue<Video>("Video")
                .HasValue<Resume>("Resume");
        }
    }

}
