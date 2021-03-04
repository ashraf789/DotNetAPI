using API.Models;
using Common;
using Core.Models;
using Core.Models.User;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace API
{
    public class JwtManager
    {
        public Token GenerateSecurityToken(TokenRequest request)
        {
            var secret = ConfigHelpers.JWT_SECRET;
            var expireInMinutes = ConfigHelpers.JWT_EXPIRE_IN_MINUTES;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, request.Email),
                    new Claim(ClaimTypes.Name, request.Name),
                    new Claim(ClaimTypes.Role, request.RoleID.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(expireInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var jwtToken = tokenHandler.CreateToken(tokenDescription);

            Token token = new Token();
            token.AccessToken = tokenHandler.WriteToken(jwtToken);
            token.UserID = request.UserID;
            token.ExpiresIn = (DateTime) tokenDescription.Expires;
            return token;
        }
    }
}