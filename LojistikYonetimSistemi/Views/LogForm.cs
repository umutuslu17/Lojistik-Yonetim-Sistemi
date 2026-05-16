using System;
using System.Drawing;
using System.Windows.Forms;

namespace LojistikYonetimSistemi.Views
{
    public partial class LogForm : Form
    {
        private readonly string[] _loglar;

        public LogForm(string[] loglar)
        {
            InitializeComponent();
            _loglar = loglar;
            LoglarıYukle();
        }

        // Logları listeye yükle ve renklendirme uygula
        private void LoglarıYukle()
        {
            lstLoglar.Items.Clear();

            if (_loglar == null || _loglar.Length == 0)
            {
                lstLoglar.Items.Add("Henüz kayıtlı log bulunmuyor.");
                lblToplamLog.Text = "Toplam: 0 kayıt";
                return;
            }

            // En yeni loglar üstte görünsün diye ters sıraya alıyoruz
            for (int i = _loglar.Length - 1; i >= 0; i--)
                lstLoglar.Items.Add(_loglar[i]);

            lblToplamLog.Text = $"Toplam: {_loglar.Length} kayıt";
        }

        // Arama kutusuna göre logları filtrele
        private void txtArama_TextChanged(object sender, EventArgs e)
        {
            lstLoglar.Items.Clear();
            string aranan = txtArama.Text.ToLower().Trim();

            if (string.IsNullOrEmpty(aranan))
            {
                LoglarıYukle();
                return;
            }

            int bulunan = 0;
            for (int i = _loglar.Length - 1; i >= 0; i--)
            {
                if (_loglar[i].ToLower().Contains(aranan))
                {
                    lstLoglar.Items.Add(_loglar[i]);
                    bulunan++;
                }
            }

            lblToplamLog.Text = $"Bulunan: {bulunan} kayıt";
        }

        // Tüm logları temizle
        private void btnTemizle_Click(object sender, EventArgs e)
        {
            var onay = MessageBox.Show(
                "Tüm loglar silinecek. Emin misiniz?",
                "Log Temizle",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (onay != DialogResult.Yes) return;

            Models.Patterns.Singleton.Logger.GetInstance().LogTemizle();
            lstLoglar.Items.Clear();
            lstLoglar.Items.Add("Loglar temizlendi.");
            lblToplamLog.Text = "Toplam: 0 kayıt";
        }

        // Listeyi yenile
        private void btnYenile_Click(object sender, EventArgs e)
        {
            var yeniLoglar = Models.Patterns.Singleton.Logger.GetInstance().TumLoglariGetir();
            lstLoglar.Items.Clear();

            for (int i = yeniLoglar.Length - 1; i >= 0; i--)
                lstLoglar.Items.Add(yeniLoglar[i]);

            lblToplamLog.Text = $"Toplam: {yeniLoglar.Length} kayıt";
            txtArama.Clear();
        }
    }
}