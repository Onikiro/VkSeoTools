using NUnit.Framework;
using System;

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

            var expectedUserPostIds = new Tuple<long, long>(1, 247797);
            var expectedCommunityPostIds = new Tuple<long, long>(-22822305, 486256);
            var breakUserPostIds = new Tuple<long, long>(247797, 1);
            var breakCommunityPostIds = new Tuple<long, long>(486256, -22822305);

            var actualUserPostIds = Parser.GetPostIds(inputUserPost);
            var actualCommunityPostIds = Parser.GetPostIds(inputCommunityPost);

            Assert.AreEqual(expectedUserPostIds.Item1, actualUserPostIds.Item1);
            Assert.AreEqual(expectedUserPostIds.Item2, actualUserPostIds.Item2);

            Assert.AreEqual(expectedCommunityPostIds.Item1, actualCommunityPostIds.Item1);
            Assert.AreEqual(expectedCommunityPostIds.Item2, actualCommunityPostIds.Item2);

            Assert.AreNotEqual(breakUserPostIds.Item1, actualUserPostIds.Item1);
            Assert.AreNotEqual(breakUserPostIds.Item2, actualUserPostIds.Item2);

            Assert.AreNotEqual(breakCommunityPostIds.Item1, actualCommunityPostIds.Item1);
            Assert.AreNotEqual(breakCommunityPostIds.Item2, actualCommunityPostIds.Item2);
        }
    }
}
