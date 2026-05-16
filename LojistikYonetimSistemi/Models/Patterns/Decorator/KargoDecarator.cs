namespace LojistikYonetimSistemi.Models.Patterns.Decorator
{
    // Decorator Pattern'in temel sınıfı bu.
    // Kargo fiyatına ek hizmet eklemek istediğimizde normalde
    // her kombinasyon için ayrı sınıf yazmak zorunda kalırdık:
    // SigortaliArasKargo, KirilganArasKargo, SigortaliKirilganArasKargo...
    // Bu yol kombinasyon sayısı arttıkça çığ gibi büyür.
    // Decorator Pattern ile her ek hizmet bir "sarmalayıcı" oluyor,
    // istediğin kadar üst üste koyabiliyorsun:
    // new Sigorta(new KirilganKoruma(new ArasKargoAdapter()))
    // Bu sınıf hem IKargoServisi'ni hem de içinde bir IKargoServisi tutuyor —
    // bu sayede zincir kurulabiliyor.
    public abstract class KargoDecorator : Adapter.IKargoServisi
    {
        // Sarmalanan kargo servisi — bir Adapter da olabilir,
        // başka bir Decorator da. Zincirin devamı burada.
        protected readonly Adapter.IKargoServisi _kargoServisi;

        protected KargoDecorator(Adapter.IKargoServisi kargoServisi)
        {
            _kargoServisi = kargoServisi;
        }

        // Varsayılan olarak içteki servise yönlendiriyoruz.
        // Ek hizmet sınıfları sadece FiyatHesapla'yı override edecek,
        // diğer metodlar olduğu gibi iletilecek.
        public virtual string TakipNoUret(int siparisId)
            => _kargoServisi.TakipNoUret(siparisId);

        public virtual decimal FiyatHesapla(double agirlikKg, double mesafeKm)
            => _kargoServisi.FiyatHesapla(agirlikKg, mesafeKm);

        public virtual string KargoDurumuSorgula(string takipNo)
            => _kargoServisi.KargoDurumuSorgula(takipNo);

        public virtual string FirmaAdi()
            => _kargoServisi.FirmaAdi();
    }
}