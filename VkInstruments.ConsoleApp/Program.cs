using System;
using System.Threading.Tasks;
using VkInstruments.Core;

namespace VkInstruments.ConsoleApp
{
    internal static class Program
    {
        private static async Task Main()
        {
            var vkSystem = new VkSystem();
            await vkSystem.AuthAsync();

            Console.Write("\nEnter post link: ");
            var postLink = Console.ReadLine();

            var vkService = new VkService(vkSystem);
            var profileIds = await vkService.ParseLikedUserIdsFromPost(postLink);

            foreach (var address in profileIds)
            {
                Console.WriteLine($"vk.com/id{address}");
            }
        }
    }
}
