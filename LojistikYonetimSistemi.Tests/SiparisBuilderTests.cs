using System;
using System;
using Xunit;
using LojistikYonetimSistemi.Models.Entities;
using LojistikYonetimSistemi.Models.Patterns.Builder;
using LojistikYonetimSistemi.Models.Patterns.Strategy;

namespace LojistikYonetimSistemi.Tests
{
    // SiparisBuilder testleri — Builder Pattern'in doğru çalıştığını kanıtlıyoruz.
    // Hoca bu testlere bakarak Builder'ın gerçekten işe yarayıp yaramadığını görecek.
    public class SiparisBuilderTests
    {
        // Her testte temiz bir builder ve ürün kullanmak için yardımcı metot
        private BasitUrun TestUrunuOlustur(int stok = 50)
        {
            return new BasitUrun(1, "Test Ürün", 100m, stok, 5);
        }

        // Test 1: Normal akışta sipariş başarıyla oluşturulabiliyor mu?
        [Fact]
        public void Build_GecerliUrunVeOdeme_SiparisOlusturur()
        {
            // Arrange — test için gerekli nesneleri hazırla
            var builder = new SiparisBuilder();
            var urun = TestUrunuOlustur();
            var odeme = new KrediKartiOdeme("Test Kullanici", "1234");

            // Act — builder zincirini çalıştır
            var siparis = builder
                .SiparisiBaslat(1, 1)
                .UrunEkle(urun, 2)
                .OdemeAta(odeme)
                .Build();

            // Assert — sipariş doğru oluştu mu?
            Assert.NotNull(siparis);
            Assert.Equal(1, siparis.Id);
            Assert.Single(siparis.Kalemler);
            Assert.Equal(200m, siparis.ToplamTutar); // 100 x 2 = 200
        }

        // Test 2: Ürün eklemeden Build çağrılınca hata fırlatıyor mu?
        [Fact]
        public void Build_UrunEklenmeden_ExceptionFirlatir()
        {
            // Arrange
            var builder = new SiparisBuilder();
            var odeme = new KrediKartiOdeme("Test", "1234");

            // Act & Assert — ürün eklenmeden Build çağrılınca exception bekliyoruz
            Assert.Throws<InvalidOperationException>(() =>
            {
                builder
                    .SiparisiBaslat(1, 1)
                    .OdemeAta(odeme)
                    .Build();
            });
        }

        // Test 3: Stoktan fazla miktar eklenince hata fırlatıyor mu?
        [Fact]
        public void UrunEkle_StokYetersiz_ExceptionFirlatir()
        {
            // Arrange — sadece 3 adet stok var
            var builder = new SiparisBuilder();
            var urun = TestUrunuOlustur(stok: 3);

            // Act & Assert — 10 adet eklemek istiyoruz, stok yetersiz
            Assert.Throws<InvalidOperationException>(() =>
            {
                builder
                    .SiparisiBaslat(1, 1)
                    .UrunEkle(urun, 10); // 10 > 3, hata vermeli
            });
        }

        // Test 4: Birden fazla ürün eklenince toplam doğru hesaplanıyor mu?
        [Fact]
        public void Build_BirdenFazlaUrun_ToplamDogruHesaplanir()
        {
            // Arrange
            var builder = new SiparisBuilder();
            var urun1 = new BasitUrun(1, "Kalem", 10m, 100, 5);
            var urun2 = new BasitUrun(2, "Defter", 25m, 100, 5);
            var odeme = new HavaleOdeme("TR123", "Test");

            // Act
            var siparis = builder
                .SiparisiBaslat(1, 1)
                .UrunEkle(urun1, 3)  // 10 x 3 = 30
                .UrunEkle(urun2, 2)  // 25 x 2 = 50
                .OdemeAta(odeme)
                .Build();

            // Assert — toplam 80 olmalı
            Assert.Equal(80m, siparis.ToplamTutar);
            Assert.Equal(2, siparis.Kalemler.Count);
        }

        // Test 5: Ödeme atanmadan Build çağrılınca hata fırlatıyor mu?
        [Fact]
        public void Build_OdemeAtanmadan_ExceptionFirlatir()
        {
            // Arrange
            var builder = new SiparisBuilder();
            var urun = TestUrunuOlustur();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
            {
                builder
                    .SiparisiBaslat(1, 1)
                    .UrunEkle(urun, 1)
                    .Build(); // ödeme yok, hata vermeli
            });
        }

        // Test 6: SiparisiBaslat çağrılmadan UrunEkle hata fırlatıyor mu?
        [Fact]
        public void UrunEkle_BaslatilmadanCagrilinca_ExceptionFirlatir()
        {
            // Arrange
            var builder = new SiparisBuilder();
            var urun = TestUrunuOlustur();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
            {
                builder.UrunEkle(urun, 1); // SiparisiBaslat çağrılmadı
            });
        }
    }
}