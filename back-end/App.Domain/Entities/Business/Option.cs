using App.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entities.Business;

public class Option
{
    public int Id { get; set; }
    public int QuestionId { get; set; }

    public string OptionText { get; set; } = string.Empty;
    public int Score { get; set; } = 0;
    public bool IsCorrect { get; set; } = false;

    public virtual Question Question { get; set; } = default!;
}