using Dorothy.Core.Enums;
using Dorothy.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dorothy.WebSearch.Models
{
    public class Result : IResult
    {
        public ResultType ResultType { get; set; }
        public ICollection<IResultString> Results { get; set; }
        public int ToltalResults { get; set; }
        public ISearch Search { get; set; }
        public int SearchId { get; set; }
        public int Id { get; set; }
        public byte[] Version { get; set; }
        [JsonIgnore]
        public int RetrievedResults => Results.Count;

        /// <summary>
        /// Used to tell any code generating a version of this class from Json, which implementation of IResultString and ISearch to use.
        /// </summary>
        /// <param name="search"></param>
        /// <param name="results"></param>
        [JsonConstructor]
        public Result(Search search, List<ResultString> results)
        {
            Search = search;
            Results = new List<IResultString>(results);
        }

        public Result()
        {

        }
    }
}
