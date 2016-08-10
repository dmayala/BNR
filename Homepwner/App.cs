using Homepwner.Stores;
using Microsoft.Practices.Unity;

namespace Homepwner
{
    public static class App
    {
        public static UnityContainer Container { get; set; }

        public static void Initialize()
        {
            Container = new UnityContainer();
            Container.RegisterInstance(new ItemStore());
        }
    }
}
