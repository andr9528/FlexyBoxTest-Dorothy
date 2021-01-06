using Dorothy.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dorothy.WebSearch.Models
{
    public class ResultString : IResultString
    {
        public string Title { get; set; }
        public IResult Result { get; set; }
        public int ResultId { get; set; }
        public int Id { get; set; }
        public byte[] Version { get; set; }
        public string Link { get; set; }
        public string Snippet { get; set; }

        /// <summary>
        /// Used to tell any code generating a version of this class from Json, which implementation of IResult to use.
        /// </summary>
        /// <param name="result"></param>
        [JsonConstructor]
        public ResultString(Result result)
        {
            Result = result;
        }

        public ResultString()
        {

        }
    }
}
