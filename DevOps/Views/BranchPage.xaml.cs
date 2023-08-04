using DevOps.Logger;
using DevOps.Managers;
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
    public interface IBranchPage : ITemplatePage
    {
    }
    /// <summary>
    /// Interaction logic for BranchPage.xaml
    /// </summary>
    public partial class BranchPage : Page, IBranchPage
    {
        private readonly ILogger _logger;
        private readonly IBranchManager _branchManager;

        public BranchPage(
            ILogger logger, 
            IBranchManager branchManager)
        {
            _logger = logger;
            _branchManager = branchManager;
        }

        public Action<string> OnBackPage { get; set; }
        public Action<string> OnNextPage { get; set; }

        public void Init()
        {
            InitializeComponent();
            RegisterLogUpdatedAction();
            _branchManager.InitProposed();
            SetFields();
        }

        private void RegisterLogUpdatedAction() 
        {
            _logger.OnErrorUpdated -= OnLogUpdated;
            _logger.OnLogUpdated -= OnLogUpdated;
            _logger.OnErrorUpdated += OnLogUpdated;
            _logger.OnLogUpdated += OnLogUpdated;
        }

        private void SetFields() 
        {
            XGitDirectory.Text = _branchManager.ProposedGitDirectory;
            XSourceBranch.Text = _branchManager.ProposedSourceBranch;
            XMergeBranch.Text = _branchManager.ProposedTargetBranch;
        }

        private void OnLogUpdated(string msg)
        {
            XLog.Text += "\n" + msg;
        }

        private void XBackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (OnBackPage != null)
            {
                OnBackPage($"Back from {nameof(this.Name)}");
            }
        }

        private void XExecuteBtn_Click(object sender, RoutedEventArgs e)
        {
            _branchManager.Merge(XSourceBranch.Text, XMergeBranch.Text, XGitDirectory.Text);
            SetFields();
        }

        private void XNextBtn_Click(object sender, RoutedEventArgs e)
        {
            if (OnNextPage != null)
            {
                OnNextPage($"From {nameof(this.Name)} to next");
            }
        }
    }
}
