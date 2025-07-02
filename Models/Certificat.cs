using ElearningBackend.Models;

public class Certificat
{
    public int Id { get; set; }
    public string NomClient { get; set; }
    public DateTime DateObtention { get; set; }

    // Relation avec Admin
    public int AdminId { get; set; }
    public Admin Admin { get; set; }
}
