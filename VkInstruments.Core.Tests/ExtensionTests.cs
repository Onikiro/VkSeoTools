using FluentAssertions;
using VkInstruments.Core.Enums;
using VkInstruments.Core.Utils;
using Xunit;

namespace VkInstruments.Core.Tests
{
    public class ExtensionTests
    {
        [Fact]
        public void GetDescription()
        {
            var actualDescription = Status.TheActiveSearch.GetDescription();

            actualDescription.Should().Be("В активном поиске");
            actualDescription.Should().NotBe("В активном");
            actualDescription.Should().NotBe("активном поиске");
            actualDescription.Should().NotBe("В активном");
        }
    }
}