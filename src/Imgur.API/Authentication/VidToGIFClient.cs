using Imgur.API.Models;

namespace Imgur.API.Authentication
{
    /// <summary>
    ///     Imgur API application credentials.
    ///     Register at https://api.imgur.com/oauth2/addclient
    /// </summary>
    public class VidToGIFClient : ApiClient, IApiClient
    {
        /// <summary>
        ///     Initializes a new instance of the ImgurClient class.
        /// </summary>
        /// <param name="clientId">The Imgur app's ClientId. </param>
        public VidToGIFClient(string clientId) : base(clientId)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the ImgurClient class.
        /// </summary>
        /// <param name="clientId">The Imgur app's ClientId. </param>
        /// <param name="clientSecret">The Imgur app's ClientSecret.</param>
        public VidToGIFClient(string clientId, string clientSecret) : base(clientId, clientSecret)
        {
        }

        /// <summary>
        ///     The Endpoint Url.
        ///     https://imgur.com/vidgif/
        /// </summary>
        public override string BaseAddress => "https://imgur.com/vidgif/"; 
    }
}