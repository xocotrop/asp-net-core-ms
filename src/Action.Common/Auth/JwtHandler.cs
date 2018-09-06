using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Action.Common.Auth
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtSecurityTokenHandler _jJwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private readonly JwtOptions _options;
        private readonly SecurityKey _issuerSigninKey;
        private readonly SigningCredentials _signingCredentials;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly JwtHeader _jwtHeader;
        public JwtHandler(IOptions<JwtOptions> options)
        {
            _options = options.Value;
            _issuerSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            _signingCredentials = new SigningCredentials(_issuerSigninKey, SecurityAlgorithms.HmacSha256);
            _jwtHeader = new JwtHeader(_signingCredentials);
            _tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidIssuer = _options.Issuer,
                IssuerSigningKey = _issuerSigninKey,
                ClockSkew = TimeSpan.Zero
            };
        }
        public JsonWebToken Create(Guid userId)
        {
            var nowUtc = DateTime.Now;
            var expires = nowUtc.AddMinutes(_options.ExpiryMinutes); // descobrir porque não ta rolando com minuto
            var centuryBegin = new DateTime(1970, 1, 1).ToUniversalTime();
            var centuryBegin2 = new DateTime(1970, 1, 1);
            // var centuryBegin = DateTime.MinValue.ToUniversalTime();
            var exp = (long)(new TimeSpan(expires.Ticks - centuryBegin.Ticks).TotalSeconds);
            var now = (long)(new TimeSpan(nowUtc.Ticks - centuryBegin.Ticks).TotalSeconds);
            var payload = new JwtPayload{
                {"sub", userId},
                {"iss", _options.Issuer},
                {"iat", now},
                {"exp", exp},
                {"unique_name", userId}
            };
            Console.WriteLine(nowUtc);
            Console.WriteLine(expires);
            Console.WriteLine(centuryBegin);
            Console.WriteLine(centuryBegin2);
            var jwt = new JwtSecurityToken(_jwtHeader, payload);
            var token = _jJwtSecurityTokenHandler.WriteToken(jwt);

            return new JsonWebToken
            {
                Token = token,
                Expires = exp
            };
        }
    }
}
