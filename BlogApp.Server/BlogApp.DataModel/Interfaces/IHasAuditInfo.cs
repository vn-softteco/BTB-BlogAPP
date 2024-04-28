namespace BlogApp.DataModel.Interfaces;

internal interface IHasAuditInfo
{
    DateTimeOffset CreationDate { get; set; }
    Guid CreatedById { get; set; }
    DateTimeOffset? LastChangeDate { get; set; }
    Guid? LastChangedById { get; set; }
}
