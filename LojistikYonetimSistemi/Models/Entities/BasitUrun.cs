namespace LojistikYonetimSistemi.Models.Entities
{
    // Kalem, defter, kablo gibi tek parçadan oluşan sıradan ürünleri temsil ediyor.
    // Composite Pattern'deki "Leaf" düğümü bu sınıf.
    public class BasitUrun : IUrun
    {
        public int Id { get; set; }
        public string Ad { get; set; } = string.Empty;
        public int Stok { get; set; }
        public int EsikDeger { get; set; }

        // Basit ürünlerin tek bir birim fiyatı var, hesaplama doğrudan buradan geliyor
        public decimal BirimFiyat { get; set; }

        // JSON'dan okurken parametre gerektirmeyen constructor lazım
        public BasitUrun() { }

        public BasitUrun(int id, string ad, decimal birimFiyat, int stok, int esikDeger)
        {
            Id = id;
            Ad = ad;
            BirimFiyat = birimFiyat;
            Stok = stok;
            // Eşik değer girilmişse onu kullan, girilmemişse veya 0 geldiyse
            // stoğun yüzde 20'sini varsayılan eşik olarak al.
            // Bu sayede admin eşik değeri boş bıraksa bile Observer yine de çalışır.
            EsikDeger = esikDeger > 0 ? esikDeger : (int)(stok * 0.2);
        }

        // Basit ürünlerde fiyat hesabı basit: direkt birim fiyatı döndür
        public decimal HesaplaFiyat() => BirimFiyat;

        // Stok eşik değerin altına düştü mü diye kontrol ediyoruz
        // Düştüyse StokYoneticisi observer'ları tetikleyecek
        public bool EsikAsildiMi() => Stok <= EsikDeger;

        public override string ToString() => $"{Ad} (Stok: {Stok})";
    }
}