using Dorothy.Proxy.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Wolf.Utility.Core.Persistence.EntityFramework.Core;

namespace Dorothy.Proxy.EntityFrameworkCore.Config
{
    public class ResultStringProxyConfig : EntityConfig<ResultStringProxy>
    {
        public override void Configure(EntityTypeBuilder<ResultStringProxy> builder)
        {
            base.Configure(builder);
        }
    }
}
