

namespace TaskManager.Data.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Description { get; set; } = "";
        // SupervisorId is in reference to table/entity Staff
        public int? SupervisorId { get; set; }
        public string SupervisorFirstName { get; set; } = "";
        // StatusId is in reference to table/entity TaskItemStatus
        public int StatusId { get; set; }
        public string Status { get; set; } = "";
        public int CommentId { get; set; }
        public string Comment { get; set; } = "";
    }
}
