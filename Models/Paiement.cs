﻿using ElearningBackend.Models;

public class Paiement
{
    public int Id { get; set; }
    public float Montant { get; set; }
    public DateTime DatePaiement { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; }

    public int CoursId { get; set; }
    public Cours Cours { get; set; }
}
