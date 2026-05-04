using App.Domain.Entities.Persons;
using App.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entities.Identity;

public class ApplicationUser : IdentityUser<int>
{
    public string FullName { get; set; } = string.Empty;
    public bool IsDisabled { get; set; }

    public virtual Parent Parent { get; set; } = default!;
    public virtual Child Child { get; set; } = default!;
}
