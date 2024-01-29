@echo off
set /p name="Name: "
dotnet ef migrations add %name% --configuration Release --context DataContext --project "./NotesWithTags.Adapters.Data.csproj"