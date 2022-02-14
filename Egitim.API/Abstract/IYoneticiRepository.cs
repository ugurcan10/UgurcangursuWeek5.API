using Egitim.API.DBEgitim.Entites;
using Egitim.API.DTO;

namespace Egitim.API.Abstract
{
    public interface IYoneticiRepository
    {
        public Yoneticiler Login(LoginDTO login);
        public void Add(Yoneticiler user);
    }
}
