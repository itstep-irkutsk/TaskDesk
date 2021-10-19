using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using DataBaseLib;

namespace TaskDeskApp
{
    /// <summary>
    /// Логика взаимодействия для CreateTask.xaml
    /// </summary>
    public partial class CreateTask : Window
    {
        public CreateTask()
        {
            InitializeComponent();
            Execution_date.SelectedDate = DateTime.Now;
            
           foreach (var status in TaskData.statusDic)
           {
               Status.Items.Add(status.Value);
           }
           foreach (var priority in TaskData.priorityDic)
           {
               Priority.Items.Add(priority.Value);
           }
           
        }

        private void Delete_OnClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
