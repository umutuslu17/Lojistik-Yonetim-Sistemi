namespace LojistikYonetimSistemi.Models.Entities
{
    // Siparişin içindeki her bir satırı temsil ediyor.
    // Örneğin: "3 adet kalem" veya "1 adet bilgisayar kasası" birer sipariş kalemi.
    public class SiparisKalemi
    {
        // Hangi ürün olduğunu IUrun üzerinden tutuyoruz,
        // böylece hem BasitUrun hem BilesikUrun buraya girebilir
        public IUrun Urun { get; set; } = null!;

        public int Miktar { get; set; }

        // Bu kalemin toplam tutarı: ürün fiyatı x miktar
        public decimal ToplamFiyat => Urun.HesaplaFiyat() * Miktar;

        public SiparisKalemi() { }

        public SiparisKalemi(IUrun urun, int miktar)
        {
            Urun = urun;
            Miktar = miktar;
        }

        public override string ToString() => $"{Urun.Ad} x{Miktar} = {ToplamFiyat:C2}";
    }
}