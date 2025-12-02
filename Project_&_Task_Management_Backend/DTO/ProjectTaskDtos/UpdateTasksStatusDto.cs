namespace Project___Task_Management_Backend.DTO.ProjectTaskDtos
{
    public class UpdateTasksStatusDto
    {

        public List<int>? TodoTaskidUpdate { get; set; }
        public List<int>? InProgressTaskidUpdate { get; set; }
        public List<int>? DoneTaskidUpdate { get; set; }
    }
}
