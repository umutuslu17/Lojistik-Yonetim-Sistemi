using LojistikYonetimSistemi.Models.Entities;
using LojistikYonetimSistemi.Models.Patterns.Singleton;

namespace LojistikYonetimSistemi.Models.Patterns.Observer
{
    // Stok kritik seviyeye düştüğünde satın alma departmanına
    // e-posta göndermekten sorumlu gözlemci.
    // Gerçek sistemde burada SMTP ile gerçek bir mail atılırdı.
    // Biz işlemi simüle edip logluyoruz, ama yapı tamamen hazır —
    // ileride sadece Log satırının yerine SmtpClient kodu yazılır.
    public class EmailBildirici : IStokGozlemci
    {
        // Mailin gideceği adres, genellikle satın alma departmanı
        private readonly string _aliciEmail;

        public EmailBildirici(string aliciEmail)
        {
            _aliciEmail = aliciEmail;
        }

        // StokYoneticisi stok eşiği aşıldığında bu metodu çağırıyor.
        // Hangi ürünün stoğu bitti, kaç adet kaldı — bunları alıcıya bildiriyoruz.
        public void BildirimAl(IUrun urun)
        {
            string mesaj = $"[E-POSTA BİLDİRİMİ] → {_aliciEmail} | " +
                           $"Ürün: '{urun.Ad}' kritik stok seviyesine düştü. " +
                           $"Mevcut stok: {urun.Stok} adet, eşik değer: {urun.EsikDeger} adet. " +
                           $"Lütfen satın alma siparişi oluşturun.";

            // Gerçekte burada bir mail gönderimi olurdu,
            // biz log dosyasına düşürüyoruz
            Logger.GetInstance().Log(mesaj);
        }
    }
}