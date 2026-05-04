using App.Domain.Entities.Persons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace App.DAL.Configuration;

public class ChildConfig : IEntityTypeConfiguration<Child>
{
    public void Configure(EntityTypeBuilder<Child> builder)
    {
        builder.HasKey(x => x.UserId);

        builder.Property(x => x.UserId)
            .ValueGeneratedNever();
    }
}
