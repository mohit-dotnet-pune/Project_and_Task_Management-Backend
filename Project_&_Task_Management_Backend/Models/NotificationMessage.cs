namespace Project___Task_Management_Backend.Models
{
    public class NotificationMessage
    {
        public string Type { get; set; }     // e.g., "taskAssigned", "projectAssigned", "statusChanged"
        public string Title { get; set; }    // short title
        public string Body { get; set; }     // human readable message
        public object? Data { get; set; }    // extra payload (taskId, projectId, status...)
        public DateTimeOffset SentAt { get; set; } = DateTimeOffset.UtcNow;
    }
}

