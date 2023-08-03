using Autofac;
using DevOps.Helpers;
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
using static DevOps.Views.ConfigPage;

namespace DevOps.Views
{
    public interface IConfigPage : ITemplatePage 
    {
    }

    /// <summary>
    /// Interaction logic for ConfigPage.xaml
    /// </summary>
    public partial class ConfigPage : Page, IConfigPage
    {
        private readonly IConfigManager _configManager;

        public Action<string> OnBackPage { get; set; }
        public Action<string> OnNextPage { get; set; }

        private IDictionary<string, TextBox> _cacheTextBoxes = new Dictionary<string, TextBox>();
        private DeployConfig _tempDeployConfig;

        public ConfigPage(
            IConfigManager configManager)
        {
            _configManager = configManager;
        }

        public void Init()
        {
            InitializeComponent();
            AddOrUpdateFields();
            XStatus.Text = "Init Success";
        }

        private void XResetBtn_Click(object sender, RoutedEventArgs e)
        {
            XStatus.Text = "Reset Started";
            _tempDeployConfig = null;
            AddOrUpdateFields();
            XStatus.Text = "Reset Success";
        }

        private void XSaveBtn_Click(object sender, RoutedEventArgs e)
        {
            XStatus.Text = "Save Started";
            var updatedJSONConfig = UpdateTempJSONConfig();
            _configManager.Update(updatedJSONConfig);
            XStatus.Text = "Save Success";
        }

        private void XStartBtn_Click(object sender, RoutedEventArgs e)
        {
            if (OnNextPage != null) 
            {
                OnNextPage($"From {this.Name} to next");
            }
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e) 
        {
            var tempJSONConfig = UpdateTempJSONConfig();
            var latestHash = _configManager.GetDeployConfig().RepoLatestHash;
            _tempDeployConfig = new DeployConfig(tempJSONConfig, latestHash);
            AddOrUpdateFields();
        }

        private void AddOrUpdateFields()
        {
            var deployConfig = _tempDeployConfig == null ? _configManager.GetDeployConfig() : _tempDeployConfig;
            var configDict = FieldHelpers.ToDict(deployConfig);
            var deployJSONConfig = _configManager.GetDeployJSONConfig();
            var configJSONDict = FieldHelpers.ToDict(deployJSONConfig);
            foreach (var item in configDict)
            {
                var isEditable = configJSONDict.ContainsKey(item.Key);
                switch (item.Value)
                {
                    case string x:
                        var itemStr = x.ToString();
                        AddOrUpdateField(item.Key, itemStr, isEditable);
                        break;
                    case DateTime x:
                        var dateStr = x.ToString(CommonConst.DateTimeFormat);
                        AddOrUpdateField(item.Key, dateStr, isEditable);
                        break;
                    case IEnumerable<string> x:
                        var combinedStr = string.Join('+', x);
                        AddOrUpdateField(item.Key, combinedStr, isEditable);
                        break;
                    default:
                        AddOrUpdateField(item.Key, string.Empty, isEditable);
                        break;
                }
            }
        }

        private void AddOrUpdateField(string fieldName, string fieldValue, bool isEditable) 
        {
            if (_cacheTextBoxes.TryGetValue(fieldName, out var textBox))
            {
                if (textBox.Text != fieldValue) 
                {
                    textBox.Text = fieldValue;
                }
                return;
            }
            AddField(fieldName, fieldValue, isEditable);
        }

        private void AddField(string fieldName, string fieldValue, bool isEditable)
        {
            var fieldText = new TextBlock()
            {
                Text = $"{fieldName}:",
                FontSize = 14,
                Padding = new Thickness(5, 0, 5, 0),
                VerticalAlignment = VerticalAlignment.Center
            };

            var fieldTextBox = new TextBox()
            {
                Name = $"XTextBox_{fieldName}",
                Text = fieldValue,
                FontSize = 14,
                Padding = new Thickness(5, 0, 5, 0),
                Margin = new Thickness(1, 1, 1, 1),
                VerticalAlignment = VerticalAlignment.Center,
                IsReadOnly = !isEditable,
                Background = isEditable ? Brushes.White : Brushes.LightGray
            };
            fieldTextBox.TextChanged += OnTextChanged;

            _cacheTextBoxes.Add(fieldName, fieldTextBox);

            var fieldPanel = new DockPanel();
            fieldPanel.Height = 26;
            var rowDef = new RowDefinition();
            rowDef.Height = GridLength.Auto;
            XFieldGrid.RowDefinitions.Add(rowDef);
            Grid.SetRow(fieldPanel, XFieldGrid.RowDefinitions.Count - 1);
            fieldPanel.Children.Add(fieldText);
            fieldPanel.Children.Add(fieldTextBox);
            XFieldGrid.Children.Add(fieldPanel);
        }

        private DeployJSONConfig UpdateTempJSONConfig()
        {
            return new DeployJSONConfig()
            {
                ReleaseBranchName = _cacheTextBoxes[nameof(DeployConfig.ReleaseBranchName)].Text,
                TargetNugetPath = _cacheTextBoxes[nameof(DeployConfig.TargetNugetPath)].Text,
                PackageBasePath = _cacheTextBoxes[nameof(DeployConfig.PackageBasePath)].Text,
                JobIds = _cacheTextBoxes[nameof(DeployConfig.JobIds)].Text.Split('+'),
                RepoPreviousMergeHash = _cacheTextBoxes[nameof(DeployConfig.RepoPreviousMergeHash)].Text,
                ProgramName = _cacheTextBoxes[nameof(DeployConfig.ProgramName)].Text,
                NugetRepoName = _cacheTextBoxes[nameof(DeployConfig.NugetRepoName)].Text,
                ProgramCompiledPath = _cacheTextBoxes[nameof(DeployConfig.ProgramCompiledPath)].Text,
                CustomPackageBackUpPaths = new string[] { _cacheTextBoxes[nameof(DeployConfig.CustomPackageBackUpPaths)].Text },
                ProductionProgramBasePath = _cacheTextBoxes[nameof(DeployConfig.ProductionProgramBasePath)].Text
            };
        }

        //var fieldText = new TextBox()
        //{
        //    Text = $"{fieldName}:",
        //    FontSize = 16,
        //    Padding = new Thickness(5,0,5,0),
        //    VerticalAlignment = VerticalAlignment.Center,
        //    Background = Brushes.Transparent,
        //    BorderThickness = new Thickness(0),
        //    IsReadOnly = true
        //};
    }
}
