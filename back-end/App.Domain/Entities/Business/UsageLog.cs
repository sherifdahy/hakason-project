using App.Domain.Entities.Profiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entities.Business;

public class UsageLog
{
    public int Id { get; set; }

    public int ChildId { get; set; }
    public virtual ChildProfile Child { get; set; } = default!;

    public string AppName { get; set; } = string.Empty;
    public string PackageName { get; set; } = string.Empty;
    public int DurationMinutes { get; set; }
    public DateTime UsageDate { get; set; }
}
