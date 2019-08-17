using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Threading.Tasks;
using VkInstruments.Core;
using VkNet;

namespace VkInstruments.ConsoleApp
{
    internal static class Program
    {
        private static async Task Main()
        {
            var vkService = new VkService(new VkApi(NullLogger<VkApi>.Instance));

            Console.Write("\nEnter post link: ");
            var postLink = Console.ReadLine();

            var profileIds = await vkService.ParseLikedUserIdsFromPost(postLink);

            foreach (var address in profileIds)
            {
                Console.WriteLine($"vk.com/id{address}");
            }
        }
    }
}