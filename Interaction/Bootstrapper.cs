using Microsoft.Practices.Unity;
using Prism.Unity;
using Interaction.Views;
using System.Windows;

namespace Interaction
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
