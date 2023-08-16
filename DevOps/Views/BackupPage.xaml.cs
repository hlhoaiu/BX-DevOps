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
    public interface IBackupPage : ITemplatePage
    {
    }
    /// <summary>
    /// Interaction logic for BackupPage.xaml
    /// </summary>
    public partial class BackupPage : Page, IBackupPage
    {
        private readonly ILogger _logger;
        private readonly IBackupManager _backupManager;

        public BackupPage(
            ILogger logger, 
            IBackupManager backupManager)
        {
            _logger = logger;
            _backupManager = backupManager;
        }

        public Action<string> OnBackPage { get; set; }
        public Action<string> OnNextPage { get; set; }

        public void Init()
        {
            InitializeComponent();
            InitLog();
            RegisterLogUpdatedAction();
            SetFields();
        }

        private void InitLog() 
        {
            XLog.Text = $"Init {nameof(BackupPage)}\n" + _logger.CombinedLogs;
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
            var result = MessageBox.Show("Have you disabled task scheduler in production?", "Reminder", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.No) return;
            XStatus.Text = "Status: Backup Started";
            _backupManager.Backup();
            XStatus.Text = "Status: Backup Success";
            XLog.ScrollToEnd();
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
