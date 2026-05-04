using App.Domain.Entities.Persons;

namespace App.Domain.Entities.Business;

public class UsageLog
{
    public int Id { get; set; }

    public int ChildId { get; set; }
    public virtual Child Child { get; set; } = default!;

    public string AppName { get; set; } = string.Empty;
    public string PackageName { get; set; } = string.Empty;
    public int DurationMinutes { get; set; }
    public DateTime UsageDate { get; set; }
}
