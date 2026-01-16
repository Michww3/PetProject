using Microsoft.EntityFrameworkCore;
using PetProject.DTOs;
using System.Collections.Generic;

namespace PetProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<NodeGraph> NodeGraphs { get; set; }
        public DbSet<CustomNodeType> CustomNodeTypes { get; set; }
    }

}
