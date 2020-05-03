using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Infrastructure.Data.Configurations
{
    internal static class EntityTypeBuilderExtensions
    {
        public static PropertyBuilder<string> IsNickname(this PropertyBuilder<string> property) 
            => property.IsRequired().HasMaxLength(12);

        public static PropertyBuilder<string> IsStock(this PropertyBuilder<string> property)
            => property.IsRequired().HasMaxLength(20);
    }
}
