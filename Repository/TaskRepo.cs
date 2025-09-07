using System.Text.Json;
using System.Text.Json.Serialization;
using TaskTrackerCLI.Models;

namespace TaskTrackerCLI.Repository;

public class TaskRepo
{
    private readonly string filePath = "Data/tasks.json";

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        Converters = { new JsonStringEnumConverter() }
    };

    public List<TaskItem> Load()
    {
        if (!File.Exists(filePath))
            return new List<TaskItem>();

        string json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<TaskItem>>(json, JsonOptions) ?? new List<TaskItem>();
    }

    public void Save(List<TaskItem> tasks)
    {
        string json = JsonSerializer.Serialize(tasks, JsonOptions);
        File.WriteAllText(filePath, json);
    }
}