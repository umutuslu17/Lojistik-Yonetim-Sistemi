using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using LojistikYonetimSistemi.Models.Entities;
using LojistikYonetimSistemi.Models.Patterns.Singleton;

namespace LojistikYonetimSistemi.Models.Repositories
{
    // Ürünlerin JSON dosyasına kaydedilmesinden ve oradan okunmasından
    // sorumlu sınıf bu. Controller katmanı veritabanı mı JSON mu
    // bilmek zorunda değil — sadece "ürünleri getir", "kaydet" diyor,
    // nasıl saklandığı burada halloluyor. İleride gerçek bir veritabanına
    // geçmek istersen sadece bu sınıfı değiştirmen yeterli.
    public class UrunRepository
    {
        private readonly string _dosyaYolu;

        // JsonSerializerOptions'ı bir kere oluşturup tekrar kullanıyoruz,
        // her seferinde new Options() yazmak hem yavaş hem gereksiz.
        private readonly JsonSerializerOptions _jsonAyarlari;

        public UrunRepository()
        {
            string dataKlasoru = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, "Data");

            if (!Directory.Exists(dataKlasoru))
                Directory.CreateDirectory(dataKlasoru);

            _dosyaYolu = Path.Combine(dataKlasoru, "urunler.json");

            _jsonAyarlari = new JsonSerializerOptions
            {
                WriteIndented = true,   // JSON dosyası okunabilir formatta olsun
                Converters = { new UrunJsonConverter() }  // IUrun için özel converter
            };
        }

        // Tüm ürünleri dosyadan oku ve listeye döndür.
        // Dosya yoksa boş liste döner, hata fırlatmaz.
        public List<IUrun> HepsiniGetir()
        {
            if (!File.Exists(_dosyaYolu))
                return new List<IUrun>();

            try
            {
                string json = File.ReadAllText(_dosyaYolu);
                var liste = JsonSerializer.Deserialize<List<UrunJson>>(json, _jsonAyarlari);
                return liste != null ? DonusturListeye(liste) : new List<IUrun>();
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Log($"Ürün dosyası okunurken hata: {ex.Message}");
                return new List<IUrun>();
            }
        }

        // Id'ye göre tek ürün getir, bulunamazsa null döner
        public IUrun IdIleGetir(int id)
        {
            return HepsiniGetir().Find(u => u.Id == id);
        }

        // Yeni ürün ekle — Id çakışmasına izin verme
        public bool Ekle(IUrun urun)
        {
            var liste = HepsiniGetir();

            if (liste.Exists(u => u.Id == urun.Id))
            {
                Logger.GetInstance().Log(
                    $"Ürün eklenemedi: ID {urun.Id} zaten mevcut.");
                return false;
            }

            liste.Add(urun);
            Kaydet(liste);
            Logger.GetInstance().Log($"Yeni ürün eklendi: '{urun.Ad}' (ID: {urun.Id})");
            return true;
        }

        // Var olan ürünü güncelle — stok değişince burası çağrılır
        public bool Guncelle(IUrun guncelUrun)
        {
            var liste = HepsiniGetir();
            int index = liste.FindIndex(u => u.Id == guncelUrun.Id);

            if (index == -1)
            {
                Logger.GetInstance().Log(
                    $"Güncellenecek ürün bulunamadı: ID {guncelUrun.Id}");
                return false;
            }

            liste[index] = guncelUrun;
            Kaydet(liste);
            Logger.GetInstance().Log($"Ürün güncellendi: '{guncelUrun.Ad}'");
            return true;
        }

        // Ürünü sistemden sil
        public bool Sil(int id)
        {
            var liste = HepsiniGetir();
            int oncekiSayı = liste.Count;
            liste.RemoveAll(u => u.Id == id);

            if (liste.Count == oncekiSayı)
            {
                Logger.GetInstance().Log($"Silinecek ürün bulunamadı: ID {id}");
                return false;
            }

            Kaydet(liste);
            Logger.GetInstance().Log($"Ürün silindi: ID {id}");
            return true;
        }

        // Yeni ürün için kullanılabilecek en büyük Id'yi üret
        public int YeniIdUret()
        {
            var liste = HepsiniGetir();
            if (liste.Count == 0) return 1;

            int maxId = 0;
            foreach (var u in liste)
                if (u.Id > maxId) maxId = u.Id;

            return maxId + 1;
        }

