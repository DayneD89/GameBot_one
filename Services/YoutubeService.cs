using System;
using System.Threading.Tasks;
using YoutubeSearchApi.Net;

namespace GameBot_One.Services
{
    public class YoutubeService
    {
        private YoutubeApiV3Client ytsClient;

        public YoutubeService()
        {
            ytsClient = new YoutubeApiV3Client(Environment.GetEnvironmentVariable("YOUTUBE_KEY"));
        }

        public async Task<dynamic> SearchVideosByQuery(string query)
        {
            dynamic response = await ytsClient.Search(query);
            return response;
        }
    }
}