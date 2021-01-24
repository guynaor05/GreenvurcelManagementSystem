using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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

namespace GreenvurcelUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static event Action<long> CustomerUpdateRequest;
        public MainWindow()
        {
            InitializeComponent();
            ReportsView.CustomerUpdateRequest += ReportsView_CustomerUpdateRequest;
        }

        private void ReportsView_CustomerUpdateRequest(long obj)
        {
            MainTabControl.SelectedIndex = 5;
            CustomerUpdateRequest?.Invoke(obj);
        }
    }
}
