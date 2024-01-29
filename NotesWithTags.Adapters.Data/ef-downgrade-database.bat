@echo off
set /p migrationId="Migration to downgrade towards: "
dotnet ef database update %migrationId% --configuration Release --context DataContext --project "./NotesWithTags.Adapters.Data.csproj" --startup-project "../NotesWithTags.API/NotesWithTags.API.csproj"