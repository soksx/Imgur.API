using Imgur.API.Models;
using System.Threading.Tasks;

namespace Imgur.API.Endpoints
{
    /// <summary>
    /// Vid2Gif actions
    /// </summary>
    public interface IVidToGifEndpoint : IEndpoint
    {
        /// <summary>
        /// Convert Video to GIF file
        /// </summary>
        /// <param name="vidUrl">Video url to convert to GIF</param>
        /// <returns></returns>
        ValueTask<IVidToGIF> ConvertVidToGIF(string vidUrl);
    }
}
