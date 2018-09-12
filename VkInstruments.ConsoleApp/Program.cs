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
		    try
		    {
		        var likes = await Parser.GetLikes(vkSystem.Vk, Console.ReadLine());
		        foreach (var likeAdress in likes)
		        {
		            Console.WriteLine($"vk.com/id{likeAdress}");
		        }
		    }
		    catch (FormatException)
		    {
		        Console.WriteLine("Введен неверный uri!");
		    }
		    catch (Exception)
		    {
		        Console.WriteLine("Неопределенная ошибка...");
		    }
		}
	}
}
