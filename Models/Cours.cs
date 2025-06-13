namespace ElearningBackend.Models
{
    public class Cours
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public bool EstPayant { get; set; }
        public float Prix { get; set; }

        public int FormateurId { get; set; }
        public Formateur Formateur { get; set; }

        public ICollection<Contenu> Contenus { get; set; }
    }

}
