using TaskManager.ConsoleUI.Service;
using TaskManager.Core.Models;
using TaskManager.Data.Migrations;
using TaskManager.Data.Services;


// Get all Tasks in Db, display to console
/*
DisplayService display = new DisplayService();
TaskItemService taskService = new TaskItemService();

List<TaskItem> tasks = await taskService.GetAllTaskItemsAsync();
display.DisplayListOfTaskItems(tasks);
*/

// Get TaskItem from Db, then display to console //
/*
DisplayService display = new DisplayService();
TaskItemService taskService = new TaskItemService();

TaskItem taskItem = await taskService.GetTaskItemByIdAsync(2);
display.DisplaySingleTaskItem(taskItem);
*/

// Add new task to Db //
/*
TaskItem newTask = new TaskItem
{
    Description = "I'm really in love with Jose, he's so hot!",
    // SupervisorId // not set!
    SupervisorFirstName = "Jose",

    //StatusId // not set!
    Status = "NotOpened",
    //CommentId // not set!
    Comment = "I don't really know what to do about this task, to be honest."
};

TaskItemService taskService = new TaskItemService();
await taskService.SaveTaskItemToDbAsync(newTask);
*/


