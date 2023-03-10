namespace TaskManager.Data.Entities
{
    public class StaffEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        // sets up navigation property to TaskItem; does not show up as column
        public ICollection<TaskItemEntity> TaskItems { get; set; } = new HashSet<TaskItemEntity>();
    }
}
