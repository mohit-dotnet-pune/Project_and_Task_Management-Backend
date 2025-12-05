using System;
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
            // === KEEPING YOUR ORIGINAL RELATIONSHIPS EXACTLY ===

            modelBuilder.Entity<Project>()
                .HasOne(c => c.file)
                .WithOne()
                .HasForeignKey<Project>(c => c.fileId)
                .OnDelete(DeleteBehavior.SetNull);

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
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.file)
                .WithOne()
                .HasForeignKey<Comment>(c => c.fileId)
                .OnDelete(DeleteBehavior.SetNull);

            // Comment -> User
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.user)
                .WithMany(u => u.comments)
                .HasForeignKey(c => c.userId)
                .OnDelete(DeleteBehavior.Restrict);

            // Comment -> Task
            modelBuilder.Entity<Comment>()
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

            //
            // -------------------------
            // Seed data (HasData)
            // -------------------------
            //

            // =========================
            // DOCS (files) - 66 entries
            // 1..6  -> Project files
            // 7..36 -> Task files (30)
            // 37..66-> Comment files (30)
            // =========================

            modelBuilder.Entity<Doc>().HasData(
                // Project files 1..6
                new Doc { fileId = 1, fileName = "project-1-spec.pdf", fileURL = "https://example.com/files/project-1-spec.pdf" },
                new Doc { fileId = 2, fileName = "project-2-spec.pdf", fileURL = "https://example.com/files/project-2-spec.pdf" },
                new Doc { fileId = 3, fileName = "project-3-spec.pdf", fileURL = "https://example.com/files/project-3-spec.pdf" },
                new Doc { fileId = 4, fileName = "project-4-spec.pdf", fileURL = "https://example.com/files/project-4-spec.pdf" },
                new Doc { fileId = 5, fileName = "project-5-spec.pdf", fileURL = "https://example.com/files/project-5-spec.pdf" },
                new Doc { fileId = 6, fileName = "project-6-spec.pdf", fileURL = "https://example.com/files/project-6-spec.pdf" },

                // Task files 7..36 (30)
                new Doc { fileId = 7, fileName = "task-1-attachment.png", fileURL = "https://example.com/files/task-1-attachment.png" },
                new Doc { fileId = 8, fileName = "task-2-attachment.png", fileURL = "https://example.com/files/task-2-attachment.png" },
                new Doc { fileId = 9, fileName = "task-3-attachment.png", fileURL = "https://example.com/files/task-3-attachment.png" },
                new Doc { fileId = 10, fileName = "task-4-attachment.png", fileURL = "https://example.com/files/task-4-attachment.png" },
                new Doc { fileId = 11, fileName = "task-5-attachment.png", fileURL = "https://example.com/files/task-5-attachment.png" },
                new Doc { fileId = 12, fileName = "task-6-attachment.png", fileURL = "https://example.com/files/task-6-attachment.png" },
                new Doc { fileId = 13, fileName = "task-7-attachment.png", fileURL = "https://example.com/files/task-7-attachment.png" },
                new Doc { fileId = 14, fileName = "task-8-attachment.png", fileURL = "https://example.com/files/task-8-attachment.png" },
                new Doc { fileId = 15, fileName = "task-9-attachment.png", fileURL = "https://example.com/files/task-9-attachment.png" },
                new Doc { fileId = 16, fileName = "task-10-attachment.png", fileURL = "https://example.com/files/task-10-attachment.png" },
                new Doc { fileId = 17, fileName = "task-11-attachment.png", fileURL = "https://example.com/files/task-11-attachment.png" },
                new Doc { fileId = 18, fileName = "task-12-attachment.png", fileURL = "https://example.com/files/task-12-attachment.png" },
                new Doc { fileId = 19, fileName = "task-13-attachment.png", fileURL = "https://example.com/files/task-13-attachment.png" },
                new Doc { fileId = 20, fileName = "task-14-attachment.png", fileURL = "https://example.com/files/task-14-attachment.png" },
                new Doc { fileId = 21, fileName = "task-15-attachment.png", fileURL = "https://example.com/files/task-15-attachment.png" },
                new Doc { fileId = 22, fileName = "task-16-attachment.png", fileURL = "https://example.com/files/task-16-attachment.png" },
                new Doc { fileId = 23, fileName = "task-17-attachment.png", fileURL = "https://example.com/files/task-17-attachment.png" },
                new Doc { fileId = 24, fileName = "task-18-attachment.png", fileURL = "https://example.com/files/task-18-attachment.png" },
                new Doc { fileId = 25, fileName = "task-19-attachment.png", fileURL = "https://example.com/files/task-19-attachment.png" },
                new Doc { fileId = 26, fileName = "task-20-attachment.png", fileURL = "https://example.com/files/task-20-attachment.png" },
                new Doc { fileId = 27, fileName = "task-21-attachment.png", fileURL = "https://example.com/files/task-21-attachment.png" },
                new Doc { fileId = 28, fileName = "task-22-attachment.png", fileURL = "https://example.com/files/task-22-attachment.png" },
                new Doc { fileId = 29, fileName = "task-23-attachment.png", fileURL = "https://example.com/files/task-23-attachment.png" },
                new Doc { fileId = 30, fileName = "task-24-attachment.png", fileURL = "https://example.com/files/task-24-attachment.png" },
                new Doc { fileId = 31, fileName = "task-25-attachment.png", fileURL = "https://example.com/files/task-25-attachment.png" },
                new Doc { fileId = 32, fileName = "task-26-attachment.png", fileURL = "https://example.com/files/task-26-attachment.png" },
                new Doc { fileId = 33, fileName = "task-27-attachment.png", fileURL = "https://example.com/files/task-27-attachment.png" },
                new Doc { fileId = 34, fileName = "task-28-attachment.png", fileURL = "https://example.com/files/task-28-attachment.png" },
                new Doc { fileId = 35, fileName = "task-29-attachment.png", fileURL = "https://example.com/files/task-29-attachment.png" },
                new Doc { fileId = 36, fileName = "task-30-attachment.png", fileURL = "https://example.com/files/task-30-attachment.png" },

                // Comment files 37..66 (30)
                new Doc { fileId = 37, fileName = "comment-file-1.jpg", fileURL = "https://example.com/files/comment-file-1.jpg" },
                new Doc { fileId = 38, fileName = "comment-file-2.jpg", fileURL = "https://example.com/files/comment-file-2.jpg" },
                new Doc { fileId = 39, fileName = "comment-file-3.jpg", fileURL = "https://example.com/files/comment-file-3.jpg" },
                new Doc { fileId = 40, fileName = "comment-file-4.jpg", fileURL = "https://example.com/files/comment-file-4.jpg" },
                new Doc { fileId = 41, fileName = "comment-file-5.jpg", fileURL = "https://example.com/files/comment-file-5.jpg" },
                new Doc { fileId = 42, fileName = "comment-file-6.jpg", fileURL = "https://example.com/files/comment-file-6.jpg" },
                new Doc { fileId = 43, fileName = "comment-file-7.jpg", fileURL = "https://example.com/files/comment-file-7.jpg" },
                new Doc { fileId = 44, fileName = "comment-file-8.jpg", fileURL = "https://example.com/files/comment-file-8.jpg" },
                new Doc { fileId = 45, fileName = "comment-file-9.jpg", fileURL = "https://example.com/files/comment-file-9.jpg" },
                new Doc { fileId = 46, fileName = "comment-file-10.jpg", fileURL = "https://example.com/files/comment-file-10.jpg" },
                new Doc { fileId = 47, fileName = "comment-file-11.jpg", fileURL = "https://example.com/files/comment-file-11.jpg" },
                new Doc { fileId = 48, fileName = "comment-file-12.jpg", fileURL = "https://example.com/files/comment-file-12.jpg" },
                new Doc { fileId = 49, fileName = "comment-file-13.jpg", fileURL = "https://example.com/files/comment-file-13.jpg" },
                new Doc { fileId = 50, fileName = "comment-file-14.jpg", fileURL = "https://example.com/files/comment-file-14.jpg" },
                new Doc { fileId = 51, fileName = "comment-file-15.jpg", fileURL = "https://example.com/files/comment-file-15.jpg" },
                new Doc { fileId = 52, fileName = "comment-file-16.jpg", fileURL = "https://example.com/files/comment-file-16.jpg" },
                new Doc { fileId = 53, fileName = "comment-file-17.jpg", fileURL = "https://example.com/files/comment-file-17.jpg" },
                new Doc { fileId = 54, fileName = "comment-file-18.jpg", fileURL = "https://example.com/files/comment-file-18.jpg" },
                new Doc { fileId = 55, fileName = "comment-file-19.jpg", fileURL = "https://example.com/files/comment-file-19.jpg" },
                new Doc { fileId = 56, fileName = "comment-file-20.jpg", fileURL = "https://example.com/files/comment-file-20.jpg" },
                new Doc { fileId = 57, fileName = "comment-file-21.jpg", fileURL = "https://example.com/files/comment-file-21.jpg" },
                new Doc { fileId = 58, fileName = "comment-file-22.jpg", fileURL = "https://example.com/files/comment-file-22.jpg" },
                new Doc { fileId = 59, fileName = "comment-file-23.jpg", fileURL = "https://example.com/files/comment-file-23.jpg" },
                new Doc { fileId = 60, fileName = "comment-file-24.jpg", fileURL = "https://example.com/files/comment-file-24.jpg" },
                new Doc { fileId = 61, fileName = "comment-file-25.jpg", fileURL = "https://example.com/files/comment-file-25.jpg" },
                new Doc { fileId = 62, fileName = "comment-file-26.jpg", fileURL = "https://example.com/files/comment-file-26.jpg" },
                new Doc { fileId = 63, fileName = "comment-file-27.jpg", fileURL = "https://example.com/files/comment-file-27.jpg" },
                new Doc { fileId = 64, fileName = "comment-file-28.jpg", fileURL = "https://example.com/files/comment-file-28.jpg" },
                new Doc { fileId = 65, fileName = "comment-file-29.jpg", fileURL = "https://example.com/files/comment-file-29.jpg" },
                new Doc { fileId = 66, fileName = "comment-file-30.jpg", fileURL = "https://example.com/files/comment-file-30.jpg" }
            );

            // =========================
            // USERS (12) - 4 Managers + 8 Employees
            // =========================

            modelBuilder.Entity<User>().HasData(
                new User { userId = 1, userName = "manager1", userEmail = "manager1@example.com", userPassword = "hashed-password-1", userRole = Role.Manager, EmailConfirmed = true, CreatedAt = new DateTime(2024, 01, 10, 8, 0, 0, DateTimeKind.Utc) },
                new User { userId = 2, userName = "manager2", userEmail = "manager2@example.com", userPassword = "hashed-password-2", userRole = Role.Manager, EmailConfirmed = true, CreatedAt = new DateTime(2024, 02, 11, 9, 0, 0, DateTimeKind.Utc) },
                new User { userId = 3, userName = "manager3", userEmail = "manager3@example.com", userPassword = "hashed-password-3", userRole = Role.Manager, EmailConfirmed = true, CreatedAt = new DateTime(2024, 03, 12, 10, 0, 0, DateTimeKind.Utc) },
                new User { userId = 4, userName = "manager4", userEmail = "manager4@example.com", userPassword = "hashed-password-4", userRole = Role.Manager, EmailConfirmed = true, CreatedAt = new DateTime(2024, 04, 13, 11, 0, 0, DateTimeKind.Utc) },

                new User { userId = 5, userName = "employee1", userEmail = "emp1@example.com", userPassword = "hashed-emp-1", userRole = Role.Employee, EmailConfirmed = true, CreatedAt = new DateTime(2024, 05, 10, 8, 0, 0, DateTimeKind.Utc) },
                new User { userId = 6, userName = "employee2", userEmail = "emp2@example.com", userPassword = "hashed-emp-2", userRole = Role.Employee, EmailConfirmed = true, CreatedAt = new DateTime(2024, 05, 11, 8, 0, 0, DateTimeKind.Utc) },
                new User { userId = 7, userName = "employee3", userEmail = "emp3@example.com", userPassword = "hashed-emp-3", userRole = Role.Employee, EmailConfirmed = true, CreatedAt = new DateTime(2024, 05, 12, 8, 0, 0, DateTimeKind.Utc) },
                new User { userId = 8, userName = "employee4", userEmail = "emp4@example.com", userPassword = "hashed-emp-4", userRole = Role.Employee, EmailConfirmed = true, CreatedAt = new DateTime(2024, 05, 13, 8, 0, 0, DateTimeKind.Utc) },
                new User { userId = 9, userName = "employee5", userEmail = "emp5@example.com", userPassword = "hashed-emp-5", userRole = Role.Employee, EmailConfirmed = true, CreatedAt = new DateTime(2024, 05, 14, 8, 0, 0, DateTimeKind.Utc) },
                new User { userId = 10, userName = "employee6", userEmail = "emp6@example.com", userPassword = "hashed-emp-6", userRole = Role.Employee, EmailConfirmed = true, CreatedAt = new DateTime(2024, 05, 15, 8, 0, 0, DateTimeKind.Utc) },
                new User { userId = 11, userName = "employee7", userEmail = "emp7@example.com", userPassword = "hashed-emp-7", userRole = Role.Employee, EmailConfirmed = true, CreatedAt = new DateTime(2024, 05, 16, 8, 0, 0, DateTimeKind.Utc) },
                new User { userId = 12, userName = "employee8", userEmail = "emp8@example.com", userPassword = "hashed-emp-8", userRole = Role.Employee, EmailConfirmed = true, CreatedAt = new DateTime(2024, 05, 17, 8, 0, 0, DateTimeKind.Utc) }
            );

            // =========================
            // PROJECTS (6) - each points to fileId 1..6
            // =========================

            modelBuilder.Entity<Project>().HasData(
                new Project
                {
                    projectId = 1,
                    fileId = 1,
                    projectName = "Project Alpha",
                    projectDescription = "Alpha project description",
                    projectStartDate = new DateTime(2024, 07, 01, 9, 0, 0, DateTimeKind.Utc),
                    projectEndDate = new DateTime(2024, 10, 01, 17, 0, 0, DateTimeKind.Utc),
                    projectCreatedAt = new DateTime(2024, 07, 01, 9, 0, 0, DateTimeKind.Utc)
                },
                new Project
                {
                    projectId = 2,
                    fileId = 2,
                    projectName = "Project Beta",
                    projectDescription = "Beta project description",
                    projectStartDate = new DateTime(2024, 08, 01, 9, 0, 0, DateTimeKind.Utc),
                    projectEndDate = new DateTime(2024, 11, 01, 17, 0, 0, DateTimeKind.Utc),
                    projectCreatedAt = new DateTime(2024, 08, 01, 9, 0, 0, DateTimeKind.Utc)
                },
                new Project
                {
                    projectId = 3,
                    fileId = 3,
                    projectName = "Project Gamma",
                    projectDescription = "Gamma project description",
                    projectStartDate = new DateTime(2024, 09, 01, 9, 0, 0, DateTimeKind.Utc),
                    projectEndDate = new DateTime(2024, 12, 01, 17, 0, 0, DateTimeKind.Utc),
                    projectCreatedAt = new DateTime(2024, 09, 01, 9, 0, 0, DateTimeKind.Utc)
                },
                new Project
                {
                    projectId = 4,
                    fileId = 4,
                    projectName = "Project Delta",
                    projectDescription = "Delta project description",
                    projectStartDate = new DateTime(2024, 07, 15, 9, 0, 0, DateTimeKind.Utc),
                    projectEndDate = new DateTime(2024, 10, 15, 17, 0, 0, DateTimeKind.Utc),
                    projectCreatedAt = new DateTime(2024, 07, 15, 9, 0, 0, DateTimeKind.Utc)
                },
                new Project
                {
                    projectId = 5,
                    fileId = 5,
                    projectName = "Project Epsilon",
                    projectDescription = "Epsilon project description",
                    projectStartDate = new DateTime(2024, 08, 15, 9, 0, 0, DateTimeKind.Utc),
                    projectEndDate = new DateTime(2024, 11, 15, 17, 0, 0, DateTimeKind.Utc),
                    projectCreatedAt = new DateTime(2024, 08, 15, 9, 0, 0, DateTimeKind.Utc)
                },
                new Project
                {
                    projectId = 6,
                    fileId = 6,
                    projectName = "Project Zeta",
                    projectDescription = "Zeta project description",
                    projectStartDate = new DateTime(2024, 09, 15, 9, 0, 0, DateTimeKind.Utc),
                    projectEndDate = new DateTime(2024, 12, 15, 17, 0, 0, DateTimeKind.Utc),
                    projectCreatedAt = new DateTime(2024, 09, 15, 9, 0, 0, DateTimeKind.Utc)
                }
            );

            // =========================
            // TASKS (30) - 5 per project, taskIds 1..30
            // tasks point to fileId 7..36
            // =========================

            modelBuilder.Entity<ProjectTask>().HasData(
                // Project 1 (tasks 1..5)
                new ProjectTask { taskId = 1, projectId = 1, userId = 5, fileId = 7, taskTitle = "Alpha - Setup DB", taskDescription = "Set up initial database schema", taskPriority = Priority.High, taskStatus = Status.Inprogress, taskDueDate = new DateTime(2024, 07, 20, 17, 0, 0, DateTimeKind.Utc), taskCreatedAt = new DateTime(2024, 07, 01, 10, 0, 0, DateTimeKind.Utc) },
                new ProjectTask { taskId = 2, projectId = 1, userId = 6, fileId = 8, taskTitle = "Alpha - API skeleton", taskDescription = "Create API endpoints", taskPriority = Priority.Medium, taskStatus = Status.Todo, taskDueDate = new DateTime(2024, 07, 25, 17, 0, 0, DateTimeKind.Utc), taskCreatedAt = new DateTime(2024, 07, 02, 10, 0, 0, DateTimeKind.Utc) },
                new ProjectTask { taskId = 3, projectId = 1, userId = 7, fileId = 9, taskTitle = "Alpha - Frontend", taskDescription = "Create basic UI", taskPriority = Priority.Medium, taskStatus = Status.Todo, taskDueDate = new DateTime(2024, 07, 28, 17, 0, 0, DateTimeKind.Utc), taskCreatedAt = new DateTime(2024, 07, 03, 10, 0, 0, DateTimeKind.Utc) },
                new ProjectTask { taskId = 4, projectId = 1, userId = 8, fileId = 10, taskTitle = "Alpha - Testing", taskDescription = "Write unit tests", taskPriority = Priority.Low, taskStatus = Status.Todo, taskDueDate = new DateTime(2024, 07, 30, 17, 0, 0, DateTimeKind.Utc), taskCreatedAt = new DateTime(2024, 07, 04, 10, 0, 0, DateTimeKind.Utc) },
                new ProjectTask { taskId = 5, projectId = 1, userId = 9, fileId = 11, taskTitle = "Alpha - Deploy", taskDescription = "Deploy to staging", taskPriority = Priority.Medium, taskStatus = Status.Todo, taskDueDate = new DateTime(2024, 08, 02, 17, 0, 0, DateTimeKind.Utc), taskCreatedAt = new DateTime(2024, 07, 05, 10, 0, 0, DateTimeKind.Utc) },

                // Project 2 (tasks 6..10)
                new ProjectTask { taskId = 6, projectId = 2, userId = 5, fileId = 12, taskTitle = "Beta - UI design", taskDescription = "Design the main screens", taskPriority = Priority.Medium, taskStatus = Status.Inprogress, taskDueDate = new DateTime(2024, 08, 20, 17, 0, 0, DateTimeKind.Utc), taskCreatedAt = new DateTime(2024, 08, 01, 10, 0, 0, DateTimeKind.Utc) },
                new ProjectTask { taskId = 7, projectId = 2, userId = 6, fileId = 13, taskTitle = "Beta - Authentication", taskDescription = "Implement auth flows", taskPriority = Priority.High, taskStatus = Status.Todo, taskDueDate = new DateTime(2024, 08, 28, 17, 0, 0, DateTimeKind.Utc), taskCreatedAt = new DateTime(2024, 08, 02, 10, 0, 0, DateTimeKind.Utc) },
                new ProjectTask { taskId = 8, projectId = 2, userId = 7, fileId = 14, taskTitle = "Beta - Integrations", taskDescription = "Integrate third-party APIs", taskPriority = Priority.Medium, taskStatus = Status.Todo, taskDueDate = new DateTime(2024, 08, 30, 17, 0, 0, DateTimeKind.Utc), taskCreatedAt = new DateTime(2024, 08, 03, 10, 0, 0, DateTimeKind.Utc) },
                new ProjectTask { taskId = 9, projectId = 2, userId = 8, fileId = 15, taskTitle = "Beta - Accessibility", taskDescription = "Improve accessibility", taskPriority = Priority.Low, taskStatus = Status.Todo, taskDueDate = new DateTime(2024, 09, 05, 17, 0, 0, DateTimeKind.Utc), taskCreatedAt = new DateTime(2024, 08, 04, 10, 0, 0, DateTimeKind.Utc) },
                new ProjectTask { taskId = 10, projectId = 2, userId = 9, fileId = 16, taskTitle = "Beta - Performance", taskDescription = "Performance optimizations", taskPriority = Priority.High, taskStatus = Status.Todo, taskDueDate = new DateTime(2024, 09, 10, 17, 0, 0, DateTimeKind.Utc), taskCreatedAt = new DateTime(2024, 08, 05, 10, 0, 0, DateTimeKind.Utc) },

                // Project 3 (tasks 11..15)
                new ProjectTask { taskId = 11, projectId = 3, userId = 10, fileId = 17, taskTitle = "Gamma - Data import", taskDescription = "Import legacy data", taskPriority = Priority.High, taskStatus = Status.Todo, taskDueDate = new DateTime(2024, 09, 25, 17, 0, 0, DateTimeKind.Utc), taskCreatedAt = new DateTime(2024, 09, 01, 10, 0, 0, DateTimeKind.Utc) },
                new ProjectTask { taskId = 12, projectId = 3, userId = 11, fileId = 18, taskTitle = "Gamma - Reporting", taskDescription = "Create reporting module", taskPriority = Priority.Medium, taskStatus = Status.Inprogress, taskDueDate = new DateTime(2024, 10, 05, 17, 0, 0, DateTimeKind.Utc), taskCreatedAt = new DateTime(2024, 09, 02, 10, 0, 0, DateTimeKind.Utc) },
                new ProjectTask { taskId = 13, projectId = 3, userId = 12, fileId = 19, taskTitle = "Gamma - UI polish", taskDescription = "Polish the UI", taskPriority = Priority.Low, taskStatus = Status.Todo, taskDueDate = new DateTime(2024, 10, 10, 17, 0, 0, DateTimeKind.Utc), taskCreatedAt = new DateTime(2024, 09, 03, 10, 0, 0, DateTimeKind.Utc) },
                new ProjectTask { taskId = 14, projectId = 3, userId = 5, fileId = 20, taskTitle = "Gamma - QA", taskDescription = "Quality Assurance", taskPriority = Priority.Medium, taskStatus = Status.Todo, taskDueDate = new DateTime(2024, 10, 15, 17, 0, 0, DateTimeKind.Utc), taskCreatedAt = new DateTime(2024, 09, 04, 10, 0, 0, DateTimeKind.Utc) },
                new ProjectTask { taskId = 15, projectId = 3, userId = 6, fileId = 21, taskTitle = "Gamma - Docs", taskDescription = "Create documentation", taskPriority = Priority.Low, taskStatus = Status.Todo, taskDueDate = new DateTime(2024, 10, 20, 17, 0, 0, DateTimeKind.Utc), taskCreatedAt = new DateTime(2024, 09, 05, 10, 0, 0, DateTimeKind.Utc) },

                // Project 4 (tasks 16..20)
                new ProjectTask { taskId = 16, projectId = 4, userId = 7, fileId = 22, taskTitle = "Delta - Setup CI", taskDescription = "CI/CD pipeline", taskPriority = Priority.Medium, taskStatus = Status.Todo, taskDueDate = new DateTime(2024, 08, 30, 17, 0, 0, DateTimeKind.Utc), taskCreatedAt = new DateTime(2024, 07, 16, 10, 0, 0, DateTimeKind.Utc) },
                new ProjectTask { taskId = 17, projectId = 4, userId = 8, fileId = 23, taskTitle = "Delta - Integration tests", taskDescription = "Write integration tests", taskPriority = Priority.High, taskStatus = Status.Todo, taskDueDate = new DateTime(2024, 09, 15, 17, 0, 0, DateTimeKind.Utc), taskCreatedAt = new DateTime(2024, 07, 17, 10, 0, 0, DateTimeKind.Utc) },
                new ProjectTask { taskId = 18, projectId = 4, userId = 9, fileId = 24, taskTitle = "Delta - Security", taskDescription = "Security review", taskPriority = Priority.High, taskStatus = Status.Todo, taskDueDate = new DateTime(2024, 09, 25, 17, 0, 0, DateTimeKind.Utc), taskCreatedAt = new DateTime(2024, 07, 18, 10, 0, 0, DateTimeKind.Utc) },
                new ProjectTask { taskId = 19, projectId = 4, userId = 10, fileId = 25, taskTitle = "Delta - Release prep", taskDescription = "Prepare release notes", taskPriority = Priority.Low, taskStatus = Status.Todo, taskDueDate = new DateTime(2024, 09, 30, 17, 0, 0, DateTimeKind.Utc), taskCreatedAt = new DateTime(2024, 07, 19, 10, 0, 0, DateTimeKind.Utc) },
                new ProjectTask { taskId = 20, projectId = 4, userId = 11, fileId = 26, taskTitle = "Delta - Rollout", taskDescription = "Rollout to prod", taskPriority = Priority.High, taskStatus = Status.Todo, taskDueDate = new DateTime(2024, 10, 05, 17, 0, 0, DateTimeKind.Utc), taskCreatedAt = new DateTime(2024, 07, 20, 10, 0, 0, DateTimeKind.Utc) },

                // Project 5 (tasks 21..25)
                new ProjectTask { taskId = 21, projectId = 5, userId = 12, fileId = 27, taskTitle = "Epsilon - Performance", taskDescription = "Profile and optimize", taskPriority = Priority.High, taskStatus = Status.Todo, taskDueDate = new DateTime(2024, 09, 30, 17, 0, 0, DateTimeKind.Utc), taskCreatedAt = new DateTime(2024, 08, 16, 10, 0, 0, DateTimeKind.Utc) },
                new ProjectTask { taskId = 22, projectId = 5, userId = 5, fileId = 28, taskTitle = "Epsilon - Security review", taskDescription = "Security audit", taskPriority = Priority.Medium, taskStatus = Status.Todo, taskDueDate = new DateTime(2024, 10, 05, 17, 0, 0, DateTimeKind.Utc), taskCreatedAt = new DateTime(2024, 08, 17, 10, 0, 0, DateTimeKind.Utc) },
                new ProjectTask { taskId = 23, projectId = 5, userId = 6, fileId = 29, taskTitle = "Epsilon - Logging", taskDescription = "Add structured logging", taskPriority = Priority.Low, taskStatus = Status.Todo, taskDueDate = new DateTime(2024, 10, 12, 17, 0, 0, DateTimeKind.Utc), taskCreatedAt = new DateTime(2024, 08, 18, 10, 0, 0, DateTimeKind.Utc) },
                new ProjectTask { taskId = 24, projectId = 5, userId = 7, fileId = 30, taskTitle = "Epsilon - Backup", taskDescription = "Set up backups", taskPriority = Priority.Medium, taskStatus = Status.Todo, taskDueDate = new DateTime(2024, 10, 20, 17, 0, 0, DateTimeKind.Utc), taskCreatedAt = new DateTime(2024, 08, 19, 10, 0, 0, DateTimeKind.Utc) },
                new ProjectTask { taskId = 25, projectId = 5, userId = 8, fileId = 31, taskTitle = "Epsilon - Documentation", taskDescription = "Operational docs", taskPriority = Priority.Low, taskStatus = Status.Todo, taskDueDate = new DateTime(2024, 10, 25, 17, 0, 0, DateTimeKind.Utc), taskCreatedAt = new DateTime(2024, 08, 20, 10, 0, 0, DateTimeKind.Utc) },

                // Project 6 (tasks 26..30)
                new ProjectTask { taskId = 26, projectId = 6, userId = 9, fileId = 32, taskTitle = "Zeta - Scalability", taskDescription = "Scale services", taskPriority = Priority.High, taskStatus = Status.Todo, taskDueDate = new DateTime(2024, 10, 20, 17, 0, 0, DateTimeKind.Utc), taskCreatedAt = new DateTime(2024, 09, 16, 10, 0, 0, DateTimeKind.Utc) },
                new ProjectTask { taskId = 27, projectId = 6, userId = 10, fileId = 33, taskTitle = "Zeta - Load testing", taskDescription = "Perform load tests", taskPriority = Priority.Medium, taskStatus = Status.Todo, taskDueDate = new DateTime(2024, 10, 25, 17, 0, 0, DateTimeKind.Utc), taskCreatedAt = new DateTime(2024, 09, 17, 10, 0, 0, DateTimeKind.Utc) },
                new ProjectTask { taskId = 28, projectId = 6, userId = 11, fileId = 34, taskTitle = "Zeta - Auto-scaling", taskDescription = "Add autoscaling", taskPriority = Priority.High, taskStatus = Status.Todo, taskDueDate = new DateTime(2024, 11, 01, 17, 0, 0, DateTimeKind.Utc), taskCreatedAt = new DateTime(2024, 09, 18, 10, 0, 0, DateTimeKind.Utc) },
                new ProjectTask { taskId = 29, projectId = 6, userId = 12, fileId = 35, taskTitle = "Zeta - Monitoring", taskDescription = "Add monitoring & alerts", taskPriority = Priority.Medium, taskStatus = Status.Todo, taskDueDate = new DateTime(2024, 11, 05, 17, 0, 0, DateTimeKind.Utc), taskCreatedAt = new DateTime(2024, 09, 19, 10, 0, 0, DateTimeKind.Utc) },
                new ProjectTask { taskId = 30, projectId = 6, userId = 5, fileId = 36, taskTitle = "Zeta - Cleanup", taskDescription = "Technical debt cleanup", taskPriority = Priority.Low, taskStatus = Status.Todo, taskDueDate = new DateTime(2024, 11, 10, 17, 0, 0, DateTimeKind.Utc), taskCreatedAt = new DateTime(2024, 09, 20, 10, 0, 0, DateTimeKind.Utc) }
            );

            // =========================
            // COMMENTS (30) - 1 per task (commentId 1..30)
            // comments point to fileId 37..66
            // =========================

            modelBuilder.Entity<Comment>().HasData(
                new Comment { commentId = 1, taskId = 1, fileId = 37, userId = 5, commentMessage = "Initial DB comment", commentCreatedAt = DateTime.SpecifyKind(new DateTime(2024, 07, 02, 10, 0, 0), DateTimeKind.Utc) },
                new Comment { commentId = 2, taskId = 2, fileId = 38, userId = 6, commentMessage = "API skeleton remarks", commentCreatedAt = DateTime.SpecifyKind(new DateTime(2024, 07, 03, 10, 0, 0), DateTimeKind.Utc) },
                new Comment { commentId = 3, taskId = 3, fileId = 39, userId = 7, commentMessage = "Frontend notes", commentCreatedAt = DateTime.SpecifyKind(new DateTime(2024, 07, 04, 10, 0, 0), DateTimeKind.Utc) },
                new Comment { commentId = 4, taskId = 4, fileId = 40, userId = 8, commentMessage = "Testing plan", commentCreatedAt = DateTime.SpecifyKind(new DateTime(2024, 07, 05, 10, 0, 0), DateTimeKind.Utc) },
                new Comment { commentId = 5, taskId = 5, fileId = 41, userId = 9, commentMessage = "Deploy checklist", commentCreatedAt = DateTime.SpecifyKind(new DateTime(2024, 07, 06, 10, 0, 0), DateTimeKind.Utc) },

                new Comment { commentId = 6, taskId = 6, fileId = 42, userId = 5, commentMessage = "UI design feedback", commentCreatedAt = DateTime.SpecifyKind(new DateTime(2024, 08, 02, 10, 0, 0), DateTimeKind.Utc) },
                new Comment { commentId = 7, taskId = 7, fileId = 43, userId = 6, commentMessage = "Auth flow details", commentCreatedAt = DateTime.SpecifyKind(new DateTime(2024, 08, 03, 10, 0, 0), DateTimeKind.Utc) },
                new Comment { commentId = 8, taskId = 8, fileId = 44, userId = 7, commentMessage = "Integration notes", commentCreatedAt = DateTime.SpecifyKind(new DateTime(2024, 08, 04, 10, 0, 0), DateTimeKind.Utc) },
                new Comment { commentId = 9, taskId = 9, fileId = 45, userId = 8, commentMessage = "Accessibility fixes", commentCreatedAt = DateTime.SpecifyKind(new DateTime(2024, 08, 05, 10, 0, 0), DateTimeKind.Utc) },
                new Comment { commentId = 10, taskId = 10, fileId = 46, userId = 9, commentMessage = "Performance tips", commentCreatedAt = DateTime.SpecifyKind(new DateTime(2024, 08, 06, 10, 0, 0), DateTimeKind.Utc) },

                new Comment { commentId = 11, taskId = 11, fileId = 47, userId = 10, commentMessage = "Data import plan", commentCreatedAt = DateTime.SpecifyKind(new DateTime(2024, 09, 02, 10, 0, 0), DateTimeKind.Utc) },
                new Comment { commentId = 12, taskId = 12, fileId = 48, userId = 11, commentMessage = "Reporting spec", commentCreatedAt = DateTime.SpecifyKind(new DateTime(2024, 09, 03, 10, 0, 0), DateTimeKind.Utc) },
                new Comment { commentId = 13, taskId = 13, fileId = 49, userId = 12, commentMessage = "UI polish suggestions", commentCreatedAt = DateTime.SpecifyKind(new DateTime(2024, 09, 04, 10, 0, 0), DateTimeKind.Utc) },
                new Comment { commentId = 14, taskId = 14, fileId = 50, userId = 5, commentMessage = "QA checklist", commentCreatedAt = DateTime.SpecifyKind(new DateTime(2024, 09, 05, 10, 0, 0), DateTimeKind.Utc) },
                new Comment { commentId = 15, taskId = 15, fileId = 51, userId = 6, commentMessage = "Docs outline", commentCreatedAt = DateTime.SpecifyKind(new DateTime(2024, 09, 06, 10, 0, 0), DateTimeKind.Utc) },

                new Comment { commentId = 16, taskId = 16, fileId = 52, userId = 7, commentMessage = "CI pipeline notes", commentCreatedAt = DateTime.SpecifyKind(new DateTime(2024, 07, 18, 10, 0, 0), DateTimeKind.Utc) },
                new Comment { commentId = 17, taskId = 17, fileId = 53, userId = 8, commentMessage = "Integration test coverage", commentCreatedAt = DateTime.SpecifyKind(new DateTime(2024, 07, 19, 10, 0, 0), DateTimeKind.Utc) },
                new Comment { commentId = 18, taskId = 18, fileId = 54, userId = 9, commentMessage = "Security review comments", commentCreatedAt = DateTime.SpecifyKind(new DateTime(2024, 07, 20, 10, 0, 0), DateTimeKind.Utc) },
                new Comment { commentId = 19, taskId = 19, fileId = 55, userId = 10, commentMessage = "Release prep notes", commentCreatedAt = DateTime.SpecifyKind(new DateTime(2024, 07, 21, 10, 0, 0), DateTimeKind.Utc) },
                new Comment { commentId = 20, taskId = 20, fileId = 56, userId = 11, commentMessage = "Rollout instructions", commentCreatedAt = DateTime.SpecifyKind(new DateTime(2024, 07, 22, 10, 0, 0), DateTimeKind.Utc) },

                new Comment { commentId = 21, taskId = 21, fileId = 57, userId = 12, commentMessage = "Performance profiling", commentCreatedAt = DateTime.SpecifyKind(new DateTime(2024, 08, 17, 10, 0, 0), DateTimeKind.Utc) },
                new Comment { commentId = 22, taskId = 22, fileId = 58, userId = 5, commentMessage = "Security audit notes", commentCreatedAt = DateTime.SpecifyKind(new DateTime(2024, 08, 18, 10, 0, 0), DateTimeKind.Utc) },
                new Comment { commentId = 23, taskId = 23, fileId = 59, userId = 6, commentMessage = "Logging config", commentCreatedAt = DateTime.SpecifyKind(new DateTime(2024, 08, 19, 10, 0, 0), DateTimeKind.Utc) },
                new Comment { commentId = 24, taskId = 24, fileId = 60, userId = 7, commentMessage = "Backup schedule", commentCreatedAt = DateTime.SpecifyKind(new DateTime(2024, 08, 20, 10, 0, 0), DateTimeKind.Utc) },
                new Comment { commentId = 25, taskId = 25, fileId = 61, userId = 8, commentMessage = "Operational docs", commentCreatedAt = DateTime.SpecifyKind(new DateTime(2024, 08, 21, 10, 0, 0), DateTimeKind.Utc) },

                new Comment { commentId = 26, taskId = 26, fileId = 62, userId = 9, commentMessage = "Scalability plan", commentCreatedAt = DateTime.SpecifyKind(new DateTime(2024, 09, 18, 10, 0, 0), DateTimeKind.Utc) },
                new Comment { commentId = 27, taskId = 27, fileId = 63, userId = 10, commentMessage = "Load test results", commentCreatedAt = DateTime.SpecifyKind(new DateTime(2024, 09, 19, 10, 0, 0), DateTimeKind.Utc) },
                new Comment { commentId = 28, taskId = 28, fileId = 64, userId = 11, commentMessage = "Autoscaling config", commentCreatedAt = DateTime.SpecifyKind(new DateTime(2024, 09, 20, 10, 0, 0), DateTimeKind.Utc) },
                new Comment { commentId = 29, taskId = 29, fileId = 65, userId = 12, commentMessage = "Monitoring alerts", commentCreatedAt = DateTime.SpecifyKind(new DateTime(2024, 09, 21, 10, 0, 0), DateTimeKind.Utc) },
                new Comment { commentId = 30, taskId = 30, fileId = 66, userId = 5, commentMessage = "Cleanup notes", commentCreatedAt = DateTime.SpecifyKind(new DateTime(2024, 09, 22, 10, 0, 0), DateTimeKind.Utc) }
            );

            // =========================
            // ACTIVITIES (30) - 1 per task
            // Minimal properties: activityId, userId, activityMessage, activityCreatedAt
            // (Adjust property names if your Activity model differs)
            // =========================

          

            // =========================
            // USERPROJECT links (22 entries) - ensure each user assigned
            // =========================

            modelBuilder.Entity<UserProject>().HasData(
                // Managers assignments
                new UserProject { userProjectId = 1, userId = 1, projectId = 1 },
                new UserProject { userProjectId = 2, userId = 1, projectId = 2 },

                new UserProject { userProjectId = 3, userId = 2, projectId = 2 },
                new UserProject { userProjectId = 4, userId = 2, projectId = 3 },

                new UserProject { userProjectId = 5, userId = 3, projectId = 3 },
                new UserProject { userProjectId = 6, userId = 3, projectId = 4 },

                new UserProject { userProjectId = 7, userId = 4, projectId = 4 },
                new UserProject { userProjectId = 8, userId = 4, projectId = 5 },

                // Managers additional
                new UserProject { userProjectId = 9, userId = 1, projectId = 6 },

                // Employees assignments (some overlap)
                new UserProject { userProjectId = 10, userId = 5, projectId = 1 },
                new UserProject { userProjectId = 11, userId = 6, projectId = 1 },
                new UserProject { userProjectId = 12, userId = 7, projectId = 2 },
                new UserProject { userProjectId = 13, userId = 8, projectId = 2 },
                new UserProject { userProjectId = 14, userId = 9, projectId = 3 },
                new UserProject { userProjectId = 15, userId = 10, projectId = 3 },
                new UserProject { userProjectId = 16, userId = 11, projectId = 4 },
                new UserProject { userProjectId = 17, userId = 12, projectId = 4 },
                new UserProject { userProjectId = 18, userId = 5, projectId = 5 },
                new UserProject { userProjectId = 19, userId = 6, projectId = 5 },
                new UserProject { userProjectId = 20, userId = 7, projectId = 6 },
                new UserProject { userProjectId = 21, userId = 8, projectId = 6 },
                new UserProject { userProjectId = 22, userId = 9, projectId = 5 }
            );

            // End of seed data
        }
    }
}
