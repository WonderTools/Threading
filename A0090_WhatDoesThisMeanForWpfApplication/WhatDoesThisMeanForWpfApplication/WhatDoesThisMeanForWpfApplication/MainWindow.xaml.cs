﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace WhatDoesThisMeanForWpfApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            MyText.Text = "started";
            //await Task.Run(JustDoSomeThing);
            await JustDoSomeThing();
            MyText.Text = "done";
        }

        async Task JustDoSomeThing()
        {
            await JustDoSomeThingElse();
        }

        async Task JustDoSomeThingElse()
        {
            Thread.Sleep(5000);
        }
    }
}
