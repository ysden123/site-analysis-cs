using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SiteAnalysisCS
{
    /// <summary>
    /// Interaction logic for KnopkaControl.xaml
    /// </summary>
    public partial class KnopkaControl : UserControl
    {
        public KnopkaControl()
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
                    var siteScraper = new KnopkaWebScraper();
                    Dispatcher.Invoke(new Action(() =>
                    {
                        WordDataListView.ItemsSource = siteScraper.Scrap(siteScraper.Url()).Result;
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
