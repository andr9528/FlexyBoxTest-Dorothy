using Dorothy.Core.Enums;
using Dorothy.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dorothy.Proxy.Models
{
    public class ResultProxy : IResult
    {
        public ResultType ResultType { get; set; }
        public List<IResultString> Results { get; set; }
        public int ToltalResults { get; set; }
        public ISearch Search { get; set; }
        public int SearchId { get; set; }
        public int Id { get; set; }
        public byte[] Version { get; set; }

        [JsonIgnore]
        public int RetrievedResults => Results != null ? Results.Count : 0;

        /// <summary>
        /// Used to tell any code generating a version of this class from Json or similar, which implementation of IResultString and ISearch to use.
        /// </summary>
        /// <param name="search"></param>
        /// <param name="results"></param>
        [JsonConstructor]
        public ResultProxy(SearchProxy search, List<ResultStringProxy> results)
        {
            Search = search;
            Results = new List<IResultString>(results);
        }

        public ResultProxy()
        {

        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append($"Result Type = {ResultType}; ");
            builder.Append($"Toltal Results = {ToltalResults}; ");

            return builder.ToString();
        }
    }
}
