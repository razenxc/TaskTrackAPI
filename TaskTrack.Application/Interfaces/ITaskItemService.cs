public interface ITaskItemService
{
    Task<Guid> Create(TaskItem taskItem);
    Task<Guid> Delete(Guid id);
    Task<List<TaskItem>> Get();
    Task<TaskItem> Get(Guid id);
    Task<Guid> Update(Guid id, string title, string description, TaskStatus status);
}
