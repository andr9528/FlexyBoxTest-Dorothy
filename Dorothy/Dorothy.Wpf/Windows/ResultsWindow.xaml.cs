using Dorothy.Core.Models;
using Dorothy.Proxy.Models;
using Dorothy.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wolf.Utility.Core.Extensions.Methods;

namespace Dorothy.Wpf.Windows
{
    /// <summary>
    /// Interaction logic for ResultsWindow.xaml
    /// </summary>
    public partial class ResultsWindow : Window
    {
        ResultsWindowModel model = null;
        public ResultsWindow(List<ResultProxy> results, string searchTerm)
        {
            InitializeComponent();

            DataContext = model = new ResultsWindowModel(results, searchTerm);

            HideEmptyLists();
        }

        private void WebListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void FileListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void FolderListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private async Task HideEmptyLists() 
        {
            if (model.WebResult == null)
            {
                WebLabel.Visibility = Visibility.Hidden;
                WebListView.Visibility = Visibility.Hidden;
            }
            else 
            {
                await Task.CompletedTask.WaitUntil(() => { return model.WebResults != null; });
                if (model.WebResults.Count == 0)
                {
                    WebLabel.Visibility = Visibility.Hidden;
                    WebListView.Visibility = Visibility.Hidden;
                }
            }

            if (model.FileResult == null)
            {
                FileLabel.Visibility = Visibility.Hidden;
                FileListView.Visibility = Visibility.Hidden;
            }
            else
            {
                await Task.CompletedTask.WaitUntil(() => { return model.FileResults != null; });
                if (model.FileResults.Count == 0)
                {
                    FileLabel.Visibility = Visibility.Hidden;
                    FileListView.Visibility = Visibility.Hidden;
                }
            }

            if (model.FolderResult == null)
            {
                FolderLabel.Visibility = Visibility.Hidden;
                FolderListView.Visibility = Visibility.Hidden;
            }
            else 
            {
                await Task.CompletedTask.WaitUntil(() => { return model.FolderResults != null; });
                if (model.FolderResults.Count == 0)
                {
                    FolderLabel.Visibility = Visibility.Hidden;
                    FolderListView.Visibility = Visibility.Hidden;
                }
            }
        }
    }
}
