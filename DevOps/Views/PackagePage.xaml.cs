﻿using DevOps.Logger;
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
    public interface IPackagePage : ITemplatePage
    {
    }
    /// <summary>
    /// Interaction logic for PackagePage.xaml
    /// </summary>
    public partial class PackagePage : Page, IPackagePage
    {
        private readonly ILogger _logger;
        private readonly IDeploymentPackageManager _deploymentPackageManager;

        public PackagePage(
            ILogger logger, 
            IDeploymentPackageManager deploymentPackageManager)
        {
            _logger = logger;
            _deploymentPackageManager = deploymentPackageManager;
        }

        public Action<string> OnBackPage { get; set; }
        public Action<string> OnNextPage { get; set; }

        public void Init()
        {
            InitializeComponent();
            RegisterLogUpdatedAction();
        }

        private void RegisterLogUpdatedAction()
        {
            _logger.OnErrorUpdated -= OnLogUpdated;
            _logger.OnLogUpdated -= OnLogUpdated;
            _logger.OnErrorUpdated += OnLogUpdated;
            _logger.OnLogUpdated += OnLogUpdated;
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
            XStatus.Text = "Release Started";
            _deploymentPackageManager.Release();
            XStatus.Text = "Release Success";
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