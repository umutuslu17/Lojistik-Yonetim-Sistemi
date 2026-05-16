using LojistikYonetimSistemi.Models.Patterns.Singleton;

namespace LojistikYonetimSistemi.Models.Patterns.Adapter
{
    // GlobalExpres API'sini standart arayüze uyduran Adapter.
    // Bu firma uluslararası olduğu için en fazla dönüşüm burada yapılıyor:
    // ülke kodu ekleniyor, gram/km → kg/km dönüşümleri yapılıyor,
    // sigorta varsayılan olarak kapalı geliyor.
    // Tüm bu karmaşıklık bu sınıfın içinde kalıyor,
    // dışarıya temiz bir IKargoServisi çıkıyor.
    public class GlobalExpresAdapter : IKargoServisi
    {
        private readonly GlobalExpresApi _globalApi;

        // Ülke kodu farklı müşteriler için farklı olabilir,
        // varsayılan olarak Türkiye (TR) kullanıyoruz
        private readonly string _ulkeKodu;

        public GlobalExpresAdapter(string ulkeKodu = "TR")
        {
            _globalApi = new GlobalExpresApi();
            _ulkeKodu = ulkeKodu;
        }

        // GlobalExpres hem sipariş ID'si hem ülke kodu istiyor.
        // Ülke kodunu constructor'da aldığımız için burada hazır.
        public string TakipNoUret(int siparisId)
        {
            string takipNo = _globalApi.CreateShipmentId(siparisId, _ulkeKodu);
            Logger.GetInstance().Log($"GlobalExpres takip no üretildi: {takipNo}");
            return takipNo;
        }

        // GlobalExpres gram cinsinden ağırlık istiyor, biz kg veriyoruz.
        // Dönüşümü burada yapıyoruz: 1 kg = 1000 gram.
        // Sigorta seçeneğini varsayılan olarak false gönderiyoruz,
        // sigortalı kargo için SigortaDecorator kullanılacak zaten.
        public decimal FiyatHesapla(double agirlikKg, double mesafeKm)
        {
            decimal agirlikGram = (decimal)(agirlikKg * 1000);
            decimal mesafe = (decimal)mesafeKm;
            return _globalApi.GetQuote(agirlikGram, mesafe, includeInsurance: false);
        }

        public string KargoDurumuSorgula(string takipNo)
        {
            return _globalApi.TrackParcel(takipNo);
        }

        public string FirmaAdi() => "GlobalExpres";
    }
}