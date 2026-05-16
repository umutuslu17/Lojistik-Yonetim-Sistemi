namespace LojistikYonetimSistemi.Models.Entities
{
    // Sistemdeki tüm ürünlerin uyması gereken temel sözleşme.
    // Hem "kalem" gibi basit ürünler hem de "bilgisayar kasası" gibi
    // içinde parçalar barındıran bileşik ürünler bu interface'i uygulayacak.
    // Bu sayede sisteme yeni bir ürün türü eklemek istesek mevcut koda dokunmak zorunda kalmayız.
    public interface IUrun
    {
        int Id { get; set; }
        string Ad { get; set; }

        // Stok kaç adet kaldı
        int Stok { get; set; }

        // Bu değerin altına düşünce otomatik bildirim gidecek
        int EsikDeger { get; set; }

        // Basit ürünlerde direkt fiyatı döner,
        // bileşik ürünlerde tüm parçaların fiyatını toplayarak döner
        decimal HesaplaFiyat();

        // Stok eşiği aşıldı mı? Observer tetiklenecek mi?
        bool EsikAsildiMi();
    }
}