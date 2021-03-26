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
            //window.Icon = new BitmapImage(new Uri(@"Images\Background.png"));
            ReportsView.CustomerUpdateRequest += ReportsView_CustomerUpdateRequest;
            ReportsView.AddProductRequest += ReportsView_AddProductRequest;
            ReportsView.ShowProdutsRequest += ReportsView_ShowProdutsRequest;
        }

        private void ReportsView_ShowProdutsRequest(long obj)
        {
            MainTabControl.SelectedItem = Products;
        }

        private void ReportsView_AddProductRequest(long obj)
        {
            MainTabControl.SelectedItem = Products;
        }

        private void ReportsView_CustomerUpdateRequest(long obj)
        {
            MainTabControl.SelectedItem = UpdateCustomerDetails;
            CustomerUpdateRequest?.Invoke(obj);
        }
       
    }
}
