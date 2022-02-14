using System;

namespace Egitim.API.DBEgitim.Entites
{
    //Egitimler sınıfım veri tabanımdaki tablom olucak
    public class Egitimler
    {

        //Aşağıdaki property'ler de eğitimler tablomdaki sütunlar olacak
        public int Id { get; set; }
        public string EgitimAdi { get; set; }
        public string Kategori { get; set; }
        public int EgitmenID { get; set; }
        public DateTime Tarihi { get; set; }
    }
}
