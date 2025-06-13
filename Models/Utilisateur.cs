namespace ElearningBackend.Models
{
    public abstract class Utilisateur
    {
        public int Id { get; set; }

            public string? Nom { get; set; }
            public string? Prenom { get; set; }
            public string? Email { get; set; }
        

        public string CompteBancaire { get; set; }
    }

}
