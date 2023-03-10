using TaskManager.Data.Entities;
using TaskManager.Data.Models;

namespace TaskManager.Data.Services
{
    public class Mapper
    {
        public TaskItem TaskItemToModel(TaskItemEntity taskItemEntity)
        {
            TaskItem taskItem = new TaskItem
            {
                // this mapping is reused and should be refactored to a function 
                // to be called, so the mapping only has to be changed once!
                Id = taskItemEntity.Id,
                Description = taskItemEntity.Description,
                SupervisorId = taskItemEntity.SupervisorId,
                SupervisorFirstName = taskItemEntity.Supervisor?.FirstName ?? "",
                StatusId = taskItemEntity.StatusId,
                Status = taskItemEntity.Status?.Message ?? "",
                CommentId = taskItemEntity.CommentId,
                Comment = taskItemEntity.Comment?.Text ?? ""
            };

            return taskItem;
        } 
    }
}
