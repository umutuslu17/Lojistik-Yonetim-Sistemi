using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LojistikYonetimSistemi.Controllers;
using LojistikYonetimSistemi.Models.Entities;
using LojistikYonetimSistemi.Models.Enums;

namespace LojistikYonetimSistemi.Views
{
    public partial class KullaniciForm : Form
    {
        private readonly KullaniciController _kullaniciController;

        // Seçili kullanıcının Id'si — silme işleminde lazım
        private int _seciliKullaniciId = -1;

        public KullaniciForm(KullaniciController kullaniciController)
        {
            InitializeComponent();
            _kullaniciController = kullaniciController;

            // Rol combobox'ını enum değerleriyle doldur
            cmbRol.Items.Add("Admin");
            cmbRol.Items.Add("DepoGorevlisi");
            cmbRol.Items.Add("Kurye");
            cmbRol.Items.Add("Musteri");
            cmbRol.SelectedIndex = 3;

            KullanicilariYukle();
        }

        // Tüm kullanıcıları listeye yükle
        private void KullanicilariYukle()
        {
            dgvKullanicilar.Rows.Clear();
            List<Kullanici> kullanicilar = _kullaniciController.TumKullanicilariGetir();

            foreach (var k in kullanicilar)
            {
                int index = dgvKullanicilar.Rows.Add(
                    k.Id,
                    k.Ad,
                    k.KullaniciAdi,
                    k.Rol.ToString()
                );

                // Aktif kullanıcıyı mavi ile vurgula
                if (k.Id == _kullaniciController.AktifKullanici.Id)
                {
                    dgvKullanicilar.Rows[index].DefaultCellStyle.BackColor =
                        System.Drawing.Color.FromArgb(210, 230, 255);
                    dgvKullanicilar.Rows[index].DefaultCellStyle.Font =
                        new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
                }
            }

            lblToplamKullanici.Text = $"Toplam: {kullanicilar.Count} kullanıcı";
        }

        // Listeden kullanıcı seçilince bilgileri forma doldur
        private void dgvKullanicilar_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvKullanicilar.SelectedRows.Count == 0) return;

            var row = dgvKullanicilar.SelectedRows[0];
            _seciliKullaniciId = (int)row.Cells[0].Value;

            // Seçili kullanıcının bilgilerini göster
            lblSeciliAd.Text = $"Seçili: {row.Cells[1].Value} ({row.Cells[3].Value})";

            // Aktif kullanıcı kendini silemesin
            btnSil.Enabled = _seciliKullaniciId != _kullaniciController.AktifKullanici.Id;
        }

        // Yeni kullanıcı ekle
        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAd.Text))
            {
                MessageBox.Show("Ad Soyad boş bırakılamaz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtKullaniciAdi.Text))
            {
                MessageBox.Show("Kullanıcı adı boş bırakılamaz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSifre.Text) || txtSifre.Text.Length < 4)
            {
                MessageBox.Show("Şifre en az 4 karakter olmalı.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ComboBox seçimini KullaniciRolu enum'una çevir
            KullaniciRolu rol = (KullaniciRolu)cmbRol.SelectedIndex;

            bool basarili = _kullaniciController.KullaniciEkle(
                txtAd.Text.Trim(),
                txtKullaniciAdi.Text.Trim(),
                txtSifre.Text,
                rol);

            if (basarili)
            {
                MessageBox.Show("Kullanıcı başarıyla eklendi.", "Başarılı",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                FormuTemizle();
                KullanicilariYukle();
            }
            else
            {
                MessageBox.Show("Kullanıcı eklenemedi. Bu kullanıcı adı zaten kullanılıyor olabilir.",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Seçili kullanıcıyı sil
        private void btnSil_Click(object sender, EventArgs e)
        {
            if (_seciliKullaniciId == -1)
            {
                MessageBox.Show("Lütfen silinecek kullanıcıyı seçin.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var onay = MessageBox.Show(
                "Bu kullanıcıyı silmek istediğinize emin misiniz?",
                "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (onay != DialogResult.Yes) return;

            bool basarili = _kullaniciController.KullaniciSil(_seciliKullaniciId);

            if (basarili)
            {
                MessageBox.Show("Kullanıcı silindi.", "Başarılı",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                _seciliKullaniciId = -1;
                lblSeciliAd.Text = "Seçili: —";
                KullanicilariYukle();
            }
            else
            {
                MessageBox.Show("Kullanıcı silinemedi. Son admin silinemez.",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Formu temizle
        private void FormuTemizle()
        {
            txtAd.Clear();
            txtKullaniciAdi.Clear();
            txtSifre.Clear();
            cmbRol.SelectedIndex = 3;
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            FormuTemizle();
        }
    }
}