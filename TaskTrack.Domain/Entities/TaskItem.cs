public class TaskItem
{
    private TaskItem(Guid id, string title, string description, TaskStatus status, DateTime createdAt)
    {
        Id = id;
        Title = title;
        Description = description;
        Status = status;
        CreatedAt = createdAt;
    }

    public Guid Id { get; }
    public string Title { get; }
    public string? Description { get; }
    public TaskStatus Status { get; } // enum: New, InProgress, Done
    public DateTime CreatedAt { get; }

    public static (TaskItem taskItem, string Error) Create(Guid id, string title, string description, TaskStatus status, DateTime createdAt)
    {
        string error = string.Empty;

        if(string.IsNullOrEmpty(title))
        {
            error = "Title cannot be empty!";
            return (null, error);
        }

        TaskItem model = new TaskItem(id, title, description, status, createdAt);

        return (model, error);
    }

    public static (TaskItem taskItem, string Error) Update(TaskItem taskItem)
    {
        string error = string.Empty;

        if(taskItem.Status == TaskStatus.Done)
        {
            error = "The task is completed and cannot be updated!";
            return (null, error);
        }

        return (taskItem, error);
    }
}