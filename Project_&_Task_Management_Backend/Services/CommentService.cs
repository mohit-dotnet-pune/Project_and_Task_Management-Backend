using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto;
using Project___Task_Management_Backend.Data;
using Project___Task_Management_Backend.DTO.CloudinaryDtos;
using Project___Task_Management_Backend.DTO.CommentDtos;
using Project___Task_Management_Backend.Interfaces;
using Project___Task_Management_Backend.Models;

namespace Project___Task_Management_Backend.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepo;
        private readonly AppDbContext _appDbContext;
        private readonly CloudinaryService _cloudinaryService;
        

        public CommentService(ICommentRepository commentRepo, AppDbContext app,CloudinaryService cloudinaryService)
        {
            _appDbContext = app;
            _commentRepo = commentRepo;
            _cloudinaryService = cloudinaryService;
        }

        //------------------ CREATE ------------------
        public async Task<Comment?> CreateComment(CreateCommentDto dto)
        {
            var comment = new Comment
            {
                commentMessage = dto.commentMessage,
                taskId = dto.taskId,
                userId = dto.userId
            };

            // Handle file upload
            if (dto.commentFileForm != null)
            {
                UploadResponse uploadResponse = await _cloudinaryService.UploadFileAsync(dto.commentFileForm);

                var file = new Doc
                {
                    fileName = uploadResponse.FileName,
                    fileURL = uploadResponse.FileUrl
                };

                _appDbContext.docs.Add(file);
                await _appDbContext.SaveChangesAsync();

                comment.fileId = file.fileId;
            }

            _appDbContext.comments.Add(comment);
            await _appDbContext.SaveChangesAsync();

            return comment;
        }


        //------------------ READ ------------------
        public async Task<Comment?> GetComment(int commentId)
        {
            var comment = await _appDbContext.comments.FindAsync(commentId);
            return comment;
        }

        public async Task<IEnumerable<Comment>> GetComments()
        {
            return await _appDbContext.comments.Include(c => c.file).ToListAsync();
        }

        //------------------ DELETE ------------------
        public async Task<bool> DeleteComment(int commentId)
        {
            Comment comment = await _appDbContext.comments.FindAsync(commentId);
            if (comment == null) return false;

             _appDbContext.comments.Remove(comment);
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        //------------------ UPDATE ------------------
        public async Task<Comment?> UpdateComment(int commentId, UpdateCommentDto dto)
        {
            var comment = await _appDbContext.comments.FindAsync(commentId);
            if (comment == null) return null;


            if (dto.commentMessage != null)
                comment.commentMessage = dto.commentMessage;

            if (dto.commentFileForm != null)
            {
                UploadResponse uploadResponse = await _cloudinaryService.UploadFileAsync(dto.commentFileForm);

                var file = new Doc
                {
                    fileName = uploadResponse.FileName,
                    fileURL = uploadResponse.FileUrl
                };

                _appDbContext.docs.Add(file);
                await _appDbContext.SaveChangesAsync();
                comment.fileId = file.fileId;
                comment.file = file;
            }

            await _appDbContext.SaveChangesAsync();

            return comment;
        }

        //------------------ EXTRA FILTERS ------------------
        public async Task<IEnumerable<Comment>> GetCommentsByTask(int taskId)
        {
            var list = await _commentRepo.GetCommentsByTaskId(taskId);
            return list;
        }

        public async Task<IEnumerable<Comment>> GetCommentsByUser(int userId)
        {
            var list = await _commentRepo.GetCommentsByUserId(userId);
            return list;
        }

        public async Task<int?> GetProjectIdByTask(int taskId)
        {
            return await _appDbContext.tasks
                .Where(t => t.taskId == taskId)
                .Select(t => (int?)t.projectId)
                .FirstOrDefaultAsync();
        }

    }

}
