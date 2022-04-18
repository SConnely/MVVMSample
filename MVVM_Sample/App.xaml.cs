
namespace MVVM_Sample
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Navigation;
    using MVVM_Sample.ViewModel;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Assembly rootAssembly = null;

        public MainViewModel MainViewModel { get; set; } = new MainViewModel();

        public MainWindow MainWindowView { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }

        public void ActivateControl(string name)
        {
            var viewNamespace = $"MVVM_Sample.View.{name}View";
            var viewModelNamespace = $"MVVM_Sample.ViewModel.{name}ViewModel";

            Type[] viewTypes = this.rootAssembly.GetTypes().Where(t => string.Equals(t.FullName, viewNamespace, StringComparison.OrdinalIgnoreCase)).ToArray();
            Type[] viewModelTypes = this.rootAssembly.GetTypes().Where(t => string.Equals(t.FullName, viewModelNamespace, StringComparison.OrdinalIgnoreCase)).ToArray();

            if (viewTypes.Any()
                && viewModelTypes.Any())
            {
                var view = Activator.CreateInstance(viewTypes.First()) as UserControl;
                var viewModel = Activator.CreateInstance(viewModelTypes.First());

                if (view != null)
                {
                    view.DataContext = viewModel;

                    this.MainViewModel.ChildControl = view;
                }
            }
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            this.MainWindow = new MainWindow();
            this.MainWindow.DataContext = this.MainViewModel;

            this.MainWindow.Show();
            
            rootAssembly = Assembly.GetExecutingAssembly();
        }
    }
}
