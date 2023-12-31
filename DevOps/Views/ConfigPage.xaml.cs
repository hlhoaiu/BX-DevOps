﻿using Autofac;
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
        private readonly IFieldConverter _fieldHelpers;

        public Action<string> OnBackPage { get; set; }
        public Action<string> OnNextPage { get; set; }

        private IDictionary<string, TextBox> _cacheTextBoxes = new Dictionary<string, TextBox>();
        private DeployConfig _tempDeployConfig;

        public ConfigPage(
            IConfigManager configManager, 
            IFieldConverter fieldHelpers)
        {
            _configManager = configManager;
            _fieldHelpers = fieldHelpers;
        }

        public void Init()
        {
            InitializeComponent();
            AddOrUpdateFields();
            SetFields();
        }

        private void SetFields()
        {
            XStatus.Text = "Status: Init Success";
            XTitle.Text = PageSequence.NameMapping[this.GetType().Name];
        }

        private void XResetBtn_Click(object sender, RoutedEventArgs e)
        {
            XStatus.Text = "Status: Reset Started";
            _tempDeployConfig = null;
            AddOrUpdateFields();
            XStatus.Text = "Status: Reset Success";
        }

        private void XSaveBtn_Click(object sender, RoutedEventArgs e)
        {
            XStatus.Text = "Status: Save Started";
            var updatedJSONConfig = UpdateTempJSONConfig();
            _configManager.Update(updatedJSONConfig);
            XStatus.Text = "Status: Save Success";
        }

        private void XStartBtn_Click(object sender, RoutedEventArgs e)
        {
            var updatedJSONConfig = UpdateTempJSONConfig();
            _configManager.Update(updatedJSONConfig);
            if (OnNextPage != null) 
            {
                OnNextPage(PageSequence.GetOnNextPageLog(this.GetType().Name));
            }
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e) 
        {
            var tempJSONConfig = UpdateTempJSONConfig();
            var config = _configManager.GetDeployConfig();
            _tempDeployConfig = new DeployConfig(tempJSONConfig, config.RepoLatestHash, config.CurrentDateTime);
            AddOrUpdateFields();
        }

        private void AddOrUpdateFields()
        {
            var deployConfig = _tempDeployConfig == null ? _configManager.GetDeployConfig() : _tempDeployConfig;
            var configStrDict = _fieldHelpers.ToStrDict(deployConfig);
            var deployJSONConfig = _configManager.GetDeployJSONConfig();
            var configJSONStrDict = _fieldHelpers.ToStrDict(deployJSONConfig);
            foreach (var item in configStrDict)
            {
                var isEditable = configJSONStrDict.ContainsKey(item.Key);
                AddOrUpdateField(item.Key, item.Value, isEditable);
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
            var dict = _cacheTextBoxes.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Text);
            return new DeployJSONConfig(dict);
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
