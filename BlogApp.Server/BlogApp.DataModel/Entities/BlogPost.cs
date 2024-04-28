using BlogApp.DataModel.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.DataModel.Entities;

public sealed class BlogPost : IEntity, IHasAuditInfo
{
    public Guid Id { get; set; }
    
    public string Title { get; set; }
    
    public string Content { get; set; }
    
    public ICollection<Comment> Comments { get; set; }
    public User CreatedBy { get; set; }
    
    #region Audit fields

    public DateTimeOffset CreationDate { get; set; }
    public Guid CreatedById { get; set; }
    public DateTimeOffset? LastChangeDate { get; set; }
    public Guid? LastChangedById { get; set; }

    #endregion
    
    public void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BlogPost>(entity =>
        {
            entity.Property(x => x.Title).HasMaxLength(100);
            
            entity.Property(x => x.Content).HasMaxLength(5000);

            entity.HasOne(x => x.CreatedBy)
                .WithMany(x => x.BlogPosts)
                .HasForeignKey(x => x.CreatedById)
                .OnDelete(DeleteBehavior.NoAction);
        });
    }
}