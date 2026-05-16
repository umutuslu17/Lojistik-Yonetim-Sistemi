using LojistikYonetimSistemi.Models.Patterns.Singleton;

namespace LojistikYonetimSistemi.Models.Patterns.Strategy
{
    // İleride eklenecek ödeme yöntemi olarak ödevde bahsediliyor.
    // Strategy Pattern'in gücü tam da burada ortaya çıkıyor:
    // yeni bir ödeme yöntemi eklemek için mevcut hiçbir koda dokunmak zorunda kalmıyoruz,
    // sadece bu sınıfı yazıp sisteme tanıtıyoruz. 
    // Formda ComboBox'a "Kripto" seçeneği eklemek yeterli olacak.
    public class KriptoOdeme : IOdemeStratejisi
    {
        // Ödemenin yapıldığı kripto para birimi: BTC, ETH, USDT vs.
        private readonly string _coinTipi;

        // Müşterinin kripto cüzdan adresi
        private readonly string _cuzdanAdresi;

        public KriptoOdeme(string coinTipi, string cuzdanAdresi)
        {
            _coinTipi = coinTipi;
            _cuzdanAdresi = cuzdanAdresi;
        }

        // Kripto ödemeyi işleme al.
        // Gerçekte burada blockchain API'sine istek atılır,
        // işlem hash'i alınır ve onay beklenir.
        // Kripto ödemelerde onay süresi birkaç dakika sürebilir,
        // bu yüzden gerçek sistemde async yapı kullanılır.
        public bool OdemeYap(decimal tutar)
        {
            Logger.GetInstance().Log(
                $"Kripto ödeme: {_coinTipi} - Cüzdan: {_cuzdanAdresi} - {tutar:C2}");

            return true;
        }

        public string YontemAdi() => $"Kripto ({_coinTipi})";
    }
}