# VK SEO instruments
###  VK Likes parser from wall-post 

Gets VK profiles links of people who liked your post. 

Presentation:

![presentation gif](https://media.giphy.com/media/fHifO6idiW6pCo0ow8/giphy.gif)


### Profile links filter by input Ids 

Gets VK profile links, filters them and returns filtered list. 

Presentation:

![presentation gif](https://media.giphy.com/media/SiM5br9dhGVkzbo06D/giphy.gif)


## Demo

```c#
        private static async Task Main()
        {
            var vkService = new VkService(new VkApi(NullLogger<VkApi>.Instance), "yourtoken");

            Console.Write("\nEnter post link: ");
            var postLink = Console.ReadLine();

            var profileIds = await vkService.ParseLikedUserIdsFromPost(postLink);

            foreach (var address in profileIds)
            {
                Console.WriteLine($"vk.com/id{address}");
            }
        }
```


## Installing
To deploy this .net Core app you must: 
1. Сreate a VK app 
2. Copy ServiceToken from your app
3. Paste VkInstruments.Web -> appsetings.json your ServiceToken

## Collaboration

I haven't implemented some features, so you can take any task from Projects menu and create a pull request with your improvements. You are welcome. :)


## Used libraries

[Vkontakte API for .NET](https://vknet.github.io/vk/)

### Supported by

[<img src="VkInstruments.Web/wwwroot/jetbrains.svg">](https://www.jetbrains.com/?from=VkInstruments)
