using JWTProject.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTProject.DataAccess.Concrete.Mapping
{
    public class AppUserMap : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(i=>i.Id);
            builder.Property(i => i.Id).UseIdentityColumn();
            builder.Property(i => i.UserName).HasMaxLength(100).IsRequired();
            builder.HasIndex(i => i.UserName).IsUnique();
            builder.Property(i => i.Password).HasMaxLength(100).IsRequired();
            builder.HasMany(i => i.AppUserRoles).WithOne(i => i.AppUser).HasForeignKey(i => i.AppUserId).OnDelete(DeleteBehavior.Cascade);
            //Cascade kullanmamızda ki neden User silinirse ona ait rol de silinmelidir
        }
    }
}
