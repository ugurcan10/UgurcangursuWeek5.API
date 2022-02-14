using Egitim.API.DBEgitim.Entites;
using Egitim.API.Abstract;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Caching.Memory;

namespace Egitim.API.Concrate
{
    public class Token : IToken
    {
        private IMemoryCache _cache;

        public Token(IMemoryCache cache)
        {
            _cache = cache;
        }

        public string Generate(Yoneticiler user)
        {
            //TokenKey cache'de var mı sorguladık. Varsa return ettik.
            if (!_cache.TryGetValue("tokenKey", out string _tokenKey))
            {
                MemoryCacheEntryOptions memoryCacheEntryOptions = new MemoryCacheEntryOptions();
                memoryCacheEntryOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                _cache.Set("tokenKey", "Karabiberim vur kadehlere hadi içelim, içelim her gece zevki sefa, doldu gönmlüme.Hadi içelim, acıların yerine...", memoryCacheEntryOptions);

                return Generate(user);
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_tokenKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    //Token içerisinde aşağıdaki bilgiler tutulacak.
                    new Claim(ClaimTypes.PrimarySid, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.NameSurname),
                    new Claim(ClaimTypes.Email, user.Username),
                    new Claim(ClaimTypes.Role, user.Authority.ToString())
                }),
                Expires = DateTime.Now.AddMinutes(1), //Tokenımın yaşam süresi 15 dk olsun

                //key ve keyin şifreleme algoritması
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public Yoneticiler ReadPayload(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            //gelen tokenı decode ettik
            var jwtSecurityToken = handler.ReadJwtToken(token);
            return new Yoneticiler
            {
                //tokendaki payloadın içerisinde bulunan değerleri alıp Yoneticiler nesnesinin içerisine atıyoruz.
                Id = Convert.ToInt32(jwtSecurityToken.Payload.FirstOrDefault(x => x.Key == "nameid").Value),
                NameSurname = jwtSecurityToken.Payload.FirstOrDefault(x => x.Key == "unique_name").Value.ToString(),
                Username = jwtSecurityToken.Payload.FirstOrDefault(x => x.Key == "email").Value.ToString(),
                Authority = Convert.ToByte(jwtSecurityToken.Payload.FirstOrDefault(x => x.Key == "role").Value)
            };
        }
    }
}
