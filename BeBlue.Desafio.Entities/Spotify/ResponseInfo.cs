using System.Net;

namespace BeBlue.Desafio.Entities.Spotify {
    public class ResponseInfo {
        public WebHeaderCollection Headers { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public static readonly ResponseInfo Empty = new ResponseInfo ();
    }
}