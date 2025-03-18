namespace TeeMugShop.Application.DTOs.Auth
{
    /// <summary>
    /// Represents the payload returned from Google OAuth token validation.
    /// </summary>
    public class GoogleTokenPayload
    {
        /// <summary>
        /// Gets or sets the user's email from the Google token.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the user's full name from the Google token.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the user's profile picture URL from the Google token.
        /// </summary>
        public string? Picture { get; set; }
    }
}
