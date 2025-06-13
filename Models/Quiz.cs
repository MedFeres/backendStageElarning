namespace ElearningBackend.Models
{
    public class Quiz : Contenu
    {
        public string Niveau { get; set; } // "easy", "medium", "hard"
        public int SeuilMinimalEasy { get; set; }
        public int SeuilMinimalMedium { get; set; }
        public int SeuilMinimalHard { get; set; }
    }
}
