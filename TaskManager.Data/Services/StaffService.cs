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
        public async Task<StaffEntity> GetStaffById(int id)
        {
            var staffEntity = await _context.Staff
                .Include(s => s.TaskItems)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (staffEntity == null)
            {
                throw new Exception($"Staff with id {id} not found");
            }                     

            return staffEntity;
        }
        public async Task<ICollection<StaffEntity>> GetAllStaff()
        {
            // list of Staff to populate and return
            ICollection<StaffEntity> collection = await _context.Staff
                .Include(s => s.TaskItems)
                    .ThenInclude(ti => ti.Comment)
                .Include(s => s.TaskItems)
                    .ThenInclude(ti => ti.Status)
                .ToListAsync();

            return collection;
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
