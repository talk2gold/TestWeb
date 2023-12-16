using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace TestWeb_youtube
{

    public class video
    {
        public string videoID { get; set; }
        public string url { get; set; }
        public string Description { get; set; }
        public string Title {get;set; }
    }
    public class returnObject
    {
        public List<video> videos { get; set; }
        public List<string> channels { get; set; }
        public List<string> playlists { get; set; }
        public returnObject()
        {
            videos = new List<video>();
            channels = new List<string>();
            playlists = new List<string>();
        }
    }
    public class mytestApi
    {
        public returnObject YouTubeApi()
        {
            returnObject retobj = new();
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyD0vjsfYw2E9FU-0YU1yhwIod6Cy6A20R8",
                ApplicationName = this.GetType().ToString()
            });
            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = "Science Think"; // Replace with your search term.
            searchListRequest.MaxResults = 50;
            var searchListResponse = searchListRequest.Execute();
            List<string> videos = new List<string>();
            List<string> channels = new List<string>();
            List<string> playlists = new List<string>();

            foreach (var searchResult in searchListResponse.Items)
            {
                switch (searchResult.Id.Kind)
                {
                    case "youtube#video":
                        video v = new();
                        v.url = searchResult.Snippet.Thumbnails.Medium.Url;
                        v.videoID= searchResult.Id.VideoId;
                        v.Description = searchResult.Snippet.Description;
                        v.Title = searchResult.Snippet.Title;
                        retobj.videos.Add(v);
                        //videos.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.VideoId));
                        //videos.Add(String.Format("{0}", searchResult.Snippet.Thumbnails.Medium.Url));
                        break;

                    case "youtube#channel":
                        channels.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.ChannelId));
                        
                        break;

                    case "youtube#playlist":
                        playlists.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.PlaylistId));
                        break;
                }

                
            }
            //retobj.videos =   videos ;
            retobj.channels =   channels;
            retobj.playlists = playlists;
            return retobj;
        }
    }
}
