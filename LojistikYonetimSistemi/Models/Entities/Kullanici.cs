using System.Collections.Generic;
using LojistikYonetimSistemi.Models.Enums;

namespace LojistikYonetimSistemi.Models.Entities
{
    // Sisteme giriş yapabilen herkesi temsil eden sınıf.
    // Admin de bu sınıftan, müşteri de — sadece rolleri farklı.
    public class Kullanici
    {
        public int Id { get; set; }
        public string Ad { get; set; } = string.Empty;
        public string KullaniciAdi { get; set; } = string.Empty;

        // Gerçek bir sistemde hash'lenmiş olurdu ama biz basit tutuyoruz
        public string Sifre { get; set; } = string.Empty;

        // Bu rol değerine bakarak hangi menülerin aktif olacağına karar vereceğiz
        public KullaniciRolu Rol { get; set; }

        // Müşterinin geçmiş siparişlerini tutuyoruz
        public List<int> SiparisIdleri { get; set; } = new List<int>();

        public Kullanici() { }

        public Kullanici(int id, string ad, string kullaniciAdi, string sifre, KullaniciRolu rol)
        {
            Id = id;
            Ad = ad;
            KullaniciAdi = kullaniciAdi;
            Sifre = sifre;
            Rol = rol;
        }

        // Login ekranında şifre kontrolü burada yapılıyor
        public bool SifreKontrol(string girilenSifre) => Sifre == girilenSifre;

        public override string ToString() => $"{Ad} ({Rol})";
    }
}