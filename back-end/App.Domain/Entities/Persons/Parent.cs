using App.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entities.Persons;

public class Parent
{
    public int UserId { get; set; }
    public virtual ApplicationUser User { get; set; } = default!;
    public virtual ICollection<Child> Children { get; set; } = new HashSet<Child>();
}
