# TaskTrackApi

## How to run the app
- `sudo docker run --name TaskTrackDB -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=postgres -e POSTGRES_DB=TaskTrackDB -p 5432:5432 -d postgres`
- `dotnet run`

### Create migrations
- `dotnet ef migrations add MigrationName -s TaskTrack.API -p TaskTrack.Infrastructure`
### Apply migrations to the database
- `dotnet ef database update  -s TaskTrack.API -p TaskTrack.Infrastructure`

## To-Do
- Add `bool isValid` to Jwt Refresh tokens
- Errors handling/logging
- Email/password validation
- Task<->User model update