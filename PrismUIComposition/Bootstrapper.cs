using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Unity;
using PrismUIComposition.Views;
using System.Windows;

namespace PrismUIComposition
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            var moduleCatalog = (ModuleCatalog)ModuleCatalog;
            moduleCatalog.AddModule(typeof(EmployeeModule.EmployeeModule));
        }
    }
}
