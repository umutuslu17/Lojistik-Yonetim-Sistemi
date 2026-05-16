using System;
using System.Collections.Generic;
using LojistikYonetimSistemi.Models.Entities;
using LojistikYonetimSistemi.Models.Enums;
using LojistikYonetimSistemi.Models.Patterns.Builder;
using LojistikYonetimSistemi.Models.Patterns.Singleton;
using LojistikYonetimSistemi.Models.Patterns.Strategy;
using LojistikYonetimSistemi.Models.Repositories;

namespace LojistikYonetimSistemi.Controllers
{
    // Sipariş yönetiminin kalbi burası.
    // Builder Pattern ile sipariş oluşturma,
    // Strategy Pattern ile ödeme işlemi,
    // State Pattern ile durum geçişleri
    // hepsi bu controller üzerinden yönetiliyor.
    // Form katmanı tüm bu karmaşıklığı görmüyor,
    // sadece basit metot çağrıları yapıyor.
    public class SiparisController
    {
        private readonly SiparisRepository _siparisRepo;
        private readonly SiparisBuilder _builder;
        private readonly StokController _stokController;
        private readonly KullaniciController _kullaniciController;

        public SiparisController(
            KullaniciController kullaniciController,
            StokController stokController)
        {
            _siparisRepo = new SiparisRepository();
            _builder = new SiparisBuilder();
            _kullaniciController = kullaniciController;
            _stokController = stokController;
        }

        // Yeni sipariş oluştur.
        // Builder Pattern burada devreye giriyor:
        // ürünler tek tek ekleniyor, ödeme atanıyor, Build() ile tamamlanıyor.
        // Ödeme başarısızsa veya stok yetersizse sipariş oluşturulmaz.
        public bool SiparisOlustur(
            int musteriId,
            List<(IUrun urun, int miktar)> kalemler,
            IOdemeStratejisi odemeStratejisi)
        {
            // Müşteri veya admin sipariş verebilir
            if (!_kullaniciController.YetkiKontrolCoklu(
                KullaniciRolu.Musteri, KullaniciRolu.Admin))
            {
                Logger.GetInstance().Log(
                    $"Yetkisiz işlem: {_kullaniciController.AktifKullanici?.Ad} sipariş oluşturmaya çalıştı.");
                return false;
            }

            try
            {
                int yeniId = _siparisRepo.YeniIdUret();

                // Builder zincirini kuruyoruz
                _builder.SiparisiBaslat(yeniId, musteriId);

                // Ürünleri tek tek ekliyoruz.
                // Builder içinde stok kontrolü var, yetersizse exception fırlatır.
                foreach (var (urun, miktar) in kalemler)
                    _builder.UrunEkle(urun, miktar);

                // Ödemeyi bağla ve gerçekleştir
                _builder.OdemeAta(odemeStratejisi);

                // Siparişi tamamla
                Siparis yeniSiparis = _builder.Build();

                // JSON'a kaydet
                _siparisRepo.Ekle(yeniSiparis);

                // Siparişteki her ürünün stokunu düş.
                // StokController bu düşümde Observer kontrolü de yapıyor.
                foreach (var (urun, miktar) in kalemler)
                    _stokController.StokDus(urun.Id, miktar);

                Logger.GetInstance().Log(
                    $"Sipariş #{yeniSiparis.Id} başarıyla oluşturuldu.");
                return true;
            }
            catch (Exception ex)
            {
                // Builder veya ödeme adımında bir şeyler ters gittiyse
                // kullanıcıya anlamlı hata mesajı iletmek için logluyoruz
                Logger.GetInstance().Log($"Sipariş oluşturma hatası: {ex.Message}");
                return false;
            }
        }

        // Siparişi bir sonraki duruma ilerlet.
        // State Pattern burada devreye giriyor — geçersiz geçişlerde
        // exception fırlatılır, biz onu yakalayıp false dönüyoruz.
        public bool SiparisDurumIlerlet(int siparisId)
        {
            if (!_kullaniciController.YetkiKontrolCoklu(
                KullaniciRolu.Admin, KullaniciRolu.DepoGorevlisi, KullaniciRolu.Kurye))
            {
                Logger.GetInstance().Log(
                    $"Yetkisiz işlem: {_kullaniciController.AktifKullanici?.Ad} sipariş durumu ilerletmeye çalıştı.");
                return false;
            }

            var siparis = _siparisRepo.IdIleGetir(siparisId);
            if (siparis == null)
            {
                Logger.GetInstance().Log($"Sipariş bulunamadı: ID {siparisId}");
                return false;
            }

            try
            {
                siparis.Ilerle();
                _siparisRepo.Guncelle(siparis);

                Logger.GetInstance().Log(
                    $"Sipariş #{siparisId} durumu güncellendi: {siparis.AktifDurum.DurumAdi()}");
                return true;
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Log(
                    $"Sipariş #{siparisId} durum geçişi başarısız: {ex.Message}");
                return false;
            }
        }

