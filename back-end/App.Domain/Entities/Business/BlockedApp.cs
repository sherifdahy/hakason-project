
using App.Domain.Entities.Persons;

namespace App.Domain.Entities.Business;

public class BlockedApp
{
    public int Id { get; set; }

    public int ChildId { get; set; }
    public virtual Child Child { get; set; } = default!;

    public string AppName { get; set; } = string.Empty;
    public DateTime BlockedAt { get; set; } = DateTime.UtcNow;
}
