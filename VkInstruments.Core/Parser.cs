using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VkNet;
using VkNet.Abstractions;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.RequestParams;

namespace VkInstruments.Core
{
    public static class Parser
    {
        private static async Task<IEnumerable<long>> GetLikesSegment(IVkApiCategories vk, Tuple<long, long> ids, uint index = 0)
        {
            return await vk.Likes.GetListAsync(new LikesGetListParams
            {
                Type = LikeObjectType.Post,
                Filter = LikesFilter.Likes,
                OwnerId = ids.Item1,
                ItemId = ids.Item2,
                Extended = false,
                Offset = index,
                Count = 1000
            });
        }

        /// <summary>
        /// Returns post ids tuple (Item1 - ownerId, Item2 - ItemId)
        /// </summary>
        /// <param name="uri">post link</param>
        public static Tuple<long, long> GetPostIds(string uri)
        {
            const string idsPattern = "([-\\d]+)_([\\d]+)";

            var groups = Regex.Match(uri, idsPattern).Groups;

            if (!long.TryParse(groups[1].Value, out var ownerId) ||
                !long.TryParse(groups[2].Value, out var itemId))
                throw new FormatException("Incorrect uri.");

            if (itemId < 0)
            {
                // swap without temp
                ownerId += itemId;
                itemId = ownerId - itemId;
                ownerId -= itemId;
            }

            return new Tuple<long, long>(ownerId, itemId);
        }

        public static async Task<List<long>> GetLikes(VkApi vk, string uri)
        {
            var linkIds = GetPostIds(uri);
            var idsList = new List<long>(100);
            var isEnded = false;
            uint offset = 0;
            while (!isEnded)
            {
                isEnded = true;
                var likeSegment = await GetLikesSegment(vk, linkIds, offset);
                idsList.AddRange(likeSegment);
                if (idsList.Count > 0 && idsList.Count % 1000 == 0)
                {
                    offset += 1000;
                    isEnded = false;
                }
            }

            return idsList;
        }
    }
}
