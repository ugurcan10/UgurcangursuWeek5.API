using Egitim.API.DBEgitim.Entites;
using Egitim.API.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Egitim.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IToken _token;

        public TokenController(IToken token)
        {
            _token = token;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult RefreshToken(string token)
        {
            Yoneticiler user = _token.ReadPayload(token);
            string newToken = _token.Generate(user);
            return Ok(newToken);
        }
    }
}
