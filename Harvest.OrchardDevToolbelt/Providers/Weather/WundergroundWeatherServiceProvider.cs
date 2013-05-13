using System.Net;
using Harvest.OrchardDevToolbelt.Services;
using Newtonsoft.Json.Linq;

namespace Harvest.OrchardDevToolbelt.Providers.Weather {
    public class WundergroundWeatherServiceProvider : IWeatherServiceProvider {
        public WeatherInfo GetWeatherInfo() {
            var client = new WebClient();
            var responseText = client.DownloadString(string.Format("http://api.wunderground.com/api/{0}/conditions/q/CA/Santa_Monica.json", SecretKeys.WundergroundWeatherKey));
            dynamic responseData = JToken.Parse(responseText);
            var currentObservation = responseData.current_observation;

            return new WeatherInfo {
                City = currentObservation.display_location.city,
                Condition = currentObservation.weather,
                Temperature = currentObservation.temperature_string,
                IconUrl = currentObservation.image.url
            };
        }
    }
}