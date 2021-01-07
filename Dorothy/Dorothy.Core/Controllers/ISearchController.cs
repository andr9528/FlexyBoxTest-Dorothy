using Dorothy.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dorothy.Core.Controllers
{
    /// <summary>
    /// The typed inputs exists to allow implementers to specify thier implementation of the specified interfaces.
    /// Prevent many cases where code complains about not being able to initialise an instance of an interface
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TSearch"></typeparam>
    public interface ISearchController<TResult, TSearch> where TResult : class, IResult where TSearch : class, ISearch
    {
        /// <summary>
        /// Api takes in the search parameter from the query.
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        Task<ActionResult<List<TResult>>> GetResults(TSearch search);
    }
}
