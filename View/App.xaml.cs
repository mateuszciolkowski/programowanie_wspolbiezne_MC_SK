using System;
using System.Windows;

namespace MyApp
{
    public partial class App : Application
    {
        [STAThread]
        public static void Main()
        {
            var app = new App();
            app.Run(new MainWindow()); // Run the MainWindow as the entry point of the application
        }
    }
}
