namespace LojistikYonetimSistemi.Models.Enums
{
    // Sistemdeki kullanıcı türlerini tanımlıyoruz.
    // Her rolün görebileceği ekranlar ve yapabileceği işlemler farklı olacak.
    public enum KullaniciRolu
    {
        // Her şeye erişebilen, sistemi yöneten kişi
        Admin,

        // Depodaki ürünleri hazırlayan, stok takibi yapan kişi
        DepoGorevlisi,

        // Paketleri teslim eden, kargo durumunu güncelleyen kişi
        Kurye,

        // Sipariş veren, ödeme yapan son kullanıcı
        Musteri
    }
}