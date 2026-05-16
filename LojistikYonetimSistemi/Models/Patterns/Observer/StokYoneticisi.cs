using System.Collections.Generic;
using LojistikYonetimSistemi.Models.Entities;
using LojistikYonetimSistemi.Models.Patterns.Singleton;

namespace LojistikYonetimSistemi.Models.Patterns.Observer
{
    // Observer Pattern'in "Subject" tarafı bu sınıf.
    // Stok hareketlerini takip eden ve gözlemcileri yöneten merkezi nokta.
    // Bir ürünün stoğu her güncellendiğinde eşik kontrolü yapılır,
    // eşik aşıldıysa kayıtlı tüm gözlemcilere haber verilir.
    // Yeni bir bildirim kanalı eklemek istersen sadece yeni bir
    // IStokGozlemci sınıfı yazar ve buraya kaydedersin,
    // bu sınıfa dokunman gerekmiyor.
    public class StokYoneticisi
    {
        // Bildirim alacak tüm gözlemciler bu listede tutuluyor.
        // Şu an EmailBildirici ve SistemBildirici var,
        // ileride SMS veya WhatsApp bildirici de eklenebilir.
        private readonly List<IStokGozlemci> _gozlemciler = new List<IStokGozlemci>();

        // Yeni bir gözlemci kaydet — uygulama başlarken çağrılır.
        // Örneğin: _stokYoneticisi.GozlemciEkle(new EmailBildirici("satin@firma.com"))
        public void GozlemciEkle(IStokGozlemci gozlemci)
        {
            if (!_gozlemciler.Contains(gozlemci))
                _gozlemciler.Add(gozlemci);
        }

        // Bir gözlemciyi listeden çıkar.
        // Örneğin kullanıcı email bildirimlerini kapattıysa
        public void GozlemciCikar(IStokGozlemci gozlemci)
        {
            _gozlemciler.Remove(gozlemci);
        }

        // Bir ürünün stok miktarını güncelle ve eşik kontrolü yap.
        // StokController her stok düşümünden sonra bu metodu çağırıyor.
        public void StokGuncelle(IUrun urun, int yeniMiktar)
        {
            int eskiMiktar = urun.Stok;
            urun.Stok = yeniMiktar;

            Logger.GetInstance().Log(
                $"Stok güncellendi: '{urun.Ad}' — {eskiMiktar} → {yeniMiktar} adet.");

            // Stok eşik değerin altına düştüyse tüm gözlemcileri uyar
            if (urun.EsikAsildiMi())
                TumGozlemcileriBildir(urun);
        }

        // Kayıtlı tüm gözlemcileri sırayla bilgilendir.
        // Bir gözlemci hata verse diğerleri etkilenmesin diye
        // try-catch içinde çağırıyoruz.
        private void TumGozlemcileriBildir(IUrun urun)
        {
            foreach (var gozlemci in _gozlemciler)
            {
                try
                {
                    gozlemci.BildirimAl(urun);
                }
                catch
                {
                    // Bir bildirim kanalı çalışmasa bile diğerleri çalışmaya devam etmeli
                    Logger.GetInstance().Log(
                        $"Uyarı: Bir gözlemci bildirim alırken hata oluştu. Ürün: '{urun.Ad}'");
                }
            }
        }
    }
}