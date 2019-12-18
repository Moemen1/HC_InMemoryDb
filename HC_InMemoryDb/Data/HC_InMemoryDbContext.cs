using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HC_InMemoryDb.Models
{
    public class HC_InMemoryDbContext : DbContext
    {
        public HC_InMemoryDbContext (DbContextOptions<HC_InMemoryDbContext> options)
            : base(options)
        {
        }

        public DbSet<HC_InMemoryDb.Models.Student> Student { get; set; }
    }
}
