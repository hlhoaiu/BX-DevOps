using Autofac;
using Autofac.Core;
using DevOps.Managers;
using DevOps.Modules;
using DevOps.Services.Test;
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
        private readonly ITestService _testService;
        private readonly IConfigManager _configManager;

        public MainWindow(
            ITestService testService, 
            IConfigManager configManager)
        {
            _testService = testService;
            _configManager = configManager;

            InitializeComponent();

            Test();
        }

        private void Test()
        {
            var testStr = "123";
            testStr = _testService.TestString();
            textElement1.Text = testStr;

            _configManager.Retrieve();
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
