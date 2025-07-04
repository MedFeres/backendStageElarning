namespace ElearningBackend.Models
{
    public class Client : Utilisateur
    {
        public ICollection<Paiement> Paiements { get; set; }
        public ICollection<Cours> CoursConsultes { get; set; }
        public ICollection<ResultatQuiz> ResultatsQuiz { get; set; } // ✅ à ajouter

    }

}
