using Dorothy.Proxy.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Wolf.Utility.Core.Persistence.EntityFramework.Core;

namespace Dorothy.Proxy.EntityFrameworkCore.Config
{
    public class ResultProxyConfig : EntityConfig<ResultProxy>
    {
        public override void Configure(EntityTypeBuilder<ResultProxy> builder)
        {
            base.Configure(builder);

            builder.HasMany(x => (List<ResultStringProxy>)x.Results).WithOne(x => (ResultProxy) x.Result).HasForeignKey(x=>x.ResultId);
        }
    }
}
