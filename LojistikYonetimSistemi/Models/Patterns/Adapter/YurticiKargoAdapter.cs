using LojistikYonetimSistemi.Models.Patterns.Singleton;

namespace LojistikYonetimSistemi.Models.Patterns.Adapter
{
    // Yurtiçi Kargo API'sini standart IKargoServisi arayüzüne uyduran Adapter.
    // Yurtiçi'nin metot isimleri İngilizce ve parametreler double cinsinden,
    // bizim sistemimiz Türkçe metot isimleri ve decimal kullanıyor.
    // Bu dönüşümlerin hepsi burada halloluyor.
    public class YurticiKargoAdapter : IKargoServisi
    {
        private readonly YurticiKargoApi _yurticiApi;

        public YurticiKargoAdapter()
        {
            _yurticiApi = new YurticiKargoApi();
        }

        // Yurtiçi string referans istiyor, biz int siparisId veriyoruz.
        // Dönüşümü burada yapıp Yurtiçi'ye yolluyoruz.
        public string TakipNoUret(int siparisId)
        {
            string takipNo = _yurticiApi.GenerateTrackingCode(siparisId.ToString());
            Logger.GetInstance().Log($"Yurtiçi Kargo takip no üretildi: {takipNo}");
            return takipNo;
        }

        // Yurtiçi double döndürüyor, biz decimal kullanıyoruz.
        // Tip dönüşümünü burada yapıyoruz, dışarıya decimal çıkıyor.
        public decimal FiyatHesapla(double agirlikKg, double mesafeKm)
        {
            double fiyat = _yurticiApi.CalculateShippingCost(agirlikKg, mesafeKm);
            return (decimal)fiyat;
        }

        public string KargoDurumuSorgula(string takipNo)
        {
            return _yurticiApi.GetShipmentStatus(takipNo);
        }

        public string FirmaAdi() => "Yurtiçi Kargo";
    }
}