using LojistikYonetimSistemi.Models.Entities;
using LojistikYonetimSistemi.Models.Enums;
using System;

namespace LojistikYonetimSistemi.Models.Patterns.State
{
    // Paket artık kargo firmasının elinde ve yolda.
    // Bu noktadan sonra iptal artık mümkün değil çünkü
    // paketi geri çağırmak kargo firmasına bağlı, bizim sistemimizin
    // kontrolü dışında. Yapılabilecek tek şey teslimattan sonra iade.
    // Bu kısıtı if-else ile yazmak yerine bu sınıf içinde tutmak
    // hem daha okunabilir hem de değiştirmesi çok daha kolay.
    public class KargodaDurumu : ISiparisDurumu
    {
        // Kurye teslim etti, artık müşterinin elinde.
        public void Ilerle(Siparis siparis)
        {
            siparis.DurumIlerlet(SiparisDurumuTip.TeslimEdildi);
        }

        // Paket yoldayken iptal kabul etmiyoruz.
        // Müşteriye anlamlı bir mesaj veriyoruz ki ne yapması gerektiğini anlasın.
        public void Iptal(Siparis siparis)
        {
            throw new InvalidOperationException(
                "Paket zaten yolda, bu aşamada iptal edilemiyor. " +
                "Teslim aldıktan sonra iade talebinde bulunabilirsiniz.");
        }

        // Kargo yoldayken iade başlatılabilir, ürün hatalıysa
        // müşteri paketi teslim almadan iade sürecini açabilir.
        public void IadeBaşlat(Siparis siparis)
        {
            siparis.DurumIlerlet(SiparisDurumuTip.IadeSurecinde);
        }

        public string DurumAdi() => "Kargoda";
    }
}