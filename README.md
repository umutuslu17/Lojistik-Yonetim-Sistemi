# Akıllı Tedarik ve Lojistik Yönetim Sistemi

Sakarya Üniversitesi — Nesneye Dayalı Analiz ve Tasarım Dersi Dönem Sonu Projesi

---

## Proje Hakkında

Bir e-ticaret lojistik merkezinin yazılım ihtiyaçlarını karşılamak amacıyla geliştirilmiştir. Stok takibi, sipariş yönetimi, kargo atama ve rol tabanlı yetkilendirme süreçlerini kapsamaktadır.

Platform olarak C# Windows Forms (.NET 8) kullanılmış, veriler JSON dosyalarında saklanmakta ve mimari olarak MVC (Model-View-Controller) deseni uygulanmıştır.

---

## Mimari Yapı

Proje üç ana katmandan oluşmaktadır.

```
LojistikYonetimSistemi/
│
├── Models/
│   ├── Entities/
│   ├── Enums/
│   ├── Patterns/
│   │   ├── State/
│   │   ├── Strategy/
│   │   ├── Observer/
│   │   ├── Adapter/
│   │   ├── Decorator/
│   │   ├── Singleton/
│   │   ├── Factory/
│   │   └── Builder/
│   └── Repositories/
│
├── Controllers/
│   ├── SiparisController.cs
│   ├── StokController.cs
│   ├── KargoController.cs
│   └── KullaniciController.cs
│
├── Views/
│   ├── LoginForm.cs
│   ├── MainForm.cs
│   ├── SiparisForm.cs
│   ├── StokForm.cs
│   ├── KargoForm.cs
│   ├── KullaniciForm.cs
│   └── LogForm.cs
│
├── Data/
│   ├── kullanicilar.json
│   ├── urunler.json
│   ├── siparisler.json
│   └── log.txt
│
└── LojistikYonetimSistemi.Tests/
    ├── SiparisBuilderTests.cs
    ├── StatePatternTests.cs
    ├── ObserverTests.cs
    └── DecoratorTests.cs
```

---

## Kullanılan Tasarım Desenleri

Projede toplam 8 tasarım deseni kullanılmıştır.

| # | Desen | Kategori | Kullanım Amacı |
|---|-------|----------|----------------|
| 1 | State | Davranışsal | Sipariş durum geçişleri |
| 2 | Strategy | Davranışsal | Ödeme yöntemi seçimi |
| 3 | Observer | Davranışsal | Stok eşik bildirimi |
| 4 | Adapter | Yapısal | Kargo firma API entegrasyonu |
| 5 | Decorator | Yapısal | Kargo ek hizmet fiyatlandırması |
| 6 | Singleton | Yaratımsal | Sistem genelinde tek Logger nesnesi |
| 7 | Factory Method | Yaratımsal | Kargo servisi üretimi |
| 8 | Builder | Yaratımsal | Adım adım sipariş oluşturma |

---

## Kullanıcı Rolleri ve Yetkiler

| İşlem | Admin | Depo Görevlisi | Kurye | Müşteri |
|-------|-------|----------------|-------|---------|
| Kullanıcı yönetimi | ✓ | ✗ | ✗ | ✗ |
| Ürün ekleme/silme | ✓ | ✗ | ✗ | ✗ |
| Stok güncelleme | ✓ | ✓ | ✗ | ✗ |
| Sipariş oluşturma | ✓ | ✗ | ✗ | ✓ |
| Sipariş durumu ilerlet | ✓ | ✓ | ✓ | ✗ |
| Kargo atama | ✓ | ✓ | ✗ | ✗ |
| Teslim işaretleme | ✓ | ✗ | ✓ | ✗ |
| Sipariş iptal/iade | ✓ | ✗ | ✗ | ✓ |
| Log görüntüleme | ✓ | ✗ | ✗ | ✗ |

---

## Sipariş Durum Akışı

```
[Beklemede] --> [Onaylandı] --> [Hazırlanıyor] --> [Kargoda] --> [Teslim Edildi]
     |               |               |                |                |
  Iptal()         Iptal()         Iptal()           Iade()           Iade()
     |               |               |                |                |
     +---------------+---------------+                +----------------+
                     |                                |
                     v                                v
              [İptal Edildi] <---- [İade Sürecinde] <-+
```

Kargoda durumundaki sipariş iptal edilemez, sadece iade başlatılabilir.

---

## Birim Testler

xUnit framework'ü kullanılarak 28 test yazılmış ve tamamı başarılı olmuştur.

| Test Sınıfı | Test Sayısı | Kapsam |
|-------------|-------------|--------|
| SiparisBuilderTests | 6 | Builder adım kontrolü, stok doğrulama, toplam hesabı |
| StatePatternTests | 9 | Durum geçişleri, kargoda iptal engeli, iade kuralları |
| ObserverTests | 7 | Eşik bildirimi, çoklu gözlemci, gözlemci çıkarma |
| DecoratorTests | 8 | Temel fiyat, sigorta, kırılgan koruma, zincirleme |
| Toplam | 28 | 28/28 Başarılı |

---

## Varsayılan Kullanıcılar

Uygulama ilk açılışta kullanıcı verilerini otomatik oluşturur.

| Kullanıcı Adı | Şifre | Rol |
|---------------|-------|-----|
| admin | admin123 | Admin |
| depo | depo123 | Depo Görevlisi |
| kurye | kurye123 | Kurye |
| musteri | musteri123 | Müşteri |

---

## Teknik Notlar

Sistem herhangi bir veritabanı kullanmaz. Tüm veriler `Data/` klasöründe JSON formatında saklanır. Uygulama her açılışta bu dosyaları okur, her işlemden sonra günceller.

Singleton Logger tüm kritik işlemleri `log.txt` dosyasına yazar. Bu sayede sistemde yapılan her işlem kayıt altındadır ve admin panelinden görüntülenebilir.
