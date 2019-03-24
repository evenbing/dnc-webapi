using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRestfulAPI.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyRestfulAPI.Infrastucture.Data.EntityConfigurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.Property(x => x.EnglishName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.ChineseName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Abbreviation).HasMaxLength(5);
        }
    }
}
