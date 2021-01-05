﻿using Dorothy.Core.Enums;
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
        public int TermLenght { get; private set; }
        public int TermLetters { get; private set; }
        public int TermNumbers { get; private set; }
        public int TermSymbols { get; private set; }
        public int TermSpaces { get; private set; }
        public int Id { get; set; }
        public byte[] Version { get; set; }
        public SearchTarget Target { get; set; }


        /// <summary>
        /// Used to tell any code generating a version of this class from Json or similar, which implementation of IResult to use.
        /// </summary>
        /// <param name="results"></param>
        [JsonConstructor]
        public SearchProxy(List<ResultProxy> results, string searchTerm)
        {
            Results = results;
            SearchTerm = searchTerm;

            SetStats(searchTerm);
        }

        public SearchProxy(string searchTerm)
        {
            SearchTerm = searchTerm;

            SetStats(searchTerm);
        }

        private void SetStats(string searchTerm)
        {
            TermLenght = searchTerm.Length;
            TermSpaces = searchTerm.Count(char.IsWhiteSpace);
            TermNumbers = searchTerm.Count(char.IsDigit);
            TermLetters = searchTerm.Count(char.IsLetter);
            TermSymbols = searchTerm.Count(char.IsSymbol);
        }
    }
}