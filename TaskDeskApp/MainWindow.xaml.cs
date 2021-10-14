using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Xml.Linq;
using DataBaseLib;

namespace TaskDeskApp
{
    public partial class MainWindow : Window
    {
        /*private readonly ObservableCollection<TaskData> _collection;
        private readonly ObservableCollection<TaskData> _collection1;*/

        private readonly ObservableCollection<ObservableCollection<TaskData>> _collection2;


        public MainWindow()
        {
            InitializeComponent();
            /*
            _collection = new ObservableCollection<TaskData>
            {
                new() { Id = 1, EventName = "Событие 1", EventDetail = "" },
                new() { Id = 2, EventName = "Событие 2", EventDetail = "" },
                new() { Id = 3, EventName = "Событие 3", EventDetail = "" }
            };
            _collection1 = new ObservableCollection<TaskData>
            {
                new() { Id = 2, EventName = "Событие 11", EventDetail = "" },
                new() { Id = 2, EventName = "Событие 21", EventDetail = "" },
                new() { Id = 3, EventName = "Событие 31", EventDetail = "" }
            };
            _collection2 = new ObservableCollection<ObservableCollection<TaskData>>();
            _collection2.Add(_collection);
            _collection2.Add(_collection1);
            for (int i = 2; i < 30; i++)
            {
                _collection2.Add(new ObservableCollection<TaskData>());
            }
            */

            UserSelecedDate.SelectedDate = DateTime.Now;
            //PushListViewIntoGrid(2, 2, Calendar, _collection);
            CalendarReDraw(_collection2);
        }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var temp = GetDayofWeek(2021, 10);
            var temp2 = GetColumnInCalendarFirsDayOfMonth(2021, 10);
            MessageBox.Show($"Первый день месяца {temp} // {temp2}");
            CreateTask createTask = new CreateTask();
            createTask.Show();
        }

        public void CalendarReDraw(ObservableCollection<ObservableCollection<TaskData>> eventsMonthCollection)
        {
            DateTime selecedDate = new DateTime();
            //selecedDate = DateTime.Now;
            //var selectedDate = UserSelecedDate.SelectedDate.Value.Date.Year;
            //TODO Обратить ОСОБОЕ внимание!!!
            var startColumn = GetColumnInCalendarFirsDayOfMonth((int)UserSelecedDate.SelectedDate.Value.Date.Year,
                UserSelecedDate.SelectedDate.Value.Date.Month);
            var index = 0;
            for (int row = 1; row < 5; row++)
            {
                for (int column = 0; column < 6; column++)
                {
                    if (row == 1 && column < startColumn)
                    {
                        continue;
                    }
                    else
                    {
                        if (!(eventsMonthCollection[index] == null))
                        {
                            PushListViewIntoGrid(row, column, Calendar, eventsMonthCollection[index]);
                        }

                        index++;
                    }
                }
            }
        }

        private int GetColumnInCalendarFirsDayOfMonth(int Year, int Month)
        {
            var numInWeek = GetDayofWeek(Year, Month);
            switch (numInWeek)
            {
                case int n when (n > 1 && n < 7): return numInWeek - 1;
                case 0: return 6;
                default: return -1;
            }
        }

        private int GetDayofWeek(int Year, int Month)
        {
            DateTime beginningOfMonth = new DateTime(Year, Month, 1);
            return (int)beginningOfMonth.DayOfWeek;
        }

        private void PushListViewIntoGrid(int row, int column, Grid gridname,
            ObservableCollection<TaskData> _OBScollection)
        {
            //var listboxitem = new ListBoxItem().Content = "Событие 1";
            //listView.Items.Add(listboxitem);

            var listView = PushEventIntoListview(_OBScollection);
            RemoveChildElementFromGrid(row, column, gridname);
            Grid.SetRow(listView, row);
            Grid.SetColumn(listView, column);

            gridname.Children.Add(listView);
        }

        private ListView PushEventIntoListview(ObservableCollection<TaskData> _OBScollection)
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
                ItemsSource = _OBScollection,
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
                        for (int i = 0; i < _collection2.Count; i++)
                        {
                            _collection2[i]?.Remove((TaskData)list.SelectedItem);    
                        }
                        
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

/*      private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
           MessageBox.Show("Работает");
           Event2.Items.Add(new ListBoxItem().Content="Задача 2");
        }*/
    }
}