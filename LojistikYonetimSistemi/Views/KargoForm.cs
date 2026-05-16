using System;
using System.Windows.Forms;
using LojistikYonetimSistemi.Controllers;
using LojistikYonetimSistemi.Models.Enums;

namespace LojistikYonetimSistemi.Views
{
    public partial class KargoForm : Form
    {
        private readonly KargoController _kargoController;
        private readonly SiparisController _siparisController;
        private readonly KullaniciController _kullaniciController;

        // Seçili siparişin Id'si — kargo atama ve teslim işlemlerinde lazım
        private int _seciliSiparisId = -1;

        public KargoForm(
            KargoController kargoController,
            SiparisController siparisController,
            KullaniciController kullaniciController)
        {
            InitializeComponent();
            _kargoController = kargoController;
            _siparisController = siparisController;
            _kullaniciController = kullaniciController;

            // Kargo firması combobox'ını doldur
            cmbFirma.Items.AddRange(_kargoController.FirmaListesiGetir());
            cmbFirma.SelectedIndex = 0;

            // Kargo atama paneli sadece Admin ve Depo Görevlisi görür
            pnlKargoAta.Visible = _kullaniciController.YetkiKontrolCoklu(
                KullaniciRolu.Admin, KullaniciRolu.DepoGorevlisi);

            SiparisleriYukle();

            // Firma değişince fiyat önizlemesini güncelle
            cmbFirma.SelectedIndexChanged += (s, e) => FiyatOnizle();
            chkSigorta.CheckedChanged += (s, e) => FiyatOnizle();
            chkKirilgan.CheckedChanged += (s, e) => FiyatOnizle();
            txtAgirlik.TextChanged += (s, e) => FiyatOnizle();
            txtMesafe.TextChanged += (s, e) => FiyatOnizle();
        }

        // Kargoya verilebilecek siparişleri yükle
        // Depo görevlisi hazırlanıyor durumundakileri, kurye kargodakileri görür
        private void SiparisleriYukle()
        {
            dgvSiparisler.Rows.Clear();

            System.Collections.Generic.List<Models.Entities.Siparis> siparisler;

            var rol = _kullaniciController.AktifKullanici.Rol;

            if (rol == KullaniciRolu.Kurye)
                siparisler = _siparisController.KargodakiSiparisleriGetir();
            else if (rol == KullaniciRolu.DepoGorevlisi)
                siparisler = _siparisController.HazirlanacakSiparisleriGetir();
            else
                siparisler = _siparisController.TumSiparisleriGetir();

            foreach (var s in siparisler)
            {
                dgvSiparisler.Rows.Add(
                    s.Id,
                    s.Tarih.ToString("dd.MM.yyyy HH:mm"),
                    s.AktifDurum.DurumAdi(),
                    s.ToplamTutar.ToString("C2"),
                    s.KargoFirmasi ?? "-",
                    s.TakipNumarasi ?? "-"
                );
            }
        }

        // Listeden sipariş seçilince bilgileri güncelle
        private void dgvSiparisler_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSiparisler.SelectedRows.Count == 0) return;

            var row = dgvSiparisler.SelectedRows[0];
            _seciliSiparisId = (int)row.Cells[0].Value;
            string durum = row.Cells[2].Value.ToString();
            string takipNo = row.Cells[5].Value.ToString();

            // Takip numarası varsa durum sorgulama mümkün
            btnDurumSorgula.Enabled = takipNo != "-";

            // Sadece hazırlanıyor durumundaki siparişlere kargo atanabilir
            btnKargoAta.Enabled = durum == "Hazırlanıyor";

            // Sadece kargodaki siparişler teslim edilebilir
            btnTeslimEdildi.Enabled = durum == "Kargoda";

            FiyatOnizle();
        }

        // Ağırlık ve mesafeye göre anlık fiyat önizlemesi göster
        private void FiyatOnizle()
        {
            if (cmbFirma.SelectedItem == null) return;

            double.TryParse(txtAgirlik.Text, out double agirlik);
            double.TryParse(txtMesafe.Text, out double mesafe);

            if (agirlik <= 0 || mesafe <= 0)
            {
                lblFiyatOnizleme.Text = "Fiyat: —";
                return;
            }

            decimal urunDegeri = 0;
            decimal.TryParse(txtUrunDegeri.Text, out urunDegeri);

            decimal fiyat = _kargoController.FiyatOnizleme(
                cmbFirma.SelectedItem.ToString(),
                agirlik,
                mesafe,
                chkSigorta.Checked,
                urunDegeri,
                chkKirilgan.Checked
            );

            lblFiyatOnizleme.Text = $"Tahmini Kargo Ücreti: {fiyat:C2}";
        }

        // Seçili siparişe kargo ata
        private void btnKargoAta_Click(object sender, EventArgs e)
        {
            if (_seciliSiparisId == -1)
            {
                MessageBox.Show("Lütfen bir sipariş seçin.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!double.TryParse(txtAgirlik.Text, out double agirlik) || agirlik <= 0)
            {
                MessageBox.Show("Geçerli bir ağırlık girin (kg).", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!double.TryParse(txtMesafe.Text, out double mesafe) || mesafe <= 0)
            {
                MessageBox.Show("Geçerli bir mesafe girin (km).", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal urunDegeri = 0;
            decimal.TryParse(txtUrunDegeri.Text, out urunDegeri);

            bool basarili = _kargoController.KargoAta(
                _seciliSiparisId,
                cmbFirma.SelectedItem.ToString(),
                agirlik,
                mesafe,
                chkSigorta.Checked,
                urunDegeri,
                chkKirilgan.Checked
            );

            if (basarili)
            {
                MessageBox.Show("Kargo başarıyla atandı ve sipariş kargoda durumuna geçti.",
                    "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SiparisleriYukle();
            }
            else
            {
                MessageBox.Show("Kargo atanamadı. Sipariş hazırlanıyor durumunda olmalı.",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Kargo durumunu sorgula
        private void btnDurumSorgula_Click(object sender, EventArgs e)
        {
            if (_seciliSiparisId == -1) return;

            string durum = _kargoController.KargoDurumuSorgula(_seciliSiparisId);
            MessageBox.Show(durum, "Kargo Durumu",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Teslim edildi olarak işaretle — Kurye yapar
        private void btnTeslimEdildi_Click(object sender, EventArgs e)
        {
            if (_seciliSiparisId == -1)
            {
                MessageBox.Show("Lütfen bir sipariş seçin.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var onay = MessageBox.Show(
                "Sipariş teslim edildi olarak işaretlenecek. Onaylıyor musunuz?",
                "Teslim Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (onay != DialogResult.Yes) return;

            bool basarili = _kargoController.TeslimEdildiIsaretle(_seciliSiparisId);

            if (basarili)
            {
                MessageBox.Show("Sipariş teslim edildi olarak işaretlendi.",
                    "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SiparisleriYukle();
            }
            else
            {
                MessageBox.Show("İşlem başarısız.", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Listeyi yenile
        private void btnYenile_Click(object sender, EventArgs e)
        {
            SiparisleriYukle();
        }
    }
}