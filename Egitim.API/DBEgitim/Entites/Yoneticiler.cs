
namespace Egitim.API.DBEgitim.Entites
{
    public class Yoneticiler
    {
        public int Id { get; set; }
        public string NameSurname { get; set; }
        public string TelNo { get; set; }
        public byte Authority { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
    }
}
