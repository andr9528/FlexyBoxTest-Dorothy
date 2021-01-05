using Dorothy.Proxy.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Wolf.Utility.Core.Persistence.EntityFramework.Core;

namespace Dorothy.Proxy.EntityFrameworkCore.Config
{
    public class SearchProxyConfig : EntityConfig<SearchProxy>
    {
        public override void Configure(EntityTypeBuilder<SearchProxy> builder)
        {
            base.Configure(builder);

            builder.Ignore(x => x.TermLenght);
            builder.Ignore(x => x.TermLetters);
            builder.Ignore(x => x.TermNumbers);
            builder.Ignore(x => x.TermSymbols);
            builder.Ignore(x => x.TermSpaces);

            builder.HasMany(x => (List<ResultProxy>) x.Results).WithOne(x => (SearchProxy) x.Search).HasForeignKey(x=> x.SearchId);
        }
    }
}
