using BL;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for EmployeeView.xaml
    /// </summary>
    public partial class EmployeeView : UserControl
    {
        private IBL BL_Object = FactoryBL.BL_instance;
        private GridViewColumnHeader sortByColumn;
        private ListSortDirection sortingDirection;
        public EmployeeView()
        {
            InitializeComponent();
            EmployeesListView.ItemsSource = BL_Object.getAllEmployees();
        }

        private void GridViewColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (GridViewColumnHeader)sender;
            ListSortDirection direction;
            if (sortByColumn != null && sortByColumn.Content == column.Content)
            {
                direction = sortingDirection == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
                sortingDirection = direction;
            }
            else
            {
                sortByColumn = column;
                direction = ListSortDirection.Descending;
                sortingDirection = ListSortDirection.Descending;
            }
            Globals.sortBy(EmployeesListView, column.Tag.ToString(), direction);
        }

        private void EmployeesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Globals.openEditOn(EmployeesListView);
        }
    }
}
