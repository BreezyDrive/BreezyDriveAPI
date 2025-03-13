using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreezyDrive.CommonService.Infrastuctures.Data
{
    public class BaseDbContext<TContext> : DbContext where TContext : DbContext
    {
        protected BaseDbContext(DbContextOptions<TContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TContext).Assembly);
        }
    }
}
