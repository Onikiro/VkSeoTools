using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using VkNet;
using VkNet.Abstractions;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.RequestParams;

namespace VkLikesParserMVC.Models
{
    internal static class Parser
    {

        private static IEnumerable<long> GetLikesSegment(IVkApiCategories vk, string uri, uint index = 0)
        {
            var ids = GetPostIds(uri);
            if (!ids.Any())
                return new List<long>(0);

            return vk.Likes.GetList(new LikesGetListParams
            {
                Type = LikeObjectType.Post,
                Filter = LikesFilter.Likes,
                OwnerId = ids[0],
                ItemId = ids[1], //ItemId must be negative
                Extended = false,
                Offset = index,
                Count = 1000
            }).ToList();
        }

        /// <summary>
        /// Returns post id array (0 - ownerId, 1 - ItemId)
        /// </summary>
        /// <param name="uri">post link</param>
        private static long[] GetPostIds(string uri)
        {
            var ids = new long[2];
            const string idsPattern = "[-\\d]+";
            var idsReg = new Regex(idsPattern);
            var matches = idsReg.Matches(uri);
            if (!long.TryParse(matches[0].Value, out ids[0]) ||
                !long.TryParse(matches[1].Value, out ids[1]))
                return new long[2];

            return ids;
        }

        public static List<long> GetLikes(VkApi vk, string uri)
        {
            var idsList = new List<long>(500);
            var isEnded = false;
            uint offset = 0;
            while (!isEnded)
            {
                isEnded = true;
                idsList.AddRange(GetLikesSegment(vk, uri, offset));
                if (idsList.Count % 1000 == 0)
                {
                    offset += 1000;
                    isEnded = false;
                }
            }

            return idsList;
        }
    }
}
