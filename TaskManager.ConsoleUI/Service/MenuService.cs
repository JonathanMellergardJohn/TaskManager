using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Data.Entities;
using TaskManager.Data.Models;
using TaskManager.Data.Services;

namespace TaskManager.ConsoleUI.Service
{
    public class MenuService
    {
        TaskItemService taskService = new TaskItemService();
        StaffService staffService = new StaffService();
        DisplayService displayService = new DisplayService();
        Mapper mapper = new Mapper();
        public void LogInView() 
        {
            Console.Clear();
            // get all Staff entities and converst to List of Staff models
            ICollection<StaffEntity> collection = staffService.GetAllStaff().Result;
            var list = mapper.StaffICollectionToList(collection);

            // list to store available ids
            List<int> staffIds = list.Select(s => s.Id).ToList();

            displayService.DisplayListOfStaff(list);

            Console.WriteLine(  "Welcome to the Task Manager App! Log in by providing your Staff \n" +
                                "ID in the console and press enter. If you have forgotten your \n" +
                                "Staff ID, please find your name in the list provided above.");

            int selectedId;
            while (true)
            {
                Console.Write("Your Staff Id: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out selectedId))
                {
                    if (staffIds.Contains(selectedId))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("The Staff Id does not exist. Please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter an integer.");
                }
            }         
            Staff user = list.Single(s => s.Id == selectedId);
            StaffMenuView(user);
        }
        public async void StaffMenuView(Staff user)
        {
            Console.Clear();
            Console.WriteLine(" -- MENU -- \n");
            Console.WriteLine("1 - View your task");
            Console.WriteLine("2 - View tasks you have reported");
            Console.WriteLine("3 - Report a new task\n");
            Console.Write("Input your selection and press enter:");
            
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    UserResponsibilitesView(user);
                    break;
                case "2":
                    // UsersReportsView(user)
                    break;
                case "3":
                    string description = MakeReportView(user);
                    
                    TaskItem taskModel = new TaskItem { Description = description };
                    TaskItemEntity taskEntity = mapper.TaskModelToEntity(taskModel);
                    await taskService.SaveTaskItemToDbAsync(taskEntity);
                    Console.WriteLine("passed saveTask...");
                    Console.ReadLine();
                    StaffMenuView(user);
                    break;
                default: 
                    Console.WriteLine("Invalid choice. Please consult the menu to input a valid selection.");
                    StaffMenuView(user);
                    break;
            }
        }
        public void AdminMenuView(Staff user)
        {
            Console.WriteLine(" -- MENU -- \n");
            Console.WriteLine("1 - View all tasks");
            Console.WriteLine("2 - View tasks by Status\n");
            Console.Write("Input your selection and press enter:");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    // AdminAllTasksView(user)
                    break;
                case "2":
                    // AdminTasksByStatusView(user)
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please consult the menu to input a valid selection.");
                    AdminMenuView(user);
                    break;
            }
        }

        
        public void UserResponsibilitesView(Staff user)
        {
            Console.Clear();
            List<TaskItem> taskList = user.TasksForSupervision;

            foreach (var task in user.TasksForSupervision)
            {
                Console.WriteLine($"Task Id: {task.Id}");
                Console.WriteLine($"Description: {task.Description}");
                Console.WriteLine("--------------------------------");
            }

            Console.WriteLine("Your selection:");
            string input = Console.ReadLine();
            while(true)
            {
                if (int.TryParse(input, out int number))
                {
                    switch (input)
                    {
                        case string caseValue when user.TasksForSupervision.Any(ti => ti.Id.ToString() == caseValue):
                            TaskItem selectedTaskItem = user.TasksForSupervision.Single(ti => ti.Id.ToString() == caseValue);
                            int taskId = selectedTaskItem.Id;
                            UserSingleResponsibilityView(user, taskId);
                            break;

                        default:
                            Console.WriteLine("Invalid input. Press enter to continue!");
                            Console.ReadLine();
                            UserResponsibilitesView(user);
                            break;
                    }
                } else
                {
                    Console.WriteLine("Invalid input. Press enter to continue!");
                    Console.ReadLine();
                }
            }
            
            
        }
        public async void UserSingleResponsibilityView(Staff user, int taskId)
        {
            Console.Clear();
            TaskItem task = user.TasksForSupervision.Single(t => t.Id == taskId);

            Console.WriteLine($"TASK ID: {task.Id}");
            Console.WriteLine($"Description: {task.Description}");
            Console.WriteLine($"Status: {task.Status}");
            Console.WriteLine($"Comments: {task.Comment}");
            Console.WriteLine("____________________________");
            Console.WriteLine(  "To view all your tasks again, input '1' and press enter.\n " +
                                "To add a comment, input '2' and press enter.\n");
            Console.WriteLine("Your input:");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    UserResponsibilitesView(user);
                    break;
                case "2":
                    TaskItem updatedTask = AddCommentView(user, taskId);
                    TaskItemEntity updatedEntity = mapper.TaskModelToEntity(updatedTask);
                    await taskService.UpdateTaskItemCommentAsync(updatedEntity);
                    Console.WriteLine("Press enter to return to viewing your other tasks");
                    Console.ReadLine();
                    UserResponsibilitesView(user);
                    break;

                default:
                    Console.WriteLine("Invalid input. Press enter to continue.");
                    Console.ReadLine();
                    UserSingleResponsibilityView(user, taskId);
                    break;
            }
        }
        public TaskItem AddCommentView(Staff user, int taskId)
        {
            TaskItem task = user.TasksForSupervision.Single(t => t.Id == taskId);

            Console.Clear();
            Console.WriteLine($"TASK ID: {task.Id}");
            Console.WriteLine($"Description: {task.Description}");
            Console.WriteLine($"Status: {task.Status}");
            Console.WriteLine($"Comments: {task.Comment}");
            Console.WriteLine("____________________________");

            Console.WriteLine("Add your comment and press enter.\n");
            Console.WriteLine("Your commwent: ");
            string comment = Console.ReadLine();

            DateTime now = DateTime.Now;
            string timestamp = now.ToString("yyyy-MM-dd-HH-mm-ss-fff");

            task.Comment = $"{task.Comment}\n " +
                            $"{comment}\n" +
                            $"--{user.FirstName} ({timestamp})";

            return task;
        }
        public string MakeReportView(Staff user)
        {
            Console.Clear();
            Console.WriteLine("Give a brief description of the task you want to report.");
            Console.Write("Description: ");

            string description = Console.ReadLine();

            Console.Clear();
            Console.WriteLine(  "Your reported task will be reviewed and and assigned a supervisor.\n" +
                                "Thank you for your help! Press enter to return return to menu.");
            Console.ReadLine();

            return description;

        }

        //  15 methods in total
        //  completed: 
        //
        //  LogInView()                                 <-- COMPLETE
        //      StaffMenuView()                         <-- COMPLETE
        //          UsersResponsibilitiesView(user)     <-- COMPLETE
        //              UserSingleResponsibilityView()  <-- COMPLETE
        //                  AddCommentView()
        //          UsersReportsView(user)
        //              UserSingleReportView()
        //          MakeReportView()
        //      AdminMenuView()                         <-- COMPLETE
        //          AdminAllTasksView()
        //              AdminSingleTaskView()
        //                  AdminEditTaskView()    
        //          AdminTasksByStatusView()
        //              AdminSingleTaskView()
        //                  AdminEditTaskView()
        //             
        //  Underlying architecture fixes:
        //      -Change comments to list
        //      -include reportee in taskitementity
    }
}
