﻿using System.Windows;

namespace View
{
    public partial class App : Application
    {
        public App()
        {
            var setupWindow = new MainWindow();
            setupWindow.Show();
        }
    }
}
