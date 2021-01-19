using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for ViewTabs.xaml
    /// </summary>
    public partial class ViewTabs : Window
    {
        public static event Action refreshViewsEvent = null;
        public static BackgroundWorker refreshViewsBackgroundWorker = new BackgroundWorker();
        public ViewTabs()
        {
            InitializeComponent();
            refreshViewsBackgroundWorker.DoWork += refreshViewsBackgroundWorkerEvent;
            if (!refreshViewsBackgroundWorker.IsBusy)
            {
                refreshViewsBackgroundWorker.RunWorkerAsync();
            }
        }
        private void refreshViewsBackgroundWorkerEvent(object sender, DoWorkEventArgs args)
        {
            while (true)
            {
                refreshViewsEvent();
                Thread.Sleep(5000);
            }
        }
    }
}
