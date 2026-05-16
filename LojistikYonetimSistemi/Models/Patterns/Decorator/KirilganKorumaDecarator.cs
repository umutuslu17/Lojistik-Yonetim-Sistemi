using LojistikYonetimSistemi.Models.Patterns.Adapter;
using LojistikYonetimSistemi.Models.Patterns.Singleton;

namespace LojistikYonetimSistemi.Models.Patterns.Decorator
{
    // Kırılgan ürünler için özel ambalaj ve taşıma hizmeti ekleyen Decorator.
    // Cam, elektronik, porselen gibi ürünleri gönderirken
    // kargo firması özel ambalaj kullanıyor ve daha dikkatli taşıyor.
    // Bunun için sabit bir ek ücret alınıyor.
    // Sigorta ile birlikte de kullanılabilir:
    // new SigortaDecorator(new KirilganKorumaDecorator(new ArasKargoAdapter()), 1000m)
    // Bu durumda fiyat: Aras fiyatı + kırılgan koruma ücreti + sigorta bedeli olur.
    public class KirilganKorumaDecorator : KargoDecorator
    {
        // Kırılgan eşya için alınan sabit ek ücret
        // Gerçekte bu firmalara göre değişir, biz sabit tutuyoruz
        private const decimal KirilganEkUcreti = 25.0m;

        public KirilganKorumaDecorator(IKargoServisi kargoServisi)
            : base(kargoServisi)
        {
        }

        // Temel fiyata kırılgan koruma ücretini ekliyoruz.
        // Zincirin başka bir halkası daha varsa o da kendi eklemesini yapar.
        public override decimal FiyatHesapla(double agirlikKg, double mesafeKm)
        {
            decimal temelFiyat = _kargoServisi.FiyatHesapla(agirlikKg, mesafeKm);

            Logger.GetInstance().Log(
                $"Kırılgan koruma eklendi: +{KirilganEkUcreti:C2}");

            return temelFiyat + KirilganEkUcreti;
        }

        // Firma adına kırılgan koruma eklendiğini belirtiyoruz
        public override string FirmaAdi()
            => $"{_kargoServisi.FirmaAdi()} + Kırılgan Koruma";
    }
}