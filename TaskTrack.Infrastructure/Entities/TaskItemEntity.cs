public class TaskItemEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public TaskStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}