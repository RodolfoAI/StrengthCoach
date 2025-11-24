namespace StrengthCoach.Models
{
    public class PunchRecord
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public decimal Force { get; set; } // in kilograms
        public DateTime RecordedAt { get; set; } = DateTime.Now;
        public Student Student { get; set; }
    }
}