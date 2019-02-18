using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace JwtService
{
    public class JwtTokenValidator : ISecurityTokenValidator
    {
        /// <summary>
        /// jwt配置文件
        /// </summary>
        private readonly JwtConfig _jwtConfig;


        public JwtTokenValidator(JwtConfig jwtConfig)
        {
            _jwtConfig = jwtConfig;
        }

        public bool CanValidateToken => true;

        public int MaximumTokenSizeInBytes { get; set; }

        public bool CanReadToken(string securityToken)
        {
            return true;
        }

        public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {
            ClaimsPrincipal principal;
            try
            {
                //配置验证参数
                validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.SecretKey));
                validationParameters.ValidAudience = _jwtConfig.Audience;
                validationParameters.ValidIssuer = _jwtConfig.Issuer;
                //验证
                var handler = new JwtSecurityTokenHandler();
                principal = handler.ValidateToken(securityToken, validationParameters, out validatedToken);
            }
            catch (Exception)
            {
                validatedToken = null;
                principal = null;
            }
            return principal;
        }

    }
}
