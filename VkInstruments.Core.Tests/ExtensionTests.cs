using FluentAssertions;
using NUnit.Framework;
using VkInstruments.Core.Enums;
using VkInstruments.Core.Utils;

namespace VkInstruments.Core.Tests
{
    [TestFixture]
    public class ExtensionTests
    {
        [Test]
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