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
        ...some code
            builder.Append($"redirect_uri={Сюда необходимо вставить свой домен!!!}/Authorization/Complete&");
        ...some code
        }
```

В классе VkInstruments.MVC/Controllers/AuthorizationController.cs необходимо указать appId приложения:


```
        public ActionResult Start()
        {
            ...some code
            vkAuth.SetAuthParams(_vk.GetParams(Вставить appId здесь));
            ...some code
        }
```

## Коллаборация.
Если вас вдруг накрыло желанием перелопатить мой код и внести вклад в развитие проекта - репозиторий всегда открыт для ваших PR!

В вкладке Projects имеются задачи для последующей реализации, можете туда заглянуть, хе.

## Использованные библиотеки:

Vkontakte API for .NET https://vknet.github.io/vk/
