using Microsoft.EntityFrameworkCore;
using TaskManagement.Data;
using TaskManager.Core.Models;
using TaskManager.Data.Entities;

namespace TaskManager.Data.Services
{
    public class TaskItemService
    {
        // instantiation do database context
        private readonly DataContext _context = new DataContext();
        public async Task SaveTaskItemToDbAsync(TaskItem taskItem)
        {
            var _taskItem = new TaskItemEntity
            {
                Description = taskItem.Description,
                Comment = new CommentEntity { Text = taskItem.Comment }
            };

            if (taskItem.SupervisorFirstName != "")
            {
                // Check if Supervisor already exists in the database
                var _supervisor = await _context.Staff.FirstOrDefaultAsync(s => s.FirstName == taskItem.SupervisorFirstName);
                if (_supervisor != null)
                {
                    _taskItem.Supervisor = _supervisor;
                }
                else
                {
                    _taskItem.Supervisor = new StaffEntity { FirstName = taskItem.SupervisorFirstName };
                }
            }
            

            // Check if TaskItemStatus already exists in the database
            var _status = await _context.TaskItemsStatus.FirstOrDefaultAsync(s => s.Message == taskItem.Status);
            if (_status != null)
            {
                _taskItem.Status = _status;
            }
            else
            {
                _taskItem.Status = new TaskItemStatusEntity { Message = taskItem.Status };
            }

            _context.Add(_taskItem);
            await _context.SaveChangesAsync();
        }
        public async Task<TaskItem> GetTaskItemByIdAsync(int id)
        {
            var taskItemEntity = await _context.TaskItems
                .Include(ti => ti.Supervisor)
                .Include(ti => ti.Status)
                .Include(ti => ti.Comment)
                .FirstOrDefaultAsync(ti => ti.Id == id);

            if (taskItemEntity == null)
            {
                throw new Exception($"Task item with id {id} not found");
            }

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
        public async Task<List<TaskItem>> GetAllTaskItemsAsync()
        {
            // list of TaskItems to populate and return
            var tasks = new List<TaskItem>();
            // fetch list of TaskItemEntity instances from Db
            var taskEntitites = await _context.TaskItems
                .Include(ti => ti.Supervisor)
                .Include(ti => ti.Status)
                .Include(ti => ti.Comment)
                .ToListAsync();
            // Iterate fetched TaskItemEntity and add each to return List of TaskItems 
            foreach(var taskItemEntity in taskEntitites) 
            {
                tasks.Add(new TaskItem 
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
                });
            }

            return tasks;
        }
        public async Task UpdateTaskItemSupervisorAsync(TaskItem taskItem)
        {
            // Find the TaskItemEntity with the given id
            var _taskItem = await _context.TaskItems.
                Include(ti => ti.Supervisor).
                FirstOrDefaultAsync(ti => ti.Id == taskItem.Id);

            if (_taskItem == null)
            {
                throw new Exception($"TaskItemEntity with id {taskItem.Id} not found.");
            }

            // Check if the new supervisor already exists in the Staff table
            var existingSupervisor = await _context.Staff.FirstOrDefaultAsync(s => s.FirstName == taskItem.SupervisorFirstName);

            if (existingSupervisor != null)
            {
                // Update the SupervisorId to the existing supervisor's Id
                _taskItem.SupervisorId = existingSupervisor.Id;
            }
            else
            {
                // Add the new supervisor to the Staff table
                var newSupervisor = new StaffEntity
                {
                    FirstName = taskItem.SupervisorFirstName,
                };
                await _context.Staff.AddAsync(newSupervisor);
                await _context.SaveChangesAsync();

                // Update the SupervisorId to the new supervisor's Id
                _taskItem.SupervisorId = newSupervisor.Id;
            }

            // Update the existing TaskItemEntity with the new SupervisorId
            _context.TaskItems.Update(_taskItem);

            // Save changes to the database
            await _context.SaveChangesAsync();
        }
        public async Task UpdateTaskItemStatusAsync(TaskItem taskItem)
        {
            // Find the TaskItemEntity with the given id
            var _taskItem = await _context.TaskItems.
                Include(ti => ti.Status).
                FirstOrDefaultAsync(ti => ti.Id == taskItem.Id);

            if (_taskItem == null)
            {
                
                throw new Exception($"TaskItemEntity with id {taskItem.Id} not found.");
            }

            // Check if the new status is available as an option in Db
            var existingStatus = await _context.TaskItemsStatus.FirstOrDefaultAsync(s => s.Message == taskItem.Status);

            if (existingStatus == null)
            {
                Console.WriteLine($"{taskItem.Status} is not a valid status option!");
                throw new Exception($"{taskItem.Status} is not a valid status option!");
            }
            else
            {               
                // Update the Status
                _taskItem.StatusId = existingStatus.Id;
            }

            // Update the existing TaskItemEntity with the new Status
            _context.TaskItems.Update(_taskItem);

            // Save changes to the database
            await _context.SaveChangesAsync();
        }
        public async Task UpdateTaskItemCommentAsync(TaskItem taskItem)
        {
            // This is a little dangerous. It assumes a tasks Id and its comment is identical.
            // This is indeed the case now, but will change later on probably
            var _comment = await _context.Comments.
                FirstOrDefaultAsync(c => c.Id == taskItem.Id);

            if (_comment == null)
            {
                throw new Exception($"TaskItemEntity with id {taskItem.Id} not found.");
            }
            else
            {
                _comment.Text = taskItem.Comment;
            }
            _context.Comments.Update(_comment);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteTaskItemByIdAsync(int id)
        {
            var taskItem = await _context.TaskItems.FindAsync(id);
            if (taskItem != null)
            {
                _context.TaskItems.Remove(taskItem);                   
            }

            var comment = _context.Comments.Find(id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
            }

            await _context.SaveChangesAsync();
        }

    }
}
