using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Infrastructure.Data_Models;
using Shared.TenantService;
using Alumni_Portal.Infrastructure.Data_Models;


namespace Alumni_Portal.Infrastructure.Persistence
{
    public class ProjectDbContext : DbContext
    {
        private readonly ITenantService _tenantService;
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options, ITenantService tenantService) : base(options)
        {
            _tenantService = tenantService;
        }

        public DbSet<Projects> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Projects>().HasQueryFilter(p =>
                p.Client_ID == 1 && p.Campus_ID == 1
            );
        }

    }
}
