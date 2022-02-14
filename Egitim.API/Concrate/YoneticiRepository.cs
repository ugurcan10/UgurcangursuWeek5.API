using Egitim.API.Abstract;
using Egitim.API.DBEgitim;
using Egitim.API.DBEgitim.Entites;
using Egitim.API.DTO;
using System.Linq;

namespace Egitim.API.Concrate
{
    public class YoneticiRepository : IYoneticiRepository
    {
        protected readonly Context _context;
        public YoneticiRepository(Context context)
        {
            _context = context;
        }

        public void Add(Yoneticiler user)
        {
            _context.Yoneticiler.Add(user);
            _context.SaveChangesAsync();
        }

        public Yoneticiler Login(LoginDTO login)
        {
            Yoneticiler user = null;
            user = _context.Yoneticiler.SingleOrDefault(x => x.Username == login.username && x.Password == login.pass);

            if (user == null)
            {
                return null;
            }

            return user;
        }
    }
}
