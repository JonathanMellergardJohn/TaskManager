using TaskManager.Data.Models;

namespace TaskManager.ConsoleUI.Service
{
    public class DisplayService
    {
        public void DisplaySingleTaskItem(TaskItem taskItem) 
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine($"TASK ID: {taskItem.Id}");
            Console.WriteLine($"Description: {taskItem.Description}");
            Console.WriteLine($"Supervisor Id: {taskItem.SupervisorId}");
            Console.WriteLine($"Supervisor First Name: {taskItem.SupervisorFirstName}");
            Console.WriteLine($"Status Id: {taskItem.StatusId}");
            Console.WriteLine($"Status: {taskItem.Status}");
            Console.WriteLine($"Comment Id: {taskItem.CommentId}");
            Console.WriteLine($"Comment: {taskItem.Comment}");
            Console.WriteLine("----------------------------------");
        }
        public void DisplayListOfTaskItems(List<TaskItem> list) 
        { 
            foreach(var taskItem in list) 
            { 
                DisplaySingleTaskItem(taskItem);
            }
        }
        public void DisplaySingleStaff(Staff staff)
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine($"STAFF ID: {staff.Id}");
            Console.WriteLine($"FirstName: {staff.FirstName}");
            Console.WriteLine("\n~~~~~~~ Assigned Tasks ~~~~~~~");
            foreach (var task in staff.TasksForSupervision)
            {
                Console.WriteLine($"TASK ID: {task.Id}");
                Console.WriteLine($"Description: {task.Description}");
                Console.WriteLine("\n");
            }
            Console.WriteLine("----------------------------------");
        }
        public void DisplayListOfStaff(List<Staff> list)
        {
            foreach (var staff in list)
            {
                DisplaySingleStaff(staff);
            }
        }
    }
}
