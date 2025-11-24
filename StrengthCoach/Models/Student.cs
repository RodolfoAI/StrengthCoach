namespace StrengthCoach.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ICollection<PunchRecord> PunchRecords { get; set; } = new List<PunchRecord>();
    }
}