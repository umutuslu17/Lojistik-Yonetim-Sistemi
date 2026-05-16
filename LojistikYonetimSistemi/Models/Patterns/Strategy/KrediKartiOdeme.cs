using System;
using LojistikYonetimSistemi.Models.Patterns.Singleton;

namespace LojistikYonetimSistemi.Models.Patterns.Strategy
{
    // En yaygın ödeme yöntemi, müşteriler genellikle bunu seçiyor.
    // Gerçek hayatta burada bir POS veya ödeme geçidi entegrasyonu olurdu,
    // kart numarası doğrulanır, banka onayı beklenir vs.
    // Biz sistemi simüle ediyoruz ama yapı tamamen gerçekçi.
    public class KrediKartiOdeme : IOdemeStratejisi
    {
        // Kart sahibinin adı soyadı
        private readonly string _kartSahibi;

        // Kartın son 4 hanesi — tam numarayı tutmak güvenlik açığı olurdu
        private readonly string _kartSonDortHane;

        public KrediKartiOdeme(string kartSahibi, string kartSonDortHane)
        {
            _kartSahibi = kartSahibi;
            _kartSonDortHane = kartSonDortHane;
        }

        // Ödemeyi işleme al.
        // Gerçekte banka API'sine istek atılır, onay kodu beklenir.
        // Burada başarılı kabul ediyoruz ve işlemi logluyoruz.
        public bool OdemeYap(decimal tutar)
        {
            // Ödeme işlemini Logger üzerinden kayıt altına alıyoruz
            Logger.GetInstance().Log(
                $"Kredi kartı ödemesi: {_kartSahibi} - ****{_kartSonDortHane} - {tutar:C2}");

            // Gerçek sistemde banka API yanıtı burada değerlendirilir,
            // biz her zaman başarılı kabul ediyoruz
            return true;
        }

        public string YontemAdi() => $"Kredi Kartı (****{_kartSonDortHane})";
    }
}