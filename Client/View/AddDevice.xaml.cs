﻿using Client.ViewModel;
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

namespace Client.View
{
    /// <summary>
    /// Interaction logic for AddDevice.xaml
    /// </summary>
    public partial class AddDevice : Window
    {
        public AddDevice(HomeVM homeVM)
        {
            InitializeComponent();
            DataContext = new AddDeviceVM(this, homeVM);
        }
    }
}
