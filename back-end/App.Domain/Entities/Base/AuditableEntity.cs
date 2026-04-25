using App.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entities.Base;

public abstract class AuditableEntity
{
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }
    public DateTime? UpdateAt { get; set; }
}
