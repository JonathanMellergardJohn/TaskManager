using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Data.Models;
using TaskManager.Data.Entities;

namespace TaskManager.Data.Services
{
    public class TaskItemService
    {
        // instantiation do database context
        private readonly DataContext _context = new DataContext();
        public async Task SaveTaskItemToDbAsync(TaskItemEntity taskItemEntity)
        {           
            if (taskItemEntity.Supervisor.FirstName != "")
            {
                // Check if Supervisor exists in the database
                var supervisorExists = await _context.Staff.FirstOrDefaultAsync(s => s.FirstName == taskItemEntity.Supervisor.FirstName);
                if (supervisorExists != null)
                {
                    taskItemEntity.Supervisor = supervisorExists;
                }
                else
                {
                    throw new Exception($"Passed Staff {taskItemEntity.Supervisor.FirstName} does not exist in the database!");
                }
            }
            
            // Check if TaskItemStatus already exists in the database
            var statusExists = await _context.TaskItemsStatus.FirstOrDefaultAsync(s => s.Message == taskItemEntity.Status.Message);
            if (statusExists != null)
            {
                taskItemEntity.Status = statusExists;
            }
            else
            {
                throw new Exception($"Passed Status {taskItemEntity.Status.Message} does not exist in the database!");
            }

            _context.Add(taskItemEntity);
            await _context.SaveChangesAsync();
        }
        public async Task<ICollection<TaskItemEntity>> GetAllTaskItemsAsync()
        {
            // list of TaskItems to populate and return
            ICollection<TaskItemEntity> collection = await _context.TaskItems
                .Include(ti => ti.Supervisor)
                .Include(ti => ti.Status)
                .Include(ti => ti.Comment)
                .ToListAsync();

            return collection;
        }
        public async Task UpdateTaskItemSupervisorAsync(TaskItemEntity entity)
        {
            // Find the TaskItemEntity with the given id
            var _taskItem = await _context.TaskItems.
                Include(ti => ti.Supervisor).
                FirstOrDefaultAsync(ti => ti.Id == entity.Id);

            if (_taskItem == null)
            {
                throw new Exception($"TaskItemEntity with id {entity.Id} not found.");
            }

            // Check if a new supervisor is attached to the passed entity 
            if (entity.Supervisor != null && !string.IsNullOrEmpty(entity.Supervisor.FirstName))
            {
                // check if passed supervisor exists in the Staff table
                var existingSupervisor = await _context.Staff.FirstOrDefaultAsync(s => s.FirstName == entity.Supervisor.FirstName);

                if (existingSupervisor != null)
                {
                    // Update the SupervisorId to the existing supervisor's Id
                    _taskItem.SupervisorId = existingSupervisor.Id;
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception($"Passed Staff {entity.Supervisor.FirstName} does not exist in the database!");
                }
            }
        }
        public async Task UpdateTaskItemStatusAsync(TaskItemEntity taskItemEntity)
        {
            // Find the TaskItemEntity with the given id
            var _taskItem = await _context.TaskItems.
                Include(ti => ti.Status).
                FirstOrDefaultAsync(ti => ti.Id == taskItemEntity.Id);

            if (_taskItem == null)
            {                
                throw new Exception($"TaskItemEntity with id {taskItemEntity.Id} not found.");
            }

            // Check if the new status is available as an option in Db
            var existingStatus = await _context.TaskItemsStatus.FirstOrDefaultAsync(s => s.Message == taskItemEntity.Status.Message);

            if (existingStatus == null)
            {
                Console.WriteLine($"{taskItemEntity.Status} is not a valid status option!");
                throw new Exception($"{taskItemEntity.Status} is not a valid status option!");
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
        public async Task UpdateTaskItemCommentAsync(TaskItemEntity taskItemEntity)
        {
            // This is a little dangerous. It assumes a tasks Id and its comment is identical.
            // This is indeed the case now, but will change later on probably
            var _comment = await _context.Comments.
                FirstOrDefaultAsync(c => c.Id == taskItemEntity.Id);

            if (_comment == null)
            {
                throw new Exception($"TaskItemEntity with id {taskItemEntity.Id} not found.");
            }
            else
            {
                _comment.Text = taskItemEntity.Comment.Text;
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
        public async Task<TaskItemEntity> GetTaskItemByIdAsync(int id) 
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

            return taskItemEntity;
        }
    }
}
