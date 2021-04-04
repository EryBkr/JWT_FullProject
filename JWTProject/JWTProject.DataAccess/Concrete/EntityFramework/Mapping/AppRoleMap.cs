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
    public class AppRoleMap : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).UseIdentityColumn();
            builder.Property(i => i.Name).HasMaxLength(100).IsRequired();
            builder.HasIndex(i => i.Name).IsUnique();
            builder.HasMany(i => i.AppUserRoles).WithOne(i => i.AppRole).HasForeignKey(i => i.AppRoleId).OnDelete(DeleteBehavior.Cascade);
            //Cascade kullanmamızda ki neden Role silinirse ona ait user larda silinmelidir
        }
    }
}
