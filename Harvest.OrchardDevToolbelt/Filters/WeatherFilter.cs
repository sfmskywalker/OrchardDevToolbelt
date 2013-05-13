using System.Web.Mvc;
using Orchard;
using Orchard.Mvc.Filters;

namespace Harvest.OrchardDevToolbelt.Filters {
    public class WeatherFilter : FilterProvider, IResultFilter {
        private readonly IOrchardServices _services;
        public WeatherFilter(IOrchardServices services) {
            _services = services;
        }

        public void OnResultExecuting(ResultExecutingContext filterContext) {
            _services.WorkContext.Layout.AsideSecond.Add(_services.New.Weather());
        }

        public void OnResultExecuted(ResultExecutedContext filterContext) {
        }
    }
}