﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Utility;
using ViewModel;

namespace BookExchange
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            IOCContainer.InitAutofac();

            VM_WindowMain main = new VM_WindowMain();
            main.Show();

            base.OnStartup(e);
        }
    }
}
