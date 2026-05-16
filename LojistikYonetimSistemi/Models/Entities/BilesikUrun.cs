using System.Collections.Generic;
using System.Linq;

namespace LojistikYonetimSistemi.Models.Entities
{
    // Bilgisayar kasası, hazır PC seti gibi içinde alt parçalar barındıran ürünleri temsil ediyor.
    // Composite Pattern'deki "Composite" düğümü bu sınıf.
    // İçindeki parçalar da IUrun olduğundan, bir bileşik ürünün içinde başka bileşik ürün de olabilir.
    public class BilesikUrun : IUrun
    {
        public int Id { get; set; }
        public string Ad { get; set; } = string.Empty;
        public int Stok { get; set; }
        public int EsikDeger { get; set; }

        // Bu ürünü oluşturan alt parçalar, örneğin: RAM, CPU, Anakart
        public List<IUrun> AltUrunler { get; set; } = new List<IUrun>();

        public BilesikUrun() { }

        public BilesikUrun(int id, string ad, int stok, int esikDeger)
        {
            Id = id;
            Ad = ad;
            Stok = stok;
            // BasitUrun'daki mantığın aynısı: eşik girilmediyse stoğun yüzde 20'si
            EsikDeger = esikDeger > 0 ? esikDeger : (int)(stok * 0.2);
        }

        // Yeni bir parça ekle, örneğin kasanın içine RAM eklemek gibi
        public void AltUrunEkle(IUrun urun) => AltUrunler.Add(urun);

        // Bir parçayı çıkar
        public void AltUrunCikar(IUrun urun) => AltUrunler.Remove(urun);

        // Bileşik ürünün fiyatı = tüm alt parçaların fiyatlarının toplamı
        // Eğer alt parça da bileşikse o da kendi içinde toplayacak, özyinelemeli çalışıyor
        public decimal HesaplaFiyat() => AltUrunler.Sum(u => u.HesaplaFiyat());

        // Ana ürünün stoğu düşünce bildirim gider
        public bool EsikAsildiMi() => Stok <= EsikDeger;

        public override string ToString() => $"{Ad} ({AltUrunler.Count} parça, Stok: {Stok})";
    }
}