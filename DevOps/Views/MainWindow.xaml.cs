using Autofac;
using Autofac.Core;
using DevOps.Helpers;
using DevOps.Managers;
using DevOps.Modules;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DevOps.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IConfigManager _configManager;

        public MainWindow( 
            IConfigManager configManager)
        {
            _configManager = configManager;

            InitializeComponent();

            Test();
        }

        private void Test()
        {
            var aa = _configManager.Retrieve();
            var bb = _configManager.Update();
            var command = @"cd ""C:\Users\HIM HO\source\repos\BX-DevOps""&git status";
            CommandLineHelpers.Run(command, out var output, out var error);
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            var aa = new TextBlock();
            aa.Text = "hey1";
            aa.FontSize = 20;
            aa.Margin = new Thickness(Left);
            mainGrid.Children.Add(aa);
            Show();
        }
    }
}
