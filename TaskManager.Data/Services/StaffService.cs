using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Data;

namespace TaskManager.Data.Services
{
    public class StaffService
    {
        private readonly DataContext _context = new DataContext();
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
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}
