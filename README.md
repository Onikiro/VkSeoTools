# VK SEO-instruments.
###  VK-likes parser from wall-post (Version 1.0). 

Parsing VK profile links, who liked the wall-post.

### Profile links filter by input ids (Soon). 

Gets in input VK profile links, filter them and return filtered list.


## Installing.
For deploy you must create vk app, choose OpenAPI and specify your domain in settings.

In VkInstruments/VkInstruments.MVC/Auth/AuthorizationVk.cs paste your domain into redirect_uri:

```
        public Uri CreateAuthorizeUrl(ulong clientId, ulong scope, Display display, string state)
        {
        ...some code
            builder.Append($"redirect_uri={Ypur domain}/Authorization/Complete&");
        ...some code
        }
```

In VkInstruments.MVC/Controllers/AuthorizationController.cs paste your appId:


```
        public ActionResult Start()
        {
            ...some code
            vkAuth.SetAuthParams(_vk.GetParams(Your appId));
            ...some code
        }
```

## Collaboration.

In Projects have some not realized features, you can take any task and create PR after improve. Repository opened for PR. :)


## Used libraries:

Vkontakte API for .NET https://vknet.github.io/vk/
