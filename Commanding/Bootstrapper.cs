﻿using Microsoft.Practices.Unity;
using Prism.Unity;
using Commanding.Views;
using System.Windows;
using Prism.Modularity;

namespace Commanding
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
            moduleCatalog.AddModule(typeof(OrderModule.OrderModule));
        }
    }
}
