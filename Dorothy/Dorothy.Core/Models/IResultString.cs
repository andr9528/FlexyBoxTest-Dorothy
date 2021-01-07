using System;
using System.Collections.Generic;
using System.Text;
using Wolf.Utility.Core.Persistence.Core;

namespace Dorothy.Core.Models
{
    /// <summary>
    /// It inherits from IEntity, to support my Entity Framework Core implementation.
    /// </summary>
    public interface IResultString : IEntity
    {
        /// <summary>
        /// Used for Web Results
        /// </summary>
        string Title { get; set; }
        /// <summary>
        /// Used for Web Results
        /// </summary>
        string Link { get; set; }
        /// <summary>
        /// Used for Web Results
        /// </summary>
        string Snippet { get; set; }
        IResult Result { get; set; }
        int ResultId { get; set; }
        /// <summary>
        /// Used to store the path of a file or folder, for results of those types
        /// </summary>
        string Path { get; set; }
    }
}
