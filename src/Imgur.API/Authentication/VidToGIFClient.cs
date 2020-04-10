using Imgur.API.Models;

namespace Imgur.API.Authentication
{
    /// <summary>
    ///     Imgur API application credentials.
    ///     Register at https://api.imgur.com/oauth2/addclient
    /// </summary>
    public class VidToGIFClient : ApiClientBase, IImgurClient
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
        ///     Initializes a new instance of the ImgurClient class.
        /// </summary>
        /// <param name="clientId">The Imgur app's ClientId. </param>
        /// <param name="oAuth2Token">An OAuth2 Token used for actions against a user's account.</param>
        public VidToGIFClient(string clientId, IOAuth2Token oAuth2Token)
            : base(clientId, oAuth2Token)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the ImgurClient class.
        /// </summary>
        /// <param name="clientId">The Imgur app's ClientId. </param>
        /// <param name="clientSecret">The Imgur app's ClientSecret.</param>
        /// <param name="oAuth2Token">An OAuth2 Token used for actions against a user's account.</param>
        public VidToGIFClient(string clientId, string clientSecret, IOAuth2Token oAuth2Token)
            : base(clientId, clientSecret, oAuth2Token)
        {
        }

        /// <summary>
        ///     The Endpoint Url.
        ///     https://imgur.com/vidgif/
        /// </summary>
        public override string EndpointUrl { get; } = "https://imgur.com/vidgif/";
    }
}