using LojistikYonetimSistemi.Models.Entities;
using LojistikYonetimSistemi.Models.Enums;
using System;

namespace LojistikYonetimSistemi.Models.Patterns.State
{
    // Paket müşteriye ulaştı, sipariş tamamlandı.
    // Artık ileriye gidecek bir durum yok, tek seçenek
    // müşterinin iade başlatması. İptal ise tamamen kapalı,
    // teslim edilmiş bir siparişi iptal etmek anlamsız.
    public class TeslimEdildiDurumu : ISiparisDurumu
    {
        // Teslim edildikten sonra ileriye gidecek bir durum yok,
        // bu metodu çağırmak mantıksız.
        public void Ilerle(Siparis siparis)
        {
            throw new InvalidOperationException(
                "Sipariş zaten teslim edildi, daha ileri gidecek bir durum yok.");
        }

        // Teslim sonrası iptal olmaz, sadece iade olur.
        // Müşteri eğer vazgeçmek istiyorsa iade başlatmalı.
        public void Iptal(Siparis siparis)
        {
            throw new InvalidOperationException(
                "Teslim edilmiş sipariş iptal edilemez. İade başlatmak ister misiniz?");
        }

        // İşte asıl kullanım burası: müşteri ürünü beğenmedi veya hatalı geldi,
        // 14 gün içinde iade başlatabilir. Bu kontrolü form tarafı yapacak.
        public void IadeBaşlat(Siparis siparis)
        {
            siparis.DurumIlerlet(SiparisDurumuTip.IadeSurecinde);
        }

        public string DurumAdi() => "Teslim Edildi";
    }
}