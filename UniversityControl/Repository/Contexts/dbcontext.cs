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
}
