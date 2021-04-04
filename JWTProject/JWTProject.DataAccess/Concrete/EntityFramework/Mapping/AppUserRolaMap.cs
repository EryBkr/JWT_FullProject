using JWTProject.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTProject.DataAccess.Concrete.EntityFramework.Mapping
{
    public class AppUserRoleMap : IEntityTypeConfiguration<AppUserRole>
    {
        public void Configure(EntityTypeBuilder<AppUserRole> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).UseIdentityColumn();
            builder.HasIndex(i => new { i.AppUserId, i.AppRoleId }).IsUnique(); //Çoka çok ilişki için bu iki FK nın kombinasyonunun Unique olması gerekir
        }
    }
}
