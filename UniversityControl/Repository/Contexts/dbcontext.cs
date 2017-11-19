using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Repository.Contexts
{
    public class dbcontext : DbContext
    {

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Science> Sciences { get; set; }

        public dbcontext()
        {
            Database.SetInitializer(new StoreDbInitializer());
        }



        public class StoreDbInitializer : DropCreateDatabaseAlways<dbcontext>
        {
            protected override void Seed(dbcontext db)
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
            }
        }
    }
}
