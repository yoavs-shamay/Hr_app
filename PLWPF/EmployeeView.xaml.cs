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
using BE;
using System.Threading;
using System.Collections.ObjectModel;

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
        private EditTabs currentEditWindow = null;
        private ObservableCollection<Employee> employeeList;
        public EmployeeView()
        {
            InitializeComponent();
            employeeList = new ObservableCollection<Employee>(BL_Object.getAllEmployees());
            EmployeesListView.ItemsSource = employeeList;
            ViewTabs.refreshViewsBackgroundWorker.DoWork += new DoWorkEventHandler(App_refreshViewsEvent);
        }

        private void App_refreshViewsEvent(object sender, DoWorkEventArgs args)
        {
            while (true)
            {
                
                Action refreshAction = () => {
                    employeeList.Clear();
                    foreach (Employee emp in BL_Object.getAllEmployees())
                    {
                        employeeList.Add(emp);
                    }
                };
                Dispatcher.BeginInvoke(refreshAction);
                Thread.Sleep(5000);
            }
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
            if (currentEditWindow != null && currentEditWindow.IsVisible)
            {
                currentEditWindow.Close();
            }
            EditTabs result = Globals.openEditOn(EmployeesListView);
            if (result != null)
            {
                currentEditWindow = result;
            }
        }

        private void SearchTextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            if (SearchByComboBox.SelectedItem != null)
            {
                string filterBy = SearchByComboBox.SelectedItem.ToString();
                EmployeesListView.Items.Filter = obj =>
                {
                    Employee emp = obj as Employee;
                    if (filterBy.Contains("Name"))
                    {
                        return (emp.FirstName + emp.LastName).Contains(SearchTextBox.Text);
                    }
                    if (filterBy.Contains("Id"))
                    {
                        return emp.Id.Contains(SearchTextBox.Text);
                    }
                    if (filterBy.Contains("Phone Number"))
                    {
                        return emp.PhoneNumber.Contains(SearchTextBox.Text);
                    }
                    if (filterBy.Contains("Email"))
                    {
                        return emp.Email.Contains(SearchTextBox.Text);
                    }
                    return true;
                };
            }
        }
    }
}
