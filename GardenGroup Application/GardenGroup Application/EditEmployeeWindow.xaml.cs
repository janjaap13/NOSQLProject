﻿using System;
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
using GardenGroupModel;
using GardenGroupLogic;
using GardenGroup_Application.WindowBaseClasses;

namespace GardenGroup_Application
{
    /// <summary>
    /// Interaction logic for EditEmployee.xaml
    /// </summary>
    public partial class EditEmployee : EmployeeWindowBase
    {
        public EditEmployee(User user) : base(user)
        {
            InitializeComponent();
            this.UserService = UserService.GetInstance();
        }
    }
}
