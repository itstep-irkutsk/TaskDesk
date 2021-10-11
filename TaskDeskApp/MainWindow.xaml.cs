using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Xml.Linq;

namespace TaskDeskApp
{
    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<DataModel_temp> _collection;

        public MainWindow()
        {
            InitializeComponent();
            _collection = new ObservableCollection<DataModel_temp>
            {
                new() { Id = 1, EventName = "Событие 1", EventDetail = "" },
                new() { Id = 2, EventName = "Событие 2", EventDetail = "" },
                new() { Id = 3, EventName = "Событие 3", EventDetail = "" }
            };
        }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Работает");
            PushListViewIntoGrid(1, 1, Calendar);

            //Event2.Items.Add(new ListBoxItem().Content="Задача 2");
        }

        private void PushListViewIntoGrid(int row, int column, Grid gridname)
        {
            //var listboxitem = new ListBoxItem().Content = "Событие 1";
            //listView.Items.Add(listboxitem);

            var listView = PushEventIntoListview();
            RemoveChildElementFromGrid(row, column, gridname);
            Grid.SetRow(listView, row);
            Grid.SetColumn(listView, column);

            gridname.Children.Add(listView);
        }

        private ListView PushEventIntoListview()
        {
            var gridColumnID = new GridViewColumn
            {
                DisplayMemberBinding = new Binding("id"),
            };
            var gridColumnName = new GridViewColumn
            {
                DisplayMemberBinding = new Binding("EventName"),
            };

            var gridView = new GridView();
            gridView.Columns.Add(gridColumnID);
            
            gridView.Columns.Add(gridColumnName);
            
            ListView listView = new ListView
            {
                View = gridView,
                ItemsSource = _collection,
                //Margin = new Thickness(2),
                //Padding = new Thickness(1),
                // HorizontalAlignment = HorizontalAlignment.Center,
                //VerticalAlignment = VerticalAlignment.Top
            };

            return listView;
        }

        private void RemoveChildElementFromGrid(int row, int column, Grid gridname)
        {
            for (int i = 0; i < gridname.Children.Count; i++)
            {
                if ((Grid.GetRow(gridname.Children[i]) == row) && ((Grid.GetColumn(gridname.Children[i])) == column))
                {
                    gridname.Children.Remove(gridname.Children[i]);
                    break;
                }
            }
        }

        private void RemoveSelectedEventFromCalendar()
        {
            foreach (var obj in Calendar.Children)
            {
                if (obj is ListView list)
                {
                   try
                    {
                        _collection.Remove((DataModel_temp)list.SelectedItem);
                       // list.Items.Remove(list.SelectedItem);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);

                    }
                   
                }
            }
        }

        private void ButtonDelete_OnClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить событие?", "Удаление записи", MessageBoxButton.OKCancel,
                MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                RemoveSelectedEventFromCalendar();
            }
        }
    }
}