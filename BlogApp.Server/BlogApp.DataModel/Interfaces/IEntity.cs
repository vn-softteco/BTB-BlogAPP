using Microsoft.EntityFrameworkCore;

namespace BlogApp.DataModel.Interfaces;

internal interface IEntity
{
    void Configure(ModelBuilder modelBuilder);
}