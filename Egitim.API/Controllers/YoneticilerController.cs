using Egitim.API.Abstract;
using Egitim.API.DBEgitim.Entites;
using Egitim.API.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Egitim.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class YoneticilerController : ControllerBase
    {
        private readonly IYoneticiRepository _yoneticiRepository;
        private readonly IToken _token;

        public YoneticilerController(IYoneticiRepository yoneticiRepository,
                        IToken token)
        {
            _yoneticiRepository = yoneticiRepository;
            _token = token;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginDTO login)
        {
            Yoneticiler user = _yoneticiRepository.Login(login);
            if (user == null)
            {
                return Unauthorized();
            }
            string token = _token.Generate(user);

            return Ok(token);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register(Yoneticiler user)
        {
            _yoneticiRepository.Add(user);
            return Created("/", "İşlem başarılı");
        }
    }
}
