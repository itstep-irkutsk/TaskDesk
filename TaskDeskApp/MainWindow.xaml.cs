using System.Windows;
using DataBaseLib;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Specialized;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Controls;

namespace TaskDeskApp
{
    public partial class MainWindow : Window
    {
        public Month Month1 { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            //DBLib DB = new();
            //DBTask task = new DBTask() { _id = 0, _name = "name55", _description = "desc55", _creation_date = "crdate55", _execution_date = "execdate55", _priority = 1, _status = 0 };
            //DB.LoadFromDB();
            //int TaskId = DB.AddTask(task).Result;

            //DB.EditTask(3, task);
            //DB._tasks.CollectionChanged
            //var foo = "sdsd";
            /*            Month1 = new()
                        {
                            weeks = new()
                            {
                                new Week() { day1 = "понедельник", day2 = "вторник", day3 = "среда", day4 = "четверг", day5 = "пятница", day6 = "суббота", day7 = "воскресенье"},
                                new Week() { day1 = "понедельник", day2 = "вторник", day3 = "среда", day4 = "четверг", day5 = "пятница", day6 = "суббота", day7 = "воскресенье" },
                                new Week() { day1 = "понедельник", day2 = "вторник", day3 = "среда", day4 = "четверг", day5 = "пятница", day6 = "суббота", day7 = "воскресенье" },
                                new Week() { day1 = "понедельник", day2 = "вторник", day3 = "среда", day4 = "четверг", day5 = "пятница", day6 = "суббота", day7 = "воскресенье" },
                                new Week() { day1 = "понедельник", day2 = "вторник", day3 = "среда", day4 = "четверг", day5 = "пятница", day6 = "суббота", day7 = "воскресенье" },
                                new Week() { day1 = "понедельник", day2 = "вторник", day3 = "среда", day4 = "четверг", day5 = "пятница", day6 = "суббота", day7 = "воскресенье" },
                            }
                        };*/
            Month1 = new();
            //dataGrid1.DataContext = Month1.Weeks;

            //DataContext = Month.Weeks;
            //listView1.ItemsSource = Month.Weeks;

            //listView1.
            //this.DataContext = Month;
        }

        private void buttonNextMonth_Click(object sender, RoutedEventArgs e)
        {
            Month1.NextMonth(1);
        }

        private void buttonPrevMonth_Click(object sender, RoutedEventArgs e)
        {
            Month1.NextMonth(-1);
        }

        private void dataGrid1_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            dataGrid1.RowHeight = (dataGrid1.ActualHeight - dataGrid1.ColumnHeaderHeight - 4) / 6;
            //MessageBox.Show(((double)dataGrid1.ColumnHeaderHeight).ToString());
            //DataGrid tempDataGrid = (DataGrid)sender;
            //tempDataGrid.RowHeight = tempDataGrid.Height +50;
            //MessageBox.Show($"{dataGrid1.ActualHeight}, {dataGrid1.ColumnHeaderHeight}");

        }
    }
    /*    public class Month
        {
            public List<Week> weeks;
        }*/
    public class Month
    {
        public ObservableCollection<Week> Weeks { get; set; }
        private DateTime _currentMonth;
        public bool IsInstanceOfcurrentMonth { get; set; }
        public Month()
        {
            _currentMonth = DateTime.Now;
            Weeks = new();
            for (int weekIndex = 0; weekIndex < 6; weekIndex++)
            {
                Week week = new()
                {
                    Monday = Init(weekIndex, 0),
                    Tuesday = Init(weekIndex, 1),
                    Wednesday = Init(weekIndex, 2),
                    Thursday = Init(weekIndex, 3),
                    Friday = Init(weekIndex, 4),
                    Saturday = Init(weekIndex, 5),
                    Sunday = Init(weekIndex, 6)
                };
                //MessageBox.Show( $"Неделя: {weekIndex.ToString()}" );
                Weeks.Add(week);
            }
        }
        public void NextMonth(int monthDifference)
        {
            //Weeks = new();
            _currentMonth = _currentMonth.AddMonths(monthDifference);
            Weeks.Clear();
            for (int weekIndex = 0; weekIndex < 6; weekIndex++)
            {
                Week week = new()
                {
                    Monday = Init(weekIndex, 0),
                    Tuesday = Init(weekIndex, 1),
                    Wednesday = Init(weekIndex, 2),
                    Thursday = Init(weekIndex, 3),
                    Friday = Init(weekIndex, 4),
                    Saturday = Init(weekIndex, 5),
                    Sunday = Init(weekIndex, 6)
                };
                Weeks.Add(week);
                //MessageBox.Show($"Неделя: {weekIndex.ToString()}");
            };

        }
        private int Init(int week, int day)
        {
            //DateTime now = DateTime.Now;
            int firstDay = dayConverter(new DateTime(_currentMonth.Year, _currentMonth.Month, 1).DayOfWeek);
            int lastDay = dayConverter(new DateTime(_currentMonth.Year, _currentMonth.Month, 1).AddMonths(1).AddDays(-1).DayOfWeek);

            //MessageBox.Show(  $"День календаря: { new DateTime(now.Year, now.Month, day).AddDays(-firstDay - week + 2).ToString()} , День недели: { day }");

            return new DateTime(_currentMonth.Year, _currentMonth.Month, day + 1).AddDays((week * 7) - firstDay).Day;
        }
        private int dayConverter(DayOfWeek day)
        {
            return (int)day switch
            {
                1 => 0,
                2 => 1,
                3 => 2,
                4 => 3,
                5 => 4,
                6 => 5,
                0 => 6,
                _ => -1,
            };
        }

    }
    public class Week
    {
        public int Monday { get; set; }
        public int Tuesday { get; set; }
        public int Wednesday { get; set; }
        public int Thursday { get; set; }
        public int Friday { get; set; }
        public int Saturday { get; set; }
        public int Sunday { get; set; }
    }
    /*    public class WithPercentageConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {

                return ((DateTime)value).ToString("dd.MM.yyyy");
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return DependencyProperty.UnsetValue;
            }
        }*/
    public class RowHeightConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            //dataGrid1.RowHeight = (dataGrid1.ActualHeight - dataGrid1.ColumnHeaderHeight - 2) / 6;
            //if (value == null) return null;
            //DataGrid dataGrid = value as DataGrid;
            //MessageBox.Show($"{values[0].ToString()}, {values[1].ToString()}");
            if (((double)values[0] - (double)values[1] - 2) / 6 < 0)
            {
                return 1;
            } else return ((double)values[0] - (double)values[1] - 4) / 6;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}