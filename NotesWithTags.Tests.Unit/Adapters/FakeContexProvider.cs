using Microsoft.EntityFrameworkCore;
using NotesWithTags.Adapters.Data;

namespace NotesWithTags.Tests.Unit.Adapters;

public static class FakeContexProvider
{
    public static DataContext GetFakeDbContext()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase($"FakeDatabase_{Guid.NewGuid()}")
            .Options;
        var context = new DataContext(options);

        context.SaveChangesAsync();

        return context;
    }
}