using Autofac;
using Harvest.OrchardDevToolbelt.Providers.Weather;
using Harvest.OrchardDevToolbelt.Services;
using Moq;
using NUnit.Framework;
using Orchard.Caching;
using Orchard.Services;
using Orchard.Tests;
using Orchard.Tests.Stubs;

namespace Harvest.OrchardDevToolbelt.Tests.Services
{
    [TestFixture]
    public class WeatherServiceTests : DatabaseEnabledTestsBase
    {

        [Test]
        public void RequestWeather()
        {

            // Arrange
            var service = _container.Resolve<IWeatherService>();
            service.ProviderName = typeof(WundergroundWeatherServiceProvider).Name;

            // Act
            var weather = service.GetTodaysWeather();
            var cachedWeather = service.GetTodaysWeather();

            // Assert
            Assert.NotNull(weather);
            Assert.That(weather.City == "Santa Monica");
            Assert.AreSame(weather, cachedWeather);
        }

        public override void Register(ContainerBuilder builder)
        {
            builder.RegisterInstance(new Mock<IRandomizer>().Object);
            builder.RegisterType<FakeWeatherServiceProvider>().As<IWeatherServiceProvider>();
            builder.RegisterType<WundergroundWeatherServiceProvider>().As<IWeatherServiceProvider>();
            builder.RegisterType<StubCacheManager>().As<ICacheManager>();
            builder.RegisterType<StubClock>().As<IClock>();
            builder.RegisterType<WeatherService>().As<IWeatherService>();
        }
    }
}