        // Siparişi iptal et.
        // Kargodaki siparişler iptal edilemez — State bunu zaten reddeder.
        public bool SiparisIptalEt(int siparisId)
        {
            if (!_kullaniciController.YetkiKontrolCoklu(
                KullaniciRolu.Admin, KullaniciRolu.Musteri))
            {
                Logger.GetInstance().Log(
                    $"Yetkisiz işlem: {_kullaniciController.AktifKullanici?.Ad} sipariş iptal etmeye çalıştı.");
                return false;
            }

            var siparis = _siparisRepo.IdIleGetir(siparisId);
            if (siparis == null) return false;

            try
            {
                siparis.Iptal();
                _siparisRepo.Guncelle(siparis);

                Logger.GetInstance().Log($"Sipariş #{siparisId} iptal edildi.");
                return true;
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Log(
                    $"Sipariş #{siparisId} iptal edilemedi: {ex.Message}");
                return false;
            }
        }

        // İade başlat — Kargoda veya TeslimEdildi durumundaki siparişler için
        public bool IadeBaslat(int siparisId)
        {
            if (!_kullaniciController.YetkiKontrolCoklu(
                KullaniciRolu.Admin, KullaniciRolu.Musteri))
            {
                Logger.GetInstance().Log(
                    $"Yetkisiz işlem: {_kullaniciController.AktifKullanici?.Ad} iade başlatmaya çalıştı.");
                return false;
            }

            var siparis = _siparisRepo.IdIleGetir(siparisId);
            if (siparis == null) return false;

            try
            {
                siparis.IadeBaslat();
                _siparisRepo.Guncelle(siparis);

                Logger.GetInstance().Log($"Sipariş #{siparisId} için iade başlatıldı.");
                return true;
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Log(
                    $"Sipariş #{siparisId} iade başlatılamadı: {ex.Message}");
                return false;
            }
        }

        // Tüm siparişleri getir — Admin görebilir
        public List<Siparis> TumSiparisleriGetir()
        {
            if (!_kullaniciController.YetkiKontrol(KullaniciRolu.Admin))
            {
                Logger.GetInstance().Log(
                    $"Yetkisiz erişim: {_kullaniciController.AktifKullanici?.Ad} tüm siparişlere erişmeye çalıştı.");
                return new List<Siparis>();
            }

            return _siparisRepo.HepsiniGetir();
        }

        // Belirli bir müşterinin siparişlerini getir.
        // Müşteri sadece kendi siparişlerini görebilir,
        // admin herkesinkini görebilir.
        public List<Siparis> MusteriSiparisleriGetir(int musteriId)
        {
            bool kendiSiparisleri = _kullaniciController.AktifKullanici?.Id == musteriId;
            bool adminMi = _kullaniciController.YetkiKontrol(KullaniciRolu.Admin);

            if (!kendiSiparisleri && !adminMi)
            {
                Logger.GetInstance().Log(
                    $"Yetkisiz erişim: {_kullaniciController.AktifKullanici?.Ad} başka müşterinin siparişlerine erişmeye çalıştı.");
                return new List<Siparis>();
            }

            return _siparisRepo.MusteriSiparisleri(musteriId);
        }

        // Hazırlanmayı bekleyen siparişleri getir — Depo Görevlisi için
        public List<Siparis> HazirlanacakSiparisleriGetir()
        {
            if (!_kullaniciController.YetkiKontrolCoklu(
                KullaniciRolu.Admin, KullaniciRolu.DepoGorevlisi))
                return new List<Siparis>();

            var tumSiparisler = _siparisRepo.HepsiniGetir();
            var bekleyenler = new List<Siparis>();

            foreach (var s in tumSiparisler)
                if (s.MevcutDurumTip == SiparisDurumuTip.Onaylandi ||
                    s.MevcutDurumTip == SiparisDurumuTip.Hazirlaniyor)
                    bekleyenler.Add(s);

            return bekleyenler;
        }

        // Kargoya verilecek siparişleri getir — Kurye için
        public List<Siparis> KargodakiSiparisleriGetir()
        {
            if (!_kullaniciController.YetkiKontrolCoklu(
                KullaniciRolu.Admin, KullaniciRolu.Kurye))
                return new List<Siparis>();

            var tumSiparisler = _siparisRepo.HepsiniGetir();
            var kargodakiler = new List<Siparis>();

            foreach (var s in tumSiparisler)
                if (s.MevcutDurumTip == SiparisDurumuTip.Kargoda)
                    kargodakiler.Add(s);

            return kargodakiler;
        }

        // Sipariş durum bilgisini string olarak döndür — formda göstermek için
        public string SiparisDurumuGetir(int siparisId)
        {
            var siparis = _siparisRepo.IdIleGetir(siparisId);
            return siparis?.AktifDurum.DurumAdi() ?? "Sipariş bulunamadı";
        }
    }
}