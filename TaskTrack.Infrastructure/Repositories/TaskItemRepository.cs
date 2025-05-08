using Microsoft.EntityFrameworkCore;

public class TaskItemRepository : ITaskItemRepository
{
    private readonly ApplicationDbContext _db;
    public TaskItemRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Guid> Create(TaskItem taskItem)
    {
        TaskItemEntity taskItemEntity = new TaskItemEntity
        {
            Id = taskItem.Id,
            Title = taskItem.Title,
            Description = taskItem.Description,
            CreatedAt = taskItem.CreatedAt,
            Status = taskItem.Status,
        };

        await _db.TaskItems.AddAsync(taskItemEntity);
        await _db.SaveChangesAsync();

        return taskItemEntity.Id;
    }

    public async Task<List<TaskItem>> Get()
    {
        List<TaskItemEntity> taskItemEntities = await _db.TaskItems
        .AsNoTracking()
        .ToListAsync();

        List<TaskItem> taskItems = taskItemEntities
        .Select(x => TaskItem.Create(x.Id, x.Title, x.Description, x.Status, x.CreatedAt).taskItem)
        .ToList();

        return taskItems;
    }

    public async Task<TaskItem?> Get(Guid id)
    {
        TaskItemEntity taskItemEntity = await _db.TaskItems
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Id == id);

        if (taskItemEntity == null)
            return null;

        TaskItem taskItem = TaskItem.Create(taskItemEntity.Id, taskItemEntity.Title, taskItemEntity.Description, taskItemEntity.Status, taskItemEntity.CreatedAt).taskItem;

        return taskItem;
    }

    public async Task<Guid> Update(Guid id, string title, string description, TaskStatus status)
    {
        await _db.TaskItems
        .Where(x => x.Id == id)
        .ExecuteUpdateAsync(x => x
            .SetProperty(x => x.Title, x => title)
            .SetProperty(x => x.Description, x => description)
            .SetProperty(x => x.Status, x => status)
            );

        return id;
    }

    public async Task<Guid> Delete(Guid id)
    {
        await _db.TaskItems
        .Where(x => x.Id == id)
        .ExecuteDeleteAsync();

        return id;
    }
}