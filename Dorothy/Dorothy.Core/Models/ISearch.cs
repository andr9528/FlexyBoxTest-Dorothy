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
    public interface ISearch : IEntity
    {
        string Term { get; set; }
        int DesiredAmount { get; set; }
        ICollection<IResult> Results { get; set; }
        SearchTarget Target { get; set; }
        int TermLenght { get; }
        int TermLetters { get; }
        int TermNumbers { get; }
        int TermSymbols { get; }
        int TermSpaces { get; }
        /// <summary>
        /// Is empty if the Target is 'Web'.
        /// </summary>
        string Path { get; set; }
        /// <summary>
        /// Is null if the Target is 'Web', otherwise it should be true or false, depending on of the search should look in folders stored at the specified path.
        /// </summary>
        bool? IncludeSubFolders { get; set; }
        /// <summary>
        /// Describes When this search was executed.
        /// </summary>
        DateTime ExecutedAt { get; set; }
    }
}
