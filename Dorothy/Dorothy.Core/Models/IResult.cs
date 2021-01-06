using Dorothy.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Wolf.Utility.Core.Persistence.Core;

namespace Dorothy.Core.Models
{
    /// <summary>
    /// It inherits from IEntity, to support my Entity Framework Core implementation.
    /// </summary>
    public interface IResult : IEntity
    {
        ResultType ResultType { get; set; }
        IEnumerable<IResultString> Results { get; set; }
        int RetrievedResults { get; }
        int ToltalResults { get; set; }
        ISearch Search { get; set; }
        int SearchId { get; set; }
    }
}
