using App.Domain.Entities.Base;
using App.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entities.Business;

public class Test : AuditableEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public TestType Type { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new HashSet<Question>();
}

