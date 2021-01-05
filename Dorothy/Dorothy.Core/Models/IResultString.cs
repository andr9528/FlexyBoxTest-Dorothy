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
        string Value { get; set; }
        IResult Result { get; set; }
        int ResultId { get; set; }
    }
}
