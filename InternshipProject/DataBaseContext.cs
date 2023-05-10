using Microsoft.EntityFrameworkCore;
using Models;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace InternshipProject
{
    /// <summary>
    /// Класс контекста, соединяющий модели с Entity Framework.
    /// </summary>
    public class DataBaseContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null;
        public DbSet<UserGroup> Groups { get; set; } = null;
        public DbSet<UserState> States { get; set; } = null;



        public DataBaseContext() => Database.EnsureCreated();

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
           Database.EnsureCreated();
        }        


    }
}
