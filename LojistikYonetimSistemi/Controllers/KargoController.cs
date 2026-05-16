using System;
using System.Collections.Generic;
using LojistikYonetimSistemi.Models.Entities;
using LojistikYonetimSistemi.Models.Enums;
using LojistikYonetimSistemi.Models.Patterns.Adapter;
using LojistikYonetimSistemi.Models.Patterns.Decorator;
using LojistikYonetimSistemi.Models.Patterns.Factory;
using LojistikYonetimSistemi.Models.Patterns.Singleton;
using LojistikYonetimSistemi.Models.Repositories;

namespace LojistikYonetimSistemi.Controllers
{
    // Kargo atama, fiyat hesaplama ve takip işlemlerinin yönetildiği controller.
    // Factory Pattern ile kargo firması seçimi,
    // Adapter Pattern ile firma API'lerinin standartlaştırılması,
    // Decorator Pattern ile ek hizmetlerin fiyata yansıtılması
    // hepsi bu controller üzerinden koordine ediliyor.
    // Form katmanı hangi kargo firmasının hangi API'yi kullandığını
    // bilmek zorunda değil, sadece firma adını ve ek hizmet tercihlerini bildiriyor.
    public class KargoController
    {
        private readonly SiparisRepository _siparisRepo;
        private readonly KullaniciController _kullaniciController;

        public KargoController(KullaniciController kullaniciController)
        {
            _siparisRepo = new SiparisRepository();
            _kullaniciController = kullaniciController;
        }

        // Siparişe kargo firması ata ve takip numarası üret.
        // Factory ile firma seçimi, Adapter ile standart arayüz,
        // Decorator ile ek hizmetler — üçü birlikte burada çalışıyor.
        public bool KargoAta(
            int siparisId,
            string firmaAdi,
            double agirlikKg,
            double mesafeKm,
            bool sigortaEkle = false,
            decimal urunDegeri = 0,
            bool kirilganKoruma = false)
        {
            if (!_kullaniciController.YetkiKontrolCoklu(
                KullaniciRolu.Admin, KullaniciRolu.DepoGorevlisi))
            {
                Logger.GetInstance().Log(
                    $"Yetkisiz işlem: {_kullaniciController.AktifKullanici?.Ad} kargo atamaya çalıştı.");
                return false;
            }

            var siparis = _siparisRepo.IdIleGetir(siparisId);
            if (siparis == null)
            {
                Logger.GetInstance().Log($"Kargo atanamadı: Sipariş #{siparisId} bulunamadı.");
                return false;
            }

            // Sadece Hazirlaniyor durumundaki siparişlere kargo atanabilir
            if (siparis.MevcutDurumTip != SiparisDurumuTip.Hazirlaniyor)
            {
                Logger.GetInstance().Log(
                    $"Kargo atanamadı: Sipariş #{siparisId} hazırlanıyor durumunda değil. " +
                    $"Mevcut durum: {siparis.MevcutDurumTip}");
                return false;
            }

            try
            {
                // Factory ile doğru kargo servisini oluşturuyoruz.
                // Hangi Adapter sınıfının döneceğini controller bilmiyor,
                // sadece firma adını veriyor.
                IKargoServisi kargoServisi = KargoFactory.Olustur(firmaAdi);

                // Ek hizmetler seçildiyse Decorator zinciri kuruyoruz.
                // Kırılgan koruma varsa önce onu sarıyoruz,
                // sigorta varsa onun üzerine sarıyoruz.
                // Sıra önemli değil ama fiyatlar doğru toplanıyor.
                if (kirilganKoruma)
                    kargoServisi = new KirilganKorumaDecorator(kargoServisi);

                if (sigortaEkle && urunDegeri > 0)
                    kargoServisi = new SigortaDecorator(kargoServisi, urunDegeri);

                // Takip numarasını üret ve siparişe ata
                string takipNo = kargoServisi.TakipNoUret(siparisId);
                siparis.TakipNumarasi = takipNo;
                siparis.KargoFirmasi = kargoServisi.FirmaAdi();

                // Kargo fiyatını hesapla ve logla
                decimal kargoFiyati = kargoServisi.FiyatHesapla(agirlikKg, mesafeKm);
                Logger.GetInstance().Log(
                    $"Sipariş #{siparisId} → {kargoServisi.FirmaAdi()} | " +
                    $"Takip: {takipNo} | Kargo ücreti: {kargoFiyati:C2}");

                // Siparişi Kargoda durumuna taşı
                siparis.Ilerle();

                // Güncel siparişi JSON'a kaydet
                _siparisRepo.Guncelle(siparis);
                return true;
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Log($"Kargo atama hatası: {ex.Message}");
                return false;
            }
        }

