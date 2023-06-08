using Application.Interfaces.DbContext;
using Domain.Common;
using Domain.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Persistence.DbContexts;

public class UniconDbContext : DbContext, IUniconDbContext
{
    private readonly IDomainEventDispatcher _dispatcher;

    public UniconDbContext(DbContextOptions options,
        IDomainEventDispatcher dispatcher) : base(options)
    {
        _dispatcher = dispatcher;
    }

    public DbSet<Discipline> Disciplines { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        if (_dispatcher == null)
        {
            return result;
        }

        var entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .ToArray();

        await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

        return result;
    }
}
