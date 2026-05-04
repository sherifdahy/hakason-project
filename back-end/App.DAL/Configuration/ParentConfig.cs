using App.Domain.Entities.Persons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.DAL.Configuration;

public class ParentConfig : IEntityTypeConfiguration<Parent>
{
    public void Configure(EntityTypeBuilder<Parent> builder)
    {
        builder.HasKey(x => x.UserId);

        builder.Property(x => x.UserId)
            .ValueGeneratedNever();

    }
}
