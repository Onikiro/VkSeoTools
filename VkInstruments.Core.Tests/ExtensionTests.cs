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
            var expectedDescription = "В активном поиске";
            var badDescription1 = "В активном";
            var badDescription2 = "В поиске";
            var badDescription3 = "активном поиске";
            var actualDescription = Status.TheActiveSearch.GetDescription();

            Assert.AreEqual(expectedDescription, actualDescription);

            Assert.AreNotEqual(badDescription1, actualDescription);
            Assert.AreNotEqual(badDescription2, actualDescription);
            Assert.AreNotEqual(badDescription3, actualDescription);
        }
    }
}
