using PureMVC.Patterns;
using PureMVC.Patterns.Facade;

namespace UnityGameFramework
{
    class UnityGameFrameworkFacade:Facade
    {
        private static UnityGameFrameworkFacade _instance = null;

        private UnityGameFrameworkFacade():base("UnityGameFrameworkFacade") { }

        public static UnityGameFrameworkFacade Get()
        {
            if (_instance == null)
            {
                _instance = new UnityGameFrameworkFacade();
            }
            return _instance;
        }
    }
}
