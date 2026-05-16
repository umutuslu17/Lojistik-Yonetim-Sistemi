using System;

namespace LojistikYonetimSistemi.Models.Patterns.Adapter
{
    // Bu sınıf Aras Kargo'nun kendi sistemimizdeki halini temsil ediyor.
    // Gerçekte bu bir dışarıdan gelen NuGet paketi veya servis referansı olurdu.
    // Metot isimleri ve parametreler Aras'ın kendi API'sine özgü —
    // sistemimizin geri kalanıyla uyuşmuyor, Adapter bu uyumsuzluğu çözecek.
    public class ArasKargoApi
    {
        // Aras'ın takip kodu üretme metodu — kendi adlandırma kurallarıyla
        public string ArasTakipKoduOlustur(int gonderiNo)
        {
            // Gerçekte burada Aras API'sine HTTP isteği atılırdı
            return $"ARS{DateTime.Now:yyyyMMdd}{gonderiNo:D5}";
        }

        // Aras'ın fiyat hesaplama metodu — parametreler bizimkinden farklı
        // desi: hacimsel ağırlık birimi (100 gram = 1 desi)
        public decimal ArasFiyatHesapla(int desi, int mesafe)
        {
            // Gerçekte Aras'ın tarifesine göre hesaplanırdı
            decimal temelFiyat = 15.0m;
            decimal desiUcreti = desi * 1.5m;
            decimal mesafeUcreti = mesafe > 500 ? 10.0m : 0.0m;
            return temelFiyat + desiUcreti + mesafeUcreti;
        }

        // Aras'ın durum sorgulama metodu
        public string ArasSorgula(string barkodNo)
        {
            // Gerçekte Aras'ın takip sistemine sorgu atılırdı
            return $"Aras Kargo: {barkodNo} nolu gönderi dağıtımda.";
        }
    }
}