using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using LojistikYonetimSistemi.Models.Enums;
using LojistikYonetimSistemi.Models.Patterns.State;

namespace LojistikYonetimSistemi.Models.Entities
{
    // Sistemin kalbindeki sınıf bu.
    // Bir siparişin tüm bilgilerini ve o anki durumunu burada tutuyoruz.
    // State Pattern açısından bu sınıf "Context" rolünde:
    // hangi işlemin yapılabileceğine kendisi karar vermiyor,
    // aktif durum nesnesine soruyor. Böylece if-else karmaşasından kurtuluyoruz.
    public class Siparis
    {
        public int Id { get; set; }

        // Siparişi veren müşterinin Id'si
        public int MusteriId { get; set; }

        public DateTime Tarih { get; set; }

        // Siparişin şu anki durumu enum olarak tutuluyor.
        // JSON'a bu değeri kaydediyoruz, uygulama açılınca
        // bu enum'a bakarak doğru State nesnesini oluşturuyoruz.
        public SiparisDurumuTip MevcutDurumTip { get; set; } = SiparisDurumuTip.Beklemede;

        // Hangi ödeme yöntemiyle ödendi: KrediKarti, Havale vs.
        public string OdemeYontemi { get; set; } = string.Empty;

        // Siparişin içindeki ürün kalemleri
        public List<SiparisKalemi> Kalemler { get; set; } = new List<SiparisKalemi>();

        // Kargoya verildiyse hangi firma ve takip numarası
        public string KargoFirmasi { get; set; } = string.Empty;
        public string TakipNumarasi { get; set; } = string.Empty;

        // Tüm kalemlerin toplam tutarı
        public decimal ToplamTutar => Kalemler.Sum(k => k.ToplamFiyat);

        // Aktif durum nesnesi JSON'a kaydedilmiyor.
        // Uygulama başlarken DurumNesnesiYukle() ile oluşturuluyor.
        // Bu alan sayesinde siparis.Ilerle() dediğimizde
        // hangi sınıfın çalışacağına Siparis değil, aktif durum karar veriyor.
        [JsonIgnore]
        public ISiparisDurumu AktifDurum { get; private set; } = new BeklemeDurumu();

        public Siparis() { }

        public Siparis(int id, int musteriId, string odemeYontemi)
        {
            Id = id;
            MusteriId = musteriId;
            OdemeYontemi = odemeYontemi;
            Tarih = DateTime.Now;
            MevcutDurumTip = SiparisDurumuTip.Beklemede;
            AktifDurum = new BeklemeDurumu();
        }

        // JSON'dan yüklenen siparişlerde AktifDurum boş kalır.
        // Repository bu metodu çağırarak durum nesnesini yeniden kurar.
        public void DurumNesnesiYukle()
        {
            AktifDurum = MevcutDurumTip switch
            {
                SiparisDurumuTip.Beklemede => new BeklemeDurumu(),
                SiparisDurumuTip.Onaylandi => new OnaylandiDurumu(),
                SiparisDurumuTip.Hazirlaniyor => new HazirlaniyorDurumu(),
                SiparisDurumuTip.Kargoda => new KargodaDurumu(),
                SiparisDurumuTip.TeslimEdildi => new TeslimEdildiDurumu(),
                SiparisDurumuTip.IadeSurecinde => new IadeSurecindeDurumu(),
                SiparisDurumuTip.IptalEdildi => new IptalEdildiDurumu(),
                _ => new BeklemeDurumu()
            };
        }

        // Siparişi bir sonraki duruma taşı.
        // Geçersiz bir geçiş yapılmaya çalışılırsa AktifDurum exception fırlatır.
        public void Ilerle() => AktifDurum.Ilerle(this);

        // İptal isteği — kargodaki sipariş için bu exception fırlatır.
        public void Iptal() => AktifDurum.Iptal(this);

        // İade başlatma — sadece Kargoda ve TeslimEdildi durumlarında çalışır.
        public void IadeBaslat() => AktifDurum.IadeBaşlat(this);

        // State sınıfları durumu değiştirmek için bu metodu çağırır.
        // Hem enum değerini hem de aktif durum nesnesini günceller.
        public void DurumIlerlet(SiparisDurumuTip yeniDurum)
        {
            MevcutDurumTip = yeniDurum;
            DurumNesnesiYukle();
        }

        public override string ToString() =>
            $"Sipariş #{Id} | {AktifDurum.DurumAdi()} | {ToplamTutar:C2}";
    }
}