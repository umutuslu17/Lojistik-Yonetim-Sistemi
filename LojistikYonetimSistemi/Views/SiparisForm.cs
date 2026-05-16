using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LojistikYonetimSistemi.Controllers;
using LojistikYonetimSistemi.Models.Entities;
using LojistikYonetimSistemi.Models.Enums;
using LojistikYonetimSistemi.Models.Patterns.Strategy;

namespace LojistikYonetimSistemi.Views
{
    public partial class SiparisForm : Form
    {
        private readonly SiparisController _siparisController;
        private readonly StokController _stokController;
        private readonly KullaniciController _kullaniciController;

        // Yeni sipariş oluştururken seçilen ürünleri geçici olarak tutuyoruz
        private List<(IUrun urun, int miktar)> _sepet = new List<(IUrun, int)>();

        public SiparisForm(
            SiparisController siparisController,
            StokController stokController,
            KullaniciController kullaniciController)
        {
            InitializeComponent();
            _siparisController = siparisController;
            _stokController = stokController;
            _kullaniciController = kullaniciController;

            // Ödeme yöntemlerini combobox'a doldur
            cmbOdemeYontemi.Items.AddRange(new string[] {
                "Kredi Kartı", "Havale", "Kripto"
            });
            cmbOdemeYontemi.SelectedIndex = 0;

            SiparisleriYukle();
            UrunleriYukle();

            // Role göre sipariş oluşturma panelini göster/gizle
            pnlYeniSiparis.Visible = _kullaniciController.YetkiKontrolCoklu(
                KullaniciRolu.Musteri, KullaniciRolu.Admin);
        }

        // Siparişleri listeye yükle
        private void SiparisleriYukle()
        {
            dgvSiparisler.Rows.Clear();

            List<Siparis> siparisler;

            // Admin tüm siparişleri görür, müşteri sadece kendinkini
            if (_kullaniciController.YetkiKontrol(KullaniciRolu.Admin))
                siparisler = _siparisController.TumSiparisleriGetir();
            else if (_kullaniciController.YetkiKontrol(KullaniciRolu.DepoGorevlisi))
                siparisler = _siparisController.HazirlanacakSiparisleriGetir();
            else if (_kullaniciController.YetkiKontrol(KullaniciRolu.Kurye))
                siparisler = _siparisController.KargodakiSiparisleriGetir();
            else
                siparisler = _siparisController.MusteriSiparisleriGetir(
                    _kullaniciController.AktifKullanici.Id);

            foreach (var s in siparisler)
            {
                dgvSiparisler.Rows.Add(
                    s.Id,
                    s.Tarih.ToString("dd.MM.yyyy HH:mm"),
                    s.AktifDurum.DurumAdi(),
                    s.ToplamTutar.ToString("C2"),
                    s.OdemeYontemi,
                    s.KargoFirmasi,
                    s.TakipNumarasi
                );
            }
        }

        // Ürünleri sipariş oluşturma listesine yükle
        private void UrunleriYukle()
        {
            lstUrunler.Items.Clear();
            var urunler = _stokController.TumUrunleriGetir();
            foreach (var u in urunler)
                lstUrunler.Items.Add(u);
        }

        // Sepete ürün ekle
        private void btnSepeteEkle_Click(object sender, EventArgs e)
        {
            if (lstUrunler.SelectedItem == null)
            {
                MessageBox.Show("Lütfen bir ürün seçin.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtMiktar.Text, out int miktar) || miktar <= 0)
            {
                MessageBox.Show("Geçerli bir miktar girin.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var urun = (IUrun)lstUrunler.SelectedItem;

            if (urun.Stok < miktar)
            {
                MessageBox.Show($"Yeterli stok yok. Mevcut: {urun.Stok}", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _sepet.Add((urun, miktar));
            lstSepet.Items.Add($"{urun.Ad} x{miktar} = {(urun.HesaplaFiyat() * miktar):C2}");
            ToplamGuncelle();
        }

        // Sepetten ürün çıkar
        private void btnSepettenCikar_Click(object sender, EventArgs e)
        {
            if (lstSepet.SelectedIndex == -1) return;
            int index = lstSepet.SelectedIndex;
            _sepet.RemoveAt(index);
            lstSepet.Items.RemoveAt(index);
            ToplamGuncelle();
        }

        // Toplam tutarı güncelle
        private void ToplamGuncelle()
        {
            decimal toplam = 0;
            foreach (var (urun, miktar) in _sepet)
                toplam += urun.HesaplaFiyat() * miktar;
            lblToplam.Text = $"Toplam: {toplam:C2}";
        }

        // Siparişi tamamla
        private void btnSiparisOlustur_Click(object sender, EventArgs e)
        {
            if (_sepet.Count == 0)
            {
                MessageBox.Show("Sepete en az bir ürün ekleyin.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ödeme stratejisini seç
            IOdemeStratejisi strateji;
            switch (cmbOdemeYontemi.SelectedIndex)
            {
                case 0:
                    strateji = new KrediKartiOdeme("Kart Sahibi", "0000");
                    break;
                case 1:
                    strateji = new HavaleOdeme("TR000000000000000000000000", "Gönderici");
                    break;
                case 2:
                    strateji = new KriptoOdeme("BTC", "wallet_address");
                    break;
                default:
                    strateji = new KrediKartiOdeme("Kart Sahibi", "0000");
                    break;
            }

            int musteriId = _kullaniciController.AktifKullanici.Id;
            bool basarili = _siparisController.SiparisOlustur(musteriId, _sepet, strateji);

            if (basarili)
            {
                MessageBox.Show("Sipariş başarıyla oluşturuldu!", "Başarılı",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                _sepet.Clear();
                lstSepet.Items.Clear();
                ToplamGuncelle();
                SiparisleriYukle();
            }
            else
            {
                MessageBox.Show("Sipariş oluşturulamadı.", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Seçili siparişi bir sonraki duruma ilerlet
        private void btnDurumIlerlet_Click(object sender, EventArgs e)
        {
            if (dgvSiparisler.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen bir sipariş seçin.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int siparisId = (int)dgvSiparisler.SelectedRows[0].Cells[0].Value;
            bool basarili = _siparisController.SiparisDurumIlerlet(siparisId);

            if (basarili)
            {
                MessageBox.Show("Sipariş durumu güncellendi.", "Başarılı",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                SiparisleriYukle();
            }
            else
            {
                MessageBox.Show("Durum güncellenemedi. Bu durumda ilerleme mümkün değil.",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Seçili siparişi iptal et
        private void btnIptalEt_Click(object sender, EventArgs e)
        {
            if (dgvSiparisler.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen bir sipariş seçin.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var onay = MessageBox.Show("Bu siparişi iptal etmek istediğinize emin misiniz?",
                "İptal Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (onay != DialogResult.Yes) return;

            int siparisId = (int)dgvSiparisler.SelectedRows[0].Cells[0].Value;
            bool basarili = _siparisController.SiparisIptalEt(siparisId);

            if (basarili)
            {
                MessageBox.Show("Sipariş iptal edildi.", "Başarılı",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                SiparisleriYukle();
            }
            else
            {
                MessageBox.Show("Sipariş iptal edilemedi. Kargodaki siparişler iptal edilemez.",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Listeyi yenile
        private void btnYenile_Click(object sender, EventArgs e)
        {
            SiparisleriYukle();
        }
    }
}