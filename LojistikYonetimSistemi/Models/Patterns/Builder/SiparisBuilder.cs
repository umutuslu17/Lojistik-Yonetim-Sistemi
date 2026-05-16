using System;
using System.Collections.Generic;
using LojistikYonetimSistemi.Models.Entities;
using LojistikYonetimSistemi.Models.Patterns.Singleton;
using LojistikYonetimSistemi.Models.Patterns.Strategy;

namespace LojistikYonetimSistemi.Models.Patterns.Builder
{
    // Builder Pattern'in kullanım sebebi şu:
    // Siparis nesnesi oluşturmak karmaşık bir süreç —
    // müşteri bilgisi, birden fazla ürün kalemi, ödeme yöntemi
    // hepsinin tek seferde constructor'a verilmesi hem çirkin hem hata prone.
    // Builder ile siparişi adım adım inşa ediyoruz:
    // önce müşteriyi belirle, sonra ürünleri ekle, sonra ödemeyi ata, sonra üret.
    // Bu sayede form tarafı her adımı ayrı ayrı yapabilir,
    // ve Build() çağrılana kadar hiçbir şey tamamlanmış sayılmaz.
    public class SiparisBuilder
    {
        // İnşa edilen sipariş nesnesi — Build() çağrılana kadar tamamlanmamış
        private Siparis _siparis;

        // Her yeni sipariş için builder'ı sıfırla ve taze bir nesneyle başla.
        // Aynı builder'ı birden fazla sipariş için kullanmak istersen
        // her seferinde SiparisiBaslat() çağırman yeterli.
        public SiparisBuilder SiparisiBaslat(int siparisId, int musteriId)
        {
            _siparis = new Siparis(siparisId, musteriId, string.Empty);
            Logger.GetInstance().Log(
                $"Yeni sipariş inşası başladı — ID: {siparisId}, Müşteri: {musteriId}");
            return this;
        }

        // Siparişe bir ürün kalemi ekle.
        // Aynı ürünü birden fazla miktarda almak için miktar parametresi var.
        // return this diyerek zincirleme kullanım sağlıyoruz:
        // builder.UrunEkle(urun1, 2).UrunEkle(urun2, 1).OdemeAta(...)
        public SiparisBuilder UrunEkle(IUrun urun, int miktar)
        {
            if (_siparis == null)
                throw new InvalidOperationException(
                    "Önce SiparisiBaslat() çağrılmalı.");

            if (miktar <= 0)
                throw new ArgumentException(
                    $"Miktar sıfırdan büyük olmalı. Gelen değer: {miktar}");

            if (urun.Stok < miktar)
                throw new InvalidOperationException(
                    $"'{urun.Ad}' için yeterli stok yok. " +
                    $"İstenen: {miktar}, Mevcut: {urun.Stok}");

            _siparis.Kalemler.Add(new SiparisKalemi(urun, miktar));

            Logger.GetInstance().Log(
                $"Sipariş #{_siparis.Id} — '{urun.Ad}' x{miktar} eklendi.");

            return this;
        }

        // Ödeme yöntemini siparişe bağla ve ödemeyi gerçekleştir.
        // Ödeme başarısız olursa sipariş oluşturulmaz.
        public SiparisBuilder OdemeAta(IOdemeStratejisi strateji)
        {
            if (_siparis == null)
                throw new InvalidOperationException(
                    "Önce SiparisiBaslat() çağrılmalı.");

            if (_siparis.Kalemler.Count == 0)
                throw new InvalidOperationException(
                    "Siparişe en az bir ürün eklenmeli.");

            // Ödemeyi gerçekleştir
            bool odemeBasarili = strateji.OdemeYap(_siparis.ToplamTutar);

            if (!odemeBasarili)
                throw new InvalidOperationException(
                    "Ödeme başarısız oldu, sipariş oluşturulamadı.");

            // Ödeme yöntemi adını siparişe kaydediyoruz
            _siparis.OdemeYontemi = strateji.YontemAdi();

            Logger.GetInstance().Log(
                $"Sipariş #{_siparis.Id} — Ödeme alındı: {strateji.YontemAdi()}, " +
                $"Tutar: {_siparis.ToplamTutar:C2}");

            return this;
        }

        // Tüm adımlar tamamlandı, siparişi bitir ve döndür.
        // Bu noktadan sonra builder temizlenir, yeni sipariş için hazır.
        public Siparis Build()
        {
            if (_siparis == null)
                throw new InvalidOperationException(
                    "Önce SiparisiBaslat() çağrılmalı.");

            if (_siparis.Kalemler.Count == 0)
                throw new InvalidOperationException(
                    "Siparişe en az bir ürün eklenmeli.");

            if (string.IsNullOrEmpty(_siparis.OdemeYontemi))
                throw new InvalidOperationException(
                    "Ödeme yöntemi atanmadan sipariş tamamlanamaz.");

            Siparis tamamlananSiparis = _siparis;

            Logger.GetInstance().Log(
                $"Sipariş #{tamamlananSiparis.Id} başarıyla oluşturuldu. " +
                $"Toplam: {tamamlananSiparis.ToplamTutar:C2}");

            // Builder'ı temizliyoruz, bir sonraki sipariş için hazır
            _siparis = null;

            return tamamlananSiparis;
        }
    }
}