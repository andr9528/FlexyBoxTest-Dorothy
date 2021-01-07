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
        public string Term { get; set; }
        public int DesiredAmount { get; set; }
        public IEnumerable<IResult> Results { get; set; }
        [JsonIgnore]
        public int TermLenght => Term.Length;
        [JsonIgnore]
        public int TermLetters => Term.Count(char.IsLetter);
        [JsonIgnore]
        public int TermNumbers => Term.Count(char.IsDigit);
        [JsonIgnore]
        public int TermSymbols => Term.Count(char.IsSymbol);
        [JsonIgnore]
        public int TermSpaces => Term.Count(char.IsWhiteSpace);
        public int Id { get; set; }
        public byte[] Version { get; set; }
        public SearchTarget Target { get; set; }
        public string Path { get; set; }
        public bool? IncludeSubFolders { get; set; }
        public DateTime ExecutedAt { get; set; }


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
