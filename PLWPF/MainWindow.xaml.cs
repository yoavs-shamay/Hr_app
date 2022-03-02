using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BL;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public BackgroundWorker downloadBankXMLWorker = new BackgroundWorker();
        public MainWindow()
        {
            InitializeComponent();
            this.Closing += MainWindow_Close;
            downloadBankXMLWorker.DoWork += DownloadBankXMLWorker_DoWork;
            downloadBankXMLWorker.RunWorkerAsync();
        }

        private void DownloadBankXMLWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                BL_imp.DownloadBanksXML();
            }
            catch
            {
                try
                {
                    FactoryBL.BL_instance.getAllBanks();
                    MessageBox.Show("Loaded banks from local file", "Hr app", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch
                {
                    MessageBox.Show("Failed loading banks", "Hr app", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void MainWindow_Close(object sender, EventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void EditWindowButton_Click(object sender, RoutedEventArgs e)
        {
            EditTabs editTabsWindow = new EditTabs();
            editTabsWindow.Show();
        }

        private void ViewWindowButton_Click(object sender, RoutedEventArgs e)
        {
            ViewTabs viewTabsWindow = new ViewTabs();
            viewTabsWindow.Show();
        }
    }
}
