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
        List<IResultString> Results { get; set; }
        /// <summary>
        /// Is the Total amount of result on the Web. 
        /// For folder/files this is the total amount of folder/files found on the specified path, with or without the subdirectories, depending on the search performed
        /// </summary>
        int RetrievedResults { get; }
        int ToltalResults { get; set; }
        ISearch Search { get; set; }
        int SearchId { get; set; }
    }
}
