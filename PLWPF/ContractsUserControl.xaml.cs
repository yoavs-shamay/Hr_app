using BE;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ContractsUserControl.xaml
    /// </summary>
    public partial class ContractsUserControl : Window
    {
        private Contract ContractData { get; set; }
        public ContractsUserControl()
        {
            InitializeComponent();
            DataContext = ContractData;
        }
    }
}
