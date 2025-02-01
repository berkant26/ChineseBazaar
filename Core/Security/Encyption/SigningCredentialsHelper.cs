using Microsoft.IdentityModel.Tokens;

namespace Core.Security.Encyption
{
    public partial class SecurityKeyHelper
    {
        public class SigningCredentialsHelper
        {
            public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
            {
                return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            }
        }
    }
}
