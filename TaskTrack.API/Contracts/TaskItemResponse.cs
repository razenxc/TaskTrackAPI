public record TaskItemResponse(
    Guid id,
    string title,
    string description,
    TaskStatus status,
    DateTime createdAt
);