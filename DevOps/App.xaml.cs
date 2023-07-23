using Autofac;
using DevOps.Modules;
using DevOps.Views;
using System.Windows;

namespace DevOps
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IContainer _container { get; set; }

        public App() 
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<TestModule>();
            builder.RegisterModule<WindowModule>();
            builder.RegisterModule<MainModule>();
            _container = builder.Build();
        }

        private void App_OnStartUp(object sender, StartupEventArgs e) 
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var mainWindow = scope.Resolve<MainWindow>();
                mainWindow.Show();
            }
        }
    }
}
