using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Repository.Contexts
{
    public class StoreDbInitializer : CreateDatabaseIfNotExists<UserContext>
    {
        protected override void Seed(UserContext db)
        {
            try
            {
                db.Teachers.Add(new Teacher
                {
                    Id = 1,
                    Login = "Dekan",
                    FirstName = "Ivan",
                    LastName = "Ivanov",
                    // 06d49632c9dc9bcb62aeaef99612ba6b unhash = 1
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

                Teacher thridTeacher = new Teacher
                {
                    Id = 3,
                    Password = "06d49632c9dc9bcb62aeaef99612ba6b",
                    Role = "Teacher",
                    FirstName = "Andrey",
                    LastName = "Nikolaev",
                    Login = "Nikolaev",
                };

                Teacher fourthTeacher = new Teacher
                {
                    Id = 4,
                    Password = "06d49632c9dc9bcb62aeaef99612ba6b",
                    Role = "Teacher",
                    FirstName = "Alex",
                    LastName = "White",
                    Login = "White",
                };

                Teacher fifthTeacher = new Teacher
                {
                    Id = 5,
                    Password = "06d49632c9dc9bcb62aeaef99612ba6b",
                    Role = "Teacher",
                    FirstName = "Igor",
                    LastName = "Levin",
                    Login = "Levin",
                };

                  Teacher sixthTeacher = new Teacher
                {
                    Id = 6,
                    Password = "06d49632c9dc9bcb62aeaef99612ba6b",
                    Role = "Teacher",
                    FirstName = "Oleg",
                    LastName = "Corn",
                    Login = "Corn",
                };

                Science firstScience = new Science
                {
                    Id = secondTeacher.Id,
                    Name = "Math",
                    Students = new List<Student>(),
                    Marks = new List<Mark>(),
                    Teacher = secondTeacher
                };

                Science secondScience = new Science
                {
                    Id = thridTeacher.Id,
                    Name = "Algorithms",
                    Students = new List<Student>(),
                    Marks = new List<Mark>(),
                    Teacher = thridTeacher
                };

                Science thridScience = new Science
                {
                    Id = fourthTeacher.Id,
                    Name = "Data structures",
                    Students = new List<Student>(),
                    Marks = new List<Mark>(),
                    Teacher = fourthTeacher
                };

                Science fourthScience = new Science
                {
                    Id = fifthTeacher.Id,
                    Name = "Big data",
                    Students = new List<Student>(),
                    Marks = new List<Mark>(),
                    Teacher = fifthTeacher
                };

                Science fifthScience = new Science
                {
                    Id = sixthTeacher.Id,
                    Name = "Machine learning",
                    Students = new List<Student>(),
                    Marks = new List<Mark>(),
                    Teacher = sixthTeacher
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

                Student studentFourth = new Student
                {
                    Id = 4,
                    Login = "Stepin",
                    FirstName = "Alex",
                    LastName = "Stepin",
                    Password = "06d49632c9dc9bcb62aeaef99612ba6b",
                    Role = "Student",
                    Sciences = new List<Science>()
                };

                Student studentFifth = new Student
                {
                    Id = 5,
                    Login = "Kopilov",
                    FirstName = "Alex",
                    LastName = "Kopilov",
                    Password = "06d49632c9dc9bcb62aeaef99612ba6b",
                    Role = "Student",
                    Sciences = new List<Science>()
                };

                Student studentSixth = new Student
                {
                    Id = 6,
                    Login = "Tarasov",
                    FirstName = "Nikita",
                    LastName = "Tarasov",
                    Password = "06d49632c9dc9bcb62aeaef99612ba6b",
                    Role = "Student",
                    Sciences = new List<Science>()
                };

                Student studentSeventh= new Student
                {
                    Id = 7,
                    Login = "Ligin",
                    FirstName = "Andrey",
                    LastName = "Ligin",
                    Password = "06d49632c9dc9bcb62aeaef99612ba6b",
                    Role = "Student",
                    Sciences = new List<Science>()
                };

                Student studentEighth = new Student
                {
                    Id = 8,
                    Login = "Lexin",
                    FirstName = "Maxim",
                    LastName = "Lexin",
                    Password = "06d49632c9dc9bcb62aeaef99612ba6b",
                    Role = "Student",
                    Sciences = new List<Science>()
                };

                Student studentNinth = new Student
                {
                    Id = 9,
                    Login = "Kasenko",
                    FirstName = "Mikhail",
                    LastName = "Kasenko",
                    Password = "06d49632c9dc9bcb62aeaef99612ba6b",
                    Role = "Student",
                    Sciences = new List<Science>()
                };

                Student studentTenth = new Student
                {
                    Id = 10,
                    Login = "Noskov",
                    FirstName = "Pavel",
                    LastName = "Noskov",
                    Password = "06d49632c9dc9bcb62aeaef99612ba6b",
                    Role = "Student",
                    Sciences = new List<Science>()
                };

                db.Teachers.AddRange(new List<Teacher> {secondTeacher, thridTeacher, fourthTeacher, fifthTeacher});
                firstScience.Teacher = secondTeacher;
                secondScience.Teacher = thridTeacher;
                thridScience.Teacher = fourthTeacher;
                fourthScience.Teacher = fifthTeacher;
                fifthScience.Teacher = sixthTeacher;
                db.Students.AddRange(new List<Student> {studentFirst, studentSecond, studentThrid,studentFourth,studentFifth,studentSixth,studentSeventh,studentEighth,studentNinth,studentTenth});
                db.Sciences.AddRange(new List<Science> {firstScience,secondScience,thridScience,fourthScience,fifthScience});
                db.SaveChanges();
                firstScience.Students = new List<Student> { studentFirst, studentSecond, studentThrid, studentFourth, studentFifth, studentSixth, studentSeventh, studentEighth, studentNinth, studentTenth };
                secondScience.Students = new List<Student> { studentFirst, studentSecond, studentThrid, studentFourth, studentFifth, studentSixth, studentSeventh, studentEighth, studentNinth, studentTenth };
                thridScience.Students = new List<Student> { studentFirst, studentSecond, studentThrid, studentFifth, studentTenth };
                fourthScience.Students = new List<Student> { studentFirst, studentFourth, studentFifth, studentSixth, studentNinth, studentTenth };
                fifthScience.Students = new List<Student> {  studentSecond, studentThrid,  studentFifth, studentSixth,  studentEighth, studentNinth,  };
                Random rnd = new Random();
                foreach (var science in db.Sciences)
                {
                    Science someScience = science;
                    foreach (var stud in someScience.Students)
                    {
                        var bal = rnd.Next(1, 6);
                        someScience.Marks.Add(new Mark()
                        {
                            Key = stud.Id,
                            Value = bal
                        });
                        stud.AverageBal = bal;
                        db.Entry(stud).State = EntityState.Modified;
                    }
                    db.Entry(science).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
