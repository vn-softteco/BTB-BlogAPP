using BlogApp.DataModel.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.DataModel.Entities;

public sealed class Comment : IEntity, IHasAuditInfo
{
    public Guid Id { get; set; }
    
    public string Text { get; set; }
    public Guid BlogPostId { get; set; }
    
    public BlogPost BlogPost { get; set; }
    public User CreatedBy { get; set; }
    
    #region Audit fields

    public DateTimeOffset CreationDate { get; set; }
    public Guid CreatedById { get; set; }
    public DateTimeOffset? LastChangeDate { get; set; }
    public Guid? LastChangedById { get; set; }

    #endregion
    
    public void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.Property(x => x.Text).HasMaxLength(1000);
            
            entity.HasOne(x => x.BlogPost)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.BlogPostId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(x => x.CreatedBy)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.CreatedById)
                .OnDelete(DeleteBehavior.NoAction);
        });
    }
}