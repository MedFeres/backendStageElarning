namespace ElearningBackend.Models
{
    public class Client : Utilisateur
    {
        public Client()
        {
            Paiements = new List<Paiement>();
            CoursConsultes = new List<Cours>();
            ResultatsQuiz = new List<ResultatQuiz>();
        }

        public ICollection<Paiement>? Paiements { get; set; }
        public ICollection<Cours>? CoursConsultes { get; set; }
        public ICollection<ResultatQuiz>? ResultatsQuiz { get; set; }
    }
}