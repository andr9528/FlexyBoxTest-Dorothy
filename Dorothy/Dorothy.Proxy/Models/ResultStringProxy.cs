using Dorothy.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dorothy.Proxy.Models
{
    public class ResultStringProxy : IResultString
    {
        public string Value { get; set; }
        public IResult Result { get; set; }
        public int ResultId { get; set; }
        public int Id { get; set; }
        public byte[] Version { get; set; }

        /// <summary>
        /// Used to tell any code generating a version of this class from Json or similar, which implementation of IResult to use.
        /// </summary>
        /// <param name="result"></param>
        [JsonConstructor]
        public ResultStringProxy(ResultProxy result)
        {
            Result = result;
        }

        public ResultStringProxy()
        {

        }
    }
}
