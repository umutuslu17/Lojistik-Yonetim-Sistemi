using LojistikYonetimSistemi.Models.Entities;
using LojistikYonetimSistemi.Models.Enums;
using System;

namespace LojistikYonetimSistemi.Models.Patterns.State
{
    // Depo görevlisi paketi hazırlıyor: ürünleri topluyor, kutulara koyuyor.
    // Bu aşamada iptal teknik olarak hâlâ mümkün ama pratikte
    // depo görevlisinin işini yarıda bırakması gerekir.
    // Bazı sistemler bu aşamada iptal kabul etmez, biz kabul ediyoruz
    // ama kullanıcıyı uyarmak mantıklı olur — bunu form tarafı yapacak.
    public class HazirlaniyorDurumu : ISiparisDurumu
    {
        // Paket hazırlandı, artık kargo firmasına teslim edilebilir.
        // Bu geçişte KargoController takip numarası üretecek.
        public void Ilerle(Siparis siparis)
        {
            siparis.DurumIlerlet(SiparisDurumuTip.Kargoda);
        }

        // Hazırlanırken iptal edilirse depo görevlisinin işi yarıda kalır.
        // Gerçek hayatta burada depo görevlisine bildirim gitmesi gerekir,
        // biz şimdilik sadece durumu güncelliyoruz.
        public void Iptal(Siparis siparis)
        {
            siparis.DurumIlerlet(SiparisDurumuTip.IptalEdildi);
        }

        // Paket elimize bile geçmedi, iade başlatılamaz.
        public void IadeBaşlat(Siparis siparis)
        {
            throw new InvalidOperationException(
                "Sipariş hâlâ hazırlanıyor, iade için önce teslim almanız gerekiyor.");
        }

        public string DurumAdi() => "Hazırlanıyor";
    }
}