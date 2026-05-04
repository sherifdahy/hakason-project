using App.Domain.Abstraction.Consts;
using App.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Configuration;

public class ApplicationRoleConfig : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.HasData(new ApplicationRole()
        {
            Id = DefaultRoles.Admin.Id,
            Name = DefaultRoles.Admin.Name,
            NormalizedName = DefaultRoles.Admin.Name.ToUpper(),
            ConcurrencyStamp = DefaultRoles.Admin.ConcurrencyStamp,
        },
        new ApplicationRole()
        {
            Id = DefaultRoles.Parent.Id,
            Name = DefaultRoles.Parent.Name,
            NormalizedName = DefaultRoles.Parent.Name.ToUpper(),
            ConcurrencyStamp = DefaultRoles.Parent.ConcurrencyStamp
        },
        new ApplicationRole()
        {
            Id = DefaultRoles.Child.Id,
            Name = DefaultRoles.Child.Name,
            NormalizedName = DefaultRoles.Child.Name.ToUpper(),
            ConcurrencyStamp = DefaultRoles.Child.ConcurrencyStamp
        });
    }
}
