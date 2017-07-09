using Microsoft.Practices.Unity;
using Prism.Unity;
using StateBasedNavigation.Views;
using System.Windows;

namespace StateBasedNavigation
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
    }
}
