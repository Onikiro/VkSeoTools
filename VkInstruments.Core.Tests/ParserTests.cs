using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace VkInstruments.Core.Tests
{
    [TestFixture]
    public class ParserTests
    {
        private readonly Parser _parser = new Parser();

        [Test]
        public void GetPostIds()
        {
            var inputUserPost = "https://vk.com/wall1?offset=80&own=1&w=wall1_247797";
            var inputCommunityPost = "https://vk.com/team?w=wall-22822305_486256";

            var expectedUserPostIds = new Tuple<long, long>(1, 247797);
            var expectedCommunityPostIds = new Tuple<long, long>(-22822305, 486256);
            var breakUserPostIds = new Tuple<long, long>(247797, 1);
            var breakCommunityPostIds = new Tuple<long, long>(486256, -22822305);

            var actualUserPostIds = _parser.GetPostIds(inputUserPost);
            var actualCommunityPostIds = _parser.GetPostIds(inputCommunityPost);

            Assert.AreEqual(expectedUserPostIds.Item1, actualUserPostIds.Item1);
            Assert.AreEqual(expectedUserPostIds.Item2, actualUserPostIds.Item2);

            Assert.AreEqual(expectedCommunityPostIds.Item1, actualCommunityPostIds.Item1);
            Assert.AreEqual(expectedCommunityPostIds.Item2, actualCommunityPostIds.Item2);

            Assert.AreNotEqual(breakUserPostIds.Item1, actualUserPostIds.Item1);
            Assert.AreNotEqual(breakUserPostIds.Item2, actualUserPostIds.Item2);

            Assert.AreNotEqual(breakCommunityPostIds.Item1, actualCommunityPostIds.Item1);
            Assert.AreNotEqual(breakCommunityPostIds.Item2, actualCommunityPostIds.Item2);
        }

        [Test]
        public async Task GetLikes()
        {
            var mock = new VkLikesCategoryMock();
            var result1 = await _parser.GetLikes(mock, @"https://vk.com/wall-22822305_3");
            result1.Should().BeEquivalentTo(mock.Likes3);

            var result2 = await _parser.GetLikes(mock, @"https://vk.com/wall-22822305_40157");
            result2.Should().BeEquivalentTo(mock.Likes40157);

            result1.Should().NotBeEquivalentTo(result2);
        }
    }
}