namespace LojistikYonetimSistemi.Views
{
    partial class KullaniciForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblBaslik;
        private System.Windows.Forms.Label lblToplamKullanici;
        private System.Windows.Forms.DataGridView dgvKullanicilar;
        private System.Windows.Forms.Label lblSeciliAd;
        private System.Windows.Forms.Button btnSil;
        private System.Windows.Forms.Panel pnlEkle;
        private System.Windows.Forms.Label lblEkleBaslik;
        private System.Windows.Forms.Label lblAd;
        private System.Windows.Forms.TextBox txtAd;
        private System.Windows.Forms.Label lblKullaniciAdi;
        private System.Windows.Forms.TextBox txtKullaniciAdi;
        private System.Windows.Forms.Label lblSifre;
        private System.Windows.Forms.TextBox txtSifre;
        private System.Windows.Forms.Label lblRol;
        private System.Windows.Forms.ComboBox cmbRol;
        private System.Windows.Forms.Button btnEkle;
        private System.Windows.Forms.Button btnTemizle;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblBaslik = new System.Windows.Forms.Label();
            this.lblToplamKullanici = new System.Windows.Forms.Label();
            this.dgvKullanicilar = new System.Windows.Forms.DataGridView();
            this.lblSeciliAd = new System.Windows.Forms.Label();
            this.btnSil = new System.Windows.Forms.Button();
            this.pnlEkle = new System.Windows.Forms.Panel();
            this.lblEkleBaslik = new System.Windows.Forms.Label();
            this.lblAd = new System.Windows.Forms.Label();
            this.txtAd = new System.Windows.Forms.TextBox();
            this.lblKullaniciAdi = new System.Windows.Forms.Label();
            this.txtKullaniciAdi = new System.Windows.Forms.TextBox();
            this.lblSifre = new System.Windows.Forms.Label();
            this.txtSifre = new System.Windows.Forms.TextBox();
            this.lblRol = new System.Windows.Forms.Label();
            this.cmbRol = new System.Windows.Forms.ComboBox();
            this.btnEkle = new System.Windows.Forms.Button();
            this.btnTemizle = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKullanicilar)).BeginInit();
            this.pnlEkle.SuspendLayout();
            this.SuspendLayout();

            // lblBaslik
            this.lblBaslik.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblBaslik.ForeColor = System.Drawing.Color.FromArgb(33, 97, 140);
            this.lblBaslik.Location = new System.Drawing.Point(10, 10);
            this.lblBaslik.Size = new System.Drawing.Size(220, 30);
            this.lblBaslik.Text = "Kullanıcı Yönetimi";

            // lblToplamKullanici
            this.lblToplamKullanici.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblToplamKullanici.ForeColor = System.Drawing.Color.Gray;
            this.lblToplamKullanici.Location = new System.Drawing.Point(240, 16);
            this.lblToplamKullanici.Name = "lblToplamKullanici";
            this.lblToplamKullanici.Size = new System.Drawing.Size(200, 20);
            this.lblToplamKullanici.Text = "Toplam: 0 kullanıcı";

            // dgvKullanicilar
            this.dgvKullanicilar.AllowUserToAddRows = false;
            this.dgvKullanicilar.AllowUserToDeleteRows = false;
            this.dgvKullanicilar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvKullanicilar.BackgroundColor = System.Drawing.Color.White;
            this.dgvKullanicilar.ColumnCount = 4;
            this.dgvKullanicilar.Columns[0].Name = "colId";
            this.dgvKullanicilar.Columns[0].HeaderText = "ID";
            this.dgvKullanicilar.Columns[0].FillWeight = 40;
            this.dgvKullanicilar.Columns[1].Name = "colAd";
            this.dgvKullanicilar.Columns[1].HeaderText = "Ad Soyad";
            this.dgvKullanicilar.Columns[1].FillWeight = 180;
            this.dgvKullanicilar.Columns[2].Name = "colKullaniciAdi";
            this.dgvKullanicilar.Columns[2].HeaderText = "Kullanıcı Adı";
            this.dgvKullanicilar.Columns[2].FillWeight = 140;
            this.dgvKullanicilar.Columns[3].Name = "colRol";
            this.dgvKullanicilar.Columns[3].HeaderText = "Rol";
            this.dgvKullanicilar.Columns[3].FillWeight = 100;
            this.dgvKullanicilar.Location = new System.Drawing.Point(10, 50);
            this.dgvKullanicilar.MultiSelect = false;
            this.dgvKullanicilar.Name = "dgvKullanicilar";
            this.dgvKullanicilar.ReadOnly = true;
            this.dgvKullanicilar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKullanicilar.Size = new System.Drawing.Size(680, 300);
            this.dgvKullanicilar.SelectionChanged += new System.EventHandler(this.dgvKullanicilar_SelectionChanged);

            // lblSeciliAd
            this.lblSeciliAd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblSeciliAd.ForeColor = System.Drawing.Color.FromArgb(33, 97, 140);
            this.lblSeciliAd.Location = new System.Drawing.Point(10, 360);
            this.lblSeciliAd.Name = "lblSeciliAd";
            this.lblSeciliAd.Size = new System.Drawing.Size(400, 22);
            this.lblSeciliAd.Text = "Seçili: —";

            // btnSil
            this.btnSil.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.btnSil.Enabled = false;
            this.btnSil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSil.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSil.ForeColor = System.Drawing.Color.White;
            this.btnSil.Location = new System.Drawing.Point(10, 390);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(160, 34);
            this.btnSil.Text = "Seçiliyi Sil";
            this.btnSil.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click);

            // pnlEkle
            this.pnlEkle.BackColor = System.Drawing.Color.White;
            this.pnlEkle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlEkle.Controls.Add(this.lblEkleBaslik);
            this.pnlEkle.Controls.Add(this.lblAd);
            this.pnlEkle.Controls.Add(this.txtAd);
            this.pnlEkle.Controls.Add(this.lblKullaniciAdi);
            this.pnlEkle.Controls.Add(this.txtKullaniciAdi);
            this.pnlEkle.Controls.Add(this.lblSifre);
            this.pnlEkle.Controls.Add(this.txtSifre);
            this.pnlEkle.Controls.Add(this.lblRol);
            this.pnlEkle.Controls.Add(this.cmbRol);
            this.pnlEkle.Controls.Add(this.btnEkle);
            this.pnlEkle.Controls.Add(this.btnTemizle);
            this.pnlEkle.Location = new System.Drawing.Point(710, 50);
            this.pnlEkle.Name = "pnlEkle";
            this.pnlEkle.Size = new System.Drawing.Size(280, 370);

            // lblEkleBaslik
            this.lblEkleBaslik.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblEkleBaslik.ForeColor = System.Drawing.Color.FromArgb(33, 97, 140);
            this.lblEkleBaslik.Location = new System.Drawing.Point(10, 10);
            this.lblEkleBaslik.Size = new System.Drawing.Size(250, 22);
            this.lblEkleBaslik.Text = "Yeni Kullanıcı Ekle";

            // lblAd
            this.lblAd.AutoSize = true;
            this.lblAd.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblAd.Location = new System.Drawing.Point(10, 42);
            this.lblAd.Text = "Ad Soyad";

            // txtAd
            this.txtAd.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtAd.Location = new System.Drawing.Point(10, 60);
            this.txtAd.Name = "txtAd";
            this.txtAd.Size = new System.Drawing.Size(255, 26);
            this.txtAd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // lblKullaniciAdi
            this.lblKullaniciAdi.AutoSize = true;
            this.lblKullaniciAdi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblKullaniciAdi.Location = new System.Drawing.Point(10, 96);
            this.lblKullaniciAdi.Text = "Kullanıcı Adı";

            // txtKullaniciAdi
            this.txtKullaniciAdi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtKullaniciAdi.Location = new System.Drawing.Point(10, 114);
            this.txtKullaniciAdi.Name = "txtKullaniciAdi";
            this.txtKullaniciAdi.Size = new System.Drawing.Size(255, 26);
            this.txtKullaniciAdi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // lblSifre
            this.lblSifre.AutoSize = true;
            this.lblSifre.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSifre.Location = new System.Drawing.Point(10, 150);
            this.lblSifre.Text = "Şifre";

            // txtSifre
            this.txtSifre.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSifre.Location = new System.Drawing.Point(10, 168);
            this.txtSifre.Name = "txtSifre";
            this.txtSifre.PasswordChar = '*';
            this.txtSifre.Size = new System.Drawing.Size(255, 26);
            this.txtSifre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // lblRol
            this.lblRol.AutoSize = true;
            this.lblRol.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRol.Location = new System.Drawing.Point(10, 204);
            this.lblRol.Text = "Rol";

            // cmbRol
            this.cmbRol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRol.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbRol.Location = new System.Drawing.Point(10, 222);
            this.cmbRol.Name = "cmbRol";
            this.cmbRol.Size = new System.Drawing.Size(255, 26);

            // btnEkle
            this.btnEkle.BackColor = System.Drawing.Color.FromArgb(33, 97, 140);
            this.btnEkle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEkle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEkle.ForeColor = System.Drawing.Color.White;
            this.btnEkle.Location = new System.Drawing.Point(10, 280);
            this.btnEkle.Size = new System.Drawing.Size(255, 38);
            this.btnEkle.Text = "Kullanıcı Ekle";
            this.btnEkle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEkle.Click += new System.EventHandler(this.btnEkle_Click);

            // btnTemizle
            this.btnTemizle.BackColor = System.Drawing.Color.Gray;
            this.btnTemizle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTemizle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnTemizle.ForeColor = System.Drawing.Color.White;
            this.btnTemizle.Location = new System.Drawing.Point(10, 326);
            this.btnTemizle.Size = new System.Drawing.Size(255, 32);
            this.btnTemizle.Text = "Temizle";
            this.btnTemizle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTemizle.Click += new System.EventHandler(this.btnTemizle_Click);

            // KullaniciForm
            this.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.ClientSize = new System.Drawing.Size(1010, 450);
            this.Controls.Add(this.lblBaslik);
            this.Controls.Add(this.lblToplamKullanici);
            this.Controls.Add(this.dgvKullanicilar);
            this.Controls.Add(this.lblSeciliAd);
            this.Controls.Add(this.btnSil);
            this.Controls.Add(this.pnlEkle);
            this.Name = "KullaniciForm";
            this.Text = "Kullanıcı Yönetimi";
            ((System.ComponentModel.ISupportInitialize)(this.dgvKullanicilar)).EndInit();
            this.pnlEkle.ResumeLayout(false);
            this.pnlEkle.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}