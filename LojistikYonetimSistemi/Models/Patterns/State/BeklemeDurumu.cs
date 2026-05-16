using LojistikYonetimSistemi.Models.Entities;
using LojistikYonetimSistemi.Models.Enums;
using System;

namespace LojistikYonetimSistemi.Models.Patterns.State
{
    // Sipariş ilk oluşturulduğunda bu durumda başlar.
    // Müşteri siparişi verdi ama henüz ödeme onayı alınmadı veya
    // sistem siparişi henüz işleme koymadı.
    // Bu aşamada sipariş hâlâ iptal edilebilir çünkü ortada
    // hazırlanmış bir paket veya yola çıkmış bir kargo yok.
    public class BeklemeDurumu : ISiparisDurumu
    {
        // Beklemede olan sipariş onaylandığında bu metot çağrılır.
        // Ödeme alındı, sipariş sisteme düştü, artık hazırlanmaya başlayabilir.
        public void Ilerle(Siparis siparis)
        {
            // Durumu Onaylandi'ya taşıyoruz ve ne zaman onaylandığını loglayabiliriz
            siparis.DurumIlerlet(SiparisDurumuTip.Onaylandi);
        }

        // Beklemedeyken iptal tamamen serbesttir,
        // henüz hiç bir işlem başlamadı ki.
        public void Iptal(Siparis siparis)
        {
            siparis.DurumIlerlet(SiparisDurumuTip.IptalEdildi);
        }

        // Sipariş daha teslim bile edilmedi, iade başlatılamaz.
        // Bu çağrı yapılırsa kullanıcıya anlamlı bir mesaj veriyoruz.
        public void IadeBaşlat(Siparis siparis)
        {
            throw new InvalidOperationException(
                "Sipariş henüz beklemede, iade başlatmak için önce teslim alınması gerekiyor.");
        }

        public string DurumAdi() => "Beklemede";
    }
}