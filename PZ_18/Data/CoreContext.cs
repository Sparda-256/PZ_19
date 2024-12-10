using Microsoft.EntityFrameworkCore;
using PZ_18.Models;

namespace PZ_18.Data
{
    /// <summary>
    /// EF Core контекст базы данных.
    /// Настройка подключения к БД через UseSqlServer.
    /// </summary>
    public class CoreContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<UserCategory> UserCategories { get; set; }
        public DbSet<ApplianceCategory> ApplianceCategories { get; set; }
        public DbSet<RepairRequest> RepairRequests { get; set; }
        public DbSet<RepairComponent> RepairComponents { get; set; }
        public DbSet<Note> Notes { get; set; }

        public CoreContext() : base() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Строка подключения к вашей базе данных
                optionsBuilder.UseSqlServer("data source=192.168.147.54;initial catalog=RepairServiceDB;user id=is;password=1;encrypt=False;");
            }
        }
    }
}
