using System.Collections.Generic;
using LojistikYonetimSistemi.Models.Entities;
using LojistikYonetimSistemi.Models.Enums;
using LojistikYonetimSistemi.Models.Patterns.Singleton;
using LojistikYonetimSistemi.Models.Repositories;

namespace LojistikYonetimSistemi.Controllers
{
    // MVC'nin C katmanı burada başlıyor.
    // KullaniciController; login, rol kontrolü ve kullanıcı yönetimi
    // işlemlerini üstleniyor. Form katmanı doğrudan Repository'ye
    // veya Logger'a ulaşmıyor — her şey bu controller üzerinden geçiyor.
    // Bu sayede iş mantığı View'dan tamamen ayrılmış oluyor,
    // SOLID'in Single Responsibility prensibi burada hayat buluyor.
    public class KullaniciController
    {
        private readonly KullaniciRepository _repo;

        // Giriş yapan kullanıcıyı burada tutuyoruz.
        // Formlar bu property'e bakarak hangi menülerin
        // aktif olacağına karar verecek.
        public Kullanici AktifKullanici { get; private set; }

        // Sisteme giriş yapılıp yapılmadığını kolayca kontrol etmek için
        public bool GiriYapildiMi => AktifKullanici != null;

        public KullaniciController()
        {
            _repo = new KullaniciRepository();
        }

        // Kullanıcı adı ve şifreyle giriş yap.
        // Başarılıysa true döner ve AktifKullanici set edilir,
        // başarısızsa false döner ve AktifKullanici null kalır.
        public bool GirisYap(string kullaniciAdi, string sifre)
        {
            var kullanici = _repo.GirisYap(kullaniciAdi, sifre);

            if (kullanici != null)
            {
                AktifKullanici = kullanici;
                Logger.GetInstance().Log(
                    $"Oturum açıldı: {kullanici.Ad} ({kullanici.Rol})");
                return true;
            }

            Logger.GetInstance().Log(
                $"Başarısız giriş: kullanıcı adı '{kullaniciAdi}'");
            return false;
        }

        // Oturumu kapat ve aktif kullanıcıyı temizle.
        // Form bu metodu çağırınca LoginForm'a geri dönmeli.
        public void CikisYap()
        {
            if (AktifKullanici != null)
            {
                Logger.GetInstance().Log(
                    $"Oturum kapatıldı: {AktifKullanici.Ad}");
                AktifKullanici = null;
            }
        }

        // Aktif kullanıcının belirtilen role sahip olup olmadığını kontrol et.
        // Formlar menü butonlarını aktif/pasif yaparken bunu kullanacak.
        // Örneğin: YetkiKontrol(KullaniciRolu.Admin) → sadece adminler görebilir
        public bool YetkiKontrol(KullaniciRolu gerekliRol)
        {
            if (!GiriYapildiMi) return false;
            return AktifKullanici.Rol == gerekliRol;
        }

        // Birden fazla rol için yetki kontrolü.
        // Örneğin depo görevlisi ve admin aynı ekrana girebilsin diye:
        // YetkiKontrolCoklu(KullaniciRolu.Admin, KullaniciRolu.DepoGorevlisi)
        public bool YetkiKontrolCoklu(params KullaniciRolu[] roller)
        {
            if (!GiriYapildiMi) return false;
            foreach (var rol in roller)
                if (AktifKullanici.Rol == rol) return true;
            return false;
        }

        // Tüm kullanıcıları getir — sadece Admin görebilir
        public List<Kullanici> TumKullanicilariGetir()
        {
            if (!YetkiKontrol(KullaniciRolu.Admin))
            {
                Logger.GetInstance().Log(
                    $"Yetkisiz erişim girişimi: {AktifKullanici?.Ad} kullanıcı listesine erişmeye çalıştı.");
                return new List<Kullanici>();
            }

            return _repo.HepsiniGetir();
        }

        // Yeni kullanıcı ekle — sadece Admin yapabilir
        public bool KullaniciEkle(string ad, string kullaniciAdi,
            string sifre, KullaniciRolu rol)
        {
            if (!YetkiKontrol(KullaniciRolu.Admin))
            {
                Logger.GetInstance().Log(
                    $"Yetkisiz işlem: {AktifKullanici?.Ad} kullanıcı eklemeye çalıştı.");
                return false;
            }

            int yeniId = _repo.YeniIdUret();
            var yeniKullanici = new Kullanici(yeniId, ad, kullaniciAdi, sifre, rol);
            return _repo.Ekle(yeniKullanici);
        }

        // Kullanıcı sil — sadece Admin yapabilir
        public bool KullaniciSil(int id)
        {
            if (!YetkiKontrol(KullaniciRolu.Admin))
            {
                Logger.GetInstance().Log(
                    $"Yetkisiz işlem: {AktifKullanici?.Ad} kullanıcı silmeye çalıştı.");
                return false;
            }

            // Kendi hesabını silmeye çalışıyorsa engelle
            if (AktifKullanici.Id == id)
            {
                Logger.GetInstance().Log(
                    "Kendi hesabını silmeye çalışan kullanıcı engellendi.");
                return false;
            }

            return _repo.Sil(id);
        }
    }
}