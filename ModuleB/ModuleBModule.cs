using Microsoft.Practices.Unity;
using ModuleB.Views;
using Prism.Modularity;
using Prism.Regions;

namespace ModuleB
{
    public class ModuleBModule : IModule
    {
        public ModuleBModule(IUnityContainer container, IRegionManager regionManager)
        {
            Container = container;
            RegionManager = regionManager;

            RegisterViewsAndServices();
        }

        private void RegisterViewsAndServices()
        {
            Container.RegisterType<IActivityView, ActivityView>();
        }

        public void Initialize()
        {
            ActivityView activityView1 = Container.Resolve<ActivityView>();
            ActivityView activityView2 = Container.Resolve<ActivityView>();

            activityView1.SetCustomerId("Customer1");
            activityView2.SetCustomerId("Customer2");

            IRegion rightRegion = RegionManager.Regions["RightRegion"];
            rightRegion.Add(activityView1);
            rightRegion.Add(activityView2);
        }

        public IRegionManager RegionManager { get; private set; }

        public IUnityContainer Container { get; private set; }
    }
}