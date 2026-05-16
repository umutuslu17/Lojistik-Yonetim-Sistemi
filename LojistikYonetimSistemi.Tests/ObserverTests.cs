using System;
using Xunit;
using LojistikYonetimSistemi.Models.Entities;
using LojistikYonetimSistemi.Models.Patterns.Observer;

namespace LojistikYonetimSistemi.Tests
{
    // Observer Pattern testleri — stok eşiği aşılınca bildirimlerin
    // doğru şekilde tetiklenip tetiklenmediğini kanıtlıyoruz.
    // Gözlemcilerin eklenip çıkarılması ve bildirim akışı burada test ediliyor.
    public class ObserverTests
    {
        // Test için sahte (mock) bir gözlemci — bildirim alındı mı takip ediyor
        private class TestGozlemci : IStokGozlemci
        {
            public bool BildirimAlindi { get; private set; } = false;
            public IUrun SonBildirimUrun { get; private set; } = null;

            public void BildirimAl(IUrun urun)
            {
                BildirimAlindi = true;
                SonBildirimUrun = urun;
            }
        }

        // Test 1: Stok eşiğin altına düşünce gözlemci bildirim alıyor mu?
        [Fact]
        public void StokGuncelle_EsikAsilinca_GozlemciBildirimAliyor()
        {
            // Arrange
            var stokYoneticisi = new StokYoneticisi();
            var gozlemci = new TestGozlemci();
            stokYoneticisi.GozlemciEkle(gozlemci);

            // Stok 100, eşik 20 olan ürün oluştur
            var urun = new BasitUrun(1, "Kalem", 5m, 100, 20);

            // Act — stoku eşiğin altına düşür (10 < 20)
            stokYoneticisi.StokGuncelle(urun, 10);

            // Assert — gözlemci bildirim almış olmalı
            Assert.True(gozlemci.BildirimAlindi);
            Assert.Equal(urun, gozlemci.SonBildirimUrun);
        }

        // Test 2: Stok eşiğin üzerindeyken gözlemci bildirim almıyor mu?
        [Fact]
        public void StokGuncelle_EsikAsilmayinca_GozlemciBildirimAlmiyor()
        {
            // Arrange
            var stokYoneticisi = new StokYoneticisi();
            var gozlemci = new TestGozlemci();
            stokYoneticisi.GozlemciEkle(gozlemci);

            var urun = new BasitUrun(1, "Defter", 10m, 100, 20);

            // Act — stoku eşiğin üzerinde tut (50 > 20)
            stokYoneticisi.StokGuncelle(urun, 50);

            // Assert — gözlemci bildirim almamış olmalı
            Assert.False(gozlemci.BildirimAlindi);
        }

        // Test 3: Birden fazla gözlemci varsa hepsi bildirim alıyor mu?
        [Fact]
        public void StokGuncelle_BirdenFazlaGozlemci_HepsineBildirimGider()
        {
            // Arrange
            var stokYoneticisi = new StokYoneticisi();
            var gozlemci1 = new TestGozlemci();
            var gozlemci2 = new TestGozlemci();
            stokYoneticisi.GozlemciEkle(gozlemci1);
            stokYoneticisi.GozlemciEkle(gozlemci2);

            var urun = new BasitUrun(1, "Kalem", 5m, 100, 20);

            // Act
            stokYoneticisi.StokGuncelle(urun, 5); // 5 < 20, eşik aşılmalı

            // Assert — her iki gözlemci de bildirim almış olmalı
            Assert.True(gozlemci1.BildirimAlindi);
            Assert.True(gozlemci2.BildirimAlindi);
        }

        // Test 4: Gözlemci listeden çıkarılınca bildirim almıyor mu?
        [Fact]
        public void GozlemciCikar_SonrasindaStokDuser_BildirimAlmiyor()
        {
            // Arrange
            var stokYoneticisi = new StokYoneticisi();
            var gozlemci = new TestGozlemci();
            stokYoneticisi.GozlemciEkle(gozlemci);

            // Gözlemciyi çıkar
            stokYoneticisi.GozlemciCikar(gozlemci);

            var urun = new BasitUrun(1, "Kalem", 5m, 100, 20);

            // Act — stok düşse de gözlemci çıkarıldı
            stokYoneticisi.StokGuncelle(urun, 5);

            // Assert — bildirim almamalı
            Assert.False(gozlemci.BildirimAlindi);
        }

        // Test 5: Stok güncellendikten sonra ürünün stok değeri değişiyor mu?
        [Fact]
        public void StokGuncelle_YeniMiktar_UrunStokuGuncellenir()
        {
            // Arrange
            var stokYoneticisi = new StokYoneticisi();
            var urun = new BasitUrun(1, "Kalem", 5m, 100, 20);

            // Act
            stokYoneticisi.StokGuncelle(urun, 45);

            // Assert — ürünün stoku 45 olmalı
            Assert.Equal(45, urun.Stok);
        }

        // Test 6: EsikAsildiMi metodu doğru çalışıyor mu?
        [Fact]
        public void EsikAsildiMi_StokEsiginAltinda_TrueDoner()
        {
            // Arrange — stok 10, eşik 20
            var urun = new BasitUrun(1, "Kalem", 5m, 10, 20);

            // Act & Assert
            Assert.True(urun.EsikAsildiMi());
        }

        // Test 7: EsikAsildiMi stok eşiğe eşit olunca da tetikleniyor mu?
        [Fact]
        public void EsikAsildiMi_StokEsikeEsit_TrueDoner()
        {
            // Arrange — stok ve eşik eşit (20 == 20)
            var urun = new BasitUrun(1, "Kalem", 5m, 20, 20);

            // Act & Assert — eşit olunca da tetiklemeli (<=)
            Assert.True(urun.EsikAsildiMi());
        }
    }
}