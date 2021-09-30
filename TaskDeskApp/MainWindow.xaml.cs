using System.Windows;
using System.Windows.Controls;

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
           Event2.Items.Add(new ListBoxItem().Content="Задача 2");
        }
    }
}