using Project___Task_Management_Backend.Models;

namespace Project___Task_Management_Backend.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetCommentsByTaskId(int taskId);
        Task<IEnumerable<Comment>> GetCommentsByUserId(int userId);
    }

}
