using Dorothy.Proxy.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dorothy.Wpf.ViewModels
{
    public class ResultsWindowModel : BaseViewModel
    {
        public ResultsWindowModel(List<ResultProxy> results, string searchTerm)
        {
            Title = $"Results: {searchTerm}";

            FileResult = results.FirstOrDefault(x => x.ResultType == Core.Enums.ResultType.Files);
            FolderResult = results.FirstOrDefault(x => x.ResultType == Core.Enums.ResultType.Folder);
            WebResult = results.FirstOrDefault(x => x.ResultType == Core.Enums.ResultType.Web);

            if (FileResult != null) Task.Run(async () => FileResults = new ObservableCollection<ResultStringProxy>(await Services.Instance.Handler.FindMultiple(new ResultStringProxy() { ResultId = FileResult.Id })));
            if (FolderResult != null) Task.Run(async () => FolderResults = new ObservableCollection<ResultStringProxy>(await Services.Instance.Handler.FindMultiple(new ResultStringProxy() { ResultId = FolderResult.Id })));
            if (WebResult != null) Task.Run(async () => WebResults = new ObservableCollection<ResultStringProxy>(await Services.Instance.Handler.FindMultiple(new ResultStringProxy() { ResultId = WebResult.Id })));
        }

        ResultProxy _fileResult = default;
        public ResultProxy FileResult 
        {
            get { return _fileResult; }
            set { SetProperty(ref _fileResult, value); }
        }

        ResultProxy _folderResult = default;
        public ResultProxy FolderResult
        {
            get { return _folderResult; }
            set { SetProperty(ref _folderResult, value); }
        }

        ResultProxy _webResult = default;
        public ResultProxy WebResult
        {
            get { return _webResult; }
            set { SetProperty(ref _webResult, value); }
        }

        ObservableCollection<ResultStringProxy> _fileResults = default;
        public ObservableCollection<ResultStringProxy> FileResults
        {
            get { return _fileResults; }
            set { SetProperty(ref _fileResults, value); }
        }

        ObservableCollection<ResultStringProxy> _folderResults = default;
        public ObservableCollection<ResultStringProxy> FolderResults
        {
            get { return _folderResults; }
            set { SetProperty(ref _folderResults, value); }
        }

        ObservableCollection<ResultStringProxy> _webResults = default;
        public ObservableCollection<ResultStringProxy> WebResults
        {
            get { return _webResults; }
            set { SetProperty(ref _webResults, value); }
        }
    }
}
