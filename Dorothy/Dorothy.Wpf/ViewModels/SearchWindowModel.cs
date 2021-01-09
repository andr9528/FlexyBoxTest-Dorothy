using Dorothy.Core.Models;
using Dorothy.Proxy.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Dorothy.Wpf.ViewModels
{
    public class SearchWindowModel : BaseViewModel
    {
        public SearchWindowModel(SearchProxy search)
        {
            Search = search;

            Title = $"Search - {SearchTerm}";
            SearchTarget = Core.Enums.Target.GetSearchTarget(Search.Target);

            Task.Run(async () => SearchResults = new ObservableCollection<ResultProxy>(await Services.Instance.Handler.FindMultiple(new ResultProxy() { SearchId = search.Id })));            
        }

        SearchProxy _search = default;
        public SearchProxy Search
        {
            get { return _search; }
            set { SetProperty(ref _search, value); }
        }

        public string SearchTerm => Search.Term;
        public DateTime SearchExecutedAt => Search.ExecutedAt;
        public string SearchPath => Search.Target == Core.Enums.SearchTarget.Web ? "Not Applicable" : Search.Path;        
        public string SearchIncludeSubFolders => Search.IncludeSubFolders == null ? "Not Applicable" : Search.IncludeSubFolders == true ? "Yes" : "No";
        public int SearchTermLenght => Search.TermLenght;
        public int SearchTermLetters => Search.TermLetters;
        public int SearchTermNumbers => Search.TermNumbers;
        public int SearchTermSymbols => Search.TermSymbols;
        public int SearchTermSpaces => Search.TermSpaces;
        public string SearchDesiredAmount
        {
            get 
            {
                if (Search.Target == Core.Enums.SearchTarget.All || 
                    Search.Target == Core.Enums.SearchTarget.Web || 
                    Search.Target == Core.Enums.SearchTarget.WebAndFiles || 
                    Search.Target == Core.Enums.SearchTarget.WebAndFolder)
                    return Search.DesiredAmount.ToString();
                else return "Not Applicable";
            }
        }


        ObservableCollection<ResultProxy> _searchResults = default;
        public ObservableCollection<ResultProxy> SearchResults
        {
            get { return _searchResults; }
            set { SetProperty(ref _searchResults, value); }
        }

        string _target = string.Empty;
        public string SearchTarget
        {
            get { return _target; }
            set { SetProperty(ref _target, value); }
        }
    }
}
