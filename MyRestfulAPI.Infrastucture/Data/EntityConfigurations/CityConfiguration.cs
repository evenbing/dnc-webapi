using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRestfulAPI.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyRestfulAPI.Infrastucture.Data.EntityConfigurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(100);

            builder.HasOne(x => x.Country).WithMany(x => x.Cities)
                                         .HasForeignKey(x => x.CountryId)
                                         .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
