﻿using Client.ViewModel;
using Common.Model;
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
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {

        //public Home()
        //{
        //    InitializeComponent();
        //    DataContext = new HomeVM();
        //}
        
        public Home(User loggedInUser)
        {
            InitializeComponent();
            DataContext = new HomeVM(loggedInUser, this);
        }
    }
}
