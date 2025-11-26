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
            // 1. ProjectTask -> User
            modelBuilder.Entity<ProjectTask>()
                .HasOne(t => t.user)
                .WithMany(u => u.tasks)
                .HasForeignKey(t => t.userId)
                .OnDelete(DeleteBehavior.NoAction);

            // 2. ProjectTask -> Project
            modelBuilder.Entity<ProjectTask>()
                .HasOne(t => t.project)
                .WithMany(p => p.tasks)
                .HasForeignKey(t => t.projectId)
                .OnDelete(DeleteBehavior.NoAction);

            // 3. Comment -> User
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.user)
                .WithMany(u => u.comments)
                .HasForeignKey(c => c.userId)
                .OnDelete(DeleteBehavior.NoAction);

            // 4. Comment -> Task
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.task)
                .WithMany(t => t.comments)
                .HasForeignKey(c => c.taskId)
                .OnDelete(DeleteBehavior.NoAction);

            // 5. UserProject -> User
            modelBuilder.Entity<UserProject>()
                .HasOne(up => up.user)
                .WithMany(u => u.userProjects)
                .HasForeignKey(up => up.userId)
                .OnDelete(DeleteBehavior.NoAction);

            // 6. UserProject -> Project
            modelBuilder.Entity<UserProject>()
                .HasOne(up => up.project)
                .WithMany(p => p.userProjects)
                .HasForeignKey(up => up.projectId)
                .OnDelete(DeleteBehavior.NoAction);

            // 7. Project -> Doc
            modelBuilder.Entity<Project>()
                .HasOne(p => p.file)
                .WithMany()
                .HasForeignKey(p => p.fileId)
                .OnDelete(DeleteBehavior.NoAction);

            // 8. ProjectTask -> Doc
            modelBuilder.Entity<ProjectTask>()
                .HasOne(t => t.file)
                .WithMany()
                .HasForeignKey(t => t.fileId)
                .OnDelete(DeleteBehavior.NoAction);

            // 9. Comment -> Doc
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.file)
                .WithMany()
                .HasForeignKey(c => c.fileId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
    }
