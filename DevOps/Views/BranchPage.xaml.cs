using DevOps.Logger;
using DevOps.Managers;
using DevOps.Models;
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
            InitLog();
            RegisterLogUpdatedAction();
            _branchManager.InitProposed();
            SetFields();
        }

        private void InitLog() 
        {
            XLog.Text = $"Init {nameof(BranchPage)}\n" + _logger.CombinedLogs;
            XLog.ScrollToEnd();
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
            XStatus.Text = "Status: Init Success";
            XTitle.Text = PageSequence.NameMapping[this.GetType().Name];
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
                OnBackPage(PageSequence.GetOnBackPageLog(this.GetType().Name));
            }
        }

        private void XExecuteBtn_Click(object sender, RoutedEventArgs e)
        {
            XStatus.Text = "Status: Started to Merge";
            _branchManager.Merge(XSourceBranch.Text, XMergeBranch.Text, XGitDirectory.Text);
            SetFields();
            XStatus.Text = "Status: Merge Success";
        }

        private void XNextBtn_Click(object sender, RoutedEventArgs e)
        {
            if (OnNextPage != null)
            {
                OnNextPage(PageSequence.GetOnNextPageLog(this.GetType().Name));
            }
        }
    }
}
