namespace LojistikYonetimSistemi.Models.Patterns.Adapter
{
    // Adapter Pattern'in temel sözleşmesi bu.
    // Aras, Yurtiçi, GlobalExpres gibi her kargo firmasının kendi API'si var
    // ve hepsinin metot isimleri, parametre yapıları birbirinden farklı.
    // Biz bu karmaşıklığı sisteme yansıtmak istemiyoruz.
    // Bunun yerine "biz sadece bu interface ile konuşuruz, siz kendi API'nizi
    // bu interface'e uydurun" diyoruz. Böylece form ve controller katmanı
    // hangi kargo firmasıyla çalıştığını bilmek zorunda kalmıyor.
    public interface IKargoServisi
    {
        // Kargoya verilen paket için takip numarası üret.
        // Aras buna "TakipNoUret" der, Yurtiçi "GenerateCode" der —
        // hepsi buraya gelince aynı isme dönüşüyor.
        string TakipNoUret(int siparisId);

        // Ağırlık ve mesafeye göre kargo ücretini hesapla
        decimal FiyatHesapla(double agirlikKg, double mesafeKm);

        // Kargonun mevcut durumunu sorgula
        string KargoDurumuSorgula(string takipNo);

        // Formda göstermek için firma adı
        string FirmaAdi();
    }
}