using System.Data.Entity;

namespace Whoops_EF_Database_Example
{
    public class EFSampleContext : DbContext
    {
        public EFSampleContext(string connectionString)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<EFSampleContext>());
            Database.Connection.ConnectionString = connectionString;
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
    }
}
