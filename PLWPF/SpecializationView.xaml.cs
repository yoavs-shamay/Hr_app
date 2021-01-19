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
    /// Interaction logic for SpecializationView.xaml
    /// </summary>
    public partial class SpecializationView : UserControl
    {
        private IBL BL_Object = FactoryBL.BL_instance;
        private GridViewColumnHeader sortByColumn;
        private ListSortDirection sortingDirection;
        private EditTabs currentEditWindow = null;
        private ObservableCollection<Specialization> specializationList;
        public SpecializationView()
        {
            InitializeComponent();
            specializationList = new ObservableCollection<Specialization>(BL_Object.getAllSpecializations());
            SpecializationsListView.ItemsSource = specializationList;
            ViewTabs.refreshViewsEvent += refreshViewsEvent;
        }

        private void refreshViewsEvent()
        {
            Action refreshAction = () => {
                specializationList.Clear();
                foreach (Specialization spec in BL_Object.getAllSpecializations())
                {
                    specializationList.Add(spec);
                }
            };
            Dispatcher.BeginInvoke(refreshAction);
        }

        private void SpecializationsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (currentEditWindow != null && currentEditWindow.IsVisible)
            {
                currentEditWindow.Close();
            }
            EditTabs result = Globals.openEditOn(SpecializationsListView);
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
            Globals.sortBy(SpecializationsListView, column.Tag.ToString(), direction);
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchByComboBox.SelectedItem != null)
            {
                string filterBy = SearchByComboBox.SelectedItem.ToString();
                SpecializationsListView.Items.Filter = obj =>
                {
                    Specialization spec = obj as Specialization;
                    if (filterBy.Contains("Name"))
                    {
                        return spec.Name.Contains(SearchTextBox.Text);
                    }
                    if (filterBy.Contains("Id"))
                    {
                        return spec.Id.Contains(SearchTextBox.Text);
                    }
                    if (filterBy.Contains("Area"))
                    {
                        return spec.Area.ToString().Contains(SearchTextBox.Text);
                    }
                    return true;
                };
            }
        }
    }
}
