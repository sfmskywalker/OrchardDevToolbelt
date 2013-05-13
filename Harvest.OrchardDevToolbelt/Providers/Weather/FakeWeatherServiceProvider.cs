using Harvest.OrchardDevToolbelt.Services;

namespace Harvest.OrchardDevToolbelt.Providers.Weather {
    public class FakeWeatherServiceProvider : IWeatherServiceProvider {
        private readonly IRandomizer _randomizer;

        public FakeWeatherServiceProvider(IRandomizer randomizer) {
            _randomizer = randomizer;
        }

        public WeatherInfo GetWeatherInfo() {
            var items = new[] {
                new WeatherInfo { City = "Santa Monica", Condition = "Sunny", IconUrl = "~/modules/harvest.orcharddevtoolbelt/content/images/sunny.png", Temperature = "20&deg; C"},
                new WeatherInfo { City = "Santa Monica", Condition = "Rainy", IconUrl = "~/modules/harvest.orcharddevtoolbelt/content/images/rainy.png", Temperature = "15&deg; C"},
                new WeatherInfo { City = "Santa Monica", Condition = "Snowy", IconUrl = "~/modules/harvest.orcharddevtoolbelt/content/images/snowy.png", Temperature = "-10&deg; C"}
            };

            return items[_randomizer.Next(0, items.Length)];
        }
    }
}