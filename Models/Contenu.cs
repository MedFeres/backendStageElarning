namespace ElearningBackend.Models
{
    public abstract class Contenu
    {
        public int Id { get; set; }
        public bool EstPayant { get; set; }
        public string Fichier { get; set; }

        public int CoursId { get; set; }
        public Cours Cours { get; set; }
    }

}
