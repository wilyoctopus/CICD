using CICD.WebAPI.Controllers;
using Microsoft.Extensions.Logging;
using System.Linq;
using Xunit;

namespace CICD.UnitTests
{
    public class WeatherForecast
    {
        [Fact]
        public void Get_ReturnsWeatherWithExpectedSummary()
        {
            throw new System.Exception();

            // Arrange
            var expectedSummaries = new string[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
            var weatherForecast = new WeatherForecastController();

            // Act
            var result = weatherForecast.Get();

            // Assert
            Assert.All(result, item => Assert.Contains(item.Summary, expectedSummaries));
        }

        [Fact]
        public void Get_ReturnsTemperatureWithinAcceptedRange()
        {
            // Arrange
            var range = (low: -20, high: 55);
            var weatherForecast = new WeatherForecastController();

            // Act
            var result = weatherForecast.Get();

            // Assert
            Assert.All(result, item => Assert.InRange(item.TemperatureC, range.low, range.high));

        }
    }
}
