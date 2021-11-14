using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PackIT.Infrastructure.EF.Models;

namespace PackIT.Infrastructure.EF.Config
{
    internal sealed class ReadConfiguration : IEntityTypeConfiguration<PackingListReadModel>, IEntityTypeConfiguration<PackingItemReadModel>
    {
        public void Configure(EntityTypeBuilder<PackingListReadModel> builder)
        {
            builder.ToTable("PackingLists");
            builder.HasKey(pl => pl.Id);

            builder
                .Property(pl => pl.Localization)
                .HasConversion(l => l.ToString(), l => LocalizationReadModel.Create(l));

            builder
                .HasMany(pl => pl.Items)
                .WithOne(pi => pi.PackingList);
        }

        public void Configure(EntityTypeBuilder<PackingItemReadModel> builder)
        {
            builder.ToTable("PackingItems");
        }
    }
}