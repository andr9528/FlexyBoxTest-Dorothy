using System;
using System.Collections.Generic;
using System.Text;

namespace Dorothy.Core.Enums
{
    public enum ResultType
    {
        // For result that hasn't had its Type set. This should never be the case. 
        // With this as a default, the type can however be used to search with in the Entity Framework Handler
        Null, 
        Files, 
        Folder, 
        Web
    }
}
