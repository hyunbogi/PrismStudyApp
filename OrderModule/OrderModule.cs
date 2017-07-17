using Microsoft.Practices.Unity;
using OrderModule.Services;
using OrderModule.Views;
using Prism.Modularity;
using Prism.Regions;

namespace OrderModule
{
    public class OrderModule : IModule
    {
        private IRegionManager _regionManager;
        private IUnityContainer _container;

        public OrderModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _container.RegisterType<IOrdersRepository, OrdersRepository>(new ContainerControlledLifetimeManager());

            _regionManager.RegisterViewWithRegion(
                "MainRegion",
                () => _container.Resolve<OrdersEditorView>());

            _regionManager.RegisterViewWithRegion(
                "GlobalCommandsRegion",
                () => _container.Resolve<OrdersToolBar>());
        }
    }
}