using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VkNet;
using VkNet.Abstractions;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.RequestParams;

namespace VkLikeParser
{
    internal static class Parser
    {

        private static IEnumerable<long> GetLikes(IVkApiCategories vk, string uri, uint index = 0)
        {
            var ids = GetPostIds(uri);
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

        public static void SaveLikes(VkApi vk)
        {
            var idsList = new List<long>();
            Console.Write("\nВведите нужный пост: ");
            var uri = Console.ReadLine();
            var isEnded = false;
            uint offset = 0;
            while (!isEnded)
            {
                isEnded = true;
                idsList.AddRange(GetLikes(vk, uri, offset));
                if (idsList.Count % 1000 == 0)
                {
                    offset += 1000;
                    isEnded = false;
                }
            }

            var file = new FileStream("parsedlikes.txt", FileMode.Create);
            using (var writer = new StreamWriter(file))
            {
                foreach (var idNumber in idsList)
                {
                    writer.WriteLine($"https://vk.com/id{idNumber}");
                }
            }

            Console.WriteLine("Успешно!");
        }
    }
}
