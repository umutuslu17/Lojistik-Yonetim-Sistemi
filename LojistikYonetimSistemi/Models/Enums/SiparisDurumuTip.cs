namespace LojistikYonetimSistemi.Models.Enums
{
    // Bir siparişin hayat döngüsündeki tüm olası durumlar burada.
    // State Pattern'deki her durum sınıfı bu enum değeriyle eşleşiyor,
    // JSON'a kaydederken de bu değeri kullanacağız.
    public enum SiparisDurumuTip
    {
        // Sipariş verildi ama henüz onaylanmadı
        Beklemede,

        // Ödeme alındı, sipariş onaylandı
        Onaylandi,

        // Depo görevlisi paketi hazırlıyor
        Hazirlaniyor,

        // Paket kargoya verildi, yolda
        Kargoda,

        // Müşteri paketi teslim aldı
        TeslimEdildi,

        // Müşteri iade başlattı
        IadeSurecinde,

        // Sipariş iptal edildi, ödeme iade edilecek
        IptalEdildi
    }
}