using System.Reflection;
using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Domain.Entities;
using CoduTeam.Infrastructure.Data.Configurations;
using CoduTeam.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoduTeam.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser, UserRole, int>(options), IApplicationDbContext
{
    public DbSet<TodoList> TodoLists => Set<TodoList>();

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Position> Positions => Set<Position>();
    public DbSet<PositionApply> PositionApplies => Set<PositionApply>();
    public DbSet<UserProject> UserProjects => Set<UserProject>();
    public DbSet<Message> Messages => Set<Message>();
    public DbSet<Chat> Chats => Set<Chat>();
    public DbSet<UserChat> UserChats => Set<UserChat>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.ApplyEnumToStringConversion();

        base.OnModelCreating(builder);
    }
}
