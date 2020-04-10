using Newtonsoft.Json;

namespace Imgur.API.Models
{
    /// <summary>
    /// VidToGIF upload Response Class
    /// </summary>
    public class VidToGIFPoll : IVidToGIFPoll
    {
        /// <summary>
        /// Idk.
        /// </summary>
        [JsonProperty("code")]
        public bool Code { get; set; }
        /// <summary>
        /// Id of Poll to wait the vid for conversion
        /// </summary>
        [JsonProperty("ticket")]
        public string Ticket { get; set; }
    }
}
