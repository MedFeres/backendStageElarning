namespace ElearningBackend.Models
{
    public class Formateur : Client
    {
        public string Cv { get; set; }
        public string Diplome { get; set; }
        public bool EstValide { get; set; }

        public ICollection<Cours> CoursCree { get; set; }
    }

}
