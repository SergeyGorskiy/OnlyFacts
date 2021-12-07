using OnlyFacts.Web.Infrastructure.Mappers.Base;
using Xunit;

namespace OnlyFacts.Web.Tests
{
    public class AutomapperTests
    {
        [Fact]
        [Trait("Automapper", "Mapper Configuration")]
        public void ItShouldCorrectlyConfigured()
        {
            // Arrange
            var config = MapperRegistration.GetMapperConfiguration();

            // Act

            // Assert
            config.AssertConfigurationIsValid();
        }
    }
}