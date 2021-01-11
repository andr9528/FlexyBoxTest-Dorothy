using Dorothy.Core.Enums;
using Dorothy.Core.Models;
using Dorothy.Proxy.Models;
using Dorothy.Wpf.ViewModels;
using Dorothy.Wpf.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox;

namespace Dorothy.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowModel model = null;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = model = new MainWindowModel();

            Services.Instance.SearchController.OnSearchComplete += SearchController_OnSearchComplete;
        }

        private void SearchController_OnSearchComplete(SearchProxy search)
        {
            Task.Run(async () =>
            {
                try
                {
                    var results = await Services.Instance.Handler.FindMultiple(new ResultProxy() { SearchId = search.Id });
                    
                    Dispatcher.Invoke(() =>
                    {
                        var window = new ResultsWindow((List<ResultProxy>)results, search.Term);
                        window.Show();
                    });
                }
                catch (Exception e)
                {
                    throw;
                }
            });            

            // As the one received from the event has been throught the database, and the one in the list hase not, they are different, while still in reality being the same.
            // Matching them on thier ExecutedAt, ensures the correct one is found and removed.
            // This is needed incase many searhes are done rapidly, and one done later is finished before one done earlier.
            var item = model.CurrentSearches.Where(x => x.ExecutedAt == search.ExecutedAt).ToList().First();
            Dispatcher.Invoke(() => model.CurrentSearches.Remove(item));            
        }

        private void IncludeSubfoldersToggle_Click(object sender, RoutedEventArgs e)
        {
            ColorToggleButton((ToggleButton)sender);
        }

        private void PathSelectorButton_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog()) 
            {
                var result = dialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
                    model.SearchPath = dialog.SelectedPath;
            }
        }

        private void ExecuteSearch_Click(object sender, RoutedEventArgs e)
        {
            var target = (SearchTarget)(TargetsDropDown.SelectedIndex + 1);

            if (!EnsureValidInputs(target)) return;

            var term = SearchBox.Text;
            var path = model.SearchPath;
            var subfolders = IncludeSubfoldersToggle.IsChecked;
            _ = int.TryParse(AmountBox.Text, out var desiredAmount);

            if (desiredAmount > 100) desiredAmount = 100;

            var executed = DateTime.Now;

            var search = new SearchProxy()
            {
                ExecutedAt = executed,
                Target = target, 
                Term = term, 
                DesiredAmount = desiredAmount, 
                IncludeSubFolders = subfolders, 
                Path = path
            };

            Task.Run(() => 
            {
                Dispatcher.Invoke(() => model.CurrentSearches.Add(search));
                Services.Instance.SearchController.GetResults(search);
            });
        }

        private bool EnsureValidInputs(SearchTarget target) 
        {
            switch (target)
            {
                case SearchTarget.All:
                    return CheckForWeb() && CheckForFolderAndFiles(false); 
                case SearchTarget.WebAndFiles:
                    return CheckForWeb() && CheckForFolderAndFiles(false);
                case SearchTarget.WebAndFolder:
                    return CheckForWeb() && CheckForFolderAndFiles(false);
                case SearchTarget.FilesAndFolders:
                    return CheckForFolderAndFiles();
                case SearchTarget.Web:
                    return CheckForWeb();
                case SearchTarget.Files:
                    return CheckForFolderAndFiles();
                case SearchTarget.Folders:
                    return CheckForFolderAndFiles();
                default:
                    return false;
            }
        }

        private bool CheckForWeb(bool checkGeneral = true) 
        {
            var parsed = int.TryParse(AmountBox.Text, out var desiredAmount);
            if (!parsed) 
            {
                _ = MessageBox.Show($"Failed to parse the desired result to a number." +
                    $"{Environment.NewLine}" +
                    $"Avoid letters, symbols and keep it hole.", "Warning");
                return false;
            }

            if (checkGeneral) return CheckForGeneral();
            else return true;
        }

        private bool CheckForFolderAndFiles(bool checkGeneral = true)
        {
            if (model.SearchPath == MainWindowModel.StartSearchPath) 
            {
                _ = MessageBox.Show("Remember to set a location where you want to perform the search.", "Warning");
                return false;
            }

            if (checkGeneral) return CheckForGeneral();
            else return true;
        }

        private bool CheckForGeneral() 
        {
            if (SearchBox.Text == string.Empty) 
            { 
                _ = MessageBox.Show("Remember to write what you want to search for.", "Warning");
                return false;
            }
            return true;
        }

        private void AllowOpenSearches_Click(object sender, RoutedEventArgs e)
        {
            ColorToggleButton((ToggleButton)sender);
        }

        private void ColorToggleButton(ToggleButton button) 
        {
            switch (button.IsChecked)
            {
                case true:
                    button.BorderBrush = new SolidColorBrush(Colors.Green);
                    button.Foreground = new SolidColorBrush(Colors.Green);
                    break;
                case false:
                    button.BorderBrush = new SolidColorBrush(Colors.Red);
                    button.Foreground = new SolidColorBrush(Colors.Red);
                    break;
                default:
                    break;
            }
        }

        private void HistoryView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AllowOpenSearches.IsChecked == true) 
            {
                var item = (SearchProxy)(HistoryView.SelectedItem);
                if (item == null) return;
                var window = new SearchWindow(item);
                window.Show();
            }
        }
    }

    
}
