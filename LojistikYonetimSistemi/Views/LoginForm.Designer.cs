namespace LojistikYonetimSistemi.Views
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        // Kontroller
        private System.Windows.Forms.Panel pnlKart;
        private System.Windows.Forms.Label lblBaslik;
        private System.Windows.Forms.Label lblKullaniciAdi;
        private System.Windows.Forms.Label lblSifre;
        private System.Windows.Forms.TextBox txtKullaniciAdi;
        private System.Windows.Forms.TextBox txtSifre;
        private System.Windows.Forms.Button btnGiris;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlKart = new System.Windows.Forms.Panel();
            this.lblBaslik = new System.Windows.Forms.Label();
            this.lblKullaniciAdi = new System.Windows.Forms.Label();
            this.txtKullaniciAdi = new System.Windows.Forms.TextBox();
            this.lblSifre = new System.Windows.Forms.Label();
            this.txtSifre = new System.Windows.Forms.TextBox();
            this.btnGiris = new System.Windows.Forms.Button();
            this.pnlKart.SuspendLayout();
            this.SuspendLayout();

            // pnlKart
            this.pnlKart.BackColor = System.Drawing.Color.White;
            this.pnlKart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlKart.Controls.Add(this.lblBaslik);
            this.pnlKart.Controls.Add(this.lblKullaniciAdi);
            this.pnlKart.Controls.Add(this.txtKullaniciAdi);
            this.pnlKart.Controls.Add(this.lblSifre);
            this.pnlKart.Controls.Add(this.txtSifre);
            this.pnlKart.Controls.Add(this.btnGiris);
            this.pnlKart.Location = new System.Drawing.Point(40, 40);
            this.pnlKart.Size = new System.Drawing.Size(340, 280);

            // lblBaslik
            this.lblBaslik.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblBaslik.ForeColor = System.Drawing.Color.FromArgb(33, 97, 140);
            this.lblBaslik.Location = new System.Drawing.Point(20, 20);
            this.lblBaslik.Size = new System.Drawing.Size(300, 40);
            this.lblBaslik.Text = "Lojistik Yönetim Sistemi";
            this.lblBaslik.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // lblKullaniciAdi
            this.lblKullaniciAdi.AutoSize = true;
            this.lblKullaniciAdi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblKullaniciAdi.Location = new System.Drawing.Point(20, 80);
            this.lblKullaniciAdi.Text = "Kullanıcı Adı";

            // txtKullaniciAdi
            this.txtKullaniciAdi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKullaniciAdi.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtKullaniciAdi.Location = new System.Drawing.Point(20, 105);
            this.txtKullaniciAdi.Name = "txtKullaniciAdi";
            this.txtKullaniciAdi.Size = new System.Drawing.Size(300, 28);

            // lblSifre
            this.lblSifre.AutoSize = true;
            this.lblSifre.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSifre.Location = new System.Drawing.Point(20, 145);
            this.lblSifre.Text = "Şifre";

            // txtSifre
            this.txtSifre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSifre.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSifre.Location = new System.Drawing.Point(20, 170);
            this.txtSifre.Name = "txtSifre";
            this.txtSifre.PasswordChar = '*';
            this.txtSifre.Size = new System.Drawing.Size(300, 28);
            this.txtSifre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSifre_KeyPress);

            // btnGiris
            this.btnGiris.BackColor = System.Drawing.Color.FromArgb(33, 97, 140);
            this.btnGiris.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGiris.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnGiris.ForeColor = System.Drawing.Color.White;
            this.btnGiris.Location = new System.Drawing.Point(20, 220);
            this.btnGiris.Size = new System.Drawing.Size(300, 40);
            this.btnGiris.Text = "Giriş Yap";
            this.btnGiris.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGiris.Click += new System.EventHandler(this.btnGiris_Click);

            // LoginForm
            this.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.ClientSize = new System.Drawing.Size(420, 360);
            this.Controls.Add(this.pnlKart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lojistik Yönetim Sistemi — Giriş";
            this.pnlKart.ResumeLayout(false);
            this.pnlKart.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}