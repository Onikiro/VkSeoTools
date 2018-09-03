using NUnit.Framework;

namespace VkInstruments.Core.Tests
{
    [TestFixture]
    public class ParserTests
    {
        [Test]
        public void GetPostIds()
        {
            var inputUserPost = "https://vk.com/wall1?offset=80&own=1&w=wall1_247797";
            var inputCommunityPost = "https://vk.com/team?w=wall-22822305_486256";

            var expectedUserPostIds = new long[] { 1, 247797 };
            var expectedCommunityPostIds = new long[] { -22822305, 486256 };
            var breakUserPostIds = new long[] { 247797, 1 };
            var breakCommunityPostIds = new long[] { 486256, -22822305 };

            var actualUserPostIds = Parser.GetPostIds(inputUserPost);
            var actualCommunityPostIds = Parser.GetPostIds(inputCommunityPost);

            for (var i = 0; i < 2; i++)
            {
                Assert.AreEqual(expectedUserPostIds[i], actualUserPostIds[i]);
                Assert.AreEqual(expectedCommunityPostIds[i], actualCommunityPostIds[i]);

                Assert.AreNotEqual(breakUserPostIds[i], actualUserPostIds[i]);
                Assert.AreNotEqual(breakCommunityPostIds[i], actualCommunityPostIds[i]);
            }
        }
    }
}
