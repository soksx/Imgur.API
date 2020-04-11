﻿using Imgur.API.Authentication;
using Imgur.API.Enums;
using Imgur.API.Models;
using Imgur.API.RequestBuilders;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Imgur.API.Endpoints
{
    /// <summary>
    ///     Image related actions.
    /// </summary>
    public class ImageEndpoint : EndpointBase, IImageEndpoint
    {
        /// <summary>
        ///     Initializes a new instance of the ImageEndpoint class.
        /// </summary>
        /// <param name="apiClient">The type of client that will be used for authentication.</param>
        public ImageEndpoint(IApiClient apiClient) : base(apiClient)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the ImageEndpoint class.
        /// </summary>
        /// <param name="apiClient">The type of client that will be used for authentication.</param>
        /// <param name="httpClient"> The class for sending HTTP requests and receiving HTTP responses from the endpoint methods.</param>
        public ImageEndpoint(IApiClient apiClient, HttpClient httpClient) : base(apiClient, httpClient)
        {
        }

        internal ImageRequestBuilder RequestBuilder { get; } = new ImageRequestBuilder();

        /// <summary>
        ///     Deletes an image. For an anonymous image, {id} must be the image's deletehash.
        ///     If the image belongs to your account then passing the ID of the image is sufficient.
        /// </summary>
        /// <param name="imageId">The image id.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public ValueTask<bool> DeleteImageAsync(string imageId)
        {
            if (string.IsNullOrWhiteSpace(imageId))
            {
                throw new ArgumentNullException(nameof(imageId));
            }

            return DeleteImageInternalAsync(imageId);
        }

        /// <summary>
        ///     Deletes an image. For an anonymous image, {id} must be the image's deletehash.
        ///     If the image belongs to your account then passing the ID of the image is sufficient.
        /// </summary>
        /// <param name="imageId">The image id.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        private async ValueTask<bool> DeleteImageInternalAsync(string imageId)
        {
            var url = $"image/{imageId}";

            using (var request = RequestBuilderBase.CreateRequest(HttpMethod.Delete, url))
            {
                var deleted = await SendRequestAsync<bool>(request).ConfigureAwait(false);
                return deleted;
            }
        }

        /// <summary>
        ///     Favorite an image with the given ID. OAuth authentication required.
        /// </summary>
        /// <param name="imageId">The image id.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public ValueTask<bool> FavoriteImageAsync(string imageId)
        {
            if (string.IsNullOrWhiteSpace(imageId))
            {
                throw new ArgumentNullException(nameof(imageId));
            }

            if (ApiClient.OAuth2Token == null)
            {
                throw new ArgumentException(OAuth2RequiredExceptionMessage);
            }

            return FavoriteImageInternalAsync(imageId);
        }

        /// <summary>
        ///     Favorite an image with the given ID. OAuth authentication required.
        /// </summary>
        /// <param name="imageId">The image id.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        private async ValueTask<bool> FavoriteImageInternalAsync(string imageId)
        {
            var url = $"image/{imageId}/favorite";

            using (var request = RequestBuilderBase.CreateRequest(HttpMethod.Post, url))
            {
                var imgurResult = await SendRequestAsync<string>(request).ConfigureAwait(false);
                return imgurResult.Equals("favorited", StringComparison.OrdinalIgnoreCase);
            }
        }

        /// <summary>
        ///     Get information about an image.
        /// </summary>
        /// <param name="imageId">The image id.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public ValueTask<IImage> GetImageAsync(string imageId)
        {
            if (string.IsNullOrWhiteSpace(imageId))
            {
                throw new ArgumentNullException(nameof(imageId));
            }

            return GetImageInternalAsync(imageId);
        }

        /// <summary>
        ///     Get information about an image.
        /// </summary>
        /// <param name="imageId">The image id.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        private async ValueTask<IImage> GetImageInternalAsync(string imageId)
        {
            var url = $"image/{imageId}";

            using (var request = RequestBuilderBase.CreateRequest(HttpMethod.Get, url))
            {
                var image = await SendRequestAsync<Image>(request).ConfigureAwait(false);
                return image;
            }
        }

        /// <summary>
        ///     Updates the title or description of an image.
        ///     You can only update an image you own and is associated with your account.
        ///     For an anonymous image, {id} must be the image's deletehash.
        /// </summary>
        /// <param name="imageId">The image id.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public ValueTask<bool> UpdateImageAsync(string imageId)
        {
            if (string.IsNullOrWhiteSpace(imageId))
            {
                throw new ArgumentNullException(nameof(imageId));
            }

            return UpdateImageInternalAsync(imageId);
        }

        /// <summary>
        ///     Updates the title or description of an image.
        ///     You can only update an image you own and is associated with your account.
        ///     For an anonymous image, {id} must be the image's deletehash.
        /// </summary>
        /// <param name="imageId">The image id.</param>
        /// <param name="title">The title of the image.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public ValueTask<bool> UpdateImageAsync(string imageId, string title)
        {
            if (string.IsNullOrWhiteSpace(imageId))
            {
                throw new ArgumentNullException(nameof(imageId));
            }

            return UpdateImageInternalAsync(imageId, title);
        }

        /// <summary>
        ///     Updates the title or description of an image.
        ///     You can only update an image you own and is associated with your account.
        ///     For an anonymous image, {id} must be the image's deletehash.
        /// </summary>
        /// <param name="imageId">The image id.</param>
        /// <param name="title">The title of the image.</param>
        /// <param name="description">The description of the image.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public ValueTask<bool> UpdateImageAsync(string imageId, string title, string description)
        {
            if (string.IsNullOrWhiteSpace(imageId))
            {
                throw new ArgumentNullException(nameof(imageId));
            }

            return UpdateImageInternalAsync(imageId, title, description);
        }

        /// <summary>
        ///     Updates the title or description of an image.
        ///     You can only update an image you own and is associated with your account.
        ///     For an anonymous image, {id} must be the image's deletehash.
        /// </summary>
        /// <param name="imageId">The image id.</param>
        /// <param name="title">The title of the image.</param>
        /// <param name="description">The description of the image.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        private async ValueTask<bool> UpdateImageInternalAsync(string imageId, string title = null, string description = null)
        {
            var url = $"image/{imageId}";

            using (var request = ImageRequestBuilder.UpdateImageRequest(url, title, description))
            {
                var updated = await SendRequestAsync<bool>(request).ConfigureAwait(false);
                return updated;
            }
        }

        /// <summary>
        ///     Upload a new image using a binary file.
        /// </summary>
        /// <param name="image">A binary file.</param>
        /// <param name="albumId">
        ///     The id of the album you want to add the image to. For anonymous albums, {albumId} should be the
        ///     deletehash that is returned at creation.
        /// </param>
        /// <param name="name">The name of the file.</param>
        /// <param name="title">The title of the image.</param>
        /// <param name="description">The description of the image.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public ValueTask<IImage> UploadImageBinaryAsync(byte[] image, string albumId = null, string name = null, string title = null,
            string description = null)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            return UploadImageBinaryInternalAsync(image, albumId, name, title);
        }

        /// <summary>
        ///     Upload a new image using a binary file.
        /// </summary>
        /// <param name="image">A binary file.</param>
        /// <param name="albumId">
        ///     The id of the album you want to add the image to. For anonymous albums, {albumId} should be the
        ///     deletehash that is returned at creation.
        /// </param>
        /// <param name="name">The name of the file.</param>
        /// <param name="title">The title of the image.</param>
        /// <param name="description">The description of the image.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        private async ValueTask<IImage> UploadImageBinaryInternalAsync(byte[] image, string albumId = null, string name = null, string title = null,
            string description = null)
        {
            const string url = nameof(image);

            using (var request = ImageRequestBuilder.UploadImageBinaryRequest(url, image, albumId, name, title, description))
            {
                var returnImage = await SendRequestAsync<Image>(request).ConfigureAwait(false);
                return returnImage;
            }
        }

        /// <summary>
        ///     Upload a new image using a stream.
        /// </summary>
        /// <param name="image">A stream.</param>
        /// <param name="albumId">
        ///     The id of the album you want to add the image to. For anonymous albums, {albumId} should be the
        ///     deletehash that is returned at creation.
        /// </param>
        /// <param name="name">The name of the file.</param>
        /// <param name="title">The title of the image.</param>
        /// <param name="description">The description of the image.</param>
        /// <param name="progressBytes">A provider for progress updates.</param>
        /// <param name="progressBufferSize">The amount of bytes that should be uploaded while performing a progress upload.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public ValueTask<IImage> UploadImageStreamAsync(Stream image, string albumId = null, string name = null, string title = null,
            string description = null, IProgress<int> progressBytes = null, int progressBufferSize = 4096)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            return UploadImageStreamInternalAsync(image, albumId, name, title, description, progressBytes, progressBufferSize);
        }

        /// <summary>
        ///     Upload a new image using a stream.
        /// </summary>
        /// <param name="image">A stream.</param>
        /// <param name="albumId">
        ///     The id of the album you want to add the image to. For anonymous albums, {albumId} should be the
        ///     deletehash that is returned at creation.
        /// </param>
        /// <param name="name">The name of the file.</param>
        /// <param name="title">The title of the image.</param>
        /// <param name="description">The description of the image.</param>
        /// <param name="progressBytes">A provider for progress updates.</param>
        /// <param name="progressBufferSize">The amount of bytes that should be uploaded while performing a progress upload.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        private async ValueTask<IImage> UploadImageStreamInternalAsync(Stream image, string albumId = null, string name = null, string title = null,
            string description = null, IProgress<int> progressBytes = null, int progressBufferSize = 4096)
        {
            const string url = nameof(image);

            using (var request = ImageRequestBuilder.UploadImageStreamRequest(url, image, albumId, name, title, description, progressBytes, progressBufferSize))
            {
                var returnImage = await SendRequestAsync<Image>(request).ConfigureAwait(false);
                return returnImage;
            }
        }

        /// <summary>
        ///     Upload a new image using a URL.
        /// </summary>
        /// <param name="image">The URL for the image.</param>
        /// <param name="albumId">
        ///     The id of the album you want to add the image to. For anonymous albums, {albumId} should be the
        ///     deletehash that is returned at creation.
        /// </param>
        /// <param name="name">The name of the file.</param>
        /// <param name="title">The title of the image.</param>
        /// <param name="description">The description of the image.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public ValueTask<IImage> UploadImageUrlAsync(string image, string albumId = null, string name = null, string title = null,
            string description = null)
        {
            if (string.IsNullOrWhiteSpace(image))
            {
                throw new ArgumentNullException(nameof(image));
            }

            return UploadImageUrlInternalAsync(image, albumId, name, title, description);
        }

        /// <summary>
        ///     Upload a new image using a URL.
        /// </summary>
        /// <param name="image">The URL for the image.</param>
        /// <param name="albumId">
        ///     The id of the album you want to add the image to. For anonymous albums, {albumId} should be the
        ///     deletehash that is returned at creation.
        /// </param>
        /// <param name="name">The name of the file.</param>
        /// <param name="title">The title of the image.</param>
        /// <param name="description">The description of the image.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        private async ValueTask<IImage> UploadImageUrlInternalAsync(string image, string albumId = null, string name = null, string title = null,
            string description = null)
        {
            const string url = nameof(image);

            using (var request = ImageRequestBuilder.UploadImageUrlRequest(url, image, albumId, name, title, description))
            {
                var returnImage = await SendRequestAsync<Image>(request).ConfigureAwait(false);
                return returnImage;
            }
        }
        /// <summary>
        ///     Upload a new image using a URL.
        /// </summary>
        /// <param name="upload">The Stream file of the image/video.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="fileType">The Filetype (image/video).</param>
        /// <param name="albumId">
        ///     The id of the album you want to add the image to. For anonymous albums, {albumId} should be the
        ///     deletehash that is returned at creation.
        /// </param>
        /// <param name="name">The name of the file.</param>
        /// <param name="title">The title of the image.</param>
        /// <param name="description">The description of the image.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        public ValueTask<IImage> UploadFileAsync(Stream upload, string fileName, FileType fileType = FileType.Image, string albumId = null, string name = null, string title = null, string description = null)
        {
            if (upload == null)
            {
                throw new ArgumentNullException(nameof(upload));
            }

            return UploadFileInternalAsync(upload, fileName, fileType, albumId, name, title, description);
        }
        /// <summary>
        ///     Upload a new image using a URL.
        /// </summary>
        /// <param name="upload">The Stream file of the image/video.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="fileType">The Filetype (image/video).</param>
        /// <param name="albumId">
        ///     The id of the album you want to add the image to. For anonymous albums, {albumId} should be the
        ///     deletehash that is returned at creation.
        /// </param>
        /// <param name="name">The name of the file.</param>
        /// <param name="title">The title of the image.</param>
        /// <param name="description">The description of the image.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        private async ValueTask<IImage> UploadFileInternalAsync(Stream upload, string fileName, FileType fileType = FileType.Image, string albumId = null, string name = null, string title = null, string description = null)
        {
            const string url = nameof(upload);

            using (var request = ImageRequestBuilder.UploadFileRequest(url, upload, fileName, fileType, albumId, name, title, description))
            {
                var returnImage = await SendRequestAsync<Image>(request).ConfigureAwait(false);
                return returnImage;
            }
        }
    }
}