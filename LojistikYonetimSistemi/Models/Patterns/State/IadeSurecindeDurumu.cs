using LojistikYonetimSistemi.Models.Entities;
using LojistikYonetimSistemi.Models.Enums;
using System;

namespace LojistikYonetimSistemi.Models.Patterns.State
{
    // Müşteri iade talebini açtı, şu an işlem sürüyor.
    // Bu aşamada ne ileri gidilebilir ne de iptal yapılabilir.
    // İade süreci tamamlandığında sistem bu siparişi
    // IptalEdildi durumuna taşır (çünkü sipariş artık geçersiz).
    public class IadeSurecindeDurumu : ISiparisDurumu
    {
        // İade onaylandı, sipariş artık kapatılıyor.
        // Ödeme iadesi bu noktada başlatılır.
        public void Ilerle(Siparis siparis)
        {
            // İade tamamlandı, siparişi kapalı duruma alıyoruz
            siparis.DurumIlerlet(SiparisDurumuTip.IptalEdildi);
        }

        // İade sürecinde ayrıca iptal açmak anlamsız,
        // zaten iade işlemi devam ediyor.
        public void Iptal(Siparis siparis)
        {
            throw new InvalidOperationException(
                "İade süreci zaten devam ediyor, ayrıca iptal açılamaz.");
        }

        // İade zaten açık, tekrar başlatılamaz.
        public void IadeBaşlat(Siparis siparis)
        {
            throw new InvalidOperationException(
                "Bu sipariş için zaten bir iade talebi açık.");
        }

        public string DurumAdi() => "İade Sürecinde";
    }
}