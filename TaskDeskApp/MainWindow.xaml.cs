using System.Windows;

namespace TaskDeskApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Create_OnSelected(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Работает");
        }
    }
}