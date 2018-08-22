# Инструменты работы с аудиторией Вконтакте.
###  Парсер лайков с постов (Version 1.0). 

Собирает ссылки на профили ВК лайкнувших выбранный пост.
### Фильтр пользователей по входящему набору id (Soon). 

Получает на входе список ссылок на профили ВК, фильтрует по выбранным параметрам и возвращает список отфильтрованных ссылок.

## Установка.
Для разворачивания Asp.net MVC5 проекта необходимо создать собственное приложение в ВК, указать в нем свой домен, на котором будет развернут сайт. 
В классе VkInstruments/VkInstruments.MVC/Auth/AuthorizationVk.cs необходимо прописать redirect_uri, идентичный указанному в приложении ВК:

```
        public Uri CreateAuthorizeUrl(ulong clientId, ulong scope, Display display, string state)
        {
            var builder = new StringBuilder("https://oauth.vk.com/authorize?");

            builder.Append($"client_id={clientId}&");
            builder.Append($"redirect_uri={Сюда необходимо вставить свой домен!!!}/Authorization/Complete&");
            builder.Append($"display={display}&");
            builder.Append($"scope={scope}&");
            builder.Append("response_type=token&");
            builder.Append($"v={_versionManager.Version}&");
            builder.Append($"state={state}&");
            builder.Append("revoke=1");

            return new Uri(builder.ToString());
        }
```

В классе VkInstruments.MVC/Controllers/AuthorizationController.cs необходимо указать appId приложения:


```
        public ActionResult Start()
        {
            var vkAuth = new AuthorizationVk(_vk.Vk.VkApiVersion);
            vkAuth.SetAuthParams(_vk.GetParams(Enter your appId there));
            var authUri = vkAuth.CreateAuthorizeUrl(vkAuth.AuthParams.ApplicationId, vkAuth.AuthParams.Settings.ToUInt64(), Display.Page, "123456");
            return Redirect(authUri.AbsoluteUri);
        }
```



## Использованные библиотеки:

Vkontakte API for .NET https://vknet.github.io/vk/
