using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using LojistikYonetimSistemi.Models.Entities;
using LojistikYonetimSistemi.Models.Enums;
using LojistikYonetimSistemi.Models.Patterns.Singleton;

namespace LojistikYonetimSistemi.Models.Repositories
{
    // Kullanıcıların JSON'a kaydedilmesinden ve okunmasından sorumlu sınıf.
    // Login işlemi, rol kontrolü gibi her şey buradan geçiyor.
    // Uygulama ilk açıldığında varsayılan Admin kullanıcısı
    // otomatik oluşturuluyor — sisteme girilecek bir kullanıcı her zaman olsun diye.
    public class KullaniciRepository
    {
        private readonly string _dosyaYolu;
        private readonly JsonSerializerOptions _jsonAyarlari;

        public KullaniciRepository()
        {
            string dataKlasoru = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, "Data");

            if (!Directory.Exists(dataKlasoru))
                Directory.CreateDirectory(dataKlasoru);

            _dosyaYolu = Path.Combine(dataKlasoru, "kullanicilar.json");

            _jsonAyarlari = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            // Dosya hiç yoksa varsayılan kullanıcıları oluştur
            if (!File.Exists(_dosyaYolu))
                VarsayilanKullanicilariOlustur();
        }

        // Tüm kullanıcıları getir
        public List<Kullanici> HepsiniGetir()
        {
            if (!File.Exists(_dosyaYolu))
                return new List<Kullanici>();

            try
            {
                string json = File.ReadAllText(_dosyaYolu);
                return JsonSerializer.Deserialize<List<Kullanici>>(json, _jsonAyarlari)
                       ?? new List<Kullanici>();
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Log($"Kullanıcı dosyası okunurken hata: {ex.Message}");
                return new List<Kullanici>();
            }
        }

        // Kullanıcı adı ve şifreyle giriş yap.
        // Başarılıysa kullanıcı nesnesini döner, başarısızsa null.
        public Kullanici GirisYap(string kullaniciAdi, string sifre)
        {
            var liste = HepsiniGetir();
            var kullanici = liste.Find(k =>
                k.KullaniciAdi == kullaniciAdi && k.SifreKontrol(sifre));

            if (kullanici != null)
                Logger.GetInstance().Log(
                    $"Giriş başarılı: {kullaniciAdi} ({kullanici.Rol})");
            else
                Logger.GetInstance().Log(
                    $"Başarısız giriş denemesi: {kullaniciAdi}");

            return kullanici;
        }

        // Yeni kullanıcı ekle
        public bool Ekle(Kullanici kullanici)
        {
            var liste = HepsiniGetir();

            if (liste.Exists(k => k.KullaniciAdi == kullanici.KullaniciAdi))
            {
                Logger.GetInstance().Log(
                    $"Kullanıcı eklenemedi: '{kullanici.KullaniciAdi}' zaten var.");
                return false;
            }

            liste.Add(kullanici);
            Kaydet(liste);
            Logger.GetInstance().Log($"Yeni kullanıcı eklendi: {kullanici.KullaniciAdi}");
            return true;
        }

        // Kullanıcı güncelle
        public bool Guncelle(Kullanici guncelKullanici)
        {
            var liste = HepsiniGetir();
            int index = liste.FindIndex(k => k.Id == guncelKullanici.Id);

            if (index == -1)
            {
                Logger.GetInstance().Log(
                    $"Güncellenecek kullanıcı bulunamadı: ID {guncelKullanici.Id}");
                return false;
            }

            liste[index] = guncelKullanici;
            Kaydet(liste);
            return true;
        }

        // Kullanıcı sil — tek Admin kalırsa silme işlemini reddet
        public bool Sil(int id)
        {
            var liste = HepsiniGetir();
            var silinecek = liste.Find(k => k.Id == id);

            if (silinecek == null) return false;

            // Son admin silinmeye çalışılıyorsa engelle
            if (silinecek.Rol == KullaniciRolu.Admin)
            {
                int adminSayisi = liste.FindAll(k => k.Rol == KullaniciRolu.Admin).Count;
                if (adminSayisi <= 1)
                {
                    Logger.GetInstance().Log(
                        "Sistemdeki son admin silinemez.");
                    return false;
                }
            }

            liste.RemoveAll(k => k.Id == id);
            Kaydet(liste);
            Logger.GetInstance().Log($"Kullanıcı silindi: ID {id}");
            return true;
        }

        // Yeni kullanıcı için Id üret
        public int YeniIdUret()
        {
            var liste = HepsiniGetir();
            if (liste.Count == 0) return 1;

            int maxId = 0;
            foreach (var k in liste)
                if (k.Id > maxId) maxId = k.Id;

            return maxId + 1;
        }

        // Listeyi JSON dosyasına yaz
        private void Kaydet(List<Kullanici> liste)
        {
            try
            {
                string json = JsonSerializer.Serialize(liste, _jsonAyarlari);
                File.WriteAllText(_dosyaYolu, json);
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Log(
                    $"Kullanıcı dosyası yazılırken hata: {ex.Message}");
            }
        }

        // Sistem ilk kurulduğunda temel kullanıcıları otomatik oluştur.
        // Bu sayede uygulamayı açtığında hemen giriş yapabilirsin.
        private void VarsayilanKullanicilariOlustur()
        {
            var varsayilanlar = new List<Kullanici>
            {
                new Kullanici(1, "Sistem Yöneticisi", "admin", "admin123", KullaniciRolu.Admin),
                new Kullanici(2, "Depo Görevlisi", "depo", "depo123", KullaniciRolu.DepoGorevlisi),
                new Kullanici(3, "Kurye", "kurye", "kurye123", KullaniciRolu.Kurye),
                new Kullanici(4, "Test Müşteri", "musteri", "musteri123", KullaniciRolu.Musteri)
            };

            Kaydet(varsayilanlar);
            Logger.GetInstance().Log("Varsayılan kullanıcılar oluşturuldu.");
        }
    }
}