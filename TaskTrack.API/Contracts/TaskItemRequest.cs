public record TaskItemRequest(
    string title,
    string description,
    TaskStatus status 
);