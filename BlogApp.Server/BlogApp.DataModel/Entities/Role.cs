using BlogApp.DataModel.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.DataModel.Entities;

public sealed class Role : IdentityRole<Guid>, IEntity
{
    public void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");
        });
    }
}