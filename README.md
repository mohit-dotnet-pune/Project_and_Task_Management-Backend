# Project & Task Management — ASP.NET Core 8 Backend

## Project Summary
A layered ASP.NET Core 8 Web API for managing projects, tasks, comments and files, with real-time notifications (SignalR), Cloudinary file uploads, email OTP flows and JWT-based authentication with role-based authorization.

- Language: C# 12
- Target: .NET 8
- DB: Microsoft SQL Server (EF Core)
- Real-time: SignalR
- File storage: Cloudinary
- Auth: JWT + role claims (Manager / Employee)

---

## Table of Contents
1. Quick start
2. Configuration
3. Run locally
4. Folder structure
5. Architecture & data flow
6. Controllers & key endpoints (examples)
7. Models & database schema
8. DTOs
9. Services & repositories
10. Middleware pipeline
11. SignalR (notifications)
12. Authentication & security
13. Error handling & validation
14. Known issues & recommended improvements
15. Next steps

---

## 1 — Quick start (local)
1. Create a SQL Server and connection string.
2. Create a `.env` at repo root (not committed) with:
   - `CONNECTION_STRING`
   - `JWT_KEY`
   - `JWT_ISSUER`
   - `JWT_AUDIENCE`
   - `JWT_EXPIRY_MINUTES`
   - `CloudName`, `ApiKey`, `ApiSecret`
3. Apply EF migrations and seed DB:
   - __dotnet ef database update__
4. Run the app:
   - __dotnet run__ (or run from Visual Studio)

Swagger is available in Development: `/swagger`.

---

## 2 — Configuration
- Program reads `.env` via DotNetEnv.Env.Load().
- appsettings.json contains non-sensitive defaults (Cloudinary placeholders can be present but secrets are loaded from `.env`).
- Important env keys (example):
  - JWT_KEY: strong symmetric key
  - CONNECTION_STRING: SQL Server connection string

---

## 3 — Run locally (commands)
- Restore & build: __dotnet restore__ / __dotnet build__
- Run migrations: __dotnet ef database update__
- Start: __dotnet run__ or press Run in Visual Studio

---

## 4 — Folder structure (high level)
- Controllers/ — HTTP endpoints
- Services/ — Business logic and orchestration
- Repository/ — EF Core data access
- Data/ — AppDbContext (EF Core)
- DTO/ — Request/response shapes
- Models/ — EF entities
- Middleware/ — custom middleware (JwtVerificationMiddleware)
- Hubs/ — SignalR Hub (NotificationHub)
- Program.cs — app bootstrap & DI
- appsettings.json / .env — configuration

---

## 5 — Architecture & data flow (text diagram)
API Client (browser/mobile)  
→ HTTPS + Bearer JWT  
→ Middleware (CORS → Authentication → JwtVerificationMiddleware → Authorization)  
→ Controller → Service → Repository → AppDbContext → SQL Server  
SignalR Hub publishes notifications to connected clients

---

## 6 — Controllers — overview & key endpoints

General: controllers return IActionResult or ActionResult<T>. Many use DTOs for input.

AuthController
- POST /api/auth/register
  - Body: RegisterDto { username, email, password, role }
  - Response: message (register saved -> request OTP)
- POST /api/auth/get-otp
  - Body: GetOtpDto { email, forceSend? }
- POST /api/auth/verify-email
  - Body: VerifyEmailOtpDto { email, otp }
- POST /api/auth/login
  - Body: LoginDto { email, password }
  - Success: returns token and message
- POST /api/auth/forgot-password, POST /api/auth/reset-password

ProjectController [Authorize(Roles = "Manager")]
- POST /api/project
  - Body: CreateProjectDto
- GET /api/project/{id}
- GET /api/project
- PUT /api/project/{id}
- DELETE /api/project/{id}
- PUT /api/project/{projectId}/attach-file/{fileId}
- PUT /api/project/{projectId}/detach-file/{fileId}
- POST /api/project/addUserToProject?userId=...&projectId=...
- DELETE /api/project/removeUserFromProject?userId=...&projectId=...
- GET /api/project/{projectId}/GetAllusers
- GET /api/project/{userId}/GetAllprojects

