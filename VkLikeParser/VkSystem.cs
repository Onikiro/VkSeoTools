using System;
using NLog;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Exception;
using VkNet.Model;

namespace VkLikeParser
{
    internal class VkSystem
    {
        public readonly VkApi Vk = new VkApi(LogManager.CreateNullLogger());

        private string _login;

        private string _password;

        private readonly Settings _settingFilters = Settings.Groups;

        public VkSystem()
        {
            SetCredentials();
            Auth();
        }

        public VkSystem(Settings settingFilters)
        {
            SetCredentials();
            Auth(settingFilters);
        }

        private void SetCredentials()
        {
            Console.Write("Введите свой номер телефона или email: ");
            _login = Console.ReadLine();
            Console.Write("\nВведите свой пароль: ");
            _password = Console.ReadLine();
        }

        private void Auth(Settings customSettings = null)
        {
            bool isLoggedIn;
            try
            {
                Vk.Authorize(new ApiAuthParams
                {
                    ApplicationId = 6634517,
                    Login = _login,
                    Password = _password,
                    Settings = customSettings ?? _settingFilters
                });
                isLoggedIn = Vk.IsAuthorized;
            }
            catch (VkApiAuthorizationException)
            {
                isLoggedIn = false;
                Console.WriteLine("Authorization failed: Incorrect credentials");
                SetCredentials();
            }
            catch (VkApiException ex)
            {
                isLoggedIn = false;
                Console.WriteLine(ex.Message + "\n");
                SetCredentials();
            }
            if (isLoggedIn)
            {
                Console.Clear();
                Console.Write("Вход выполнен успешно!");
            }
        }
    }
}
