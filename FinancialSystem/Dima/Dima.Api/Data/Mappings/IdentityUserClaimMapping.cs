using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings
{
    public class IdentityUserClaimMapping : IEntityTypeConfiguration<IdentityUserClaim<long>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserClaim<long>> builder)
        {
            builder.ToTable("IdentityClaim");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ClaimType);
            builder.Property(x => x.ClaimValue);
        }
    }
}
