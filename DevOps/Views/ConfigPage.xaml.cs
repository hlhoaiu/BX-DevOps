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
            InitializeComponent();

            _configManager = configManager;

            Test();
            
        }

        private void Test()
        {
            var aa = _configManager.Retrieve();
            var bb = _configManager.Update();

        }

        private void XSaveBtn_Click(object sender, RoutedEventArgs e)
        {
            AddField("xxxx", "xxxxxxxx");
        }

        private void XClearBtn_Click(object sender, RoutedEventArgs e)
        {
            AddField("xxxx", "xxxxxxxx");
        }

        private void XStartBtn_Click(object sender, RoutedEventArgs e)
        {
            onNextPage("From ConfigPage to next");
        }

        private void AddField(string fieldName, string fieldValue)
        {
            var fieldText = new TextBlock()
            {
                Text = $"{fieldName}: ",
                FontSize = 16,
                Padding = new Thickness(2),
                VerticalAlignment = VerticalAlignment.Center
            };
            var fieldTextBox = new TextBox()
            {
                Text = fieldValue,
                FontSize = 16,
                Padding = new Thickness(2),
                Margin = new Thickness(2),
                VerticalAlignment = VerticalAlignment.Center
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
