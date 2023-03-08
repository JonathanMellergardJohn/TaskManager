using TaskManager.ConsoleUI.Service;
using TaskManager.Core.Models;
using TaskManager.Data.Services;

TaskItemService taskService = new TaskItemService();

await taskService.DeleteTaskItemByIdAsync(4);

// UPDATE COMMENT
/*
DisplayService display = new DisplayService();
TaskItemService taskService = new TaskItemService();

// get and display task
TaskItem taskToUpdate = await taskService.GetTaskItemByIdAsync(1);
display.DisplaySingleTaskItem(taskToUpdate);

// edit task
DateTime now = DateTime.Now;
string timestamp = now.ToString("yyyy-MM-dd-HH-mm-ss-fff");

taskToUpdate.Comment = taskToUpdate.Comment + "\nNew Comment: " + timestamp + "\nOn second thought, I hate toilets. I ain't fixing shit.";

// update task to Db
await taskService.UpdateTaskItemCommentAsync(taskToUpdate);

// fetch and display updated task
var updatedTask = await taskService.GetTaskItemByIdAsync(1);
display.DisplaySingleTaskItem(updatedTask);
*/

// UPDATE STATUS
/*
DisplayService display = new DisplayService();
TaskItemService taskService = new TaskItemService();

TaskItem taskToUpdate = await taskService.GetTaskItemByIdAsync(4);
display.DisplaySingleTaskItem(taskToUpdate);

taskToUpdate.Status = "NotOpened";
await taskService.UpdateTaskItemStatusAsync(taskToUpdate);

TaskItem updatedTask = await taskService.GetTaskItemByIdAsync(4);
display.DisplaySingleTaskItem(updatedTask);
*/

// UPDATE SUPERVISOR

/*
DisplayService display = new DisplayService();
TaskItemService taskService = new TaskItemService();

TaskItem taskToUpdate = await taskService.GetTaskItemByIdAsync(4);
display.DisplaySingleTaskItem(taskToUpdate);

taskToUpdate.SupervisorFirstName = "Jose";
await taskService.UpdateTaskItemSupervisorAsync(taskToUpdate);

TaskItem updatedTask = await taskService.GetTaskItemByIdAsync(4);
display.DisplaySingleTaskItem(updatedTask);
*/

// GET ALL TASKS IN DB, DISPLAY TO CONSOLE
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

// ADD NEW TASK TO DB //
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


