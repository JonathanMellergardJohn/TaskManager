

namespace TaskManager.Data.Entities
{
    public class TaskItemStatusEntity
    {
        public int Id { get; set; }
        public string Message { get; set; } = "";
        // sets up navigation property to TaskItem; does not show up as column
        public ICollection<TaskItemEntity> TaskItems { get; set; } = new HashSet<TaskItemEntity>();
    }
}
