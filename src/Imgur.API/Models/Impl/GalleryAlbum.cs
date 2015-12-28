﻿using System;
using System.Collections.Generic;
using Imgur.API.Enums;
using Imgur.API.JsonConverters;
using Newtonsoft.Json;

namespace Imgur.API.Models.Impl
{
    /// <summary>
    ///     The data model formatted for gallery albums.
    /// </summary>
    public class GalleryAlbum : GalleryItem, IGalleryAlbum
    {
        /// <summary>
        ///     The account ID of the account that uploaded it, or null.
        /// </summary>
        [JsonProperty("account_id")]
        public int? AccountId { get; set; }

        /// <summary>
        ///     The account username or null if it's anonymous.
        /// </summary>
        [JsonProperty("account_url")]
        public string AccountUrl { get; set; }

        /// <summary>
        ///     Number of comments on the gallery album.
        /// </summary>
        [JsonProperty("comment_count")]
        public int? CommentCount { get; set; }

        /// <summary>
        ///     Up to 10 top level comments, sorted by "best".
        /// </summary>
        [JsonProperty("comment_preview")]
        [JsonConverter(typeof (TypeConverter<IEnumerable<Comment>>))]
        public IEnumerable<IComment> CommentPreview { get; set; } = new List<IComment>();

        /// <summary>
        ///     The ID of the album cover image.
        /// </summary>
        public string Cover { get; set; }

        /// <summary>
        ///     The height, in pixels, of the album cover image.
        /// </summary>
        [JsonProperty("cover_height")]
        public int? CoverHeight { get; set; }

        /// <summary>
        ///     The width, in pixels, of the album cover image.
        /// </summary>
        [JsonProperty("cover_width")]
        public int? CoverWidth { get; set; }

        /// <summary>
        ///     Utc timestamp of when the album was inserted into the gallery, converted from epoch time.
        /// </summary>
        [JsonConverter(typeof (EpochTimeConverter))]
        public DateTimeOffset DateTime { get; set; }

        /// <summary>
        ///     The description of the album in the gallery.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Number of downvotes for the image.
        /// </summary>
        public int? Downs { get; set; }

        /// <summary>
        ///     Indicates if the current user favorited the image. Defaults to false if not signed in.
        /// </summary>
        public bool? Favorite { get; set; }

        /// <summary>
        ///     The ID for the image.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     The total number of images in the album.
        /// </summary>
        [JsonProperty("images_count")]
        public int ImageCount { get; set; }

        /// <summary>
        ///     A list of all the images in the album (only available when requesting the direct album).
        /// </summary>
        [JsonConverter(typeof (TypeConverter<IEnumerable<Image>>))]
        public IEnumerable<IImage> Images { get; set; } = new List<IImage>();

        /// <summary>
        ///     The view layout of the album.
        /// </summary>
        public AlbumLayout? Layout { get; set; }

        /// <summary>
        ///     The URL link to the album.
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        ///     Indicates if the image has been marked as nsfw or not. Defaults to null if information is not available.
        /// </summary>
        public bool? Nsfw { get; set; }

        /// <summary>
        ///     The privacy level of the album, you can only view public if not logged in as album owner.
        /// </summary>
        public AlbumPrivacy? Privacy { get; set; }

        /// <summary>
        ///     Imgur popularity score.
        /// </summary>
        public int? Score { get; set; }

        /// <summary>
        ///     The title of the album in the gallery.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Topic of the gallery album.
        /// </summary>
        public string Topic { get; set; }

        /// <summary>
        ///     Topic ID of the gallery album.
        /// </summary>
        [JsonProperty("topic_id")]
        public int? TopicId { get; set; }

        /// <summary>
        ///     Upvotes for the image.
        /// </summary>
        public int? Ups { get; set; }

        /// <summary>
        ///     The number of album views.
        /// </summary>
        public int Views { get; set; }

        /// <summary>
        ///     The current user's vote on the album. null if not signed in or if the user hasn't voted on it.
        /// </summary>
        public Vote? Vote { get; set; }
    }
}