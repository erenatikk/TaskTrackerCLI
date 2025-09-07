using TaskTrackerCLI.Models;
using TaskTrackerCLI.Repository;
using TaskTrackerCLI.Services;

namespace TaskTrackerCLI;

class Program
{
    static void Main(string[] args)
    {
        var repo = new TaskRepo();
        var service = new TaskService(repo);

        if (args.Length == 0)
        {
            Console.WriteLine("No task found. Commads: add | list | delete | mark");
            return;
        }

        string command = args[0];

        if (command == "add")
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: add <task name> <task description>");
                return;
            }

            service.AddTask(args[1]);
        }

        else if (command == "list")
        {
            service.ListTasks();
        }

        else if (command == "delete")
        {
            if (args.Length < 2 || !int.TryParse(args[1], out int id))
            {
                Console.WriteLine("Usage: delete <task id>");
                return;
            }

            service.DeleteTask(id);
        }

        else if (command == "mark")
        {
            if (args.Length < 3 || !int.TryParse(args[1], out int id))
            {
                Console.WriteLine("Usage: mark <task id> <task status>");

                return;
            }

            service.MarkTask(id, args[2]);
        }
        else
        {
            Console.WriteLine("Unknown command.");
        }
    }
}