using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egitim.API.DBEgitim.Entites
{
    //Ogrenciler sınıfım veri tabanımdaki tablom olucak
    public class Ogrenciler
    {

        //Aşağıdaki property'ler de Ogrenciler tablomdaki sütunlar olacak
        public int Id { get; set; }
        public string AdiSoyadi { get; set; }
        public string TC { get; set; }
        public string TelefonNumarası { get; set; }
    }
}
