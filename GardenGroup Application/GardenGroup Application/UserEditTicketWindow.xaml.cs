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
using GardenGroup_Application.UserControls;
using GardenGroupLogic;
using GardenGroupModel;

namespace GardenGroup_Application
{
    /// <summary>
    /// Interaction logic for UserEditTicketWindow.xaml
    /// </summary>
    public partial class UserEditTicketWindow : TicketWindowBase
    {
        public UserEditTicketWindow(Ticket ticket) : base()
        {
            InitializeComponent();
            this.ticket = ticket;
            this.titleText = "Edit ticket";
        }
    }
}
