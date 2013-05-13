using System;
using System.Collections.Generic;
using System.Linq;
using Harvest.OrchardDevToolbelt.Providers.Weather;
using Orchard;
using Orchard.Caching;
using Orchard.Services;

namespace Harvest.OrchardDevToolbelt.Services {
    public interface IWeatherService : IDependency {
        string ProviderName { get; set; }
        WeatherInfo GetTodaysWeather();
    }

    public class WeatherService : Component, IWeatherService {
        private readonly IEnumerable<IWeatherServiceProvider> _providers;
        private readonly ICacheManager _cacheManager;
        private readonly IClock _clock;
        public string ProviderName { get; set; }
        
        public WeatherService(IEnumerable<IWeatherServiceProvider> providers, ICacheManager cacheManager, IClock clock) {
            _providers = providers;
            _cacheManager = cacheManager;
            _clock = clock;
            ProviderName = typeof (WundergroundWeatherServiceProvider).Name;
        }

        public WeatherInfo GetTodaysWeather() {
            return _cacheManager.Get("CurrentWeatherInfo", context => {
                context.Monitor(_clock.When(TimeSpan.FromHours(4)));
                var provider = _providers.FirstOrDefault(x => x.GetType().Name == ProviderName);

                if (provider == null)
                    throw new OrchardException(T("No provider of type {0} could be found", ProviderName));

                return provider.GetWeatherInfo();
            });
        }
    }

    public interface IWeatherServiceProvider : IDependency {
        WeatherInfo GetWeatherInfo();
    }

    public class WeatherInfo {
        public string City { get; set; }
        public string Condition { get; set; }
        public string Temperature { get; set; }
        public string IconUrl { get; set; }
    }
}