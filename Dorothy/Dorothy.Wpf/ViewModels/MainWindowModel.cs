using Dorothy.Core.Enums;
using Dorothy.Proxy.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolf.Utility.Core.Extensions.Methods;

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

            Task.Run(async () => Searches = new ObservableCollection<SearchProxy>(await Services.Instance.Handler.FindMultiple(new SearchProxy())));
            Services.Instance.Context.OnSearchesChanged += Context_OnSearchesChanged;

            Task.Run(async () => Results = new ObservableCollection<ResultProxy>(await Services.Instance.Handler.FindMultiple(new ResultProxy())));
            Services.Instance.Context.OnResultsChanged += Context_OnResultsChanged;

            Task.Run(async () => await UpdateStats());
        }

        private async Task UpdateStats()
        {
            // As the lists needed to get the stats from are being set via other tasks, we simply wait for the lists to not be null any longer.
            await Task.CompletedTask.WaitUntil(() => { return Searches != null && Results != null; });

            var fileResults = Results.Where(x=>x.ResultType == ResultType.Files).ToList();
            var folderResults = Results.Where(x=>x.ResultType == ResultType.Folder).ToList();
            var webResults = Results.Where(x=>x.ResultType == ResultType.Web).ToList();
            var searches = Searches.ToList();

            #region Lowest

            LengthLowest = searches.Min(x => x.TermLenght).ToString();
            LettersLowest = searches.Min(x => x.TermLetters).ToString();
            NumbersLowest = searches.Min(x => x.TermNumbers).ToString();
            SymbolsLowest = searches.Min(x => x.TermSymbols).ToString();
            SpacesLowest = searches.Min(x => x.TermSpaces).ToString();
            FileLowest = fileResults.Min(x => x.ToltalResults).ToString();
            FolderLowest = folderResults.Min(x => x.ToltalResults).ToString();
            WebLowest = webResults.Min(x => x.ToltalResults).ToString();

            #endregion

            #region Average

            LengthAverage = Math.Round(searches.Average(x => x.TermLenght), 3).ToString();
            LettersAverage = Math.Round(searches.Average(x => x.TermLetters), 3).ToString();
            NumbersAverage = Math.Round(searches.Average(x => x.TermNumbers), 3).ToString();
            SymbolsAverage = Math.Round(searches.Average(x => x.TermSymbols), 3).ToString();
            SpacesAverage = Math.Round(searches.Average(x => x.TermSpaces), 3).ToString();
            FileAverage = Math.Round(fileResults.Average(x => x.ToltalResults), 3).ToString();
            FolderAverage = Math.Round(folderResults.Average(x => x.ToltalResults), 3).ToString();
            WebAverage = Math.Round(webResults.Average(x => x.ToltalResults), 3).ToString();

            #endregion

            #region Highest

            LengthHighest = searches.Max(x => x.TermLenght).ToString();
            LettersHighest = searches.Max(x => x.TermLetters).ToString();
            NumbersHighest = searches.Max(x => x.TermNumbers).ToString();
            SymbolsHighest = searches.Max(x => x.TermSymbols).ToString();
            SpacesHighest = searches.Max(x => x.TermSpaces).ToString();
            FileHighest = fileResults.Max(x => x.ToltalResults).ToString();
            FolderHighest = folderResults.Max(x => x.ToltalResults).ToString();
            WebHighest = webResults.Max(x => x.ToltalResults).ToString();

            #endregion
            
            #region Total

            LengthTotal = searches.Sum(x => x.TermLenght).ToString();
            LettersTotal = searches.Sum(x => x.TermLetters).ToString();
            NumbersTotal = searches.Sum(x => x.TermNumbers).ToString();
            SymbolsTotal = searches.Sum(x => x.TermSymbols).ToString();
            SpacesTotal = searches.Sum(x => x.TermSpaces).ToString();
            FileTotal = fileResults.Sum(x => x.ToltalResults).ToString();
            FolderTotal = folderResults.Sum(x => x.ToltalResults).ToString();
            WebTotal = webResults.Sum(x => x.ToltalResults).ToString();

            #endregion
        }

        private async Task Context_OnResultsChanged(Task<List<ResultProxy>> results, EventArgs args)
        {
            var newResults = await results;
            Results = new ObservableCollection<ResultProxy>(newResults);
            await UpdateStats();
        }

        private async Task Context_OnSearchesChanged(Task<List<SearchProxy>> searches, EventArgs args)
        {
            var newSearches = await searches;
            Searches = new ObservableCollection<SearchProxy>(newSearches);
            await UpdateStats();
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

        ObservableCollection<SearchProxy> _searches = default;
        public ObservableCollection<SearchProxy> Searches
        {
            get { return _searches; }
            set { SetProperty(ref _searches, value); }
        }

        ObservableCollection<ResultProxy> _results = default;
        public ObservableCollection<ResultProxy> Results
        {
            get { return _results; }
            set { SetProperty(ref _results, value); }
        }

        #region Stats

        string _lengthLowest = string.Empty;
        public string LengthLowest
        {
            get { return _lengthLowest; }
            set { SetProperty(ref _lengthLowest, value); }
        }
        string _lengthAverage = string.Empty;
        public string LengthAverage
        {
            get { return _lengthAverage; }
            set { SetProperty(ref _lengthAverage, value); }
        }
        string _lengthHighest = string.Empty;
        public string LengthHighest
        {
            get { return _lengthHighest; }
            set { SetProperty(ref _lengthHighest, value); }
        }
        string _lengthTotal = string.Empty;
        public string LengthTotal
        {
            get { return _lengthTotal; }
            set { SetProperty(ref _lengthTotal, value); }
        }

        string _lettersLowest = string.Empty;
        public string LettersLowest
        {
            get { return _lettersLowest; }
            set { SetProperty(ref _lettersLowest, value); }
        }
        string _lettersAverage = string.Empty;
        public string LettersAverage
        {
            get { return _lettersAverage; }
            set { SetProperty(ref _lettersAverage, value); }
        }
        string _lettersHighest = string.Empty;
        public string LettersHighest
        {
            get { return _lettersHighest; }
            set { SetProperty(ref _lettersHighest, value); }
        }
        string _lettersTotal = string.Empty;
        public string LettersTotal
        {
            get { return _lettersTotal; }
            set { SetProperty(ref _lettersTotal, value); }
        }

        string _numbersLowest = string.Empty;
        public string NumbersLowest
        {
            get { return _numbersLowest; }
            set { SetProperty(ref _numbersLowest, value); }
        }
        string _numbersAverage = string.Empty;
        public string NumbersAverage
        {
            get { return _numbersAverage; }
            set { SetProperty(ref _numbersAverage, value); }
        }
        string _numbersHighest = string.Empty;
        public string NumbersHighest
        {
            get { return _numbersHighest; }
            set { SetProperty(ref _numbersHighest, value); }
        }
        string _numbersTotal = string.Empty;
        public string NumbersTotal
        {
            get { return _numbersTotal; }
            set { SetProperty(ref _numbersTotal, value); }
        }

        string _symbolsLowest = string.Empty;
        public string SymbolsLowest
        {
            get { return _symbolsLowest; }
            set { SetProperty(ref _symbolsLowest, value); }
        }
        string _symbolsAverage = string.Empty;
        public string SymbolsAverage
        {
            get { return _symbolsAverage; }
            set { SetProperty(ref _symbolsAverage, value); }
        }
        string _symbolsHighest = string.Empty;
        public string SymbolsHighest
        {
            get { return _symbolsHighest; }
            set { SetProperty(ref _symbolsHighest, value); }
        }
        string _symbolsTotal = string.Empty;
        public string SymbolsTotal
        {
            get { return _symbolsTotal; }
            set { SetProperty(ref _symbolsTotal, value); }
        }

        string _spacesLowest = string.Empty;
        public string SpacesLowest
        {
            get { return _spacesLowest; }
            set { SetProperty(ref _spacesLowest, value); }
        }
        string _spacesAverage = string.Empty;
        public string SpacesAverage
        {
            get { return _spacesAverage; }
            set { SetProperty(ref _spacesAverage, value); }
        }
        string _spacesHighest = string.Empty;
        public string SpacesHighest
        {
            get { return _spacesHighest; }
            set { SetProperty(ref _spacesHighest, value); }
        }
        string _spacesTotal = string.Empty;
        public string SpacesTotal
        {
            get { return _spacesTotal; }
            set { SetProperty(ref _spacesTotal, value); }
        }

        string _fileLowest = string.Empty;
        public string FileLowest
        {
            get { return _fileLowest; }
            set { SetProperty(ref _fileLowest, value); }
        }
        string _fileAverage = string.Empty;
        public string FileAverage
        {
            get { return _fileAverage; }
            set { SetProperty(ref _fileAverage, value); }
        }
        string _fileHighest = string.Empty;
        public string FileHighest
        {
            get { return _fileHighest; }
            set { SetProperty(ref _fileHighest, value); }
        }
        string _fileTotal = string.Empty;
        public string FileTotal
        {
            get { return _fileTotal; }
            set { SetProperty(ref _fileTotal, value); }
        }

        string _folderLowest = string.Empty;
        public string FolderLowest
        {
            get { return _folderLowest; }
            set { SetProperty(ref _folderLowest, value); }
        }
        string _folderAverage = string.Empty;
        public string FolderAverage
        {
            get { return _folderAverage; }
            set { SetProperty(ref _folderAverage, value); }
        }
        string _folderHighest = string.Empty;
        public string FolderHighest
        {
            get { return _folderHighest; }
            set { SetProperty(ref _folderHighest, value); }
        }
        string _folderTotal = string.Empty;
        public string FolderTotal
        {
            get { return _folderTotal; }
            set { SetProperty(ref _folderTotal, value); }
        }

        string _webLowest = string.Empty;
        public string WebLowest
        {
            get { return _webLowest; }
            set { SetProperty(ref _webLowest, value); }
        }
        string _webAverage = string.Empty;
        public string WebAverage
        {
            get { return _webAverage; }
            set { SetProperty(ref _webAverage, value); }
        }
        string _webHighest = string.Empty;
        public string WebHighest
        {
            get { return _webHighest; }
            set { SetProperty(ref _webHighest, value); }
        }
        string _webTotal = string.Empty;
        public string WebTotal
        {
            get { return _webTotal; }
            set { SetProperty(ref _webTotal, value); }
        }



        #endregion
    }
}
