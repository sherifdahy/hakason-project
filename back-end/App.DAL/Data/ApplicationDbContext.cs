using App.Domain.Entities.Business;
using App.Domain.Entities.Identity;
using App.Domain.Entities.Persons;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace App.DAL.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,int>
{
    public ApplicationDbContext(DbContextOptions dbContext) : base(dbContext)
    {
        
    }

    public virtual DbSet<Parent> Parents { get; set; }
    public virtual DbSet<Child> Children { get; set; }
    public virtual DbSet<Invitation> Invitations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys())
            .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
            .ToList()
            .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
    }
}
