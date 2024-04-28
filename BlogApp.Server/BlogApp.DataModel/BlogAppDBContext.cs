using System.Reflection;
using BlogApp.DataModel.Entities;
using BlogApp.DataModel.Helpers;
using BlogApp.DataModel.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.DataModel;

public sealed class BlogAppDbContext : IdentityDbContext<User, Role, Guid>
{
    private readonly IUserInfoProvider _userInfoProvider;

    public BlogAppDbContext(
        DbContextOptions<BlogAppDbContext> options,
        IUserInfoProvider userInfoProvider)
        : base(options)
    {
        _userInfoProvider = userInfoProvider;
    }
    
    public DbSet<BlogPost> BlogPosts => Set<BlogPost>();
    public DbSet<Comment> Comments => Set<Comment>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var entityTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(r => r.IsClass && typeof(IEntity).IsAssignableFrom(r));

        foreach (var entityType in entityTypes)
        {
            var entity = (IEntity?)Activator.CreateInstance(entityType);

            entity?.Configure(modelBuilder);
        }

        modelBuilder.Entity<IdentityUserClaim<Guid>>()
            .ToTable("UserClaim");
        
        modelBuilder.Entity<IdentityUserRole<Guid>>()
            .ToTable("UserRole");

        modelBuilder.Entity<IdentityRoleClaim<Guid>>()
            .ToTable("RoleClaim");

        modelBuilder.Entity<IdentityUserToken<Guid>>()
            .ToTable("UserToken");

        modelBuilder.Entity<IdentityUserLogin<Guid>>()
            .ToTable("UserLogin");
    }

    public override int SaveChanges()
    {
        ApplyAuditRules();

        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyAuditRules();

        return base.SaveChangesAsync(cancellationToken);
    }

    public int SaveChangesWithoutAudit()
        => base.SaveChanges();

    public Task<int> SaveChangesWithoutAuditAsync(CancellationToken cancellationToken = default)
        => base.SaveChangesAsync(cancellationToken);

    private void ApplyAuditRules()
    {
        Guid currentUserId = _userInfoProvider.UserId;

        if (currentUserId == Guid.Empty)
            return;

        var entries = ChangeTracker.Entries();

        foreach (var entry in entries.Where(e =>
                     e.Entity is not null
                     && e.Entity is IHasAuditInfo
                     && (e.State == EntityState.Added
                         || e.State == EntityState.Modified)))
        {
            var now = DateTimeOffset.UtcNow;
            var entity = (IHasAuditInfo)entry.Entity;

            if (entry.State == EntityState.Added)
            {
                entity.CreationDate = now;

                if (entity.CreatedById == Guid.Empty)
                    entity.CreatedById = currentUserId;
            }

            entity.LastChangeDate = now;

            if (entity.LastChangedById == Guid.Empty)
                entity.LastChangedById = currentUserId;
        }
    }
}