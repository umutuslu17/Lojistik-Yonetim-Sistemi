using System;
using LojistikYonetimSistemi.Models.Patterns.Singleton;

namespace LojistikYonetimSistemi.Models.Patterns.Adapter
{
    // İşte Adapter Pattern'in özü bu sınıf.
    // ArasKargoApi'nin kendine özgü metotlarını alıp
    // IKargoServisi'nin standart arayüzüne uyduruyor.
    // Controller ve formlar sadece IKargoServisi ile konuşuyor,
    // arkada Aras mı Yurtiçi mi çalışıyor bilmek zorunda değiller.
    public class ArasKargoAdapter : IKargoServisi
    {
        // Adapte edeceğimiz Aras API nesnesi
        private readonly ArasKargoApi _arasApi;

        public ArasKargoAdapter()
        {
            _arasApi = new ArasKargoApi();
        }

        // IKargoServisi'nin standart metodunu Aras'ın metoduna yönlendiriyoruz.
        // Aras desi kullanıyor ama biz sipariş Id'si veriyoruz —
        // dönüşümü burada yapıyoruz, dışarıya hiç yansıtmıyoruz.
        public string TakipNoUret(int siparisId)
        {
            string takipNo = _arasApi.ArasTakipKoduOlustur(siparisId);
            Logger.GetInstance().Log($"Aras Kargo takip no üretildi: {takipNo}");
            return takipNo;
        }

        // Bizim sistemimiz kg ve km kullanıyor,
        // Aras desi ve metre kullanıyor — dönüşümü burada yapıyoruz.
        // 1 kg = 10 desi, km zaten tam sayıya yuvarlanıyor.
        public decimal FiyatHesapla(double agirlikKg, double mesafeKm)
        {
            int desi = (int)(agirlikKg * 10);
            int mesafe = (int)mesafeKm;
            return _arasApi.ArasFiyatHesapla(desi, mesafe);
        }

        // Standart takip sorgusunu Aras'ın metoduna yönlendiriyoruz
        public string KargoDurumuSorgula(string takipNo)
        {
            return _arasApi.ArasSorgula(takipNo);
        }

        public string FirmaAdi() => "Aras Kargo";
    }
}