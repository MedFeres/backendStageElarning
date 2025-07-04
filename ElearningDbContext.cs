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
        public DbSet<ResultatQuiz> ResultatsQuiz { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contenu>()
                .HasDiscriminator<string>("TypeContenu")
                .HasValue<Quiz>("Quiz")
                .HasValue<Video>("Video")
                .HasValue<Resume>("Resume");

            modelBuilder.Entity<Certificat>()
                .HasOne(c => c.Admin)
                .WithMany(a => a.CertificatsGeneres)
                .HasForeignKey(c => c.AdminId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Expediteur)
                .WithMany()
                .HasForeignKey(m => m.ExpediteurId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Destinataire)
                .WithMany()
                .HasForeignKey(m => m.DestinataireId)
                .OnDelete(DeleteBehavior.Restrict);

            // Ajout important ici : empêche la suppression en cascade sur Paiement.ClientId
            modelBuilder.Entity<Paiement>()
                .HasOne(p => p.Client)
                .WithMany(c => c.Paiements)  // colle bien à ta propriété ICollection<Paiement> dans Client
                .HasForeignKey(p => p.ClientId)
                .OnDelete(DeleteBehavior.Restrict); // <== interdit cascade delete ici

            // Garde la suppression en cascade sur Paiement.CoursId si souhaité
            modelBuilder.Entity<Paiement>()
                .HasOne(p => p.Cours)
                .WithMany()
                .HasForeignKey(p => p.CoursId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ResultatQuiz>()
    .HasOne(rq => rq.Client)
    .WithMany(c => c.ResultatsQuiz)
    .HasForeignKey(rq => rq.ClientId)
    .OnDelete(DeleteBehavior.Restrict); // ✅ ou .NoAction()

        }

    }

}
