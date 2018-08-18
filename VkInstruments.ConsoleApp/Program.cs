using System;
using VkInstruments.Core;

namespace VkInstruments.ConsoleApp
{
	internal static class Program
	{
		private static void Main()
		{
			var vkSystem = new VkSystem();
            vkSystem.Auth();
			Console.Write("\nВведите ссылку на пост: ");
			try
			{
				var likes = Parser.GetLikes(vkSystem.Vk, Console.ReadLine());
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
