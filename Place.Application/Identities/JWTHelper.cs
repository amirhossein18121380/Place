using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Place.Application.Identities.Exceptions;
using Place.Application.Identities.Interface;
using Place.Domain.Interface;
using Place.Domain.Models;

namespace Place.Application.Identities
{
    public class JWTHelper : IJWTHelper
    {
        IUserDal _userRepo;
        public JWTHelper(IUserDal repo)
        {
            _userRepo = repo;
        }
        public ClaimsPrincipal GetPrincipal(string token, string jwtSecurityKey)
        {
            try
            {
                ///var Secret = Startup.StaticConfig.GetSection("JWTSecurityKey").Value;
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;

                var symmetricKey = Encoding.UTF8.GetBytes(jwtSecurityKey);

                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

                return principal;
            }
            catch (Exception ex)
            {
                //should write log
                return null;
            }
        }


        public string CreateToken(User user, string jwtSecurityKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecurityKey));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Name,Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.MobilePhone,user.UserName,typeof(string).ToString()),
                    //new Claim(ClaimTypes.Hash,user.PasswordSecurityCode,typeof(string).ToString())
                }),
                IssuedAt = DateTime.Now,
                Expires = DateTime.UtcNow.AddMonths(4),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var generatedToken = tokenHandler.WriteToken(token);
            return generatedToken;
        }

        public bool ValidateToken(string token, out User user, string jwtSecurityKey)
        {
            user = null;
            var simplePrinciple = GetPrincipal(token, jwtSecurityKey);
            var identity = simplePrinciple?.Identity as ClaimsIdentity;
            if (identity == null)
                return false;
            if (!identity.IsAuthenticated)
                return false;
            var userIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
            var passwordSecurityCode = identity.FindFirst(ClaimTypes.Hash)?.Value;
            var userId = long.Parse(userIdClaim?.Value);
            user = _userRepo.GetById(userId);
            if (user == null)
                return false;
            //if (user.PasswordSecurityCode != passwordSecurityCode)
            //    throw new PasswordChangedException();
            return true;
        }

        //public JwtSecurityToken GenerateToken(string mobileNumber, string fullName,
        //    Guid userId, string passwordSecurityCode, string jwtSecurityKey, string tokenIssuer, string tokenAudience,
        //    string tokenExpireTimeinHour)
        //{
        //    SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecurityKey));
        //    var claims = new[]
        //    {
        //        new Claim(ClaimTypes.MobilePhone,mobileNumber),
        //        new Claim(ClaimTypes.Name,fullName),
        //        new Claim(ClaimTypes.NameIdentifier,userId.ToString()),
        //        new Claim(ClaimTypes.Hash,passwordSecurityCode),
        //    };
        //    return new JwtSecurityToken(
        //             issuer: tokenIssuer,
        //             audience: tokenAudience,
        //             expires: DateTime.Now.AddHours(int.Parse(tokenExpireTimeinHour)),
        //             claims: claims,
        //             signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        //             );
        //}

        //public JwtSecurityToken GenerateToken(User user, string passwordSecurityCode,
        //    string jwtSecurityKey, string tokenIssuer,
        //    string tokenAudience, string tokenExpireTimeinHour)
        //{
        //    return GenerateToken(user.MobileNo, user.FullName, user.Id, passwordSecurityCode,
        //        jwtSecurityKey,
        //        tokenIssuer, tokenAudience, tokenExpireTimeinHour);
        //}
    }
}
