using LojistikYonetimSistemi.Controllers;
using LojistikYonetimSistemi.Models.Enums;
using LojistikYonetimSistemi.Models.Patterns.Singleton;
using System;
using System.Drawing.Interop;
using System.Windows.Forms;

namespace LojistikYonetimSistemi.Views
{
    public partial class MainForm : Form
    {
        private readonly KullaniciController _kullaniciController;
        private readonly StokController _stokController;
        private readonly SiparisController _siparisController;
        private readonly KargoController _kargoController;

        public MainForm(KullaniciController kullaniciController)
        {
            InitializeComponent();

            _kullaniciController = kullaniciController;
            _stokController = new StokController(_kullaniciController);
            _siparisController = new SiparisController(_kullaniciController, _stokController);
            _kargoController = new KargoController(_kullaniciController);

            // Kullanıcı adını ve rolünü sol menüde göster
            lblKullanici.Text = $"{_kullaniciController.AktifKullanici.Ad}\n" +
                                $"{_kullaniciController.AktifKullanici.Rol}";

            this.Text = $"Lojistik Yönetim Sistemi — " +
                        $"{_kullaniciController.AktifKullanici.Ad}";

            MenuButonlariniAyarla();

            // Stok uyarılarını dinle
            _stokController.SistemBildirici.YeniBildirimGeldi += StokUyarisiGoster;
        }

        // Role göre hangi butonların görüneceğini ayarla
        private void MenuButonlariniAyarla()
        {
            var rol = _kullaniciController.AktifKullanici.Rol;

            btnSiparisler.Visible = true;
            btnStok.Visible = (rol == KullaniciRolu.Admin || rol == KullaniciRolu.DepoGorevlisi);
            btnKargo.Visible = (rol == KullaniciRolu.Admin ||
                                rol == KullaniciRolu.DepoGorevlisi ||
                                rol == KullaniciRolu.Kurye);
            btnKullanicilar.Visible = (rol == KullaniciRolu.Admin);
            btnLoglar.Visible = (rol == KullaniciRolu.Admin);
        }

        // Observer'dan gelen stok uyarısını UI thread'inde göster
        private void StokUyarisiGoster(string mesaj)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => StokUyarisiGoster(mesaj)));
                return;
            }
            MessageBox.Show(mesaj, "Stok Uyarisi",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnSiparisler_Click(object sender, EventArgs e)
        {
            AltFormuAc(new SiparisForm(_siparisController, _stokController, _kullaniciController));
        }

        private void btnStok_Click(object sender, EventArgs e)
        {
            AltFormuAc(new StokForm(_stokController, _kullaniciController));
        }

        private void btnKargo_Click(object sender, EventArgs e)
        {
            AltFormuAc(new KargoForm(_kargoController, _siparisController, _kullaniciController));
        }

        private void btnKullanicilar_Click(object sender, EventArgs e)
        {
            AltFormuAc(new KullaniciForm(_kullaniciController));
        }

        private void btnLoglar_Click(object sender, EventArgs e)
        {
            AltFormuAc(new LogForm(Logger.GetInstance().TumLoglariGetir()));
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            var sonuc = MessageBox.Show(
                "Cikis yapmak istediginize emin misiniz?",
                "Cikis",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (sonuc == DialogResult.Yes)
            {
                _kullaniciController.CikisYap();
                this.Close();
            }
        }

        // Alt formu içerik paneline göm
        private void AltFormuAc(Form form)
        {
            pnlIcerik.Controls.Clear();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            pnlIcerik.Controls.Add(form);
            form.Show();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            Application.Exit();
        }
    }
}