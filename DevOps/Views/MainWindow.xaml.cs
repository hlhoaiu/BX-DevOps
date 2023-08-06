using Autofac;
using Autofac.Core;
using DevOps.Helpers;
using DevOps.Logger;
using DevOps.Managers;
using DevOps.Models;
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
        private readonly IDictionary<string, ITemplatePage> _pageMappings;
        private readonly ILogger _logger;

        private IList<ITemplatePage> _pageSequence;

        public MainWindow(
            IConfigPage configPage,
            IBranchPage branchPage,
            IPackagePage packagePage,
            IFormPage formPage,
            IBackupPage backupPage,
            IDeployPage deploymentPage,
            ILogger logger)
        {
            _pageMappings = new Dictionary<string, ITemplatePage>
            {
                { nameof(ConfigPage), configPage },
                { nameof(BranchPage), branchPage },
                { nameof(PackagePage), packagePage },
                { nameof(FormPage), formPage },
                { nameof(BackupPage), backupPage },
                { nameof(DeployPage), deploymentPage }
            };
            _logger = logger;

            InitializeComponent();
            Init();
        }

        private void Init() 
        {
            _pageSequence = PageSequence.Sequence.Select(x => _pageMappings[x]).ToList();

            for (int i = 0; i < _pageSequence.Count; i++)
            {
                var currentPage = _pageSequence[i];

                if (i - 1 >= 0) 
                {
                    var backPage = _pageSequence[i - 1];
                    currentPage.OnBackPage += (x) =>
                    {
                        _logger.Log(x);
                        this.Content = backPage;
                        backPage.Init();
                    };
                }

                if (i + 1 < _pageSequence.Count)
                {
                    var nextPage = _pageSequence[i + 1];
                    currentPage.OnNextPage += (x) =>
                    {
                        _logger.Log(x);
                        this.Content = nextPage;
                        nextPage.Init();
                    };
                }
                else 
                {
                    currentPage.OnNextPage += End;
                }
            }

            var firstPage = _pageSequence.First();
            this.Content = firstPage;
            firstPage.Init();
        }

        private void End(string msg) 
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
