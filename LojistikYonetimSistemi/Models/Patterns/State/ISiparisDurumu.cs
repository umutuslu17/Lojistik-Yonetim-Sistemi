using LojistikYonetimSistemi.Models.Entities;
using LojistikYonetimSistemi.Models.Enums;

namespace LojistikYonetimSistemi.Models.Patterns.State
{
    // Bu interface State Pattern'in temel taşı.
 
    // sadece aktif durum nesnesine "ne yapacağını" soruyor.
    public interface ISiparisDurumu
    {
        // Siparişi bir sonraki mantıklı adıma taşı.
        // Her durum sınıfı bunu kendine göre yorumlar:
        // Beklemede için "onayla", Onaylandi için "hazırlamaya başla" gibi.
        void Ilerle(Siparis siparis);

        // Siparişi iptal et. Bazı durumlarda bu mümkün değil,
        // örneğin kargo yoldayken iptal edemezsin.
        // O durum sınıfları bu metodu çağırınca hata fırlatır veya uyarı verir.
        void Iptal(Siparis siparis);

        // İade sürecini başlat. Sadece kargoda veya teslim edilmiş
        // siparişler için geçerli, diğerleri bu çağrıyı reddeder.
        void IadeBaşlat(Siparis siparis);

        // Formda veya logda göstermek için mevcut durumun adını döndür
        string DurumAdi();
    }
}