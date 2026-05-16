using System.Collections.Generic;
using LojistikYonetimSistemi.Models.Entities;
using LojistikYonetimSistemi.Models.Enums;
using LojistikYonetimSistemi.Models.Patterns.Observer;
using LojistikYonetimSistemi.Models.Patterns.Singleton;
using LojistikYonetimSistemi.Models.Repositories;

namespace LojistikYonetimSistemi.Controllers
{
    // Ürün ve stok yönetiminin tüm iş mantığı burada.
    // Observer Pattern'i burada hayata geçiriyoruz:
    // StokYoneticisi'ne EmailBildirici ve SistemBildirici kaydediyoruz,
    // stok her güncellendiğinde eşik kontrolü otomatik yapılıyor.
    // Form katmanı Observer'ın nasıl çalıştığını bilmek zorunda değil,
    // sadece StokGuncelle() metodunu çağırıyor, gerisini controller hallediyor.
    public class StokController
    {
        private readonly UrunRepository _repo;
        private readonly StokYoneticisi _stokYoneticisi;
        private readonly KullaniciController _kullaniciController;

        // SistemBildirici'yi dışarıdan erişilebilir tutuyoruz.
        // StokForm bu nesnenin YeniBildirimGeldi event'ine abone olacak,
        // eşik aşılınca form anında uyarı gösterecek.
        public SistemBildirici SistemBildirici { get; private set; }

        public StokController(KullaniciController kullaniciController)
        {
            _repo = new UrunRepository();
            _kullaniciController = kullaniciController;
            _stokYoneticisi = new StokYoneticisi();

            // Observer'ları sisteme kaydediyoruz.
            // Email bildirici satın alma departmanına gidecek,
            // sistem bildirici ise forma event fırlatacak.
            var emailBildirici = new EmailBildirici("satinalma@firma.com");
            SistemBildirici = new SistemBildirici();

            _stokYoneticisi.GozlemciEkle(emailBildirici);
            _stokYoneticisi.GozlemciEkle(SistemBildirici);
        }

        // Tüm ürünleri getir.
        // Admin ve Depo Görevlisi görebilir.
        public List<IUrun> TumUrunleriGetir()
        {
            if (!_kullaniciController.YetkiKontrolCoklu(
                KullaniciRolu.Admin, KullaniciRolu.DepoGorevlisi))
            {
                Logger.GetInstance().Log(
                    $"Yetkisiz erişim: {_kullaniciController.AktifKullanici?.Ad} ürün listesine erişmeye çalıştı.");
                return new List<IUrun>();
            }

            return _repo.HepsiniGetir();
        }

        // Id ile tek ürün getir — tüm roller görebilir
        public IUrun UrunGetir(int id)
        {
            return _repo.IdIleGetir(id);
        }

        // Yeni ürün ekle — sadece Admin
        public bool UrunEkle(string ad, decimal fiyat, int stok, int esikDeger)
        {
            if (!_kullaniciController.YetkiKontrol(KullaniciRolu.Admin))
            {
                Logger.GetInstance().Log(
                    $"Yetkisiz işlem: {_kullaniciController.AktifKullanici?.Ad} ürün eklemeye çalıştı.");
                return false;
            }

            int yeniId = _repo.YeniIdUret();
            var yeniUrun = new BasitUrun(yeniId, ad, fiyat, stok, esikDeger);
            return _repo.Ekle(yeniUrun);
        }

        // Bileşik ürün ekle — sadece Admin
        public bool BilesikUrunEkle(string ad, int stok, int esikDeger, List<IUrun> altUrunler)
        {
            if (!_kullaniciController.YetkiKontrol(KullaniciRolu.Admin))
            {
                Logger.GetInstance().Log(
                    $"Yetkisiz işlem: {_kullaniciController.AktifKullanici?.Ad} bileşik ürün eklemeye çalıştı.");
                return false;
            }

            int yeniId = _repo.YeniIdUret();
            var bilesik = new BilesikUrun(yeniId, ad, stok, esikDeger);

            foreach (var alt in altUrunler)
                bilesik.AltUrunEkle(alt);

            return _repo.Ekle(bilesik);
        }

        // Ürün sil — sadece Admin
        public bool UrunSil(int id)
        {
            if (!_kullaniciController.YetkiKontrol(KullaniciRolu.Admin))
            {
                Logger.GetInstance().Log(
                    $"Yetkisiz işlem: {_kullaniciController.AktifKullanici?.Ad} ürün silmeye çalıştı.");
                return false;
            }

            return _repo.Sil(id);
        }

        // Stok miktarını güncelle — Admin veya Depo Görevlisi yapabilir.
        // Bu metot çağrılınca StokYoneticisi devreye girer,
        // eşik aşıldıysa Observer'lar otomatik tetiklenir.
        public bool StokGuncelle(int urunId, int yeniMiktar)
        {
            if (!_kullaniciController.YetkiKontrolCoklu(
                KullaniciRolu.Admin, KullaniciRolu.DepoGorevlisi))
            {
                Logger.GetInstance().Log(
                    $"Yetkisiz işlem: {_kullaniciController.AktifKullanici?.Ad} stok güncellemeye çalıştı.");
                return false;
            }

            var urun = _repo.IdIleGetir(urunId);
            if (urun == null)
            {
                Logger.GetInstance().Log($"Stok güncellenemedi: ID {urunId} bulunamadı.");
                return false;
            }

            // StokYoneticisi hem stoku güncelliyor hem de eşik kontrolü yapıyor.
            // Eşik aşıldıysa kayıtlı tüm Observer'lar otomatik haberdar ediliyor.
            _stokYoneticisi.StokGuncelle(urun, yeniMiktar);

            // Güncel ürünü JSON'a kaydet
            return _repo.Guncelle(urun);
        }

        // Sipariş verilince stoktan düş — SiparisController buraya çağrı yapacak.
        // Negatife düşmesine izin vermiyoruz.
        public bool StokDus(int urunId, int miktar)
        {
            var urun = _repo.IdIleGetir(urunId);
            if (urun == null) return false;

            int yeniMiktar = urun.Stok - miktar;

            if (yeniMiktar < 0)
            {
                Logger.GetInstance().Log(
                    $"Stok düşürülemedi: '{urun.Ad}' için yeterli stok yok. " +
                    $"İstenen: {miktar}, Mevcut: {urun.Stok}");
                return false;
            }

            _stokYoneticisi.StokGuncelle(urun, yeniMiktar);
            return _repo.Guncelle(urun);
        }

        // Eşiğin altındaki ürünleri listele — Admin ve Depo Görevlisi görebilir
        public List<IUrun> KritikStokluUrunleriGetir()
        {
            var tumUrunler = _repo.HepsiniGetir();
            var kritikler = new List<IUrun>();

            foreach (var urun in tumUrunler)
                if (urun.EsikAsildiMi())
                    kritikler.Add(urun);

            return kritikler;
        }
    }
}