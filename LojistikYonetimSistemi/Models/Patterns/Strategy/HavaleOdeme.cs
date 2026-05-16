using LojistikYonetimSistemi.Models.Patterns.Singleton;

namespace LojistikYonetimSistemi.Models.Patterns.Strategy
{
    // Havale ile ödeme, özellikle kurumsal müşterilerin tercih ettiği yöntem.
    // Kredi kartından farkı: ödeme anında değil, banka onayıyla gerçekleşir.
    // Gerçek sistemde burada IBAN doğrulaması ve banka bildirimi olurdu.
    public class HavaleOdeme : IOdemeStratejisi
    {
        // Ödemeyi yapan kişinin ya da kurumun IBAN numarası
        private readonly string _ibanNumarasi;

        // Havale yapan kişinin adı, banka dekontunda görünür
        private readonly string _gondericAdi;

        public HavaleOdeme(string ibanNumarasi, string gondericAdi)
        {
            _ibanNumarasi = ibanNumarasi;
            _gondericAdi = gondericAdi;
        }

        // Havale ödemesini kayıt altına al.
        // Gerçekte burada bankanın havale API'si çağrılır,
        // dekont numarası alınır ve sipariş buna bağlanır.
        public bool OdemeYap(decimal tutar)
        {
            Logger.GetInstance().Log(
                $"Havale ödemesi: {_gondericAdi} - IBAN: {_ibanNumarasi} - {tutar:C2}");

            return true;
        }

        public string YontemAdi() => $"Havale ({_gondericAdi})";
    }
}