        // Listeyi JSON'a yaz — tüm kaydetme işlemleri buradan geçer
        private void Kaydet(List<IUrun> liste)
        {
            try
            {
                // IUrun listesini JSON'a yazabilmek için sarmalıyoruz
                var jsonListe = new List<UrunJson>();
                foreach (var u in liste)
                    jsonListe.Add(UrunJson.UrundenOlustur(u));

                string json = JsonSerializer.Serialize(jsonListe, _jsonAyarlari);
                File.WriteAllText(_dosyaYolu, json);
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Log($"Ürün dosyası yazılırken hata: {ex.Message}");
            }
        }

        // JSON listesini IUrun listesine dönüştür
        private List<IUrun> DonusturListeye(List<UrunJson> jsonListe)
        {
            var liste = new List<IUrun>();
            foreach (var item in jsonListe)
                liste.Add(item.IUruneDonustur());
            return liste;
        }
    }

    // IUrun interface'ini JSON'a yazmak için yardımcı sınıf.
    // System.Text.Json interface'leri doğrudan serialize edemez,
    // hangi tip olduğunu bilmesi için "Tip" alanını da saklıyoruz.
    internal class UrunJson
    {
        public string Tip { get; set; } = string.Empty;  // "Basit" veya "Bilesik"
        public int Id { get; set; }
        public string Ad { get; set; } = string.Empty;
        public int Stok { get; set; }
        public int EsikDeger { get; set; }
        public decimal BirimFiyat { get; set; }          // Sadece BasitUrun için
        public List<UrunJson> AltUrunler { get; set; }   // Sadece BilesikUrun için

        public static UrunJson UrundenOlustur(IUrun urun)
        {
            if (urun is BasitUrun basit)
                return new UrunJson
                {
                    Tip = "Basit",
                    Id = basit.Id,
                    Ad = basit.Ad,
                    Stok = basit.Stok,
                    EsikDeger = basit.EsikDeger,
                    BirimFiyat = basit.BirimFiyat
                };

            if (urun is BilesikUrun bilesik)
            {
                var jsonAltlar = new List<UrunJson>();
                foreach (var alt in bilesik.AltUrunler)
                    jsonAltlar.Add(UrundenOlustur(alt));

                return new UrunJson
                {
                    Tip = "Bilesik",
                    Id = bilesik.Id,
                    Ad = bilesik.Ad,
                    Stok = bilesik.Stok,
                    EsikDeger = bilesik.EsikDeger,
                    AltUrunler = jsonAltlar
                };
            }

            throw new InvalidOperationException($"Bilinmeyen ürün tipi: {urun.GetType().Name}");
        }

        public IUrun IUruneDonustur()
        {
            if (Tip == "Basit")
                return new BasitUrun(Id, Ad, BirimFiyat, Stok, EsikDeger);

            if (Tip == "Bilesik")
            {
                var bilesik = new BilesikUrun(Id, Ad, Stok, EsikDeger);
                if (AltUrunler != null)
                    foreach (var alt in AltUrunler)
                        bilesik.AltUrunEkle(alt.IUruneDonustur());
                return bilesik;
            }

            throw new InvalidOperationException($"Bilinmeyen ürün tipi: {Tip}");
        }
    }

    // System.Text.Json'ın IUrun interface'ini okuyabilmesi için
    // özel bir JsonConverter yazıyoruz
    internal class UrunJsonConverter : JsonConverter<IUrun>
    {
        public override IUrun Read(ref Utf8JsonReader reader,
            Type typeToConvert, JsonSerializerOptions options)
        {
            // UrunJson üzerinden okuyup dönüştürüyoruz
            var jsonUrun = JsonSerializer.Deserialize<UrunJson>(ref reader, options);
            return jsonUrun?.IUruneDonustur();
        }

        public override void Write(Utf8JsonWriter writer,
            IUrun value, JsonSerializerOptions options)
        {
            var jsonUrun = UrunJson.UrundenOlustur(value);
            JsonSerializer.Serialize(writer, jsonUrun, options);
        }
    }
}