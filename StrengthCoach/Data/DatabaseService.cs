using StrengthCoach.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        public static List<StudentRankingViewModel> GetStudentRanking(RankingCategory category = RankingCategory.General)
        {
            using (var context = new StrengthCoachDbContext())
            {
                var studentsWithRecords = context.Students
                    .Include(s => s.PunchRecords)
                    .AsNoTracking()
                    .ToList();

                var filteredStudents = category switch
                {
                    RankingCategory.Kids => studentsWithRecords.Where(s => s.Age >= 1 && s.Age <= 8),
                    RankingCategory.SuperKids => studentsWithRecords.Where(s => s.Age >= 9 && s.Age <= 11),
                    RankingCategory.Teenagers => studentsWithRecords.Where(s => s.Age >= 12 && s.Age <= 15),
                    RankingCategory.Open => studentsWithRecords.Where(s => s.Age >= 16),
                    _ => studentsWithRecords 
                };

                return filteredStudents
                    .Where(s => s.PunchRecords.Any())
                    .Select(student => 
                    {
                        var maxPunch = student.PunchRecords.MaxBy(r => r.Force);
                        return new StudentRankingViewModel
                        {
                            Name = student.Name,
                            HitForce = maxPunch.Force,
                            Date = maxPunch.RecordedAt
                        };
                    })
                    .OrderByDescending(r => r.HitForce)
                    .ToList();
            }
        }

        public static void DeleteStudent(string studentName)
        {
            using (var context = new StrengthCoachDbContext())
            {
                var student = context.Students.FirstOrDefault(s => s.Name == studentName);
      
                if (student == null)
                {
                    throw new InvalidOperationException($"Student '{studentName}' not found in database.");
                }

                var punchRecords = context.PunchRecords.Where(p => p.StudentId == student.Id).ToList();
                context.PunchRecords.RemoveRange(punchRecords);

                context.Students.Remove(student);
                context.SaveChanges();
            }
        }
    }
}