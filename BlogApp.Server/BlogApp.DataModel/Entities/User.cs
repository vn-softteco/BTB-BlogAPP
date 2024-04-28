using BlogApp.DataModel.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.DataModel.Entities;

public sealed class User : IdentityUser<Guid>, IEntity
{
    public string FullName => $"{FirstName} {LastName}".Trim();
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public ICollection<BlogPost> BlogPosts { get; set; }
    public ICollection<Comment> Comments { get; set; }
    
    public void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");
        });
    }
}