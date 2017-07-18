using Microsoft.Practices.Unity;
using Prism.Unity;
using EventAggregation.Views;
using System.Windows;
using Prism.Modularity;
using ModuleA;
using ModuleB;

namespace EventAggregation
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

        protected override void InitializeModules()
        {
            base.InitializeModules();

            IModule moduleA = Container.Resolve<ModuleAModule>();
            IModule moduleB = Container.Resolve<ModuleBModule>();

            moduleA.Initialize();
            moduleB.Initialize();
        }
    }
}
