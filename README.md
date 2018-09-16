# VK SEO instruments
###  VK Likes parser from wall-post 

Gets VK profiles links of people who liked your post. 
([Release](https://github.com/Onikiro/VkInstruments/releases/tag/2.0))

Presentation:

![presentation gif](https://media.giphy.com/media/fHifO6idiW6pCo0ow8/giphy.gif)


### Profile links filter by input Ids 

Gets VK profile links, filters them and returns filtered list. 
([Release](https://github.com/Onikiro/VkInstruments/releases/tag/2.0))

Presentation:

![presentation gif](https://media.giphy.com/media/SiM5br9dhGVkzbo06D/giphy.gif)


## Installing
To deploy this .net Core app you must: 
1. Сreate a VK app 
2. Choose OpenAPI
3. Specify your domain (where your web app will be deployed) in settings.

4. In VkInstruments.CoreWebApp.AppProperties paste your domain into DOMAIN and your AppId int APP_ID:

```c#
    public static class AppProperties
    {
        public const int APP_ID = YourAppId;
        public const string DOMAIN = "YourDomain";
    }
```

## Collaboration

I haven't realized some features, so you can take any task from Projects menu and create a pull request with your improvements. You are welcome. :)


## Used libraries

[Vkontakte API for .NET](https://vknet.github.io/vk/)

### Supported by

[<img src="VkInstruments.CoreWebApp/wwwroot/jetbrains.svg">](https://www.jetbrains.com/?from=VkInstruments)
