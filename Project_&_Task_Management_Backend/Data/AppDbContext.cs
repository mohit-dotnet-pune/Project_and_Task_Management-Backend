//using Microsoft.EntityFrameworkCore;
//using Project___Task_Management_Backend.Models;

//namespace Project___Task_Management_Backend.Data
//{
//    public class AppDbContext : DbContext
//    {
//        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
//        {

//        }

//        public DbSet<User> users { get; set; }
//        public DbSet<Project> projects { get; set; }
//        public DbSet<ProjectTask> tasks { get; set; }
//        public DbSet<Comment> comments { get; set; }
//        public DbSet<Doc> docs { get; set; }
//        public DbSet<Activity> activities { get; set; }
//        public DbSet<UserProject> userProjects { get; set; }


//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            // ProjectTask -> Project
//            modelBuilder.Entity<ProjectTask>()
//                .HasOne(pt => pt.project)
//                .WithMany(p => p.tasks)
//                .HasForeignKey(pt => pt.projectId)
//                .OnDelete(DeleteBehavior.Cascade);

//            // ProjectTask -> User (assignee)
//            modelBuilder.Entity<ProjectTask>()
//                .HasOne(pt => pt.user)
//                .WithMany(u => u.tasks)
//                .HasForeignKey(pt => pt.userId)
//                .OnDelete(DeleteBehavior.SetNull);

//            // Comment -> File (one-to-one)
//            modelBuilder.Entity<Comment>()
//                .HasOne(c => c.file)
//                .WithOne()
//                .HasForeignKey<Comment>(c => c.fileId)
//                .OnDelete(DeleteBehavior.SetNull);

//            // Comment -> User
//            modelBuilder.Entity<Comment>()
//                .HasOne(c => c.user)
//                .WithMany(u => u.comments)
//                .HasForeignKey(c => c.userId)
//                .OnDelete(DeleteBehavior.Restrict);

//            // Comment -> Task - THIS IS THE KEY FIX
//            modelBuilder.Entity<Comment>()
//                .HasOne(c => c.task)
//                .WithMany(t => t.comments)
//                .HasForeignKey(c => c.taskId)
//                .OnDelete(DeleteBehavior.NoAction);

//            // UserProject -> User
//            modelBuilder.Entity<UserProject>()
//                .HasOne(up => up.user)
//                .WithMany(u => u.userProjects)
//                .HasForeignKey(up => up.userId)
//                .OnDelete(DeleteBehavior.Cascade);

//            // UserProject -> Project
//            modelBuilder.Entity<UserProject>()
//                .HasOne(up => up.project)
//                .WithMany(p => p.userProjects)
//                .HasForeignKey(up => up.projectId)
//                .OnDelete(DeleteBehavior.Cascade);

//            // Activity -> User 
//            modelBuilder.Entity<Activity>()
//                .HasOne(a => a.user)
//                .WithMany(u => u.activities)
//                .HasForeignKey(a => a.userId)
//                .OnDelete(DeleteBehavior.Restrict);

