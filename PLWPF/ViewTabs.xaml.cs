﻿using System;
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
using System.Windows.Shapes;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for ViewTabs.xaml
    /// </summary>
    public partial class ViewTabs : Window
    {
        public static BackgroundWorker refreshViewsBackgroundWorker = new BackgroundWorker();
        public ViewTabs()
        {
            InitializeComponent();
            if (!refreshViewsBackgroundWorker.IsBusy)
            {
                refreshViewsBackgroundWorker.RunWorkerAsync();
            }
        }
    }
}
