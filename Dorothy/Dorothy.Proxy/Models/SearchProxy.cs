using Dorothy.Core.Enums;
using Dorothy.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dorothy.Proxy.Models
{
    public class SearchProxy : ISearch
    {
        public string SearchTerm { get; set; }
        public int DesiredAmount { get; set; }
        public IEnumerable<IResult> Results { get; set; }
        [JsonIgnore]
        public int TermLenght => SearchTerm.Length;
        [JsonIgnore]
        public int TermLetters => SearchTerm.Count(char.IsLetter);
        [JsonIgnore]
        public int TermNumbers => SearchTerm.Count(char.IsDigit);
        [JsonIgnore]
        public int TermSymbols => SearchTerm.Count(char.IsSymbol);
        [JsonIgnore]
        public int TermSpaces => SearchTerm.Count(char.IsWhiteSpace);
        public int Id { get; set; }
        public byte[] Version { get; set; }
        public SearchTarget Target { get; set; }


        /// <summary>
        /// Used to tell any code generating a version of this class from Json or similar, which implementation of IResult to use.
        /// </summary>
        /// <param name="results"></param>
        [JsonConstructor]
        public SearchProxy(List<ResultProxy> results)
        {
            Results = results;
        }

        public SearchProxy()
        {

        }
    }
}
