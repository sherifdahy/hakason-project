using App.Domain.Entities.Base;
using App.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entities.Profiles;

public class ChildProfile : AuditableEntity
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public virtual ApplicationUser User { get; set; } = default!;
    public int ParentId { get; set; }
    public virtual ApplicationUser Parent { get; set; } = default!;

    public int Age { get; set; }
    public int TotalPoints { get; set; } = 0;
}
