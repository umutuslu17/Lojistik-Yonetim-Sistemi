using LojistikYonetimSistemi.Models.Entities;
using LojistikYonetimSistemi.Models.Patterns.Singleton;

namespace LojistikYonetimSistemi.Models.Patterns.Observer
{
    // Stok kritik seviyeye düştüğünde depo görevlisinin ekranına
    // sistem içi bildirim düşürmekten sorumlu gözlemci.
    // Email'den farkı: bu bildirim uygulama içinde gösteriliyor,
    // dışarıya çıkmıyor. Formda bir bildirim paneli veya
    // MessageBox ile gösterilebilir.
    public class SistemBildirici : IStokGozlemci
    {
        // Son gelen bildirimi dışarıdan okuyabilmek için tutuyoruz.
        // StokForm bu property'yi polling ile kontrol edebilir
        // veya event mekanizmasıyla tetiklenebilir.
        public string SonBildirim { get; private set; } = string.Empty;

        // Bildirim geldiğinde dışarıya haber vermek için event kullanıyoruz.
        // Form bu event'e abone olursa bildirim anında ekranda görünür.
        public event System.Action<string> YeniBildirimGeldi;

        // StokYoneticisi bu metodu çağırdığında depo görevlisine
        // sistem içi uyarı düşürüyoruz.
        public void BildirimAl(IUrun urun)
        {
            SonBildirim = $"[SİSTEM UYARISI] '{urun.Ad}' ürününün stoğu kritik seviyede! " +
                          $"Kalan: {urun.Stok} adet (eşik: {urun.EsikDeger}).";

            // Log dosyasına da yazıyoruz ki admin görebilsin
            Logger.GetInstance().Log(SonBildirim);

            // Forma event fırlat — form bu event'e abone olduysa
            // bildirim anında ekranda gösterilecek
            YeniBildirimGeldi?.Invoke(SonBildirim);
        }
    }
}