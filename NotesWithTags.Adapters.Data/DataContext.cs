using Microsoft.EntityFrameworkCore;
using NotesWithTags.Adapters.Data.Models;

namespace NotesWithTags.Adapters.Data;

public class DataContext : DbContext
{
    public DbSet<Note> Notes { get; set; }
    public DbSet<User> Users { get; set; }
    
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureNotes(modelBuilder);
        ConfigureUsers(modelBuilder);
        
        base.OnModelCreating(modelBuilder);
    }

    private void ConfigureNotes(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Note>()
            .HasIndex(n => n.Id)
            .IsUnique();

        modelBuilder.Entity<Note>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.UserId);
    }

    private void ConfigureUsers(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Id)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Login)
            .IsUnique();
    }
}