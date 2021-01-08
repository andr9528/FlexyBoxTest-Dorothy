using Dorothy.Core.Enums;
using Dorothy.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dorothy.WebSearch.Models
{
    public class Search : ISearch
    {
        public string Term { get; set; }
        public int DesiredAmount { get; set; }
        public ICollection<IResult> Results { get; set; }
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
        /// Used to tell any code generating a version of this class from Json, which implementation of IResult to use.
        /// </summary>
        /// <param name="results"></param>
        [JsonConstructor]
        public Search(List<Result> results)
        {
            Results = new List<IResult>(results);
        }

        public Search()
        {

        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append($"Search Term = {Term}; ");
            builder.Append($"Search Target = {Core.Enums.Target.GetSearchTarget(Target)}; ");
            //if (DesiredAmount != default)
            //    builder.Append($"Desired Web Results = {DesiredAmount}; ");
            //if (Target != SearchTarget.Web)
            //{
            //    builder.Append($"Search Path = {Path}; ");
            //    var include = IncludeSubFolders == true ? "Yes" : "No"; 
            //    builder.Append($"Include Subfolders = {include}; ");
            //}

            return builder.ToString();
        }
    }
}
