using System.Windows;

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
        }
    }
}