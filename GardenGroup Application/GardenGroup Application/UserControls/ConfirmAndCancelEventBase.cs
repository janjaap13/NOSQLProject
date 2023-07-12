using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GardenGroup_Application.UserControls
{
    public class ConfirmAndCancelEventBase : UserControl
    {
        public event RoutedEventHandler ConfirmButtonClicked;
        public event RoutedEventHandler CancelButtonClicked;

        public void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (ConfirmButtonClicked != null)
            {
                ConfirmButtonClicked(this, new RoutedEventArgs());
            }
            Window.GetWindow(this).Close();
        }

        public void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (CancelButtonClicked != null)
            {
                CancelButtonClicked(this, new RoutedEventArgs());
            }
        }
    }
}
