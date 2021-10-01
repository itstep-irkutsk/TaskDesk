using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Xml.Linq;

namespace TaskDeskApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Работает");
            PushListBoxIntoGrid(1, 1, Calendar);
            //Event2.Items.Add(new ListBoxItem().Content="Задача 2");
        }

        private void PushListBoxIntoGrid(int row, int column, Grid gridname)
        {
            var listBox = new ListBox
            {
                //Foreground = new Brushes(BlurEffect.RadiusProperty.);
                Margin = new Thickness(2),
                Padding = new Thickness(1),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top
            };
            var listboxitem = new ListBoxItem().Content = "Событие 1";
            listBox.Items.Add(listboxitem);

            RemoveChildElementFromGrid(row,column,gridname);
            Grid.SetRow(listBox, row);
            Grid.SetColumn(listBox, column);

            gridname.Children.Add(listBox);
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

        private void ButtonDelete_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (var obj  in Calendar.Children)
            {
                if (obj is ListBox)
                {
                    (obj as ListBox).Items.Remove((obj as ListBox).SelectedItem);
                }
                
            }
        }
    }
}