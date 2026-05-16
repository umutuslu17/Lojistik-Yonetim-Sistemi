using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using LojistikYonetimSistemi.Controllers;
using LojistikYonetimSistemi.Models.Entities;
using LojistikYonetimSistemi.Models.Enums;

namespace LojistikYonetimSistemi.Views
{
    public partial class StokForm : Form
    {
        private readonly StokController _stokController;
        private readonly KullaniciController _kullaniciController;

        // Seçili ürünün Id'sini tutuyoruz, güncelleme ve silmede lazım
        private int _seciliUrunId = -1;

        public StokForm(StokController stokController, KullaniciController kullaniciController)
        {
            InitializeComponent();
            _stokController = stokController;
            _kullaniciController = kullaniciController;

            // Sadece admin ürün ekleyip silebilir, depo görevlisi sadece stok güncelleyebilir
            bool adminMi = _kullaniciController.YetkiKontrol(KullaniciRolu.Admin);
            pnlUrunEkle.Visible = adminMi;
            btnSil.Visible = adminMi;

            // Stok bildirimlerini dinle — kritik ürünler kırmızı gösterilecek
            _stokController.SistemBildirici.YeniBildirimGeldi += (mesaj) =>
            {
                if (this.InvokeRequired)
                    this.Invoke(new Action(() => UrunleriYukle()));
                else
                    UrunleriYukle();
            };

            UrunleriYukle();
        }

        // Tüm ürünleri DataGridView'a yükle
        // Stoku eşiğin altında olan satırları kırmızıyla işaretle
        private void UrunleriYukle()
        {
            dgvUrunler.Rows.Clear();
            var urunler = _stokController.TumUrunleriGetir();

            foreach (var u in urunler)
            {
                string tip = u is BilesikUrun ? "Bileşik" : "Basit";
                string fiyat = u.HesaplaFiyat().ToString("C2");

                int index = dgvUrunler.Rows.Add(
                    u.Id,
                    u.Ad,
                    tip,
                    fiyat,
                    u.Stok,
                    u.EsikDeger
                );

                // Stok eşiğin altındaysa satırı kırmızıyla renklendir
                if (u.EsikAsildiMi())
                {
                    dgvUrunler.Rows[index].DefaultCellStyle.BackColor = Color.FromArgb(255, 220, 220);
                    dgvUrunler.Rows[index].DefaultCellStyle.ForeColor = Color.DarkRed;
                }
            }

            lblKritikSayisi.Text = $"Kritik stok: {_stokController.KritikStokluUrunleriGetir().Count} ürün";
        }

        // Listeden bir satır seçilince sağ panele bilgileri doldur
        private void dgvUrunler_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUrunler.SelectedRows.Count == 0) return;

            var row = dgvUrunler.SelectedRows[0];
            _seciliUrunId = (int)row.Cells[0].Value;

            txtStokGuncelle.Text = row.Cells[4].Value.ToString();
        }

        // Stok miktarını güncelle
        private void btnStokGuncelle_Click(object sender, EventArgs e)
        {
            if (_seciliUrunId == -1)
            {
                MessageBox.Show("Lütfen bir ürün seçin.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtStokGuncelle.Text, out int yeniMiktar) || yeniMiktar < 0)
            {
                MessageBox.Show("Geçerli bir stok miktarı girin.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool basarili = _stokController.StokGuncelle(_seciliUrunId, yeniMiktar);

            if (basarili)
            {
                MessageBox.Show("Stok güncellendi.", "Başarılı",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                UrunleriYukle();
            }
            else
            {
                MessageBox.Show("Stok güncellenemedi.", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Yeni basit ürün ekle
        private void btnUrunEkle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUrunAdi.Text))
            {
                MessageBox.Show("Ürün adı boş bırakılamaz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtFiyat.Text, out decimal fiyat) || fiyat <= 0)
            {
                MessageBox.Show("Geçerli bir fiyat girin.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtIlkStok.Text, out int stok) || stok < 0)
            {
                MessageBox.Show("Geçerli bir stok miktarı girin.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int.TryParse(txtEsikDeger.Text, out int esik);

            bool basarili = _stokController.UrunEkle(
                txtUrunAdi.Text.Trim(), fiyat, stok, esik);

            if (basarili)
            {
                MessageBox.Show("Ürün eklendi.", "Başarılı",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                FormuTemizle();
                UrunleriYukle();
            }
            else
            {
                MessageBox.Show("Ürün eklenemedi.", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Seçili ürünü sil
        private void btnSil_Click(object sender, EventArgs e)
        {
            if (_seciliUrunId == -1)
            {
                MessageBox.Show("Lütfen silinecek ürünü seçin.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var onay = MessageBox.Show(
                "Bu ürünü silmek istediğinize emin misiniz?",
                "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (onay != DialogResult.Yes) return;

            bool basarili = _stokController.UrunSil(_seciliUrunId);

            if (basarili)
            {
                MessageBox.Show("Ürün silindi.", "Başarılı",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                _seciliUrunId = -1;
                UrunleriYukle();
            }
            else
            {
                MessageBox.Show("Ürün silinemedi.", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Ürün ekleme formunu temizle
        private void FormuTemizle()
        {
            txtUrunAdi.Clear();
            txtFiyat.Clear();
            txtIlkStok.Clear();
            txtEsikDeger.Clear();
        }

        // Listeyi yenile
        private void btnYenile_Click(object sender, EventArgs e)
        {
            UrunleriYukle();
        }
    }
}