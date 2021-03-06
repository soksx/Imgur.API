﻿using Imgur.API.Enums;
using Imgur.API.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Imgur.API.RequestBuilders
{
    internal static class ImageRequestBuilder
    {
        internal static HttpRequestMessage UpdateImageRequest(string url,
                                                              string title = null,
                                                              string description = null)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            var parameters = new Dictionary<string, string>();

            if (!string.IsNullOrWhiteSpace(title))
            {
                parameters.Add("title", title);
            }

            if (!string.IsNullOrWhiteSpace(description))
            {
                parameters.Add("description", description);
            }

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(parameters.ToArray())
            };

            return request;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S107:Methods should not have too many parameters", Justification = "<Pending>")]
        internal static HttpRequestMessage UploadImageStreamRequest(string url,
                                                                    Stream image = null,
                                                                    string album = null,
                                                                    string name = null,
                                                                    string title = null,
                                                                    string description = null,
                                                                    IProgress<int> progressBytes = null,
                                                                    int? bufferSize = null)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                name = "name";
            }

            var content = new MultipartFormDataContent($"{DateTime.UtcNow.Ticks}")
            {
                {new StringContent("file"), "type"}
            };

            if (progressBytes != null)
            {
                content.Add(new ProgressStreamContent(image,
                                                      progressBytes,
                                                      bufferSize), "image", name);
            }
            else
            {
                content.Add(new StreamContent(image), "image", name);
            }

            if (!string.IsNullOrWhiteSpace(album))
            {
                content.Add(new StringContent(album), "album");
            }

            if (!string.IsNullOrWhiteSpace(title))
            {
                content.Add(new StringContent(title), "title");
            }

            if (!string.IsNullOrWhiteSpace(description))
            {
                content.Add(new StringContent(description), "description");
            }

            var request = new ProgressHttpRequestMessage(HttpMethod.Post, url)
            {
                Content = content
            };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
           
            return request;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S107:Methods should not have too many parameters", Justification = "<Pending>")]
        internal static HttpRequestMessage UploadVideoStreamRequest(string url,
                                                                    Stream video,
                                                                    string album = null,
                                                                    string name = null,
                                                                    string title = null,
                                                                    string description = null,
                                                                    IProgress<int> progressBytes = null,
                                                                    int? bufferSize = null)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            if (video == null)
            {
                throw new ArgumentNullException(nameof(video));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                name = "name";
            }

            var content = new MultipartFormDataContent($"{DateTime.UtcNow.Ticks}")
            {
                {new StringContent("file"), "type"}
            };

            if (progressBytes != null)
            {
                content.Add(new ProgressStreamContent(video,
                                                      progressBytes,
                                                      bufferSize), "video", name);
            }
            else
            {
                content.Add(new StreamContent(video), "video", name);
            }

            if (!string.IsNullOrWhiteSpace(album))
            {
                content.Add(new StringContent(album), "album");
            }

            if (!string.IsNullOrWhiteSpace(title))
            {
                content.Add(new StringContent(title), "title");
            }

            if (!string.IsNullOrWhiteSpace(description))
            {
                content.Add(new StringContent(description), "description");
            }

            var request = new ProgressHttpRequestMessage(HttpMethod.Post, url)
            {
                Content = content
            };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));

            return request;
        }

        internal static HttpRequestMessage UploadImageUrlRequest(string url,
                                                                 string imageUrl,
                                                                 string album = null,
                                                                 string name = null,
                                                                 string title = null,
                                                                 string description = null)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                throw new ArgumentNullException(nameof(imageUrl));
            }

            var parameters = new Dictionary<string, string>
            {
                {"image", imageUrl},
                {"type", "URL"},
            };

            if (!string.IsNullOrWhiteSpace(album))
            {
                parameters.Add("album", album);
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                parameters.Add("name", name);
            }

            if (!string.IsNullOrWhiteSpace(title))
            {
                parameters.Add("title", title);
            }

            if (!string.IsNullOrWhiteSpace(description))
            {
                parameters.Add("description", description);
            }

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(parameters.ToArray())
            };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));

            return request;
        }

        internal static HttpRequestMessage UploadFileRequest(string url, Stream stream, string fileName, FileType fileType = FileType.Image, string albumId = null,
            string name = null, string title = null, string description = null)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            var content = new MultipartFormDataContent($"{DateTime.UtcNow.Ticks}")
            {
                {new StringContent("file"), "type"},
                {new StreamContent(stream), fileType.ToLowerCase(), fileName}
            };

            if (!string.IsNullOrWhiteSpace(albumId))
            {
                content.Add(new StringContent(albumId), "album");
            }

            if (name != null)
            {
                content.Add(new StringContent(name), nameof(name));
            }

            if (title != null)
            {
                content.Add(new StringContent(title), nameof(title));
            }

            if (description != null)
            {
                content.Add(new StringContent(description), nameof(description));
            }

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = content
            };

            return request;
        }
    }
}