        // Takip numarasına göre kargo durumunu sorgula.
        // Hangi firmaya ait olduğunu kargo firması adından anlıyoruz.
        public string KargoDurumuSorgula(int siparisId)
        {
            var siparis = _siparisRepo.IdIleGetir(siparisId);
            if (siparis == null)
                return "Sipariş bulunamadı.";

            if (string.IsNullOrEmpty(siparis.TakipNumarasi))
                return "Bu siparişe henüz kargo atanmamış.";

            try
            {
                // Factory ile aynı firmayı tekrar oluşturup sorgu yapıyoruz
                IKargoServisi kargoServisi = KargoFactory.Olustur(siparis.KargoFirmasi);
                return kargoServisi.KargoDurumuSorgula(siparis.TakipNumarasi);
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Log($"Kargo durum sorgusu hatası: {ex.Message}");
                return "Kargo durumu sorgulanamadı.";
            }
        }

        // Kargo fiyatını önceden hesapla — formda kullanıcıya göstermek için.
        // Henüz kargo atanmadan, sadece bilgilendirme amaçlı.
        public decimal FiyatOnizleme(
            string firmaAdi,
            double agirlikKg,
            double mesafeKm,
            bool sigortaEkle = false,
            decimal urunDegeri = 0,
            bool kirilganKoruma = false)
        {
            try
            {
                IKargoServisi kargoServisi = KargoFactory.Olustur(firmaAdi);

                if (kirilganKoruma)
                    kargoServisi = new KirilganKorumaDecorator(kargoServisi);

                if (sigortaEkle && urunDegeri > 0)
                    kargoServisi = new SigortaDecorator(kargoServisi, urunDegeri);

                return kargoServisi.FiyatHesapla(agirlikKg, mesafeKm);
            }
            catch
            {
                return 0;
            }
        }

        // Kargodaki siparişi teslim edildi olarak işaretle — Kurye yapar
        public bool TeslimEdildiIsaretle(int siparisId)
        {
            if (!_kullaniciController.YetkiKontrolCoklu(
                KullaniciRolu.Admin, KullaniciRolu.Kurye))
            {
                Logger.GetInstance().Log(
                    $"Yetkisiz işlem: {_kullaniciController.AktifKullanici?.Ad} teslim işareti koymaya çalıştı.");
                return false;
            }

            var siparis = _siparisRepo.IdIleGetir(siparisId);
            if (siparis == null) return false;

            if (siparis.MevcutDurumTip != SiparisDurumuTip.Kargoda)
            {
                Logger.GetInstance().Log(
                    $"Teslim işareti konulamadı: Sipariş #{siparisId} kargoda değil.");
                return false;
            }

            try
            {
                siparis.Ilerle();
                _siparisRepo.Guncelle(siparis);

                Logger.GetInstance().Log(
                    $"Sipariş #{siparisId} teslim edildi olarak işaretlendi.");
                return true;
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Log($"Teslim işareti hatası: {ex.Message}");
                return false;
            }
        }

        // Formda kargo firması ComboBox'ını doldurmak için
        public string[] FirmaListesiGetir()
        {
            return KargoFactory.FirmaListesi();
        }
    }
}