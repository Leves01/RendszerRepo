using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users => Set<User>();

        public DbSet<Part> Parts => Set<Part>();

        public DbSet<Storage> Storages => Set<Storage>();

        public DbSet<Project> Project => Set<Project>();

        public DbSet<Project_properties> ProjectProperties => Set<Project_properties>();

        public DbSet<reservedParts> Reserves => Set<reservedParts>();
    }
}