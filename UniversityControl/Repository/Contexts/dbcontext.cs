using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Repository.Contexts
{
    public class UserContext : DbContext
    {

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Science> Sciences { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Science>()
                .HasMany(c => c.Students)
                .WithMany(p => p.Sciences)
                .Map(m =>
                {
            // Ссылка на промежуточную таблицу
            m.ToTable("StudentSciences");

            // Настройка внешних ключей промежуточной таблицы
            m.MapLeftKey("ScienceId");
                    m.MapRightKey("StudentId");
                });
        }

        public UserContext()
        {
            Database.SetInitializer(new StoreDbInitializer());
        }

        public UserContext(string connectionString)
            : base(connectionString)
        {
            Database.SetInitializer(new StoreDbInitializer());
        }
    }

    public class StoreDbInitializer : DropCreateDatabaseAlways<UserContext>
    {
        protected override void Seed(UserContext db)
        {
            try
            {
                db.Teachers.Add(new Teacher
                {
                    Id = 1,
                    Login = "1",
                    FirstName = "Ivan",
                    LastName = "Ivanov",
                    Password = "06d49632c9dc9bcb62aeaef99612ba6b",
                    Role = "admin",
                    Science = null
                });

                Teacher secondTeacher = new Teacher
                {
                    Id = 2,
                    Password = "06d49632c9dc9bcb62aeaef99612ba6b",
                    Role = "Teacher",
                    FirstName = "Oleg",
                    LastName = "Bayutov",
                    Login = "Bayutov",
                };

                Science firstScience = new Science
                {
                    Id = secondTeacher.Id,
                    Name = "Math",
                    Students = new List<Student>(),
                    Marks = new List<Mark>(),
                    Teacher = secondTeacher
                };

                Student studentFirst = new Student
                {
                    Id = 1,
                    Login = "Andrey",
                    FirstName = "Andrey",
                    LastName = "Lepokurov",
                    Password = "06d49632c9dc9bcb62aeaef99612ba6b",
                    Role = "Student",
                    Sciences = new List<Science>()
                    
                };

                Student studentSecond = new Student
                {
                    Id = 2,
                    Login = "Artem",
                    FirstName = "Artem",
                    LastName = "Horev",
                    Password = "06d49632c9dc9bcb62aeaef99612ba6b",
                    Role = "Student",
                    Sciences = new List<Science>()
                };

                Student studentThrid = new Student
                {
                    Id = 3,
                    Login = "Igor",
                    FirstName = "Igor",
                    LastName = "Maksimov",
                    Password = "06d49632c9dc9bcb62aeaef99612ba6b",
                    Role = "Student",
                    Sciences = new List<Science>()
                };

                db.Teachers.Add(secondTeacher);
                firstScience.Teacher=secondTeacher;
                db.Students.AddRange(new List<Student> {studentFirst, studentSecond, studentThrid});
                db.Sciences.Add(firstScience);
                db.SaveChanges();
                firstScience.Students = new List<Student> {studentFirst, studentSecond, studentThrid };
                Random rnd = new Random();
                foreach (var stud in firstScience.Students)
                {
                    var bal = rnd.Next(1, 6);
                    firstScience.Marks.Add(new Mark()
                    {
                        Key = stud.Id,
                        Value = bal
                    });
                    stud.AverageBal = bal;
                    db.Entry(stud).State = EntityState.Modified;
                }
                db.Entry(firstScience).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
