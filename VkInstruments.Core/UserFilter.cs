using System;
using System.Collections.Generic;
using System.Linq;
using VkNet.Enums;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace VkInstruments.Core
{
    internal static class UserFilter
    {
        internal static IEnumerable<User> ApplyFilter(IEnumerable<User> users, UserSearchParams searchParams)
        {
            var result = new List<User>();

            foreach (var user in users)
            {
                var isMatch = true;

                var isFullDate = user.BirthDate?.Count(char.IsPunctuation) > 1;
                user.BirthDate = isFullDate ? user.BirthDate : user.BirthDate + ".1900";
                DateTime.TryParse(user.BirthDate, out var birthDate);
                var age = isFullDate ? (DateTime.Now.Year - birthDate.Year) : (int?)null;

                if (searchParams.Sex != Sex.Unknown) isMatch &= (user.Sex == searchParams.Sex);

                if (searchParams.BirthDay.HasValue) isMatch &= (birthDate.Day == searchParams.BirthDay.Value);

                if (searchParams.BirthMonth.HasValue) isMatch &= (birthDate.Day == searchParams.BirthMonth.Value);

                if (searchParams.AgeFrom.HasValue) isMatch &= (age >= searchParams.AgeFrom.Value);

                if (searchParams.AgeTo.HasValue) isMatch &= (age <= searchParams.AgeTo.Value);

                if (searchParams.Country.HasValue) isMatch &= (user.Country?.Id == searchParams.Country.Value);

                if (searchParams.City.HasValue) isMatch &= (user.City?.Id == searchParams.City.Value);

                if (searchParams.Status.HasValue) isMatch &= (user.Relation == (RelationType)searchParams.Status.Value);

                if (isMatch) result.Add(user);
            }

            return result;
        }

        internal static ProfileFields GetProfileFields(UserSearchParams searchParams)
        {
            var fields = new ProfileFields();

            if (searchParams.AgeFrom.HasValue || searchParams.AgeTo.HasValue || searchParams.BirthDay.HasValue || searchParams.BirthMonth.HasValue || searchParams.BirthYear.HasValue)
                fields |= ProfileFields.BirthDate;

            if (searchParams.Country.HasValue) fields |= ProfileFields.Country;

            if (searchParams.City.HasValue) fields |= ProfileFields.City;

            if (searchParams.Sex != Sex.Unknown) fields |= ProfileFields.Sex;

            if (searchParams.Status.HasValue) fields |= ProfileFields.Relation;

            return fields;
        }
    }
}
