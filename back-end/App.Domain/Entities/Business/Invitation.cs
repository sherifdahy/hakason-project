using App.Domain.Entities.Base;
using App.Domain.Entities.Identity;
using App.Domain.Entities.Persons;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entities.Business;

public class Invitation : AuditableEntity
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    public virtual Parent Parent { get; set; } = default!;

    public string ChildEmail { get; set; } = string.Empty;
    public string ChildName { get; set; } = string.Empty;

    public string HashedCode { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }

    public bool IsAccepted { get; set; } = false;
    public int Attempts { get; set; } = 0;

    public bool IsExpired => DateTime.UtcNow > ExpiresAt;
    public bool IsValid => !IsAccepted && !IsExpired && Attempts < 5;
}
