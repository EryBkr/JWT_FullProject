using JWTProject.Business.Abstracts;
using JWTProject.Business.StringInfos;
using JWTProject.Entities.Concrete;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JWTProject.Business.Concrete
{
    public class JwtManager:IJwtService
    {
        public string GenerateJwt(AppUser appUser, List<AppRole> roles)
        {
            var codingKey = Encoding.UTF8.GetBytes(JwtInfo.SecurityKey); //UTF8 formatında key imizi oluşturduk.Middleware tarafındaki ile aynı olması gerekiyor yoksa token geçersiz kabul edilir
            SymmetricSecurityKey key = new SymmetricSecurityKey(codingKey); //Credential için simetrik bir key oluşturduk
            SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); //Tokenin imzası için kullanacağımız yapıyı key imiz ve belirlediğimiz şifreleme algoritması ile oluşturduk

          

            JwtSecurityToken securityToken = new JwtSecurityToken //Tokenin özelliklerini belirtiyorum
                (
                     issuer: JwtInfo.Issuer, //Üreten bilgisi
                     audience: JwtInfo.Auidience, //Tüketen bilgisi
                     notBefore: DateTime.Now, //Hangi zamandan önce geçerliliğini yitirsin
                     expires: DateTime.Now.AddMinutes(JwtInfo.TokenExpiration), //Ne kadar süre sonra geçerliliğini yitirsin
                     signingCredentials: signingCredentials, //Key ve şifreleme algoritmamız eşliğinde oluştuduğumuz imzayı token'e veriyoruz
                     claims: GetClaims(appUser,roles)
                );

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler(); //Token'ı oluşturacak sınıfımız
            var token = handler.WriteToken(securityToken); //Verdiğimiz özellikler neticesinde token i oluşturduk

            return token;
        }

        private List<Claim> GetClaims(AppUser appUser, List<AppRole> roles)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,appUser.Id.ToString()),
                new Claim(ClaimTypes.Name, appUser.UserName), //User.Identity.Name ile kullanıcı adına ulaşabilmek için claimslere ekledik.Kişiye özel işlemler yapmak adına gerekiyor
            };

            if (roles!=null && roles.Count>0)
            {
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Name)); //Birden fazla rol olabileceği için çoklu olarak döndük
                }
            }

            return claims;
        }
    }
}
