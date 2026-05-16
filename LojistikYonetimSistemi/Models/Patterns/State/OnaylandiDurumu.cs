using LojistikYonetimSistemi.Models.Entities;
using LojistikYonetimSistemi.Models.Enums;
using System;

namespace LojistikYonetimSistemi.Models.Patterns.State
{
    // Ödeme alındı ve sipariş sisteme onaylandı.
    // Artık depo görevlisi bu siparişi görebilir ve hazırlamaya başlayabilir.
    // İptal hâlâ mümkün çünkü paket henüz hazırlanmaya başlamadı,
    // ama müşteriye ödeme iadesi yapılması gerekecek.
    public class OnaylandiDurumu : ISiparisDurumu
    {
        // Depo görevlisi siparişi aldı ve hazırlamaya başlıyor,
        // durum Hazirlaniyor'a geçiyor.
        public void Ilerle(Siparis siparis)
        {
            siparis.DurumIlerlet(SiparisDurumuTip.Hazirlaniyor);
        }

        // Onaylanmış ama henüz hazırlanmaya başlanmamış sipariş iptal edilebilir.
        // Gerçek hayatta bu noktada ödeme iade süreci de başlatılır.
        public void Iptal(Siparis siparis)
        {
            siparis.DurumIlerlet(SiparisDurumuTip.IptalEdildi);
        }

        // Sipariş onaylandı ama henüz elimize ulaşmadı,
        // iade başlatmak mantıksız olur.
        public void IadeBaşlat(Siparis siparis)
        {
            throw new InvalidOperationException(
                "Sipariş onaylandı ama henüz teslim edilmedi. İade için teslimattan sonra tekrar deneyin.");
        }

        public string DurumAdi() => "Onaylandı";
    }
}