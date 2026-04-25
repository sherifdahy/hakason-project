using App.Domain.Entities.Base;
using App.Domain.Entities.Profiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entities.Business;

public class DeviceSettings : AuditableEntity
{
    public int Id { get; set; }

    public int ChildId { get; set; }
    public virtual ChildProfile Child { get; set; } = default!;

    public int DailyLimitMinutes { get; set; } = 120;
    public bool IsPhoneLocked { get; set; } = false;
}
