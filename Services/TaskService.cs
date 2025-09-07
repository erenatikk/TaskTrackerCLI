using TaskTrackerCLI.Models;
using TaskTrackerCLI.Repository;
using TaskStatus = TaskTrackerCLI.Models.TaskStatus;

namespace TaskTrackerCLI.Services;

public class TaskService
{
    private readonly TaskRepo _taskRepo;

    public TaskService(TaskRepo taskRepo)
    {
        _taskRepo = taskRepo;
    }

    public void AddTask(string description)
    {
        var tasks = _taskRepo.Load();
        
        int newId = tasks.Count == 0 ? 1 : tasks.Max(t => t.Id ) + 1;

        var task = new TaskItem
        {
            Id = newId,
            Description = description,
        };
        
        tasks.Add(task);
        _taskRepo.Save(tasks);
        
        Console.WriteLine($"Task {task.Id} has been added. Description: {task.Description}");
    }
    
    public void ListTasks()
    {
        var tasks = _taskRepo.Load();

        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks found.");
            return;
        }

        foreach (var task in tasks)
        {
            Console.WriteLine($"{task.Id}. {task.Description} -  [{task.Status}]");
        }
    }

    public void DeleteTask(int id)
    {
        var tasks = _taskRepo.Load();
        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task == null)
        {
            Console.WriteLine("No task found.");
            return;
        }
        
        tasks.Remove(task);
        _taskRepo.Save(tasks);
        
        Console.WriteLine($"Task {id} has been deleted. Description: {task.Description}");
    }

    public void MarkTask(int id, string status)
    {
        var tasks = _taskRepo.Load();
        var task = tasks.FirstOrDefault(t => t.Id == id);

        if (task == null)
        {
            Console.WriteLine("No task found.");
            return;
        }

        if (Enum.TryParse<TaskStatus>(status, ignoreCase: true, out TaskStatus parsedStatus))
        {
            task.Status = parsedStatus;
            task.UpdatedAt = DateTime.UtcNow;
            _taskRepo.Save(tasks);
            Console.WriteLine($"Task {id} has been updated. Description: {task.Description}, Status: {task.Status}");
        }

        else
        {
            Console.WriteLine("Invalid status provided. Please use: 'Todo', 'In Progress', 'Done'. ");
        }
    }
}