using App.Domain.Entities.Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.DAL.Configuration;

public class InvitationConfig : IEntityTypeConfiguration<Invitation>
{
    public void Configure(EntityTypeBuilder<Invitation> builder)
    {
        builder.HasIndex(x => new { x.ChildEmail, x.IsAccepted });
        builder.HasIndex(x => x.ParentId);
    }
}