//            // Doc -> Task
//            modelBuilder.Entity<Doc>()
//                .HasOne(d => d.task)
//                .WithMany(t => t.files)
//                .HasForeignKey(d => d.taskId)
//                .OnDelete(DeleteBehavior.Cascade);
//        }
//    }
//    }



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
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Use static dates - choose a base date and calculate all others from it
            var baseDate = new DateTime(2024, 1, 15, 10, 0, 0, DateTimeKind.Utc);

            // Seed Users
            var users = new[]
            {
                new User
                {
                    userId = 1,
                    userName = "John Doe",
                    userEmail = "john.doe@company.com",
                    userPassword = "hashed_password_1",
                    userRole = Role.Manager
                },
                new User
                {
                    userId = 2,
                    userName = "Jane Smith",
                    userEmail = "jane.smith@company.com",
                    userPassword = "hashed_password_2",
                    userRole = Role.Employee
                },
                new User
                {
                    userId = 3,
                    userName = "Mike Johnson",
                    userEmail = "mike.johnson@company.com",
                    userPassword = "hashed_password_3",
                    userRole = Role.Employee
                },
                new User
                {
                    userId = 4,
                    userName = "Sarah Wilson",
                    userEmail = "sarah.wilson@company.com",
                    userPassword = "hashed_password_4",
                    userRole = Role.Employee
                }
            };

            // Seed Projects
            var projects = new[]
            {
                new Project
                {
                    projectId = 1,
                    fileId = 4,
                    projectName = "E-Commerce Website",
                    projectDescription = "Build a modern e-commerce platform with React and .NET",
                    projectStartDate = baseDate.AddDays(-30),
                    projectEndDate = baseDate.AddDays(60),
                    projectCreatedAt = baseDate.AddDays(-30)
                },
                new Project
                {
                    projectId = 2,
                    projectName = "Mobile App Development",
                    projectDescription = "Create a cross-platform mobile application",
                    projectStartDate = baseDate.AddDays(-15),
                    projectEndDate = baseDate.AddDays(45),
                    projectCreatedAt = baseDate.AddDays(-15)
                },
                new Project
                {
                    projectId = 3,
                    projectName = "Database Migration",
                    projectDescription = "Migrate from legacy database to cloud solution",
                    projectStartDate = baseDate.AddDays(-7),
                    projectEndDate = baseDate.AddDays(30),
                    projectCreatedAt = baseDate.AddDays(-7)
                }
            };

            // Seed UserProjects (assign users to projects)
            var userProjects = new[]
            {
                new UserProject { userProjectId = 1, userId = 1, projectId = 1 },
                new UserProject { userProjectId = 2, userId = 2, projectId = 1 },
                new UserProject { userProjectId = 3, userId = 3, projectId = 1 },
                new UserProject { userProjectId = 4, userId = 1, projectId = 2 },
                new UserProject { userProjectId = 5, userId = 2, projectId = 2 },
                new UserProject { userProjectId = 6, userId = 4, projectId = 2 },
                new UserProject { userProjectId = 7, userId = 1, projectId = 3 },
                new UserProject { userProjectId = 8, userId = 3, projectId = 3 },
                new UserProject { userProjectId = 9, userId = 4, projectId = 3 }
            };

            // Seed ProjectTasks
            var tasks = new[]
            {
                // E-Commerce Project Tasks
                new ProjectTask
                {
                    taskId = 1,
                    projectId = 1,
                    userId = 2,
                    fileId = 3,
                    taskTitle = "Design Homepage",
                    taskDescription = "Create responsive homepage design with product listings",
                    taskPriority = Priority.High,
                    taskStatus = Status.Done,
                    taskDueDate = baseDate.AddDays(-10),
                    taskCreatedAt = baseDate.AddDays(-25)
                },
                new ProjectTask
                {
                    taskId = 2,
                    projectId = 1,
                    userId = 3,
                    taskTitle = "Implement Shopping Cart",
                    taskDescription = "Develop shopping cart functionality with add/remove items",
                    taskPriority = Priority.High,
                    taskStatus = Status.Inprogress,
                    taskDueDate = baseDate.AddDays(15),
                    taskCreatedAt = baseDate.AddDays(-20)
                },
                new ProjectTask
                {
                    taskId = 3,
                    projectId = 1,
                    userId = 2,
                    taskTitle = "User Authentication",
                    taskDescription = "Implement login/register system with JWT tokens",
                    taskPriority = Priority.Medium,
                    taskStatus = Status.Todo,
                    taskDueDate = baseDate.AddDays(25),
                    taskCreatedAt = baseDate.AddDays(-15)
                },
                
                // Mobile App Project Tasks
                new ProjectTask
                {
                    taskId = 4,
                    projectId = 2,
                    userId = 4,
                    taskTitle = "UI/UX Design",
                    taskDescription = "Design mobile app interface and user experience",
                    taskPriority = Priority.Medium,
                    taskStatus = Status.Inprogress,
                    taskDueDate = baseDate.AddDays(10),
                    taskCreatedAt = baseDate.AddDays(-12)
                },
                new ProjectTask
                {
                    taskId = 5,
                    projectId = 2,
                    userId = 2,
                    taskTitle = "Backend API Integration",
                    taskDescription = "Connect mobile app to backend REST API",
                    taskPriority = Priority.High,
                    taskStatus = Status.Todo,
                    taskDueDate = baseDate.AddDays(20),
                    taskCreatedAt = baseDate.AddDays(-8)
                },
                
                // Database Migration Project Tasks
                new ProjectTask
                {
                    taskId = 6,
                    projectId = 3,
                    userId = 3,
                    taskTitle = "Data Analysis",
                    taskDescription = "Analyze existing database structure and data",
                    taskPriority = Priority.Medium,
                    taskStatus = Status.Done,
                    taskDueDate = baseDate.AddDays(-2),
                    taskCreatedAt = baseDate.AddDays(-7)
                },
                new ProjectTask
                {
                    taskId = 7,
                    projectId = 3,
                    userId = 4,
                    
                    taskTitle = "Migration Script Development",
                    taskDescription = "Create scripts to migrate data to new cloud database",
                    taskPriority = Priority.High,
                    taskStatus = Status.Inprogress,
                    taskDueDate = baseDate.AddDays(15),
                    taskCreatedAt = baseDate.AddDays(-5)
                }
            };

            // Seed Docs
            var docs = new[]
            {
                new Doc
                {
                    fileId = 1,
                    fileName = "homepage-design.sketch",
                    fileURL = "/files/designs/homepage-design.sketch",
                
                },
                new Doc
                {
                    fileId = 2,
                    fileName = "shopping-cart-specs.pdf",
                    fileURL = "/files/specs/shopping-cart-specs.pdf",
                   
                },
                new Doc
                {
                    fileId = 3,
                    fileName = "ui-design-mockups.fig",
                    fileURL = "/files/designs/ui-design-mockups.fig",

                },
                new Doc
                {
                    fileId = 4,
                    fileName = "database-schema.sql",
                    fileURL = "/files/sql/database-schema.sql",
                  
                }
            };

            // Seed Comments
            var comments = new[]
            {
                new Comment
                {
                    commentId = 1,
                    taskId = 1,
                    userId = 1,
                    fileId = 1,
                    commentMessage = "Great work on the homepage design! The layout looks clean and modern.",
                    commentCreatedAt = baseDate.AddDays(-8)
                },
                new Comment
                {
                    commentId = 2,
                    taskId = 1,
                    userId = 2,
                    commentMessage = "Thanks! I'll start working on the mobile responsive version next.",
                    commentCreatedAt = baseDate.AddDays(-7)
                },
                new Comment
                {
                    commentId = 3,
                    taskId = 2,
                    userId = 3,
                    commentMessage = "Having some issues with the cart state management. Need to review the approach.",
                    commentCreatedAt = baseDate.AddDays(-5)
                },
                new Comment
                {
                    commentId = 4,
                    taskId = 2,
                    fileId = 2,
                    userId = 1,
                    commentMessage = "Check the updated specifications document for the cart requirements.",
                    commentCreatedAt = baseDate.AddDays(-4)
                },
                new Comment
                {
                    commentId = 5,
                    taskId = 4,
                    userId = 4,
                    commentMessage = "The design is 80% complete. Waiting for feedback on the color scheme.",
                    commentCreatedAt = baseDate.AddDays(-3)
                },
                new Comment
                {
                    commentId = 6,
                    taskId = 6,
                    userId = 3,
                    commentMessage = "Data analysis completed. Found some inconsistencies in the legacy data.",
                    commentCreatedAt = baseDate.AddDays(-1)
                }
            };

            // Seed Activities
            var activities = new[]
            {
                new Activity
                {
                    activityId = 1,
                    userId = 1,
                    activityDescription = "Created project E-Commerce Website",
                    activityEntityType = EntityType.Project,
                    activityEntityId = 1,
                    activityCreatedAt = baseDate.AddDays(-30)
                },
                new Activity
                {
                    activityId = 2,
                    userId = 2,
                    activityDescription = "Completed task Design Homepage",
                    activityEntityType = EntityType.Task,
                    activityEntityId = 1,
                    activityCreatedAt = baseDate.AddDays(-10)
                },
                new Activity
                {
                    activityId = 3,
                    userId = 3,
                    activityDescription = "Started working on Implement Shopping Cart",
                    activityEntityType = EntityType.Task,
                    activityEntityId = 2,
                    activityCreatedAt = baseDate.AddDays(-5)
                },
                new Activity
                {
                    activityId = 4,
                    userId = 1,
                    activityDescription = "Commented on shopping cart implementation",
                    activityEntityType = EntityType.Comment,
                    activityEntityId = 4,
                    activityCreatedAt = baseDate.AddDays(-4)
                },
                new Activity
                {
                    activityId = 5,
                    userId = 4,
                    activityDescription = "Uploaded design file ui-design-mockups.fig",
                    activityEntityType = EntityType.File,
                    activityEntityId = 3,
                    activityCreatedAt = baseDate.AddDays(-3)
                },
                new Activity
                {
                    activityId = 6,
                    userId = 3,
                    activityDescription = "Completed data analysis for migration",
                    activityEntityType = EntityType.Task,
                    activityEntityId = 6,
                    activityCreatedAt = baseDate.AddDays(-1)
                }
            };

            // Add all seed data to modelBuilder
            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<Project>().HasData(projects);
            modelBuilder.Entity<UserProject>().HasData(userProjects);
            modelBuilder.Entity<ProjectTask>().HasData(tasks);
            modelBuilder.Entity<Doc>().HasData(docs);
            modelBuilder.Entity<Comment>().HasData(comments);
            modelBuilder.Entity<Activity>().HasData(activities);
        }
    }
}
