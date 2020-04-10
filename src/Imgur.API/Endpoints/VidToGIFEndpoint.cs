using System;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Models;
using Imgur.API.Authentication;
using Imgur.API.RequestBuilders;
using System.Threading;

namespace Imgur.API.Endpoints
{
    /// <summary>
    /// Vid to GIF Actions
    /// </summary>
    public class VidToGIFEndpoint : EndpointBase, IVidToGifEndpoint
    {
        /// <summary>
        ///     Initializes a new instance of the ImageEndpoint class.
        /// </summary>
        /// <param name="apiClient">The type of client that will be used for authentication.</param>
        public VidToGIFEndpoint(IApiClient apiClient) : base(apiClient)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the ImageEndpoint class.
        /// </summary>
        /// <param name="apiClient">The type of client that will be used for authentication.</param>
        /// <param name="httpClient"> The class for sending HTTP requests and receiving HTTP responses from the endpoint methods.</param>
        public VidToGIFEndpoint(IApiClient apiClient, HttpClient httpClient) : base(apiClient, httpClient)
        {
        }

        /// <summary>
        /// Convert Video to GIF file
        /// </summary>
        /// <param name="vidUrl">Video url to convert to GIF</param>
        /// <returns></returns>
        public Task<IVidToGIF> ConvertVidToGIF(string vidUrl)
        {
            if (string.IsNullOrWhiteSpace(vidUrl))
            {
                throw new ArgumentNullException(nameof(vidUrl));
            }
            return ConvertVidToGIFInternal(vidUrl);
        }
        
        /// <summary>
        /// Create start conversion from video to GIF.
        /// </summary>
        /// <param name="vidUrl">Video url to convert to GIF</param>
        /// <returns></returns>
        private async Task<IVidToGIF> ConvertVidToGIFInternal(string vidUrl)
        {
            var prePoll = await CreatePollVidToGIFInternal(vidUrl);
            if (string.IsNullOrEmpty(prePoll.Ticket))
                throw new ImgurException("Create poll request failed.");
            string url = $"poll";
            for (int i = 0; i < 1200; i++) /* Loop for 2 min polling waiting 100 ms per iteration*/
            {
                using (var request = VidToGifRequestBuilder.PollVidToGIFRequest(url, prePoll.Ticket))
                {
                    var returnGif = await SendRequestAsync<VidToGIF>(request).ConfigureAwait(false);
                    if (returnGif?.ProgressOverall == 1 && !string.IsNullOrEmpty(returnGif?.Id))
                        return returnGif;
                }               

                Thread.Sleep(100);
            }
            return null;
        }
        private async Task<IVidToGIFPoll> CreatePollVidToGIFInternal(string vidUrl)
        {
            string url = "upload";
            using (var request = VidToGifRequestBuilder.ConvertVidToGIFRequest(url, vidUrl))
            {
                var returnPoll = await SendRequestAsync<VidToGIFPoll>(request).ConfigureAwait(false);
                return returnPoll;
            }
        }
    }
}
