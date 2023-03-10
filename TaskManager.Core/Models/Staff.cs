using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Data.Models
{
    public class Staff
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public List<TaskItem> TasksForSupervision { get; set; } = new List<TaskItem>();
    }
}
