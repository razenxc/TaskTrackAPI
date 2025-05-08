using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly ITaskItemService _taskItemService;
    public TasksController(ITaskItemService taskService)
    {
        _taskItemService = taskService;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] TaskItemRequest request)
    {
        (TaskItem taskItem, string error) = TaskItem.Create(Guid.NewGuid(), request.title, request.description, request.status, DateTime.UtcNow);

        if(!string.IsNullOrEmpty(error))
        {
            BadRequest(error);
        }

        return Ok(await _taskItemService.Create(taskItem));
    }

    [HttpGet]
    public async Task<ActionResult<List<TaskItem>>> GetAll()
    {
        List<TaskItem> tasks = await _taskItemService.Get();

        List<TaskItemResponse> response = tasks.Select(x => new TaskItemResponse(x.Id, x.Title, x.Description, x.Status, x.CreatedAt)).ToList();

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TaskItemResponse>> GetById([FromRoute] Guid id)
    {
        TaskItem task = await _taskItemService.Get(id);

        TaskItemResponse response = new TaskItemResponse(task.Id, task.Title, task.Description, task.Status, task.CreatedAt);
        
        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Guid>> Update([FromRoute] Guid id, [FromBody] TaskItemRequest request)
    {
        return Ok(await _taskItemService.Update(id, request.title, request.description, request.status));
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> Delete([FromRoute] Guid id)
    {
        return Ok(await _taskItemService.Delete(id));
    }
}