using Autofac;
using Harvest.OrchardDevToolbelt.Services;

namespace Harvest.OrchardDevToolbelt.Autofac {
    public class CustomAutofacModule : Module {
        protected override void Load(ContainerBuilder builder) {
            builder.RegisterType<ContactFormService>().As<IContactFormService>().InstancePerLifetimeScope();
        }
    }
}