ProjectTaskController
- POST /api/projecttask
- PUT /api/projecttask/{id}
- DELETE /api/projecttask/{id}
- GET /api/projecttask/{id}
- GET /api/projecttask/getAll
- GET /api/projecttask/getAllTask/{userId}
- POST /api/projecttask/{taskId}/attach-user/{userId}
- POST /api/projecttask/{taskId}/attach-file/{fileId}
- GET /api/projecttask/project/{projectId}/tasks

CommentController
- CRUD on comments and queries by task/user.

FileControllers
- NOTE: There are two controllers with the same class name `FileController` (one lists DB docs, the other handles Cloudinary uploads). Rename one class to avoid a compile-time duplicate-type error (e.g., rename Cloudinary controller class to `CloudinaryController` or DocsController).

---

## 7 — Models & database schema (summary)
Main entities:
- User (userId PK, userName, userEmail, userPassword(hashed), userRole, EmailConfirmed, JwtToken, RefreshToken, CreatedAt, navigation to tasks/comments/userProjects)
- Project (projectId PK, projectName, projectDescription, projectStartDate, projectEndDate, fileId, tasks)
- ProjectTask (taskId PK, projectId FK, userId FK (nullable), fileId FK (nullable), taskTitle, taskStatus, taskPriority, taskDueDate)
- Comment (commentId PK, taskId FK, userId FK, fileId FK (nullable), commentMessage)
- Doc (fileId PK, fileName, fileURL)
- UserProject (userProjectId PK) — join table between user & project

Relationships configured in AppDbContext.OnModelCreating (Includes, cascade behavior, seed data).

---

## 8 — DTOs (examples)
- CreateProjectDto / UpdateProjectDto
- CreateTaskDto / UpdateTaskDto / UpdateTasksStatusDto
- RegisterDto, GetOtpDto, VerifyEmailOtpDto, LoginDto, ForgotPasswordDto, ResetPasswordDto
- ResponseDto { IsSuccess: bool, Message: string }

Sample CreateProjectDto

---

## 9 — Services & Repositories
- AuthService
  - Handles registration (temporary store + OTP), OTP send/verify, login (JWT + refresh token stored), forgot/reset password.
  - Stores temp registration and OTPs in-memory (Dictionary) — suitable for dev only.
- ProjectService / ProjectRepository
  - CRUD projects, attach/detach files, manage user-project mappings. ProjectRepository uses EF Core includes to return tasks and files.
- ProjectTaskService / ProjectTaskRepository
  - Create/update tasks, attach/detach user/file, bulk status update, send notifications via NotificationService.
- NotificationService
  - Wraps IHubContext<NotificationHub> to send group messages.

---

## 10 — Middleware pipeline
Ordered in Program.cs:
- UseCors("AllowFrontend")
- UseHttpsRedirection
- UseAuthentication
- UseMiddleware<JwtVerificationMiddleware> (checks that paths not under /api/auth are authenticated and that JWT expiry "exp" claim isn't past)
- UseAuthorization
- MapControllers
- MapHub<NotificationHub>("/notifications") (CORS must be enabled before MapHub)

Important: __UseAuthentication__ must run before custom JwtVerificationMiddleware — Program.cs currently respects this.

---

## 11 — SignalR — NotificationHub
- Clients call `RegisterUser(userId)` to join a group named `user-{userId}`.
- NotificationService sends to group `user-{userId}` via IHubContext.
- Typical messages are NotificationMessage { Type, Title, Body }.

---

## 12 — Authentication & Security
- JWT issued in AuthService.GenerateJwtToken using env JWT_KEY; includes role claim and userId.
- Program.cs configures JwtBearer validation (issuer, audience, signing key).
- Role-based authorization using [Authorize(Roles = "Manager")] on ProjectController.
- JwtVerificationMiddleware validates token expiry by reading the `exp` claim; also allows /api/auth routes unauthenticated.
- Recommendations:
  - Use secure, long JWT_KEY in production.
  - Do not commit secrets; use environment or secret store.
  - Consider validating presented token against DB-stored JwtToken for server-side revocation.
  - Add a refresh-token endpoint and rotate refresh tokens.

---

## 13 — Error handling & validation
- Controllers validate ModelState and return 400 Bad Request for invalid models.
- Services return (bool success, message) tuples or nulls; controllers map them to appropriate status codes (400, 404, 401).
- No global exception middleware currently — consider adding centralized error handling for consistent JSON error responses.

---


---

