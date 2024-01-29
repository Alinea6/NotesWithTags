# Setup steps:
* setup local database in postgres
* fill in user secret values for NotesWithTags.API and NotesWithTags.Tests.Integration (ConnectionStrings:DataContext and JSONWebTokensSettings:Key)
```json
{
  "ConnectionStrings": {
    "DataContext": "Host=localhost;Database=Notes;Username=postgres;Password=password"
  },
  "JSONWebTokensSettings": {
    "Key": "81234CFB77034ECCDDD547F5FF4F2EFC"
  }
}
```
* run ef-update-database.bat file (for windows) or run command from `NotesWithTags.Adapters.Data` directory: 
```bash
dotnet ef database update --configuration Release --context DataContext --project "./NotesWithTags.Adapters.Data.csproj" --startup-project "../NotesWithTags.API/NotesWithTags.API.csproj"
```

* run API project and open http://localhost:5003/swagger/index.html

# Logic for note tags:
* "EMAIL" - e.g. email@example.com
* "PHONE" - templates: +48123456789 OR 123456789