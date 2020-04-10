namespace Imgur.API.Models
{
    /// <summary>
    /// VidToGIF upload Response Interface
    /// </summary>
    public interface IVidToGIFPoll: IDataModel
    {
        /// <summary>
        /// Idk.
        /// </summary>
        bool Code { get; set; }
        /// <summary>
        /// Id of Poll to wait the vid for conversion
        /// </summary>
        string Ticket { get; set; }
    }
}
