using System;
using Xunit;
using LojistikYonetimSistemi.Models.Entities;
using LojistikYonetimSistemi.Models.Enums;

namespace LojistikYonetimSistemi.Tests
{
    // State Pattern testleri — sipariş durum geçişlerinin doğru çalıştığını kanıtlıyoruz.
    // Kargodaki sipariş iptal edilememeli, beklemedeki sipariş ilerleyebilmeli gibi
    // iş kurallarının State Pattern sayesinde doğru uygulandığını gösteriyoruz.
    public class StatePatternTests
    {
        // Test için temel sipariş oluştur
        private Siparis TestSiparisiOlustur(SiparisDurumuTip durum = SiparisDurumuTip.Beklemede)
        {
            var siparis = new Siparis(1, 1, "Kredi Kartı");
            siparis.DurumIlerlet(durum);
            return siparis;
        }

        // Test 1: Beklemede durumundan Onaylandı'ya geçiş çalışıyor mu?
        [Fact]
        public void Ilerle_BeklemedeDurumu_OnaylandiYaGecer()
        {
            // Arrange
            var siparis = TestSiparisiOlustur(SiparisDurumuTip.Beklemede);

            // Act
            siparis.Ilerle();

            // Assert
            Assert.Equal(SiparisDurumuTip.Onaylandi, siparis.MevcutDurumTip);
        }

        // Test 2: Onaylandı → Hazırlanıyor geçişi çalışıyor mu?
        [Fact]
        public void Ilerle_OnaylandiDurumu_HazirlaniyoraGecer()
        {
            // Arrange
            var siparis = TestSiparisiOlustur(SiparisDurumuTip.Onaylandi);

            // Act
            siparis.Ilerle();

            // Assert
            Assert.Equal(SiparisDurumuTip.Hazirlaniyor, siparis.MevcutDurumTip);
        }

        // Test 3: Kargodaki sipariş iptal edilemiyor mu? — en kritik iş kuralı
        [Fact]
        public void Iptal_KargodaDurumu_ExceptionFirlatir()
        {
            // Arrange — sipariş kargoda durumuna al
            var siparis = TestSiparisiOlustur(SiparisDurumuTip.Kargoda);

            // Act & Assert — kargodaki sipariş iptal edilemez
            Assert.Throws<InvalidOperationException>(() => siparis.Iptal());
        }

        // Test 4: Beklemedeki sipariş iptal edilebiliyor mu?
        [Fact]
        public void Iptal_BeklemeDurumu_IptalEdilir()
        {
            // Arrange
            var siparis = TestSiparisiOlustur(SiparisDurumuTip.Beklemede);

            // Act
            siparis.Iptal();

            // Assert
            Assert.Equal(SiparisDurumuTip.IptalEdildi, siparis.MevcutDurumTip);
        }

        // Test 5: Kargodaki siparişe iade başlatılabiliyor mu?
        [Fact]
        public void IadeBaslat_KargodaDurumu_IadeSurecineGecer()
        {
            // Arrange
            var siparis = TestSiparisiOlustur(SiparisDurumuTip.Kargoda);

            // Act
            siparis.IadeBaslat();

            // Assert
            Assert.Equal(SiparisDurumuTip.IadeSurecinde, siparis.MevcutDurumTip);
        }

        // Test 6: Teslim edilmiş siparişe iade başlatılabiliyor mu?
        [Fact]
        public void IadeBaslat_TeslimEdildiDurumu_IadeSurecineGecer()
        {
            // Arrange
            var siparis = TestSiparisiOlustur(SiparisDurumuTip.TeslimEdildi);

            // Act
            siparis.IadeBaslat();

            // Assert
            Assert.Equal(SiparisDurumuTip.IadeSurecinde, siparis.MevcutDurumTip);
        }

        // Test 7: Beklemedeki siparişe iade başlatılamıyor mu?
        [Fact]
        public void IadeBaslat_BeklemeDurumu_ExceptionFirlatir()
        {
            // Arrange
            var siparis = TestSiparisiOlustur(SiparisDurumuTip.Beklemede);

            // Act & Assert — henüz teslim edilmemiş, iade başlatılamaz
            Assert.Throws<InvalidOperationException>(() => siparis.IadeBaslat());
        }

        // Test 8: İptal edilmiş siparişi ilerletime çalışınca hata fırlatıyor mu?
        [Fact]
        public void Ilerle_IptalEdildiDurumu_ExceptionFirlatir()
        {
            // Arrange
            var siparis = TestSiparisiOlustur(SiparisDurumuTip.IptalEdildi);

            // Act & Assert — iptal edilmiş sipariş üzerinde işlem yapılamaz
            Assert.Throws<InvalidOperationException>(() => siparis.Ilerle());
        }

        // Test 9: DurumNesnesiYukle doğru State nesnesini oluşturuyor mu?
        [Fact]
        public void DurumNesnesiYukle_KargodaTipi_KargodaDurumuOlusturur()
        {
            // Arrange — JSON'dan yüklenen sipariş simülasyonu
            var siparis = new Siparis();
            siparis.MevcutDurumTip = SiparisDurumuTip.Kargoda;

            // Act — Repository'nin yaptığı gibi yükle
            siparis.DurumNesnesiYukle();

            // Assert — aktif durum doğru mu?
            Assert.Equal("Kargoda", siparis.AktifDurum.DurumAdi());
        }
    }
}