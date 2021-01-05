using System;
using System.Collections.Generic;
using System.Text;
using Wolf.Utility.Core.Web;
using Dorothy.Core;
using Dorothy.Core.Controllers;
using Dorothy.Proxy.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wolf.Utility.Core.Persistence.EntityFramework.Core;

namespace Dorothy.Proxy.Controllers
{
    public class SearchControllerProxy : ControllerProxy, ISearchController<ResultProxy, SearchProxy>
    {
        public SearchControllerProxy(IHandler handler) : base($"localhost", Core.Enums.Controllers.Search.ToString(), handler)
        {

        }

        public Task<ActionResult<ResultProxy>> GetResults(SearchProxy search)
        {
            throw new NotImplementedException();
        }
    }
}
