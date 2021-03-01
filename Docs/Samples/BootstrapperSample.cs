namespace Samples
{
    using UnityStarterKit.Unity;

    public class RunOfRiverBootstrapper : Bootstrapper
    {
        protected override void RegisterServices(IUnityServiceContainerBuilder builder)
        {
            // call default constructor
            builder.AddService<IServiceSample, ServiceSample>();
            // or call another constructor with specific arguments
            //builder.AddService<IServiceSample>(() => new ServiceSample("Another name"));
        }
    }
}