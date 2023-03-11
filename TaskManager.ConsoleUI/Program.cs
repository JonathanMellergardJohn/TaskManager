using TaskManager.ConsoleUI.Service;
using TaskManager.Data.Models;
using TaskManager.Data.Services;
using TaskManager.Data.Entities;

// GET ALL TASKS
DisplayService display = new DisplayService();
TaskItemService taskService = new TaskItemService();
Mapper mapper = new Mapper();

ICollection<TaskItemEntity> collection = await taskService.GetAllTaskItemsAsync();
List<TaskItem> list = mapper.ICollectionTaskToList(collection);
display.DisplayListOfTaskItems(list);

// GET TASK
/*
DisplayService display = new DisplayService();
TaskItemService taskService = new TaskItemService();
Mapper mapper = new Mapper();

TaskItemEntity entity = await taskService.GetTaskReturnEntity(2);
TaskItem model = mapper.TaskItemToModel(entity);
display.DisplaySingleTaskItem(model);
*/

// DISPLAY STAFF WITH ASSIGNED TASKS
/*
DisplayService display = new DisplayService();
StaffService service = new StaffService();

List<Staff> staffList = await service.GetAllStaff();
display.DisplayListOfStaff(staffList);
*/

// SAVE STAFF TO DB
/*
Staff staff = new Staff
{
    FirstName = "Johnny"
};

StaffService service = new StaffService();
await service.SaveStaffAsync(staff);
*/

// GET ALL STAFF
/*
StaffService service = new StaffService();
DisplayService display = new DisplayService();

List<Staff> list = await service.GetAllStaff();
display.DisplayListOfStaff(list);
*/

// GET STAFF BY ID
/*
StaffService service = new StaffService();
DisplayService display = new DisplayService();

Staff staff = await service.GetStaffById(1);
display.DisplaySingleStaff(staff);
*/

// DELETE STAFF
/*
StaffService service = new StaffService();
await service.DeleteStaffByIdAsync(4);
*/

// DELETE TASKITEM
/*
TaskItemService taskService = new TaskItemService();

await taskService.DeleteTaskItemByIdAsync(4);
*/

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
    Description = "I can't deal with this heat no more",
    // SupervisorId // not set!

    //StatusId // not set!
    Status = "NotOpened"
    //CommentId // not set!
};

TaskItemService taskService = new TaskItemService();
await taskService.SaveTaskItemToDbAsync(newTask);
*/



