using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace ModuleA
{
    public class ModuleAModule : IModule
    {
        public ModuleAModule(IUnityContainer container, IRegionManager regionManager)
        {
            Container = container;
            RegionManager = regionManager;
        }

        public void Initialize()
        {
            var addFundView = Container.Resolve<Views.AddFundView>();
            RegionManager.Regions["LeftRegion"].Add(addFundView);
        }

        public IUnityContainer Container { get; private set; }
        public IRegionManager RegionManager { get; private set; }
    }
}