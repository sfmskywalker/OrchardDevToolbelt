using Harvest.OrchardDevToolbelt.Services;
using Orchard.DisplayManagement.Implementation;

namespace Harvest.OrchardDevToolbelt.Shapes {
    public class WeatherShapes : ShapeDisplayEvents {
        private readonly IWeatherService _weatherService;

        public WeatherShapes(IWeatherService weatherService) {
            _weatherService = weatherService;
        }

        public override void Displaying(ShapeDisplayingContext context) {
            if (context.ShapeMetadata.Type != "Weather")
                return;

            context.Shape.WeatherInfo = _weatherService.GetTodaysWeather();
        }

        public override void Displayed(ShapeDisplayedContext context) {
        }
    }
}