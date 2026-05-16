using LojistikYonetimSistemi.Models.Entities;

namespace LojistikYonetimSistemi.Models.Patterns.Observer
{
    // Observer Pattern'in gözlemci tarafı bu interface.
    // Stok belirli bir seviyenin altına düştüğünde sisteme bağlı
    // tüm gözlemcilerin haberdar edilmesi gerekiyor.
    // Bunu if-else ile yapsaydık her yeni bildirim kanalı eklendiğinde
    // StokYoneticisi sınıfını değiştirmek zorunda kalırdık.
    // Bunun yerine "kim dinlemek istiyorsa bu interface'i uygulasın,
    // biz sadece listeyi dolaşıp herkesi haberdar ederiz" diyoruz.
    public interface IStokGozlemci
    {
        // Stok eşiği aşıldığında bu metot çağrılır.
        // Hangi ürünün stoğu kritik seviyeye düştüyse o ürün buraya gelir.
        void BildirimAl(IUrun urun);
    }
}