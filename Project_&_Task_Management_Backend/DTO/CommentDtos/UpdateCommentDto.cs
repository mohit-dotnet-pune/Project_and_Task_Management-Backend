namespace Project___Task_Management_Backend.DTO.CommentDtos
{
    public class UpdateCommentDto
    {
        public string? commentMessage { get; set; }

        public int? fileId { get; set; }  // allow updating file
    }

}
