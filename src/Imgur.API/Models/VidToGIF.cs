using System.Text.Json.Serialization;

namespace Imgur.API.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class VidToGIF : IVidToGIF
    {
        /// <summary>
        /// Current poll Current step??
        /// </summary>
        [JsonPropertyName("step")]
        public string Step { get; set; }
        /// <summary>
        /// Current poll Progress Overall
        /// </summary>
        [JsonPropertyName("progress_overall")]
        public float ProgressOverall { get; set; }
        /// <summary>
        /// Current poll Progress Step
        /// </summary>
        [JsonPropertyName("progress_step")]
        public int ProgressStep { get; set; }
        /// <summary>
        /// GIF ID
        /// </summary>
        [JsonPropertyName("hash")]
        public string Id { get; set; }
        /// <summary>
        /// Delete Hash for GIF
        /// </summary>
        [JsonPropertyName("deletehash")]
        public string DelteHash { get; set; }
    }
}
