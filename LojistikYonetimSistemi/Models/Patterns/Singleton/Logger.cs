using System;
using System.IO;

namespace LojistikYonetimSistemi.Models.Patterns.Singleton
{
    // Singleton Pattern'in en klasik kullanım yeri: loglama sistemi.
    // Neden Singleton? Çünkü uygulamanın her yerinden loglama yapılıyor:
    // sipariş oluşturulurken, ödeme alınırken, kargo atanırken...
    // Eğer her seferinde yeni bir Logger nesnesi oluşturursak
    // dosyaya eş zamanlı yazma çakışmaları yaşanır, log sırası karışır.
    // Tek bir Logger nesnesi olduğunda bu sorunların hiçbiri olmaz.
    public class Logger
    {
        // Sınıfın tek örneği burada tutuluyor.
        // static olduğu için tüm uygulama boyunca tek bir nesne var.
        private static Logger _instance;

        // Çok iş parçacıklı ortamlarda aynı anda iki Logger oluşmasını
        // engellemek için lock nesnesi kullanıyoruz.
        private static readonly object _lock = new object();

        // Log dosyasının yolu — Data klasörüne yazıyoruz
        private readonly string _logDosyaYolu;

        // Constructor private çünkü dışarıdan new Logger() diyemezsiniz,
        // sadece GetInstance() üzerinden erişilebilir.
        private Logger()
        {
            // Projenin Data klasörüne log dosyası oluşturuyoruz
            string dataKlasoru = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, "Data");

            // Data klasörü yoksa oluştur
            if (!Directory.Exists(dataKlasoru))
                Directory.CreateDirectory(dataKlasoru);

            _logDosyaYolu = Path.Combine(dataKlasoru, "log.txt");
        }

        // Sistemin her yerinden Logger'a buradan ulaşılıyor.
        // İlk çağrıda nesneyi oluşturur, sonrakilerde hep aynısını döndürür.
        // lock bloğu sayesinde aynı anda iki thread birden nesne oluşturamaz.
        public static Logger GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    // İkinci kontrol gerekli çünkü lock'a girmeden önce
                    // başka bir thread nesneyi oluşturmuş olabilir.
                    if (_instance == null)
                        _instance = new Logger();
                }
            }
            return _instance;
        }

        // Mesajı hem dosyaya hem konsola yazar.
        // Zaman damgası sayesinde hangi işlemin ne zaman yapıldığı görülür.
        public void Log(string mesaj)
        {
            string satirr = $"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}] {mesaj}";

            try
            {
                // Dosyaya ekleyerek yazıyoruz, üzerine yazmıyoruz
                File.AppendAllText(_logDosyaYolu, satirr + Environment.NewLine);
            }
            catch
            {
                // Dosyaya yazma başarısız olursa en azından konsola düşsün,
                // loglama hatası uygulamayı çökertmemeli
            }
        }

        // Tüm log geçmişini döndürür, Admin ekranında göstermek için kullanılır
        public string[] TumLoglariGetir()
        {
            if (!File.Exists(_logDosyaYolu))
                return new string[0];

            return File.ReadAllLines(_logDosyaYolu);
        }

        // Log dosyasını temizle — Admin yetkisi gerektirmeli
        public void LogTemizle()
        {
            if (File.Exists(_logDosyaYolu))
                File.WriteAllText(_logDosyaYolu, string.Empty);

            Log("Log dosyası temizlendi.");
        }
    }
}