using System;
using System.Windows.Forms;
using LojistikYonetimSistemi.Controllers;

namespace LojistikYonetimSistemi.Views
{
    public partial class LoginForm : Form
    {
        private readonly KullaniciController _kullaniciController;

        public LoginForm()
        {
            InitializeComponent();
            _kullaniciController = new KullaniciController();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = txtKullaniciAdi.Text.Trim();
            string sifre = txtSifre.Text;

            if (string.IsNullOrEmpty(kullaniciAdi) || string.IsNullOrEmpty(sifre))
            {
                MessageBox.Show("Kullanıcı adı ve şifre boş bırakılamaz.",
                    "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool basarili = _kullaniciController.GirisYap(kullaniciAdi, sifre);

            if (basarili)
            {
                var mainForm = new MainForm(_kullaniciController);
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı.",
                    "Giriş Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSifre.Clear();
                txtSifre.Focus();
            }
        }

        private void txtSifre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                btnGiris_Click(sender, e);
        }
    }
}