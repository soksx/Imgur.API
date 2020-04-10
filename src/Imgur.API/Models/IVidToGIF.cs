namespace Imgur.API.Models
{
    /// <summary>
    /// VidToGif Result Model
    /// </summary>
    public interface IVidToGIF: IDataModel
    {
        /// <summary>
        /// Idk.
        /// </summary>
        string Step { get; set; }
        /// <summary>
        /// Progress Overall 
        /// </summary>
        float ProgressOverall { get; set; }
        /// <summary>
        /// Progress Step
        /// </summary>
        int ProgressStep { get; set; }
        /// <summary>
        /// ID of converted GIF
        /// </summary>
        string Id { get; set; }
        /// <summary>
        /// ID for delete hash of uploaded GIF
        /// </summary>
        string DelteHash { get; set; }
    }
}
