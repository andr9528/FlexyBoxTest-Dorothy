using Dorothy.Core.Enums;
using Dorothy.Proxy.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

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

    }
}
