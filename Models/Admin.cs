namespace ElearningBackend.Models
{
    public class Admin : Utilisateur
    {
        public bool EstVerifie { get; set; }

        // Liste des certificats générés
        public ICollection<Certificat> CertificatsGeneres { get; set; }
    }

}
