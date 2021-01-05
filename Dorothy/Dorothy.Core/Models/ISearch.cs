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
        string SearchTerm { get; set; }
        int DesiredAmount { get; set; }
        IEnumerable<IResult> Results { get; set; }
        SearchTarget Target { get; set; }
        int TermLenght { get; }
        int TermLetters { get; }
        int TermNumbers { get; }
        int TermSymbols { get; }
        int TermSpaces { get; }
    }
}
