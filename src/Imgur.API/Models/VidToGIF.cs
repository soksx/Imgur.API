using Newtonsoft.Json;

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
        [JsonProperty("step")]
        public string Step { get; set; }
        /// <summary>
        /// Current poll Progress Overall
        /// </summary>
        [JsonProperty("progress_overall")]
        public float ProgressOverall { get; set; }
        /// <summary>
        /// Current poll Progress Step
        /// </summary>
        [JsonProperty("progress_step")]
        public int ProgressStep { get; set; }
        /// <summary>
        /// GIF ID
        /// </summary>
        [JsonProperty("hash")]
        public string Id { get; set; }
        /// <summary>
        /// Delete Hash for GIF
        /// </summary>
        [JsonProperty("deletehash")]
        public string DelteHash { get; set; }
    }
}
