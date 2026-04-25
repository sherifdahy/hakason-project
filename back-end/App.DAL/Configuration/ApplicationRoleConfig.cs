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
            Name = DefaultRoles.Admin.name,
            NormalizedName = DefaultRoles.Admin.name.ToUpper(),
            ConcurrencyStamp = DefaultRoles.Admin.ConcurrencyStamp,
        },
        new ApplicationRole()
        {
            Id = DefaultRoles.Client.Id,
            Name = DefaultRoles.Client.name,
            NormalizedName = DefaultRoles.Client.name.ToUpper(),
            ConcurrencyStamp = DefaultRoles.Client.ConcurrencyStamp
        });
    }
}
