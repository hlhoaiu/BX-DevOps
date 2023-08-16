using DevOps.Logger;
using DevOps.Managers;
using DevOps.Models;
using DevOps.Models.Config;
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
    public interface IFormPage : ITemplatePage
    {
    }
    /// <summary>
    /// Interaction logic for FormPage.xaml
    /// </summary>
    public partial class FormPage : Page, IFormPage
    {
        private readonly ILogger _logger;
        private readonly IImplFormManager _implFormManager;
        private readonly IDeployConfigModel _deployConfigModel;

        public FormPage(
            ILogger logger,
            IImplFormManager implFormManager,
            IDeployConfigModel deployConfigModel)
        {
            _logger = logger;
            _implFormManager = implFormManager;
            _deployConfigModel = deployConfigModel;
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
            XLog.Text = $"Init {nameof(FormPage)}\n" + _logger.CombinedLogs;
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
            XDeploymentFormName.Text = _deployConfigModel.GetDeployConfig().DeploymentFormName;
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
            var result = MessageBox.Show("Confirm to generate Implemenation Form?", "Reminder", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.No) return;
            XStatus.Text = "Status: Release Started";
            var fieldsFromUser = new Dictionary<string, string>() 
            {
                { nameof(XExcelTemplates).Substring(1),  XExcelTemplates.Text},
                { nameof(XPreviousFeatures).Substring(1),  XPreviousFeatures.Text},
                { nameof(XNewFeatures).Substring(1),  XNewFeatures.Text}
            };
            _implFormManager.Generate(fieldsFromUser);
            XStatus.Text = "Status: Release Success";
            XLog.ScrollToEnd();
        }

        private void XNextBtn_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Have you check implementation form information are all correct?", "Reminder", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.No) return;
            if (OnNextPage != null)
            {
                OnNextPage(PageSequence.GetOnNextPageLog(this.GetType().Name));
            }
        }
    }
}
