namespace VkLikeParser
{
    internal static class Program
    {
        private static void Main()
        {
            var vkSystem = new VkSystem();
            Parser.SaveLikes(vkSystem.Vk);
        }
    }
}
