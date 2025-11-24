using StrengthCoach.Models;

namespace StrengthCoach.Data
{
    public class DatabaseService
    {
        public static void InitializeDatabase()
        {
            using (var context = new StrengthCoachDbContext())
            {
                context.Database.EnsureCreated();
            }
        }

        public static void SavePunchRecord(string studentName, decimal force)
        {
            using (var context = new StrengthCoachDbContext())
            {
                var student = context.Students.FirstOrDefault(s => s.Name == studentName);
                
                if (student == null)
                {
                    throw new InvalidOperationException($"Student '{studentName}' not found in database.");
                }

                var punchRecord = new PunchRecord
                {
                    StudentId = student.Id,
                    Force = force,
                    RecordedAt = DateTime.Now
                };

                context.PunchRecords.Add(punchRecord);
                context.SaveChanges();
            }
        }

        public static void RegisterStudent(string name, int age)
        {
            using (var context = new StrengthCoachDbContext())
            {
                var student = new Student
                {
                    Name = name,
                    Age = age,
                    CreatedAt = DateTime.Now
                };

                context.Students.Add(student);
                context.SaveChanges();
            }
        }

        public static List<string> GetAllStudents()
        {
            using (var context = new StrengthCoachDbContext())
            {
                return context.Students
                    .Select(s => s.Name)
                    .ToList();
            }
        }
    }
}