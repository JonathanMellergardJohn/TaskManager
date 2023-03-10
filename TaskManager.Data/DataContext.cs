using Microsoft.EntityFrameworkCore;
using TaskManager.Data.Entities;

namespace TaskManager.Data
{
    public class DataContext : DbContext
    {
        private readonly string _connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\46727\Desktop\ec-utbildning\datalagring\TaskManager\TaskManager.Data\taskManager_DB.mdf;Integrated Security=True;Connect Timeout=30";

        // Uncertain about these constructors
        public DataContext() { }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        // sets
        public DbSet<TaskItemEntity> TaskItems { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }
        public DbSet<StaffEntity> Staff { get; set; }
        public DbSet<TaskItemStatusEntity> TaskItemsStatus { get; set; }

        // Connecting to Db
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connString);
            }
        }
        
        
    }
}
