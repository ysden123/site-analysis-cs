using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SiteAnalysisCS
{
    /// <summary>
    /// Interaction logic for StartPageControl.xaml
    /// </summary>
    public partial class StartPageControl : UserControl
    {
        public StartPageControl()
        {
            InitializeComponent();

            FillData();
        }

        private async void FillData()
        {
            // You can add code here to populate the start page with dynamic data if needed.
            var currentCursor = WordDataListView.Cursor;
            WordDataListView.Cursor = Cursors.Wait;

            await Task.Run(async () =>
            {
                try
                {
                    var siteScraper = new StartPageWebScraper();
                    Dispatcher.Invoke(new Action(() =>
                    {
                        WordDataListView.ItemsSource = siteScraper.Scrap().Result;
                    }));
                }
                catch (Exception)
                {
                    MessageBox.Show("Error site analysing. See logs.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });

            WordDataListView.Cursor = currentCursor;
        }
    }
}
