using Microsoft.EntityFrameworkCore;
using PetProject.Entitys;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasOne(p => p.User)
                .WithMany(u => u.Projects)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<NodeGraph>()
                .HasOne(g => g.Project)
                .WithMany(p => p.NodeGraphs)
                .HasForeignKey(g => g.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
