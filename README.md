# VK SEO instruments
###  VK Likes parser from wall-post 

Gets VK profiles links of people who liked your post. 
([Release](https://github.com/Onikiro/VkInstruments/releases/tag/2.0))

Presentation:
![presentation gif](https://media.giphy.com/media/2kVnesYPphyGOOsXRF/giphy.gif)


### Profile links filter by input Ids 

Gets VK profile links, filters them and returns filtered list. 
([Release](https://github.com/Onikiro/VkInstruments/releases/tag/2.0))


## Installing
To deploy this ASP.Net MVC5 app you must: 
1. Сreate a VK app 
2. Choose OpenAPI
3. Specify your domain (where your web app will be deployed) in settings.

4. In VkInstruments.MVC.Auth.AuthorizationVk paste your domain into redirect_uri:

```c#
        public Uri CreateAuthorizeUrl(ulong clientId, ulong scope, Display display, string state)
        {
        ...some code
            builder.Append($"redirect_uri={Your domain}/Authorization/Complete&");
        ...some code
        }
```

5. In VkInstruments.MVC.Controllers.AuthorizationController paste your appId from VK:


```c#
        public ActionResult Start()
        {
            ...some code
            vkAuth.SetAuthParams(_vk.GetParams({Your appId}));
            ...some code
        }
```

## Collaboration

I haven't realized some features, so you can take any task from Projects menu and create a pull request with your improvements. You are welcome. :)


## Used libraries

[Vkontakte API for .NET](https://vknet.github.io/vk/)
