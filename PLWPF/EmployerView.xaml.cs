using BE;
using BL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for EmployerView.xaml
    /// </summary>
    public partial class EmployerView : UserControl
    {
        private IBL BL_Object = FactoryBL.BL_instance;
        private GridViewColumnHeader sortByColumn;
        private ListSortDirection sortingDirection;
        private EditTabs currentEditWindow = null;
        private ObservableCollection<Employer> employerList;
        public EmployerView()
        {
            InitializeComponent();
            employerList = new ObservableCollection<Employer>(BL_Object.getAllEmployers());
            EmployersListView.ItemsSource = employerList;
            ViewTabs.refreshViewsBackgroundWorker.DoWork += new DoWorkEventHandler(App_refreshViewsEvent);
        }

        private void App_refreshViewsEvent(object sender, DoWorkEventArgs args)
        {
            while (true)
            {
                Action refreshAction = () => {
                    employerList.Clear();
                    foreach (Employer emp in BL_Object.getAllEmployers())
                    {
                        employerList.Add(emp);
                    }
                };
                Dispatcher.BeginInvoke(refreshAction);
                Thread.Sleep(5000);
            }
        }

        private void EmployersListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (currentEditWindow != null && currentEditWindow.IsVisible)
            {
                currentEditWindow.Close();
            }
            EditTabs result = Globals.openEditOn(EmployersListView);
            if (result != null)
            {
                currentEditWindow = result;
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
            Globals.sortBy(EmployersListView, column.Tag.ToString(), direction);
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchByComboBox.SelectedItem != null)
            {
                string filterBy = SearchByComboBox.SelectedItem.ToString();
                EmployersListView.Items.Filter = obj =>
                {
                    Employer emp = obj as Employer;
                    if (filterBy.Contains("Name") && !filterBy.Contains("Company Name"))
                    {
                        return (emp.FirstName + emp.LastName).Contains(SearchTextBox.Text);
                    }
                    if (filterBy.Contains("Id"))
                    {
                        return emp.Id.Contains(SearchTextBox.Text);
                    }
                    if (filterBy.Contains("Company Name"))
                    {
                        return emp.CompanyName.Contains(SearchTextBox.Text);
                    }
                    if (filterBy.Contains("Phone Number"))
                    {
                        return emp.PhoneNumber.Contains(SearchTextBox.Text);
                    }
                    return true;
                };
            }
        }
    }
}
