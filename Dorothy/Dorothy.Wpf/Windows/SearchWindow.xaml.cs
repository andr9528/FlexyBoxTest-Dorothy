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
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        SearchWindowModel model;
        Window Results = null;
        public SearchWindow(SearchProxy search)
        {
            InitializeComponent();

            DataContext = model = new SearchWindowModel(search);

            CreateResultWindow(search.Term);
        }

        private void DeleteSearch_Click(object sender, RoutedEventArgs e)
        {
            var dialog = MessageBox.Show("Are you sure you want to delete this Search from the Database?", "Delete Confirmation", MessageBoxButton.YesNo);
            if (dialog == MessageBoxResult.Yes)
            {
                Services.Instance.Handler.Delete(model.Search);
                Close();
            }
        }

        private void ExpandResults_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(async () => 
            {
                try
                {
                    await Task.CompletedTask.WaitUntil(() => { return Results != null; });

                    Dispatcher.Invoke(() => 
                    {
                        ExpandResults.IsEnabled = false;
                        Results.Show();
                    });
                }
                catch (Exception e)
                {
                    throw;
                }
            });
        }

        private void Results_Closed(object sender, EventArgs e)
        {
            ExpandResults.IsEnabled = true;
        }

        private async Task CreateResultWindow(string searchTerm) 
        {
            try
            {
                var results = await Services.Instance.Handler.FindMultiple(new ResultProxy() { SearchId = model.Search.Id });

                Dispatcher.Invoke(() =>
                {
                    Results = new ResultsWindow((List<ResultProxy>)results, searchTerm);
                    Results.Closed += Results_Closed;
                });
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
