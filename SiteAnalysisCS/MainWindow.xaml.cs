using Serilog;
using System.IO;
using System.Windows;

namespace SiteAnalysisCS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var folder = YSCommon.Utils.GetAssemblyFolderInLocalData("site-analysis-cs");
#if DEBUG
            string fileName = Path.Combine(folder, "logs", "site-analysis-cs-debug.log");
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .Enrich.WithThreadId()
               .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext} [{ThreadId}] {Message:lj}{NewLine}{Exception}")
               .WriteTo.File(fileName,
               rollingInterval: RollingInterval.Month,
               outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {SourceContext} [{ThreadId}] {Message:lj}{NewLine}{Exception}")
           .CreateLogger();
#else
            string fileName = Path.Combine(folder, "logs", "site-analysis-cs.log");
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Error()
               .Enrich.WithThreadId()
               .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext} [{ThreadId}] {Message:lj}{NewLine}{Exception}")
               .WriteTo.File(fileName,
               rollingInterval: RollingInterval.Month,
               outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {SourceContext} [{ThreadId}] {Message:lj}{NewLine}{Exception}")
           .CreateLogger();
#endif
            InitializeComponent();

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            if (fvi != null && fvi.FileVersion != null)
            {
                string version = fvi.FileVersion;
                Title = $"{Title} {version}";
            }
        }

        private void NewsStartPageMenuItemClick(object sender, RoutedEventArgs e)
        {
            CC.Content = new StartPageControl();
        }

        private void KnopkaMenuItemClick(object sender, RoutedEventArgs e)
        {
            CC.Content = new KnopkaControl();
        }

        private void RadioSvobodaMenuItemClick(object sender, RoutedEventArgs e)
        {
            CC.Content = new RadioSvobodaControl();
        }
    }
}