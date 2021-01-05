using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dorothy.Core.Controllers;
using Dorothy.Core.Models;
using Dorothy.WebSearch.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dorothy.WebSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase, ISearchController<Result, Search>
    {
        public Task<ActionResult<Result>> GetResults([FromQuery] Search search)
        {
            throw new NotImplementedException();
        }
    }
}
