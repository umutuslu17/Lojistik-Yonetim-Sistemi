using LojistikYonetimSistemi.Models.Patterns.Adapter;
using LojistikYonetimSistemi.Models.Patterns.Singleton;

namespace LojistikYonetimSistemi.Models.Patterns.Decorator
{
    // Kargo paketine sigorta ekleyen Decorator.
    // Müşteri "sigortalı gönderim" seçtiğinde temel kargo ücretinin
    // üzerine sigorta bedeli ekleniyor.
    // Bu sınıfı kullanmak şöyle:
    // IKargoServisi servis = new SigortaDecorator(new ArasKargoAdapter(), 500m);
    // servis.FiyatHesapla(2.5, 300) → Aras fiyatı + sigorta bedeli döner.
    // Müşteri başka bir firmaya geçmek isterse sadece içteki Adapter değişir,
    // sigorta mantığına hiç dokunulmaz.
    public class SigortaDecorator : KargoDecorator
    {
        // Sigortalanacak ürünün beyan değeri — hasar durumunda bu kadar ödenir
        private readonly decimal _urunDegeri;

        // Sigorta bedeli genellikle ürün değerinin belirli bir yüzdesi olur.
        // Biz yüzde 0.5 kullanıyoruz ama bu oran dışarıdan da verilebilir.
        private const decimal SigortaOrani = 0.005m;

        public SigortaDecorator(IKargoServisi kargoServisi, decimal urunDegeri)
            : base(kargoServisi)
        {
            _urunDegeri = urunDegeri;
        }

        // Temel kargo fiyatının üzerine sigorta bedelini ekliyoruz.
        // Temel fiyatı hesaplamak için içteki servise soruyoruz —
        // o da bir Adapter veya başka bir Decorator olabilir.
        public override decimal FiyatHesapla(double agirlikKg, double mesafeKm)
        {
            decimal temelFiyat = _kargoServisi.FiyatHesapla(agirlikKg, mesafeKm);
            decimal sigortaBedeli = _urunDegeri * SigortaOrani;

            Logger.GetInstance().Log(
                $"Sigorta eklendi: ürün değeri {_urunDegeri:C2}, " +
                $"sigorta bedeli {sigortaBedeli:C2}");

            return temelFiyat + sigortaBedeli;
        }

        // Firma adına sigortalı olduğunu belirtiyoruz
        public override string FirmaAdi()
            => $"{_kargoServisi.FirmaAdi()} + Sigorta";
    }
}