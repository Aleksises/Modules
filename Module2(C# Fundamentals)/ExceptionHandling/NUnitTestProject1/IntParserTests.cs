using IntParser;
using NUnit.Framework;

namespace NUnitTestProject1
{
    [TestFixture]
    public class IntParserTests
    {
        [Test]
        public void TryParse_WhenProvidedStringIsNumberString_ShouldReturnTrueAndNotDefaultIntValue()
        {
            // Arrange
            var source = "123321";

            // Act
            var successParse = CustomIntParser.TryParse(source, out var result);

            // Assert
            Assert.That(successParse, Is.EqualTo(true));
            Assert.That(result, Is.TypeOf<int>());
            Assert.That(result, Is.EqualTo(123321));
        }

        [Test]
        public void TryParse_WhenProvidedIsNotValidNumberString_ShouldReturnFalseAndDefaultIntValue()
        {
            // Arrange
            var source = "123qw2";

            // Act
            var failedParse = CustomIntParser.TryParse(source, out var result);

            // Assert
            Assert.That(failedParse, Is.EqualTo(false));
            Assert.That(result, Is.TypeOf<int>());
            Assert.That(result, Is.EqualTo(default(int)));
        }
    }
}
