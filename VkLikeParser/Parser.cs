using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.RequestParams;
using VkNet.Utils;

namespace VkLikeParser
{
    internal class Parser
    {
        private bool _isLoggedIn;
        public ulong AppId = 1001;
        private string _login;
        private string _password;
        private readonly Settings _settingFilters = Settings.Groups;

        public Parser()
        {
            SetCredentials();
            var vk = new VkApi();
            Auth(vk);
            OutputLikeIds(vk);
        }

        private void SetCredentials()
        {
            Console.Write("Введите свой номер телефона или email: ");
            _login = Console.ReadLine();
            Console.Write("\nВведите свой пароль: ");
            _password = Console.ReadLine();
        }

        private void Auth(VkApi vk)
        {
            try
            {
                vk.Authorize(new ApiAuthParams
                {
                    ApplicationId = AppId,
                    Login = _login,
                    Password = _password,
                    Settings = _settingFilters
                });
                _isLoggedIn = true;
            }
            catch (VkNet.Exception.VkApiException ex)
            {
                _isLoggedIn = false;
                Console.WriteLine(ex.Message + "\n");
                SetCredentials();
            }

            if (!_isLoggedIn) return;

            Console.Clear();
            Console.Write("Вход выполнен успешно!");
        }

        private VkCollection<long> GetLikes(VkApi vk, string uri, uint index = 0)
        {
            var ids = GetPostIds(uri);
            return vk.Likes.GetList(new LikesGetListParams
            {
                Type = LikeObjectType.Post,
                Filter = LikesFilter.Likes,
                OwnerId = ids[0],
                ItemId = ids[1],
                Extended = true,
                Offset = index,
                Count = 1000
            });
        }

        private long[] GetPostIds(string uri)
        {
            long[] ids = new long[2];
            const string ownerPattern = @"([A-Za-z]|(-))([0-9].*)(_)";
            const string postPattern = @"(_)[0-9].*";
            const string cleanPattern = "[-\\d]+";
            Regex ownerReg = new Regex(ownerPattern);
            Regex postReg = new Regex(postPattern);
            Regex cleaner = new Regex(cleanPattern);
            Match ownerMatch = ownerReg.Match(uri);
            Match cleanOwner = cleaner.Match(ownerMatch.ToString());
            Match postMatch = postReg.Match(uri);
            Match cleanPost = cleaner.Match(postMatch.ToString());

            ids[0] = Convert.ToInt64(cleanOwner.Value);
            ids[1] = Convert.ToInt64(cleanPost.Value);
            return ids;
        }

        private void OutputLikeIds(VkApi vk)
        {
            List<VkCollection<long>> collections = new List<VkCollection<long>>();
            Console.Write("\nВведите нужный пост: ");
            string uri = Console.ReadLine();
            bool isEnded = false;
            uint offset = 0;
            while (!isEnded)
            {
                isEnded = true;
                collections.Add(GetLikes(vk, uri, offset));
                if (collections[collections.Count - 1].Count >= 1000)
                {
                    offset += 1000;
                    isEnded = false;
                }
            }
            FileStream file = new FileStream("parsedlikes.txt", FileMode.Create);
            using (StreamWriter writer = new StreamWriter(file))
            {
                foreach(var el in collections)
                {
                    foreach(var i in el)
                    {
                        writer.WriteLine($"https://vk.com/id{i}");
                    }
                }
            }
            Console.WriteLine("Успешно!");
        }
    }
}
