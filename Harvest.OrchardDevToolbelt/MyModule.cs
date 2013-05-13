using System.Reflection;

namespace Harvest.OrchardDevToolbelt {
    public class MyModule {
        public static readonly string Name = Assembly.GetExecutingAssembly().GetName().Name;
    }
}