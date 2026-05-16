using LojistikYonetimSistemi.Models.Entities;
using LojistikYonetimSistemi.Models.Enums;
using System;

namespace LojistikYonetimSistemi.Models.Patterns.State
{
    // Bu son durum, siparişin yaşam döngüsünün bittiği yer.
    // İptal edilmiş siparişte artık hiçbir şey yapılamaz.
    // Sadece görüntülenebilir, geçmişte kalır.
    public class IptalEdildiDurumu : ISiparisDurumu
    {
        // İptal edilmiş sipariş üzerinde ilerleme olmaz.
        public void Ilerle(Siparis siparis)
        {
            throw new InvalidOperationException(
                "Sipariş iptal edildi, üzerinde işlem yapılamaz.");
        }

        // Zaten iptal, tekrar iptal edilemez.
        public void Iptal(Siparis siparis)
        {
            throw new InvalidOperationException(
                "Sipariş zaten iptal edilmiş durumda.");
        }

        // İptal edilmiş bir sipariş için iade açmak anlamsız.
        public void IadeBaşlat(Siparis siparis)
        {
            throw new InvalidOperationException(
                "İptal edilmiş sipariş için iade başlatılamaz.");
        }

        public string DurumAdi() => "İptal Edildi";
    }
}