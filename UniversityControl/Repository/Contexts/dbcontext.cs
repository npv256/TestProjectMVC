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

        public UserContext() 
        {
            Database.SetInitializer(new StoreDbInitializer());
        }

        // Пересоздает бд при изменении модели
        public class StoreDbInitializer : DropCreateDatabaseIfModelChanges<UserContext>
        {
            protected override void Seed(UserContext db)
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

                Student studentFirst = new Student
                {
                    Id = 1,
                    Login = "Andrey",
                    FirstName = "Andrey",
                    LastName = "Lepokurov",
                    Password = "06d49632c9dc9bcb62aeaef99612ba6b",
                    Role = "Student",
                    Sciences = new List<Science>(),
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

                Teacher teacherSecond = new Teacher
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
                    Id = teacherSecond.Id,
                    Name = "Math",
                    Rating = null,
                    Students = new List<Student>()
                };

                db.Students.AddRange(new List<Student> { studentFirst, studentSecond, studentThrid });
                db.Teachers.Add(teacherSecond);
                firstScience.Students.AddRange(new List<Student> { studentFirst, studentSecond, studentThrid });
                db.Sciences.Add(firstScience);
                db.SaveChanges();

            }
        }
    }
}
