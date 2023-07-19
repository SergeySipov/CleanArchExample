using Application.Interfaces.DbContext;
using Domain.Common;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Persistence.DbContexts;

public class UniconDbContext : DbContext, IUniconDbContext
{
    private readonly IMediator _mediator;

    public UniconDbContext(DbContextOptions options, 
        IMediator mediator) : base(options)
    {
        _mediator = mediator;
    }

    public DbSet<Discipline> Disciplines { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        await DispatchDomainEventsAsync(_mediator);

        return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    private async Task DispatchDomainEventsAsync(IMediator mediator)
    {
        var entities = ChangeTracker
            .Entries<BaseEntity>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity);

        var domainEvents = entities
            .SelectMany(e => e.DomainEvents)
            .ToList();

        entities.ToList().ForEach(e => e.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}
