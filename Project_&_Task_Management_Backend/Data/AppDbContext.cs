
using Microsoft.EntityFrameworkCore;
using Project___Task_Management_Backend.Models;

namespace Project___Task_Management_Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> users { get; set; }
        public DbSet<Project> projects { get; set; }
        public DbSet<ProjectTask> tasks { get; set; }
        public DbSet<Comment> comments { get; set; }
        public DbSet<Doc> docs { get; set; }
        public DbSet<Activity> activities { get; set; }
        public DbSet<UserProject> userProjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasOne(c => c.file)
                .WithOne()
                .HasForeignKey<Project>(c => c.fileId)
                .OnDelete(DeleteBehavior.SetNull);

            // Configure relationships
            modelBuilder.Entity<ProjectTask>()
                .HasOne(c => c.file)
                .WithOne()
                .HasForeignKey<ProjectTask>(c => c.fileId)
                .OnDelete(DeleteBehavior.SetNull);

            // ProjectTask -> Project
            modelBuilder.Entity<ProjectTask>()
                .HasOne(pt => pt.project)
                .WithMany(p => p.tasks)
                .HasForeignKey(pt => pt.projectId)
                .OnDelete(DeleteBehavior.Cascade);

            // ProjectTask -> User (assignee)
            modelBuilder.Entity<ProjectTask>()
                .HasOne(pt => pt.user)
                .WithMany(u => u.tasks)
                .HasForeignKey(pt => pt.userId)
                .OnDelete(DeleteBehavior.SetNull);

            // Comment -> File (one-to-one)
            modelBuilder.Entity<Comment>() //✅
                .HasOne(c => c.file)
                .WithOne()
                .HasForeignKey<Comment>(c => c.fileId)
                .OnDelete(DeleteBehavior.SetNull);

            // Comment -> User
            modelBuilder.Entity<Comment>() //✅
                .HasOne(c => c.user)
                .WithMany(u => u.comments)
                .HasForeignKey(c => c.userId)
                .OnDelete(DeleteBehavior.Restrict);

            // Comment -> Task
            modelBuilder.Entity<Comment>() //✅
                .HasOne(c => c.task)
                .WithMany(t => t.comments)
                .HasForeignKey(c => c.taskId)
                .OnDelete(DeleteBehavior.Cascade);

            // UserProject -> User
            modelBuilder.Entity<UserProject>()
                .HasOne(up => up.user)
                .WithMany(u => u.userProjects)
                .HasForeignKey(up => up.userId)
                .OnDelete(DeleteBehavior.Cascade);

            // UserProject -> Project
            modelBuilder.Entity<UserProject>()
                .HasOne(up => up.project)
                .WithMany(p => p.userProjects)
                .HasForeignKey(up => up.projectId)
                .OnDelete(DeleteBehavior.Cascade);

            // Activity -> User 
            modelBuilder.Entity<Activity>()
                .HasOne(a => a.user)
                .WithMany(u => u.activities)
                .HasForeignKey(a => a.userId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed data
        }

       
    }
}
