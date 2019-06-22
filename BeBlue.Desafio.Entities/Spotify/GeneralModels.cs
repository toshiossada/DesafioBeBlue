using System;
using System.Collections.Generic;
using BeBlue.Desafio.Entities.Spotify.Models;
using Newtonsoft.Json;

namespace BeBlue.Desafio.Entities.Spotify {

    public class Image {
        [JsonProperty ("url")]
        public string Url { get; set; }

        [JsonProperty ("width")]
        public int? Width { get; set; }

        [JsonProperty ("height")]
        public int? Height { get; set; }
    }

    public class ErrorResponse : BasicModel { }

    public class Error {
        [JsonProperty ("status")]
        public int Status { get; set; }

        [JsonProperty ("message")]
        public string Message { get; set; }
    }
    public class Followers {
        [JsonProperty ("href")]
        public string Href { get; set; }

        [JsonProperty ("total")]
        public int Total { get; set; }
    }
    public class PlaylistTrackCollection {
        [JsonProperty ("href")]
        public string Href { get; set; }

        [JsonProperty ("total")]
        public int Total { get; set; }
    }
    public class LinkedFrom
    {
        [JsonProperty("external_urls")]
        public Dictionary<string, string> ExternalUrls { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
    public class PlaylistTrack
    {
        [JsonProperty("added_at")]
        public DateTime AddedAt { get; set; }

        [JsonProperty("added_by")]
        public PublicProfile AddedBy { get; set; }

        [JsonProperty("track")]
        public FullTrack Track { get; set; }

        [JsonProperty("is_local")]
        public bool IsLocal { get; set; }
    }
}