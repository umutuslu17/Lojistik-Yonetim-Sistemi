namespace LojistikYonetimSistemi.Views
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.Panel pnlIcerik;
        private System.Windows.Forms.Label lblKullanici;
        private System.Windows.Forms.Button btnSiparisler;
        private System.Windows.Forms.Button btnStok;
        private System.Windows.Forms.Button btnKargo;
        private System.Windows.Forms.Button btnKullanicilar;
        private System.Windows.Forms.Button btnLoglar;
        private System.Windows.Forms.Button btnCikis;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.lblKullanici = new System.Windows.Forms.Label();
            this.btnSiparisler = new System.Windows.Forms.Button();
            this.btnStok = new System.Windows.Forms.Button();
            this.btnKargo = new System.Windows.Forms.Button();
            this.btnKullanicilar = new System.Windows.Forms.Button();
            this.btnLoglar = new System.Windows.Forms.Button();
            this.btnCikis = new System.Windows.Forms.Button();
            this.pnlIcerik = new System.Windows.Forms.Panel();
            this.pnlMenu.SuspendLayout();
            this.SuspendLayout();

            // pnlMenu
            this.pnlMenu.BackColor = System.Drawing.Color.FromArgb(33, 37, 41);
            this.pnlMenu.Controls.Add(this.lblKullanici);
            this.pnlMenu.Controls.Add(this.btnSiparisler);
            this.pnlMenu.Controls.Add(this.btnStok);
            this.pnlMenu.Controls.Add(this.btnKargo);
            this.pnlMenu.Controls.Add(this.btnKullanicilar);
            this.pnlMenu.Controls.Add(this.btnLoglar);
            this.pnlMenu.Controls.Add(this.btnCikis);
            this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMenu.Width = 200;

            // lblKullanici
            this.lblKullanici.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblKullanici.ForeColor = System.Drawing.Color.FromArgb(173, 181, 189);
            this.lblKullanici.Location = new System.Drawing.Point(0, 10);
            this.lblKullanici.Name = "lblKullanici";
            this.lblKullanici.Size = new System.Drawing.Size(200, 60);
            this.lblKullanici.Text = "Kullanıcı";
            this.lblKullanici.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // btnSiparisler
            this.btnSiparisler.BackColor = System.Drawing.Color.FromArgb(52, 58, 64);
            this.btnSiparisler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSiparisler.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSiparisler.ForeColor = System.Drawing.Color.White;
            this.btnSiparisler.Location = new System.Drawing.Point(0, 90);
            this.btnSiparisler.Name = "btnSiparisler";
            this.btnSiparisler.Size = new System.Drawing.Size(200, 40);
            this.btnSiparisler.Text = "Siparisler";
            this.btnSiparisler.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSiparisler.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnSiparisler.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSiparisler.Click += new System.EventHandler(this.btnSiparisler_Click);

            // btnStok
            this.btnStok.BackColor = System.Drawing.Color.FromArgb(52, 58, 64);
            this.btnStok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStok.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnStok.ForeColor = System.Drawing.Color.White;
            this.btnStok.Location = new System.Drawing.Point(0, 140);
            this.btnStok.Name = "btnStok";
            this.btnStok.Size = new System.Drawing.Size(200, 40);
            this.btnStok.Text = "Stok Yonetimi";
            this.btnStok.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStok.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnStok.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStok.Click += new System.EventHandler(this.btnStok_Click);

            // btnKargo
            this.btnKargo.BackColor = System.Drawing.Color.FromArgb(52, 58, 64);
            this.btnKargo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKargo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnKargo.ForeColor = System.Drawing.Color.White;
            this.btnKargo.Location = new System.Drawing.Point(0, 190);
            this.btnKargo.Name = "btnKargo";
            this.btnKargo.Size = new System.Drawing.Size(200, 40);
            this.btnKargo.Text = "Kargo";
            this.btnKargo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnKargo.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnKargo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKargo.Click += new System.EventHandler(this.btnKargo_Click);

            // btnKullanicilar
            this.btnKullanicilar.BackColor = System.Drawing.Color.FromArgb(52, 58, 64);
            this.btnKullanicilar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKullanicilar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnKullanicilar.ForeColor = System.Drawing.Color.White;
            this.btnKullanicilar.Location = new System.Drawing.Point(0, 240);
            this.btnKullanicilar.Name = "btnKullanicilar";
            this.btnKullanicilar.Size = new System.Drawing.Size(200, 40);
            this.btnKullanicilar.Text = "Kullanicilar";
            this.btnKullanicilar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnKullanicilar.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnKullanicilar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKullanicilar.Click += new System.EventHandler(this.btnKullanicilar_Click);

            // btnLoglar
            this.btnLoglar.BackColor = System.Drawing.Color.FromArgb(52, 58, 64);
            this.btnLoglar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoglar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnLoglar.ForeColor = System.Drawing.Color.White;
            this.btnLoglar.Location = new System.Drawing.Point(0, 290);
            this.btnLoglar.Name = "btnLoglar";
            this.btnLoglar.Size = new System.Drawing.Size(200, 40);
            this.btnLoglar.Text = "Loglar";
            this.btnLoglar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLoglar.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnLoglar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoglar.Click += new System.EventHandler(this.btnLoglar_Click);

            // btnCikis
            this.btnCikis.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.btnCikis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCikis.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCikis.ForeColor = System.Drawing.Color.White;
            this.btnCikis.Location = new System.Drawing.Point(0, 580);
            this.btnCikis.Name = "btnCikis";
            this.btnCikis.Size = new System.Drawing.Size(200, 40);
            this.btnCikis.Text = "Cikis Yap";
            this.btnCikis.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCikis.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnCikis.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCikis.Click += new System.EventHandler(this.btnCikis_Click);

            // pnlIcerik
            this.pnlIcerik.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.pnlIcerik.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlIcerik.Name = "pnlIcerik";
            this.pnlIcerik.Padding = new System.Windows.Forms.Padding(10);

            // MainForm
            this.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Controls.Add(this.pnlIcerik);
            this.Controls.Add(this.pnlMenu);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lojistik Yonetim Sistemi";
            this.pnlMenu.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}