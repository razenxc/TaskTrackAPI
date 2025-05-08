public class TaskItemService : ITaskItemService
{
    private readonly ITaskItemRepository _taskItemRepo;
    public TaskItemService(ITaskItemRepository taskItemRepo)
    {
        _taskItemRepo = taskItemRepo;
    }

    public async Task<Guid> Create(TaskItem taskItem)
    {
        return await _taskItemRepo.Create(taskItem);
    }

    public async Task<List<TaskItem>> Get()
    {
        return await _taskItemRepo.Get();
    }

    public async Task<TaskItem> Get(Guid id)
    {
        return await _taskItemRepo.Get(id);
    }

    public async Task<Guid> Update(Guid id, string title, string description, TaskStatus status)
    {
        return await _taskItemRepo.Update(id, title, description, status);
    }

    public async Task<Guid> Delete(Guid id)
    {
        return await _taskItemRepo.Delete(id);
    }
}