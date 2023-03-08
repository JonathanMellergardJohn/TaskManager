using TaskManager.Core.Models;

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
            Console.WriteLine($"Comment Id: {taskItem.Comment}");
            Console.WriteLine("----------------------------------");
        }
        public void DisplayListOfTaskItems(List<TaskItem> list) 
        { 
            foreach(var taskItem in list) 
            { 
                DisplaySingleTaskItem(taskItem);
            }
        }
    }
}
