using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeeMugShop.Application.Common.Settings
{
    /// <summary>
    /// JWT configuration settings loaded from appsettings.json
    /// </summary>
    public class JwtSettings
    {
        /// <summary>
        /// The issuer (who generated the token).
        /// </summary>
        public string Issuer { get; set; } = string.Empty;

        /// <summary>
        /// The audience (who will consume the token).
        /// </summary>
        public string Audience { get; set; } = string.Empty;

        /// <summary>
        /// The secret key used to sign the token.
        /// </summary>
        public string SecretKey { get; set; } = string.Empty;

        /// <summary>
        /// Token expiration time in hours.
        /// </summary>
        public int TokenExpirationHours { get; set; } = 1;
    }
}
