using App.Domain.Entities.Base;
using App.Domain.Entities.Business;
using App.Domain.Entities.Identity;

namespace App.Domain.Entities.Relations;

public class ChildQuestionOption : AuditableEntity
{
    public int ChildId { get; set; }
    public virtual ApplicationUser Child { get; set; } = default!;

    public int QuestionId { get; set; }
    public virtual Question Question { get; set; } = default!;

    public int OptionId { get; set; }
    public virtual Option Option { get; set; } = default!;
}
