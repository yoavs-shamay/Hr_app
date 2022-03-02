using BE;
using BL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for ContractsView.xaml
    /// </summary>
    public partial class ContractsView : UserControl
    {
        private IBL BL_Object = FactoryBL.BL_instance;
        private GridViewColumnHeader sortByColumn;
        private ListSortDirection sortingDirection;
        private EditTabs currentEditWindow = null;
        private ObservableCollection<ContractGroupContainer> contractList;
        private Predicate<object> searchPredicate = x => true;
        private Predicate<object> filterByPredicate = x => true;
        private GroupingType grouping = GroupingType.None;
        public enum GroupingType
        {
            None,Specialization,Address,EstablishmentDate
        }
        public ContractsView()
        {
            InitializeComponent();
            ContractsListView.ItemsSource = FactoryBL.BL_instance.getAllContracts();
            contractList = new ObservableCollection<ContractGroupContainer>();
            foreach (Contract c in BL_Object.getAllContracts())
            {
                contractList.Add(new ContractGroupContainer { Key = 0, Value = c });
            }
            ContractsListView.ItemsSource = contractList;
            ViewTabs.refreshViewsEvent += refreshViewsEvent;
        }

        private void refreshViewsEvent()
        {
            Action refreshAction = () => {
                contractList.Clear();
                if (grouping == GroupingType.None)
                {
                    foreach (Contract c in BL_Object.getAllContracts())
                    {
                        contractList.Add(new ContractGroupContainer { Key = 0, Value = c });
                    }
                }
                else if (grouping == GroupingType.Address)
                {
                    foreach(ContractGroupContainer c in BL_Object.getAllCountractsGroupedByAddress(true))
                    {
                        contractList.Add(c);
                    }
                }
                else if (grouping == GroupingType.EstablishmentDate)
                {
                    foreach (ContractGroupContainer c in BL_Object.getAllCountractsGroupedByTime(true))
                    {
                        contractList.Add(c);
                    }
                }    
                else if (grouping == GroupingType.Specialization)
                {
                    foreach (ContractGroupContainer c in BL_Object.getAllCountractsGroupedBySpecialization(true))
                    {
                        contractList.Add(c);
                    }
                }
            };
            Dispatcher.BeginInvoke(refreshAction);
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchByComboBox.SelectedItem != null)
            {
                string filterBy = SearchByComboBox.SelectedItem.ToString();
                searchPredicate = obj =>
                {
                    Contract c = (obj as ContractGroupContainer).Value;
                    if (filterBy.Contains("Id"))
                    {
                        return c.Id.Contains(SearchTextBox.Text);
                    }
                    if (filterBy.Contains("Employer Company Name"))
                    {
                        Employer employer = BL_Object.getAllEmployers().Find(emp => emp.Id == c.EmployerId);
                        return employer.CompanyName.Contains(SearchTextBox.Text);
                    }
                    if (filterBy.Contains("Employee Name"))
                    {
                        Employee employee = BL_Object.getAllEmployees().Find(emp => emp.Id == c.EmployeeId);
                        return (employee.FirstName + employee.LastName).Contains(SearchTextBox.Text);
                    }
                    return true;
                };
                ContractsListView.Items.Filter = obj => searchPredicate(obj) && filterByPredicate(obj);
            }
        }

        private void ContractsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (currentEditWindow != null && currentEditWindow.IsVisible)
            {
                currentEditWindow.Close();
            }
            EditTabs result = Globals.openEditOn(ContractsListView);
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
            Globals.sortBy(ContractsListView, column.Tag.ToString(), direction);
        }

        private void FilterByComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FilterByComboBox.SelectedItem != null)
            {
                string filterBy = FilterByComboBox.SelectedItem.ToString();
                filterByPredicate = obj =>
                {
                    Contract c = (obj as ContractGroupContainer).Value;
                    if (filterBy.Contains("Interviewed"))
                    {
                        return c.IsInterview;
                    }
                    if (filterBy.Contains("Finalized"))
                    {
                        return c.IsFinalized;
                    }
                    if (filterBy.Contains("Terminated"))
                    {
                        return c.TerminateDate < DateTime.Today;
                    }
                    return true;
                };
                ContractsListView.Items.Filter = obj => searchPredicate(obj) && filterByPredicate(obj);
            }
        }

        private void GroupByComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GroupByComboBox.SelectedItem != null)
            {
                string groupBy = GroupByComboBox.SelectedItem.ToString();
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ContractsListView.ItemsSource);
                view.GroupDescriptions.Clear();
                if (groupBy.Contains("Specialization"))
                {
                    IEnumerable<ContractGroupContainer> contractGroupContainers = BL_Object.getAllCountractsGroupedBySpecialization(true);
                    ContractsListView.ItemsSource = contractGroupContainers;
                    view = (CollectionView)CollectionViewSource.GetDefaultView(ContractsListView.ItemsSource);
                    view.GroupDescriptions.Add(new PropertyGroupDescription("Key"));
                    grouping = GroupingType.Specialization;
                }
                if (groupBy.Contains("Address"))
                {
                    IEnumerable<ContractGroupContainer> contractGroupContainers = BL_Object.getAllCountractsGroupedByAddress(true);
                    ContractsListView.ItemsSource = contractGroupContainers;
                    view = (CollectionView)CollectionViewSource.GetDefaultView(ContractsListView.ItemsSource);
                    view.GroupDescriptions.Add(new PropertyGroupDescription("Key"));
                    grouping = GroupingType.Address;
                }
                if (groupBy.Contains("Establishment Date"))
                {
                    IEnumerable<ContractGroupContainer> contractGroupContainers = BL_Object.getAllCountractsGroupedByTime(true);
                    ContractsListView.ItemsSource = contractGroupContainers;
                    view = (CollectionView)CollectionViewSource.GetDefaultView(ContractsListView.ItemsSource);
                    view.GroupDescriptions.Add(new PropertyGroupDescription("Key"));
                    grouping = GroupingType.EstablishmentDate;
                }
            }
        }
    }
}
