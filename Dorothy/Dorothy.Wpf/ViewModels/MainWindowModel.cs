using Dorothy.Core.Enums;
using Dorothy.Proxy.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dorothy.Wpf.ViewModels
{
    public class MainWindowModel : BaseViewModel
    {
        public const string StartSearchPath = "Select a Location...";
        public MainWindowModel()
        {
            Title = "Super Searcher";

            SearchPath = StartSearchPath;
            
            Targets = Target.SearchTargetDictionary.Values.ToList();

            CurrentSearches = new ObservableCollection<SearchProxy>();

            Task.Run(async () => HistorySearches = new ObservableCollection<SearchProxy>(await Services.Instance.Handler.FindMultiple(new SearchProxy())));
            Services.Instance.Context.OnSearchesChanged += Context_OnSearchesChanged;
        }

        private async Task Context_OnSearchesChanged(Task<List<SearchProxy>> searches, EventArgs args)
        {
            var newSearches = await searches;
            HistorySearches = new ObservableCollection<SearchProxy>(newSearches);
        }

        string _searchPath = string.Empty;
        public string SearchPath
        {
            get { return _searchPath; }
            set { SetProperty(ref _searchPath, value); }
        }

        List<string> _targets = default;
        public List<string> Targets 
        {
            get { return _targets; }
            set { SetProperty(ref _targets, value); }
        }

        ObservableCollection<SearchProxy> _currentSearches = default;
        public ObservableCollection<SearchProxy> CurrentSearches 
        {
            get { return _currentSearches; }
            set { SetProperty(ref _currentSearches, value); }
        }

        ObservableCollection<SearchProxy> _historySearches = default;
        public ObservableCollection<SearchProxy> HistorySearches
        {
            get { return _historySearches; }
            set { SetProperty(ref _historySearches, value); }
        }
    }
}
