# TaskTrackApi

## How to run the app
- `docker run --name TaskTrackDB -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=postgres -e POSTGRES_DB=TaskTrackDB -p 5432:5432 -d postgres`
- `dotnet run`

## To-Do
- Errors handling (Repository>Service>Controller)