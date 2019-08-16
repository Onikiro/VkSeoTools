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
            const string userPost = "https://vk.com/wall1?offset=80&own=1&w=wall1_247797";
            const string communityPost = "https://vk.com/team?w=wall-22822305_486256";

            var (userPostOwnerId, userPostItemId) = _parser.GetPostIds(userPost);
            var (communityPostOwnerId, communityPostItemId) = _parser.GetPostIds(communityPost);

            userPostOwnerId.Should().Be(1);
            userPostItemId.Should().Be(247797);

            communityPostOwnerId.Should().Be(-22822305);
            communityPostItemId.Should().Be(486256);

            userPostOwnerId.Should().NotBe(247797);
            userPostItemId.Should().NotBe(1);

            communityPostOwnerId.Should().NotBe(486256);
            communityPostItemId.Should().NotBe(-22822305);
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