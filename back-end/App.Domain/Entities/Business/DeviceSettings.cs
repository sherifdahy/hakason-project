using App.Domain.Entities.Base;
using App.Domain.Entities.Persons;

namespace App.Domain.Entities.Business;

public class DeviceSettings : AuditableEntity
{
    public int Id { get; set; }

    public int ChildId { get; set; }
    public virtual Child Child { get; set; } = default!;

    public int DailyLimitMinutes { get; set; } = 120;
    public bool IsPhoneLocked { get; set; } = false;
}
