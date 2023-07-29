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
using static DevOps.Views.ConfigPage;
using static System.Net.Mime.MediaTypeNames;

namespace DevOps.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IConfigManager _configManager;
        private readonly IComponentContext _componentContext;

        public MainWindow(
            IConfigManager configManager,
            IComponentContext componentContext)
        {
            _configManager = configManager;
            _componentContext = componentContext;

            InitializeComponent();
            Init();
        }

        private void Init() 
        {
            GoToConfigPage("Init ConfigPage");
        }

        private void GoToConfigPage(string msg) 
        {
            var page = _componentContext.Resolve<ConfigPage>();
            this.Content = page;
            page.onNextPage += new OnNextPage(GoToPage1);
        }

        private void GoToPage1(string msg) 
        {
            //var page = _componentContext.Resolve<ConfigPage>();
            //this.Content = page;
            //page.onNextPage += new OnNextPage(GoToPage1);
        }

        private void TestCommandLine() {
            var command = @"cd ""C:\Users\HIM HO\source\repos\BX-DevOps""&git status";
            CommandLineRunner.Run(command, out var output, out var error);
        }
    }
}
