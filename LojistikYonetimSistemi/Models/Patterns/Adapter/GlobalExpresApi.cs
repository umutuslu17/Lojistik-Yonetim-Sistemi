using System;

namespace LojistikYonetimSistemi.Models.Patterns.Adapter
{
    // GlobalExpres'in kendi API'si — uluslararası kargo firması olduğu için
    // hem parametre isimleri hem de hesaplama mantığı tamamen farklı.
    // Üstelik İngilizce metot isimleri kullanıyor, diğerlerinin bir kısmı Türkçeydi.
    // Adapter olmadan bu üç firmayı tek bir yapıda kullanmak mümkün olmazdı.
    public class GlobalExpresApi
    {
        // GlobalExpres shipment ID'sini kendi formatında üretiyor
        public string CreateShipmentId(int orderId, string countryCode)
        {
            return $"GX{countryCode}{DateTime.Now:yyyyMMdd}{orderId:D6}";
        }

        // Uluslararası kargo için fiyat hesabı çok daha karmaşık:
        // ağırlık, mesafe ve gümrük vergisi birlikte hesaplanıyor
        public decimal GetQuote(decimal weightGrams, decimal distanceKm, bool includeInsurance)
        {
            decimal baseRate = 45.0m;
            decimal weightRate = (weightGrams / 1000) * 8.0m;
            decimal distanceRate = distanceKm * 0.05m;
            decimal insuranceRate = includeInsurance ? 25.0m : 0.0m;
            return baseRate + weightRate + distanceRate + insuranceRate;
        }

        // Durum sorgulama metodu tamamen farklı bir isimde
        public string TrackParcel(string shipmentId)
        {
            return $"GlobalExpres: Shipment {shipmentId} is in transit.";
        }
    }
}