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

            Console.Write("\nВведите ссылку на пост: ");
            var postLink = Console.ReadLine();

            var vkService = new VkService(vkSystem);
            var likes = await vkService.ParseLikesFromPost(postLink);

            foreach (var likeAdress in likes)
            {
                Console.WriteLine($"vk.com/id{likeAdress}");
            }
        }
    }
}
