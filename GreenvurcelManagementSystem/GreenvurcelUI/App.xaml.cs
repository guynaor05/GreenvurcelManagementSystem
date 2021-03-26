using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GreenvurcelUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var comException = e.Exception as System.Runtime.InteropServices.COMException;

            if (comException != null && comException.ErrorCode == -2147221040)
                e.Handled = true;
        }

        private void DataGridCell_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            DataGridCell cell = sender as DataGridCell;
            if (cell.IsEditing == false &&
                ((Keyboard.Modifiers & ModifierKeys.Control) != ModifierKeys.Control)) //So that Ctrl+C keeps working
            {
                cell.IsEditing = true;
                e.Handled = true;
            }
        }  
    }
}
