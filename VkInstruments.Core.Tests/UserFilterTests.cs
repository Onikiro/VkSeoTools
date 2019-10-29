using FluentAssertions;
using System.Collections.Generic;
using VkNet.Enums;
using VkNet.Model;
using VkNet.Model.RequestParams;
using Xunit;

namespace VkInstruments.Core.Tests
{
    public class UserFilterTests
    {
        [Fact]
        public void ApplyFilter()
        {
            var users = new List<User>
            {
                new User
                {
                    Sex = Sex.Male,
                    City = new City{Id = 1}
                },
                new User
                {
                    Sex = Sex.Female,
                    City = new City{Id = 1}
                },
                new User
                {
                    Sex = Sex.Male,
                    City = new City{Id = 1}
                },
                new User
                {
                    Sex = Sex.Female,
                    City = new City{Id = 2}
                },
                new User
                {
                    Sex = Sex.Female,
                    City = new City{Id = 3}
                },
            };

            var firstParams = new UserSearchParams
            {
                Sex = Sex.Male,
                City = 1
            };

            var secondParams = new UserSearchParams
            {
                Sex = Sex.Female
            };

            var firstFilteredUsers = UserFilter.ApplyFilter(users, firstParams);
            var secondFilteredUsers = UserFilter.ApplyFilter(users, secondParams);

            firstFilteredUsers.Should().HaveCount(2);
            secondFilteredUsers.Should().HaveCount(3);
        }
    }
}