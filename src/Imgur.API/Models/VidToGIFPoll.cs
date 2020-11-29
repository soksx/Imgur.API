using System.Text.Json.Serialization;

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
        [JsonPropertyName("code")]
        public bool Code { get; set; }
        /// <summary>
        /// Id of Poll to wait the vid for conversion
        /// </summary>
        [JsonPropertyName("ticket")]
        public string Ticket { get; set; }
    }
}
