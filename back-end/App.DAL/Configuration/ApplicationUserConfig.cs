using App.Domain.Abstraction.Consts;
using App.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Configuration;

public class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasData(new ApplicationUser()
        {
            Id = DefaultUsers.Admin.Id,
            UserName = DefaultUsers.Admin.Email,
            Email = DefaultUsers.Admin.Email,
            NormalizedEmail = DefaultUsers.Admin.Email.ToUpper(),
            NormalizedUserName = DefaultUsers.Admin.Email.ToUpper(),
            ConcurrencyStamp = DefaultUsers.Admin.ConcurrencyStamp,
            SecurityStamp = DefaultUsers.Admin.SecurityStamp,
            EmailConfirmed = true,
            PasswordHash = DefaultUsers.Admin.Password
        });
    }
}
