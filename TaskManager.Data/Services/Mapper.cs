using TaskManager.Data.Entities;
using TaskManager.Data.Models;

namespace TaskManager.Data.Services
{
    public class Mapper
    {
        public TaskItem TaskEntityToModel(TaskItemEntity taskItemEntity)
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

            Console.WriteLine(taskItem.Comment);
            Console.WriteLine(taskItem.Status);
            Console.ReadLine();

            return taskItem;
        } 
        public TaskItemEntity TaskModelToEntity(TaskItem taskItem)
        {
            var taskItemEntity = new TaskItemEntity
            {
                Id = taskItem.Id,
                Description = taskItem.Description,
                CommentId = taskItem.CommentId,
                Comment = new CommentEntity { Text = taskItem.Comment },
                SupervisorId = taskItem.SupervisorId,
                Supervisor = new StaffEntity { FirstName = taskItem.SupervisorFirstName },
                StatusId = taskItem.StatusId,
                Status = new TaskItemStatusEntity { Message = taskItem.Status }
            };

            return taskItemEntity;
        }
        public List<TaskItem> TaskICollectionToList(ICollection<TaskItemEntity> collection)
        {
            List<TaskItem> list = new List<TaskItem>();

            foreach (var taskItemEntity in collection)
            {
                var taskItem = TaskEntityToModel(taskItemEntity);
                list.Add(taskItem);
            }
            return list;
        }
        public Staff StaffEntityToModel(StaffEntity staffEntity)
        {
            var staff = new Staff
            {
                Id = staffEntity.Id,
                FirstName = staffEntity.FirstName,
            };
            foreach (var taskItemEntity in staffEntity.TaskItems)
            {
                staff.TasksForSupervision.Add(new TaskItem
                {
                    Id = taskItemEntity.Id,
                    Description = taskItemEntity.Description
                    // Add other properties as needed
                });
            }

            return staff;
        }
        public List<Staff> StaffICollectionToList(ICollection<StaffEntity> collection)
        {
            var list = new List<Staff>();

            foreach (var staff in collection)
            {
                Staff newStaff = new Staff
                {
                    Id = staff.Id,
                    FirstName = staff.FirstName,
                };
                foreach (var taskItemEntity in staff.TaskItems)
                {
                    newStaff.TasksForSupervision.Add(new TaskItem
                    {
                        Id = taskItemEntity.Id,
                        Description = taskItemEntity.Description,
                        Comment = taskItemEntity.Comment?.Text ?? "",  
                        Status = taskItemEntity.Status?.Message ?? ""
                    });
                }
                list.Add(newStaff);
            }

            return list;
        }
    }
}
