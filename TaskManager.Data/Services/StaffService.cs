using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using TaskManager.Data.Models;
using TaskManager.Data;
using TaskManager.Data.Entities;

namespace TaskManager.Data.Services
{
    public class StaffService
    {
        private readonly DataContext _context = new DataContext();
        public async Task<Staff> GetStaffById(int id)
        {
            var _staff = await _context.Staff
                .Include(s => s.TaskItems)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (_staff == null)
            {
                throw new Exception($"Task item with id {id} not found");
            }
            Staff staff = new Staff
            {
                Id = _staff.Id,
                FirstName = _staff.FirstName,
            };
            foreach (var taskItemEntity in _staff.TaskItems)
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
        public async Task<List<Staff>> GetAllStaff()
        {
            // list of Staff to populate and return
            var staffList = new List<Staff>();

            var _staffList = await _context.Staff
                .Include(s => s.TaskItems)
                .ToListAsync();

            foreach(var _staff in _staffList)
            {
                Staff newStaff = new Staff
                { 
                    Id = _staff.Id, 
                    FirstName = _staff.FirstName,
                };
                foreach (var taskItemEntity in _staff.TaskItems)
                {
                    newStaff.TasksForSupervision.Add(new TaskItem
                    {
                        Id = taskItemEntity.Id,
                        Description = taskItemEntity.Description
                        // Add other properties as needed
                    });
                }
                staffList.Add(newStaff);

            }

            return staffList;
        }
        public async Task DeleteStaffByIdAsync(int id)
        {
            var _staff = await _context.Staff.FindAsync(id);
            if (_staff != null)
            {
                _context.Staff.Remove(_staff);

                var _taskItems = await _context.TaskItems.Where(ti => ti.SupervisorId == id).ToListAsync();
                foreach (var _taskItem in _taskItems)
                {
                    _taskItem.SupervisorId = null;
                    // My inclination is that I actually SHOULD run this before .SaveChangesAsync:
                    // _context.TaskItems.Add(_taskItem);
                    // But it seems this is not needed? So why use .Update() at all..?
                    // It seems I don't fully grasp how DbContext works..?
                }
                await _context.SaveChangesAsync();
            }
        }
        public async Task SaveStaffAsync(Staff staff)
        {
            var _staff = new StaffEntity
            {
                FirstName = staff.FirstName,
            };

            _context.Staff.Add(_staff);
            await _context.SaveChangesAsync();
        }
    }
}
