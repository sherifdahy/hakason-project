using App.Domain.Entities.Base;
using App.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entities.Business;

public class Invitation : AuditableEntity
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    public virtual ApplicationUser Parent { get; set; } = default!;

    public string ChildEmail { get; set; } = string.Empty;
    public string ChildName { get; set; } = string.Empty;

    public string Token { get; set; } = string.Empty;
    public bool IsAccepted { get; set; } = false;
    public DateTime ExpiresAt { get; set; }
}
