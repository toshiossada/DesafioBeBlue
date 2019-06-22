namespace BeBlue.Desafio.Entities.Spotify {
    public class SpotifyAuthentication {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string AccessToken { get; set; }
        public string TokenType { get; set; }

        public SpotifyAuthentication (string _clientId, string _clientSecret, string _tokenType) {
            ClientId = _clientId;
            ClientSecret = _clientSecret;
            TokenType = _tokenType;
        }
    }
}