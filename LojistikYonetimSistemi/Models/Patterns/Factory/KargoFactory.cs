using System;
using LojistikYonetimSistemi.Models.Patterns.Adapter;
using LojistikYonetimSistemi.Models.Patterns.Singleton;

namespace LojistikYonetimSistemi.Models.Patterns.Factory
{
    // Factory Method Pattern'in kullanım yeri burası.
    // KargoController hangi firmayı kullanacağına karar verirken
    // doğrudan "new ArasKargoAdapter()" yazmak zorunda kalmamalı.
    // Çünkü o zaman Controller, Adapter sınıflarına sıkı sıkıya bağlanır —
    // yeni firma eklemek için Controller'a dokunmak gerekir.
    // Bunun yerine Controller sadece "Aras istiyorum" der, fabrika üretir.
    // Yeni bir kargo firması eklemek istersen sadece buraya
    // bir case daha yazman yeterli, başka hiçbir şeye dokunmazsın.
    public class KargoFactory
    {
        // Firma adına göre doğru kargo servisi nesnesini üretip döndürür.
        // Dışarıya sadece IKargoServisi çıkıyor, hangi Adapter olduğu gizli.
        public static IKargoServisi Olustur(string firmaAdi)
        {
            Logger.GetInstance().Log($"Kargo servisi oluşturuluyor: {firmaAdi}");

            switch (firmaAdi.ToLower().Trim())
            {
                case "aras":
                case "aras kargo":
                    return new ArasKargoAdapter();

                case "yurtici":
                case "yurtiçi":
                case "yurtici kargo":
                case "yurtiçi kargo":
                    return new YurticiKargoAdapter();

                case "globalexpres":
                case "global expres":
                case "global":
                    return new GlobalExpresAdapter();

                default:
                    // Bilinmeyen bir firma adı gelirse exception fırlatmak yerine
                    // varsayılan olarak Aras dönüyoruz ve logluyoruz.
                    // Gerçek sistemde burada exception daha mantıklı olabilir
                    // ama formda dropdown kullanacağımız için bu durum oluşmaz.
                    Logger.GetInstance().Log(
                        $"Uyarı: '{firmaAdi}' tanımlı değil, varsayılan olarak Aras Kargo atandı.");
                    return new ArasKargoAdapter();
            }
        }

        // Formda ComboBox'ı doldurmak için mevcut firma listesini döndürür.
        // Yeni firma eklenince buraya da eklemek gerekiyor — bu küçük bir eksiklik
        // ama ödev kapsamında kabul edilebilir.
        public static string[] FirmaListesi()
        {
            return new string[] { "Aras Kargo", "Yurtiçi Kargo", "GlobalExpres" };
        }
    }
}