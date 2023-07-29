using Autofac;
using DevOps.Helpers;
using DevOps.Managers;
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
    /// <summary>
    /// Interaction logic for ConfigPage.xaml
    /// </summary>
    public partial class ConfigPage : Page
    {
        private readonly IConfigManager _configManager;

        public delegate void OnNextPage(string msg);
        public event OnNextPage onNextPage;


        public ConfigPage(
            IConfigManager configManager)
        {
            _configManager = configManager;

            InitializeComponent();
            Init();
        }

        private void Init()
        {
            var deployConfig = _configManager.Retrieve();
            var configDict = ToDict(deployConfig);
            foreach (var item in configDict)
            {
                var fieldType = item.Value?.GetType();
                if (fieldType != null)
                {
                    if (fieldType == typeof(string))
                    {
                        var itemStr = item.Value?.ToString() ?? string.Empty;
                        AddField(item.Key, itemStr, true);
                    }
                    else if (fieldType == typeof(DateTime))
                    {
                        var itemStr = item.Value == null ? 
                                            string.Empty : 
                                            ((DateTime)item.Value).ToString("yyyyMMddhhmm");
                        AddField(item.Key, itemStr, false);
                    }
                    else 
                    {
                    }
                }
            }

        }
        private IDictionary<string, object?> ToDict(object config)
        {
            var configProp = config.GetType().GetProperties();
            var configDict = configProp.ToDictionary(x => x.Name, y => y.GetValue(config, null));
            return configDict;
        }

        private void XClearBtn_Click(object sender, RoutedEventArgs e)
        {
            AddField("xxxx", "xxxxxxxx", true);
        }

        private void XSaveBtn_Click(object sender, RoutedEventArgs e)
        {
            AddField("xxxx", "xxxxxxxx", false);
        }

        private void XStartBtn_Click(object sender, RoutedEventArgs e)
        {
            onNextPage("From ConfigPage to next");
        }

        private void tempSelectableTextBox() 
        {
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

        private void AddField(string fieldName, string fieldValue, bool isEditable)
        {
            var fieldText = new TextBlock()
            {
                Text = $"{fieldName}:",
                FontSize = 16,
                Padding = new Thickness(5, 0, 5, 0),
                VerticalAlignment = VerticalAlignment.Center
            };

            var fieldTextBox = new TextBox()
            {
                Text = fieldValue,
                FontSize = 16,
                Padding = new Thickness(5, 0, 5, 0),
                Margin = new Thickness(2, 2, 2, 2),
                VerticalAlignment = VerticalAlignment.Center,
                IsReadOnly = !isEditable,
                Background = isEditable ? Brushes.White : Brushes.LightGray
            };

            var fieldPanel = new DockPanel();
            fieldPanel.Height = 30;
            var rowDef = new RowDefinition();
            rowDef.Height = GridLength.Auto;
            XFieldGrid.RowDefinitions.Add(rowDef);
            Grid.SetRow(fieldPanel, XFieldGrid.RowDefinitions.Count - 1);
            fieldPanel.Children.Add(fieldText);
            fieldPanel.Children.Add(fieldTextBox);
            XFieldGrid.Children.Add(fieldPanel);
        }
    }
}
