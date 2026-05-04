using App.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entities.Persons;

public class Child
{
    public int UserId { get; set; }
    public int ParentId { get; set; }
    public int Age { get; set; }
    public int TotalPoints { get; set; } = 0;

    public virtual Parent Parent { get; set; } = default!;
    public virtual ApplicationUser User { get; set; } = default!;
}
