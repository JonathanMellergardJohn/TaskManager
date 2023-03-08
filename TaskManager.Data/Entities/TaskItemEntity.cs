using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Data.Entities
{
    public class TaskItemEntity
    {
        public int Id { get; set; }
        public string Description { get; set; } = "";

        [ForeignKey("Supervisor")] // FK to Staff table; one to many
        public int SupervisorId { get; set; }
        public StaffEntity? Supervisor { get; set; }

        [ForeignKey("Status")] // FK to TaskItemStatus table; one to many
        public int StatusId { get; set; }
        public TaskItemStatusEntity? Status { get; set; }

        [ForeignKey("Comment")] // FK to Comment table; one to one
        public int CommentId { get; set; } 
        public CommentEntity? Comment { get; set; } 
    }
}
