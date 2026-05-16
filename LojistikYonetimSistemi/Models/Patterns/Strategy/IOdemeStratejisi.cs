namespace LojistikYonetimSistemi.Models.Patterns.Strategy
{
    // Strategy Pattern'in temel sözleşmesi bu.
    // Ödeme yöntemi seçimi normalde şöyle yapılırdı:
    // if (yontem == "kredi") { ... } else if (yontem == "havale") { ... }
    // Bu yaklaşım her yeni ödeme yöntemi eklendiğinde mevcut kodu değiştirmek
    // zorunda bırakır. Bunun yerine her ödeme yöntemini kendi sınıfına taşıyıp
    // hepsini bu interface üzerinden kullanıyoruz.
    // Yarın kripto para eklemek istersen sadece yeni bir sınıf yazıyorsun,
    // başka hiçbir şeye dokunmuyorsun.
    public interface IOdemeStratejisi
    {
        // Ödemeyi gerçekleştir ve başarılı olup olmadığını döndür.
        // Gerçek bir sistemde burada banka API'si veya ödeme geçidi çağrısı olurdu,
        // biz simüle edeceğiz.
        bool OdemeYap(decimal tutar);

        // Formda ve loglarda göstermek için ödeme yönteminin adı
        string YontemAdi();
    }
}