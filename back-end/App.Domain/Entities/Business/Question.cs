using App.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entities.Business;

public class Question : AuditableEntity
{
    public int Id { get; set; }
    public int TestId { get; set; }

    public string QuestionText { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }

    public virtual Test Test { get; set; } = default!;
    public virtual ICollection<Option> Options { get; set; } = new HashSet<Option>();
}
