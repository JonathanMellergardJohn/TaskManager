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
            var taskItemEntity = new TaskItemEntity
            {
                Description = taskItem.Description,
                Comment = new CommentEntity { Text = taskItem.Comment }
            };

            // Check if Supervisor already exists in the database
            var supervisorEntity = await _context.Staff.FirstOrDefaultAsync(s => s.FirstName == taskItem.SupervisorFirstName);
            if (supervisorEntity != null)
            {
                taskItemEntity.Supervisor = supervisorEntity;
            }
            else
            {
                taskItemEntity.Supervisor = new StaffEntity { FirstName = taskItem.SupervisorFirstName };
            }

            // Check if TaskItemStatus already exists in the database
            var statusEntity = await _context.TaskItemsStatus.FirstOrDefaultAsync(s => s.Message == taskItem.Status);
            if (statusEntity != null)
            {
                taskItemEntity.Status = statusEntity;
            }
            else
            {
                taskItemEntity.Status = new TaskItemStatusEntity { Message = taskItem.Status };
            }

            _context.Add(taskItemEntity);
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
        public void UpdateTaskItemInDb(TaskItem taskItem)
        {
            // Find the TaskItemEntity with the given id
            var _taskItem = _context.TaskItems.
                Include(ti => ti.Supervisor).
                Include(ti => ti.Status).
                Include(ti => ti.Comment).
                FirstOrDefault(ti => ti.Id == taskItem.Id);

            if (_taskItem == null)
            {
                throw new Exception($"TaskItemEntity with id {taskItem.Id} not found.");
            }
            // UPDATE SUPERVISOR

            // Check if the new supervisor already exists in the Staff table
            var existingSupervisor = _context.Staff.FirstOrDefault(s => s.FirstName == taskItem.SupervisorFirstName);

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
                _context.Staff.Add(newSupervisor);
                _context.SaveChanges();

                // Update the SupervisorId to the new supervisor's Id
                _taskItem.SupervisorId = newSupervisor.Id;
            }

            // Update the existing TaskItemEntity with the new SupervisorId
            _context.TaskItems.Update(_taskItem);

            // Save changes to the database
            _context.SaveChanges();
        }
    }
}
