using System;
using System.Net.Http;

namespace Imgur.API.RequestBuilders
{
    internal static class VidToGifRequestBuilder
    {
        internal static HttpRequestMessage ConvertVidToGIFRequest(string url, string vidUrl)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }
            if (string.IsNullOrWhiteSpace(vidUrl))
            {
                throw new ArgumentNullException(nameof(vidUrl));
            }

            return new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new MultipartFormDataContent($"{DateTime.UtcNow.Ticks}")
                {
                    {new StringContent(vidUrl), "source"}
                }
            };
        }

        internal static HttpRequestMessage PollVidToGIFRequest(string url, string ticket)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException(nameof(ticket));
            }
            return new HttpRequestMessage(HttpMethod.Get, $"{url}/{ticket}");
        }
    }
}
