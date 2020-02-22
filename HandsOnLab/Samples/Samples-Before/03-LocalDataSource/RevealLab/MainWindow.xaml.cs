using Infragistics.Sdk;
using System;
using System.IO;
using System.Windows;

namespace RevealLab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            _revealView.VisualizationLinkingDashboard += RevealView_VisualizationLinkingDashboard;      
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {            
            var marketingDashboardPath = Path.Combine(Environment.CurrentDirectory, "Dashboards/Marketing.rdash");

            RVDashboard dashboard = null;
            using (var fileStream = File.OpenRead(marketingDashboardPath))
            {
                dashboard = await RevealUtility.LoadDashboard(fileStream);
            }
            _revealView.Settings = new RevealSettings(dashboard);
        }

        private void RevealView_VisualizationLinkingDashboard(object sender, VisualizationLinkingDashboardEventArgs e)
        {
            var campaignsDashboardPath = Path.Combine(Environment.CurrentDirectory, "Dashboards/Campaigns.rdash");
            using (var fileStream = File.OpenRead(campaignsDashboardPath))
            {
                e.Callback("Campaigns", fileStream);
            }
        }
    }
}
