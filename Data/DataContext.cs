using ApiDevBP.Entities;
using Microsoft.EntityFrameworkCore;
using SQLite;

namespace ApiDevBP.Data
{
    public class DataContext: DbContext 
    { 

        public DataContext(DbContextOptions options): base(options)
        {
        }

        DbSet<UserEntity> Users { get; set; }
    }
}
