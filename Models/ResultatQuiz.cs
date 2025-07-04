namespace ElearningBackend.Models
{
    public class ResultatQuiz
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }

        public int Score { get; set; }
        public DateTime DateSoumission { get; set; }
    }

}
