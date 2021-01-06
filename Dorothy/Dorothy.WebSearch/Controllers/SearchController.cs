using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dorothy.Core.Controllers;
using Dorothy.Core.Models;
using Dorothy.WebSearch.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Dorothy.WebSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase, ISearchController<Result, Search>
    {
        private ILogger<SearchController> Logger { get; }
        public SearchController(ILogger<SearchController> logger)
        {
            Logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<Result>> GetResults([FromQuery] Search search)
        {
            try
            {
                var proxy = new GoogleProxy();

                var amount = search.DesiredAmount;
                if (search.DesiredAmount == default)
                    amount = 10;

                var response = await proxy.Search(search.SearchTerm, amount);

                var result = new Result() { Results = response.Item1, Search = search, ResultType = Core.Enums.ResultType.Web, ToltalResults = response.Item2 };
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.LogCritical(e, "Something went really bad...");
                return UnprocessableEntity();
            }
        }
    }
}
