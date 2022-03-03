using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace Back.Data
{
    public class DBcs:DbContext
    {
        public DBcs(DbContextOptions<DBcs> options): base(options)
        {

        }
        public Microsoft.EntityFrameworkCore.DbSet<Users> User { get; set; }

    }
}
