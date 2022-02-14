using Egitim.API.DBEgitim.Entites;

namespace Egitim.API.Abstract
{
    public interface IToken
    {
        string Generate(Yoneticiler user);
        Yoneticiler ReadPayload(string token);
    }
}
