using System;

namespace LojistikYonetimSistemi.Models.Patterns.Adapter
{
    // Yurtiçi Kargo'nun kendi sistemimizdeki simülasyonu.
    // Aras'tan farklı metot isimleri ve farklı parametre yapısı var —
    // bu yüzden ikisini aynı interface üzerinden kullanamayız,
    // Adapter olmadan her kargo firması için ayrı kod yazmak zorunda kalırdık.
    public class YurticiKargoApi
    {
        // Yurtiçi'nin takip kodu kendi formatında
        public string GenerateTrackingCode(string referenceId)
        {
            return $"YK{DateTime.Now:MMddyyyy}{referenceId}";
        }

        // Yurtiçi fiyat hesaplaması kg cinsinden, Aras desi kullanıyordu
        public double CalculateShippingCost(double weightKg, double distanceKm)
        {
            double baseCost = 12.0;
            double weightCost = weightKg * 3.5;
            double distanceCost = distanceKm * 0.02;
            return baseCost + weightCost + distanceCost;
        }

        // Yurtiçi'nin durum sorgulama metodu farklı isimde
        public string GetShipmentStatus(string trackingCode)
        {
            return $"Yurtiçi Kargo: {trackingCode} teslimatta.";
        }
    }
}