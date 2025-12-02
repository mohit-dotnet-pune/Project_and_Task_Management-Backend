using Project___Task_Management_Backend.DTO.CommentDtos;
using Project___Task_Management_Backend.Models;

namespace Project___Task_Management_Backend.Interfaces
{
    public interface ICommentService
    {
        Task<Comment?> CreateComment(CreateCommentDto dto);
        Task<Comment?> GetComment(int commentId);
        Task<IEnumerable<Comment>> GetComments();
        Task<bool> DeleteComment(int commentId);
        Task<Comment?> UpdateComment(int commentId, UpdateCommentDto dto);

        Task<IEnumerable<Comment>> GetCommentsByTask(int taskId);
        Task<IEnumerable<Comment>> GetCommentsByUser(int userId);
        Task<int?> GetProjectIdByTask(int taskId);

    }

}
