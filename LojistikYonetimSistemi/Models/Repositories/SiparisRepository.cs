using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using LojistikYonetimSistemi.Models.Entities;
using LojistikYonetimSistemi.Models.Enums;
using LojistikYonetimSistemi.Models.Patterns.Singleton;

namespace LojistikYonetimSistemi.Models.Repositories
{
    // Siparişlerin JSON'a kaydedilmesinden ve okunmasından sorumlu sınıf.
    // Siparis sınıfının State nesnesi JSON'a yazılmıyor (JsonIgnore ile),
    // bu yüzden dosyadan okurken DurumNesnesiYukle() çağırmak zorunlu.
    // Bunu burada hallettik, Controller ve formlar bu detayı bilmek zorunda değil.
    public class SiparisRepository
    {
        private readonly string _dosyaYolu;
        private readonly JsonSerializerOptions _jsonAyarlari;

        public SiparisRepository()
        {
            string dataKlasoru = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, "Data");

            if (!Directory.Exists(dataKlasoru))
                Directory.CreateDirectory(dataKlasoru);

            _dosyaYolu = Path.Combine(dataKlasoru, "siparisler.json");

            _jsonAyarlari = new JsonSerializerOptions
            {
                WriteIndented = true,
                // SiparisKalemi içindeki IUrun için converter gerekiyor
                Converters = { new SiparisKalemiConverter() }
            };
        }

        // Tüm siparişleri getir ve State nesnelerini yükle
        public List<Siparis> HepsiniGetir()
        {
            if (!File.Exists(_dosyaYolu))
                return new List<Siparis>();

            try
            {
                string json = File.ReadAllText(_dosyaYolu);
                var liste = JsonSerializer.Deserialize<List<Siparis>>(json, _jsonAyarlari)
                            ?? new List<Siparis>();

                // JSON'dan okunan her siparişin State nesnesini yeniden oluştur.
                // Bu adım atlanırsa AktifDurum null kalır ve exception alırsın.
                foreach (var siparis in liste)
                    siparis.DurumNesnesiYukle();

                return liste;
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Log($"Sipariş dosyası okunurken hata: {ex.Message}");
                return new List<Siparis>();
            }
        }

        // Belirli bir müşterinin siparişlerini getir
        public List<Siparis> MusteriSiparisleri(int musteriId)
        {
            return HepsiniGetir().FindAll(s => s.MusteriId == musteriId);
        }

        // Id'ye göre tek sipariş getir
        public Siparis IdIleGetir(int id)
        {
            return HepsiniGetir().Find(s => s.Id == id);
        }

        // Yeni sipariş kaydet
        public bool Ekle(Siparis siparis)
        {
            var liste = HepsiniGetir();

            if (liste.Exists(s => s.Id == siparis.Id))
            {
                Logger.GetInstance().Log(
                    $"Sipariş eklenemedi: ID {siparis.Id} zaten mevcut.");
                return false;
            }

            liste.Add(siparis);
            Kaydet(liste);
            Logger.GetInstance().Log(
                $"Yeni sipariş kaydedildi: ID {siparis.Id}, " +
                $"Müşteri: {siparis.MusteriId}, Tutar: {siparis.ToplamTutar:C2}");
            return true;
        }

        // Sipariş güncelle — durum değişince burası çağrılır
        public bool Guncelle(Siparis guncelSiparis)
        {
            var liste = HepsiniGetir();
            int index = liste.FindIndex(s => s.Id == guncelSiparis.Id);

            if (index == -1)
            {
                Logger.GetInstance().Log(
                    $"Güncellenecek sipariş bulunamadı: ID {guncelSiparis.Id}");
                return false;
            }

            liste[index] = guncelSiparis;
            Kaydet(liste);
            Logger.GetInstance().Log(
                $"Sipariş güncellendi: ID {guncelSiparis.Id}, " +
                $"Durum: {guncelSiparis.MevcutDurumTip}");
            return true;
        }

        // Yeni sipariş için Id üret
        public int YeniIdUret()
        {
            var liste = HepsiniGetir();
            if (liste.Count == 0) return 1;

            int maxId = 0;
            foreach (var s in liste)
                if (s.Id > maxId) maxId = s.Id;

            return maxId + 1;
        }

        // Listeyi JSON dosyasına yaz
        private void Kaydet(List<Siparis> liste)
        {
            try
            {
                string json = JsonSerializer.Serialize(liste, _jsonAyarlari);
                File.WriteAllText(_dosyaYolu, json);
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Log(
                    $"Sipariş dosyası yazılırken hata: {ex.Message}");
            }
        }
    }

    // SiparisKalemi içindeki IUrun alanını JSON'a yazıp okumak için converter.
    // Ürün tipini (Basit/Bilesik) saklayarak doğru sınıfı oluşturuyoruz.
    internal class SiparisKalemiConverter : JsonConverter<SiparisKalemi>
    {
        public override SiparisKalemi Read(ref Utf8JsonReader reader,
            Type typeToConvert, JsonSerializerOptions options)
        {
            using var doc = JsonDocument.ParseValue(ref reader);
            var root = doc.RootElement;

            int miktar = root.GetProperty("Miktar").GetInt32();
            var urunElem = root.GetProperty("Urun");
            string tip = urunElem.GetProperty("Tip").GetString();

            IUrun urun;
            if (tip == "Basit")
            {
                urun = new BasitUrun(
                    urunElem.GetProperty("Id").GetInt32(),
                    urunElem.GetProperty("Ad").GetString(),
                    urunElem.GetProperty("BirimFiyat").GetDecimal(),
                    urunElem.GetProperty("Stok").GetInt32(),
                    urunElem.GetProperty("EsikDeger").GetInt32()
                );
            }
            else
            {
                urun = new BilesikUrun(
                    urunElem.GetProperty("Id").GetInt32(),
                    urunElem.GetProperty("Ad").GetString(),
                    urunElem.GetProperty("Stok").GetInt32(),
                    urunElem.GetProperty("EsikDeger").GetInt32()
                );
            }

            return new SiparisKalemi(urun, miktar);
        }

        public override void Write(Utf8JsonWriter writer,
            SiparisKalemi value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteNumber("Miktar", value.Miktar);
            writer.WritePropertyName("Urun");

            // Ürün tipini de yazıyoruz ki okurken hangisi olduğunu bilelim
            writer.WriteStartObject();
            if (value.Urun is BasitUrun basit)
            {
                writer.WriteString("Tip", "Basit");
                writer.WriteNumber("Id", basit.Id);
                writer.WriteString("Ad", basit.Ad);
                writer.WriteNumber("Stok", basit.Stok);
                writer.WriteNumber("EsikDeger", basit.EsikDeger);
                writer.WriteNumber("BirimFiyat", basit.BirimFiyat);
            }
            else if (value.Urun is BilesikUrun bilesik)
            {
                writer.WriteString("Tip", "Bilesik");
                writer.WriteNumber("Id", bilesik.Id);
                writer.WriteString("Ad", bilesik.Ad);
                writer.WriteNumber("Stok", bilesik.Stok);
                writer.WriteNumber("EsikDeger", bilesik.EsikDeger);
            }
            writer.WriteEndObject();
            writer.WriteEndObject();
        }
    }
}