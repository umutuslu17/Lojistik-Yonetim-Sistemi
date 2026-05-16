namespace LojistikYonetimSistemi.Views
{
    partial class StokForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblBaslik;
        private System.Windows.Forms.Label lblKritikSayisi;
        private System.Windows.Forms.DataGridView dgvUrunler;
        private System.Windows.Forms.Panel pnlSagPanel;
        private System.Windows.Forms.Label lblStokGuncelle;
        private System.Windows.Forms.TextBox txtStokGuncelle;
        private System.Windows.Forms.Button btnStokGuncelle;
        private System.Windows.Forms.Button btnSil;
        private System.Windows.Forms.Button btnYenile;
        private System.Windows.Forms.Panel pnlUrunEkle;
        private System.Windows.Forms.Label lblUrunEkle;
        private System.Windows.Forms.Label lblUrunAdi;
        private System.Windows.Forms.TextBox txtUrunAdi;
        private System.Windows.Forms.Label lblFiyat;
        private System.Windows.Forms.TextBox txtFiyat;
        private System.Windows.Forms.Label lblIlkStok;
        private System.Windows.Forms.TextBox txtIlkStok;
        private System.Windows.Forms.Label lblEsikDeger;
        private System.Windows.Forms.TextBox txtEsikDeger;
        private System.Windows.Forms.Button btnUrunEkle;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblBaslik = new System.Windows.Forms.Label();
            this.lblKritikSayisi = new System.Windows.Forms.Label();
            this.dgvUrunler = new System.Windows.Forms.DataGridView();
            this.pnlSagPanel = new System.Windows.Forms.Panel();
            this.lblStokGuncelle = new System.Windows.Forms.Label();
            this.txtStokGuncelle = new System.Windows.Forms.TextBox();
            this.btnStokGuncelle = new System.Windows.Forms.Button();
            this.btnSil = new System.Windows.Forms.Button();
            this.btnYenile = new System.Windows.Forms.Button();
            this.pnlUrunEkle = new System.Windows.Forms.Panel();
            this.lblUrunEkle = new System.Windows.Forms.Label();
            this.lblUrunAdi = new System.Windows.Forms.Label();
            this.txtUrunAdi = new System.Windows.Forms.TextBox();
            this.lblFiyat = new System.Windows.Forms.Label();
            this.txtFiyat = new System.Windows.Forms.TextBox();
            this.lblIlkStok = new System.Windows.Forms.Label();
            this.txtIlkStok = new System.Windows.Forms.TextBox();
            this.lblEsikDeger = new System.Windows.Forms.Label();
            this.txtEsikDeger = new System.Windows.Forms.TextBox();
            this.btnUrunEkle = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUrunler)).BeginInit();
            this.pnlSagPanel.SuspendLayout();
            this.pnlUrunEkle.SuspendLayout();
            this.SuspendLayout();

            // lblBaslik
            this.lblBaslik.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblBaslik.ForeColor = System.Drawing.Color.FromArgb(33, 97, 140);
            this.lblBaslik.Location = new System.Drawing.Point(10, 10);
            this.lblBaslik.Size = new System.Drawing.Size(220, 30);
            this.lblBaslik.Text = "Stok Yönetimi";

            // lblKritikSayisi
            this.lblKritikSayisi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblKritikSayisi.ForeColor = System.Drawing.Color.DarkRed;
            this.lblKritikSayisi.Location = new System.Drawing.Point(240, 15);
            this.lblKritikSayisi.Name = "lblKritikSayisi";
            this.lblKritikSayisi.Size = new System.Drawing.Size(200, 22);
            this.lblKritikSayisi.Text = "Kritik stok: 0 ürün";

            // dgvUrunler
            this.dgvUrunler.AllowUserToAddRows = false;
            this.dgvUrunler.AllowUserToDeleteRows = false;
            this.dgvUrunler.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUrunler.BackgroundColor = System.Drawing.Color.White;
            this.dgvUrunler.ColumnCount = 6;
            this.dgvUrunler.Columns[0].Name = "colId";
            this.dgvUrunler.Columns[0].HeaderText = "ID";
            this.dgvUrunler.Columns[0].FillWeight = 40;
            this.dgvUrunler.Columns[1].Name = "colAd";
            this.dgvUrunler.Columns[1].HeaderText = "Ürün Adı";
            this.dgvUrunler.Columns[1].FillWeight = 200;
            this.dgvUrunler.Columns[2].Name = "colTip";
            this.dgvUrunler.Columns[2].HeaderText = "Tip";
            this.dgvUrunler.Columns[2].FillWeight = 60;
            this.dgvUrunler.Columns[3].Name = "colFiyat";
            this.dgvUrunler.Columns[3].HeaderText = "Birim Fiyat";
            this.dgvUrunler.Columns[3].FillWeight = 80;
            this.dgvUrunler.Columns[4].Name = "colStok";
            this.dgvUrunler.Columns[4].HeaderText = "Stok";
            this.dgvUrunler.Columns[4].FillWeight = 60;
            this.dgvUrunler.Columns[5].Name = "colEsik";
            this.dgvUrunler.Columns[5].HeaderText = "Eşik Değer";
            this.dgvUrunler.Columns[5].FillWeight = 70;
            this.dgvUrunler.Location = new System.Drawing.Point(10, 50);
            this.dgvUrunler.MultiSelect = false;
            this.dgvUrunler.Name = "dgvUrunler";
            this.dgvUrunler.ReadOnly = true;
            this.dgvUrunler.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUrunler.Size = new System.Drawing.Size(750, 400);
            this.dgvUrunler.SelectionChanged += new System.EventHandler(this.dgvUrunler_SelectionChanged);

            // btnYenile
            this.btnYenile.BackColor = System.Drawing.Color.FromArgb(33, 97, 140);
            this.btnYenile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYenile.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnYenile.ForeColor = System.Drawing.Color.White;
            this.btnYenile.Location = new System.Drawing.Point(10, 460);
            this.btnYenile.Size = new System.Drawing.Size(100, 32);
            this.btnYenile.Text = "Yenile";
            this.btnYenile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnYenile.Click += new System.EventHandler(this.btnYenile_Click);

            // pnlSagPanel
            this.pnlSagPanel.BackColor = System.Drawing.Color.White;
            this.pnlSagPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSagPanel.Controls.Add(this.lblStokGuncelle);
            this.pnlSagPanel.Controls.Add(this.txtStokGuncelle);
            this.pnlSagPanel.Controls.Add(this.btnStokGuncelle);
            this.pnlSagPanel.Controls.Add(this.btnSil);
            this.pnlSagPanel.Location = new System.Drawing.Point(775, 50);
            this.pnlSagPanel.Size = new System.Drawing.Size(250, 160);

            // lblStokGuncelle
            this.lblStokGuncelle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblStokGuncelle.Location = new System.Drawing.Point(10, 15);
            this.lblStokGuncelle.Size = new System.Drawing.Size(220, 22);
            this.lblStokGuncelle.Text = "Stok Güncelle";

            // txtStokGuncelle
            this.txtStokGuncelle.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtStokGuncelle.Location = new System.Drawing.Point(10, 45);
            this.txtStokGuncelle.Name = "txtStokGuncelle";
            this.txtStokGuncelle.Size = new System.Drawing.Size(230, 28);
            this.txtStokGuncelle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // btnStokGuncelle
            this.btnStokGuncelle.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnStokGuncelle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStokGuncelle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnStokGuncelle.ForeColor = System.Drawing.Color.White;
            this.btnStokGuncelle.Location = new System.Drawing.Point(10, 85);
            this.btnStokGuncelle.Size = new System.Drawing.Size(230, 32);
            this.btnStokGuncelle.Text = "Stoku Güncelle";
            this.btnStokGuncelle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStokGuncelle.Click += new System.EventHandler(this.btnStokGuncelle_Click);

            // btnSil
            this.btnSil.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.btnSil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSil.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSil.ForeColor = System.Drawing.Color.White;
            this.btnSil.Location = new System.Drawing.Point(10, 125);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(230, 32);
            this.btnSil.Text = "Ürünü Sil";
            this.btnSil.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click);

            // pnlUrunEkle
            this.pnlUrunEkle.BackColor = System.Drawing.Color.White;
            this.pnlUrunEkle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlUrunEkle.Controls.Add(this.lblUrunEkle);
            this.pnlUrunEkle.Controls.Add(this.lblUrunAdi);
            this.pnlUrunEkle.Controls.Add(this.txtUrunAdi);
            this.pnlUrunEkle.Controls.Add(this.lblFiyat);
            this.pnlUrunEkle.Controls.Add(this.txtFiyat);
            this.pnlUrunEkle.Controls.Add(this.lblIlkStok);
            this.pnlUrunEkle.Controls.Add(this.txtIlkStok);
            this.pnlUrunEkle.Controls.Add(this.lblEsikDeger);
            this.pnlUrunEkle.Controls.Add(this.txtEsikDeger);
            this.pnlUrunEkle.Controls.Add(this.btnUrunEkle);
            this.pnlUrunEkle.Location = new System.Drawing.Point(775, 225);
            this.pnlUrunEkle.Name = "pnlUrunEkle";
            this.pnlUrunEkle.Size = new System.Drawing.Size(250, 280);

            // lblUrunEkle
            this.lblUrunEkle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblUrunEkle.Location = new System.Drawing.Point(10, 10);
            this.lblUrunEkle.Size = new System.Drawing.Size(220, 22);
            this.lblUrunEkle.Text = "Yeni Ürün Ekle";

            // lblUrunAdi
            this.lblUrunAdi.AutoSize = true;
            this.lblUrunAdi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUrunAdi.Location = new System.Drawing.Point(10, 42);
            this.lblUrunAdi.Text = "Ürün Adı";

            // txtUrunAdi
            this.txtUrunAdi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUrunAdi.Location = new System.Drawing.Point(10, 60);
            this.txtUrunAdi.Name = "txtUrunAdi";
            this.txtUrunAdi.Size = new System.Drawing.Size(230, 26);
            this.txtUrunAdi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // lblFiyat
            this.lblFiyat.AutoSize = true;
            this.lblFiyat.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFiyat.Location = new System.Drawing.Point(10, 94);
            this.lblFiyat.Text = "Birim Fiyat (TL)";

            // txtFiyat
            this.txtFiyat.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtFiyat.Location = new System.Drawing.Point(10, 112);
            this.txtFiyat.Name = "txtFiyat";
            this.txtFiyat.Size = new System.Drawing.Size(110, 26);
            this.txtFiyat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // lblIlkStok
            this.lblIlkStok.AutoSize = true;
            this.lblIlkStok.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblIlkStok.Location = new System.Drawing.Point(130, 94);
            this.lblIlkStok.Text = "İlk Stok";

            // txtIlkStok
            this.txtIlkStok.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtIlkStok.Location = new System.Drawing.Point(130, 112);
            this.txtIlkStok.Name = "txtIlkStok";
            this.txtIlkStok.Size = new System.Drawing.Size(110, 26);
            this.txtIlkStok.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // lblEsikDeger
            this.lblEsikDeger.AutoSize = true;
            this.lblEsikDeger.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblEsikDeger.Location = new System.Drawing.Point(10, 148);
            this.lblEsikDeger.Text = "Eşik Değer (boş=otomatik)";

            // txtEsikDeger
            this.txtEsikDeger.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEsikDeger.Location = new System.Drawing.Point(10, 166);
            this.txtEsikDeger.Name = "txtEsikDeger";
            this.txtEsikDeger.Size = new System.Drawing.Size(230, 26);
            this.txtEsikDeger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // btnUrunEkle
            this.btnUrunEkle.BackColor = System.Drawing.Color.FromArgb(33, 97, 140);
            this.btnUrunEkle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUrunEkle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnUrunEkle.ForeColor = System.Drawing.Color.White;
            this.btnUrunEkle.Location = new System.Drawing.Point(10, 210);
            this.btnUrunEkle.Size = new System.Drawing.Size(230, 38);
            this.btnUrunEkle.Text = "Ürün Ekle";
            this.btnUrunEkle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUrunEkle.Click += new System.EventHandler(this.btnUrunEkle_Click);

            // StokForm
            this.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.ClientSize = new System.Drawing.Size(1040, 510);
            this.Controls.Add(this.lblBaslik);
            this.Controls.Add(this.lblKritikSayisi);
            this.Controls.Add(this.dgvUrunler);
            this.Controls.Add(this.btnYenile);
            this.Controls.Add(this.pnlSagPanel);
            this.Controls.Add(this.pnlUrunEkle);
            this.Name = "StokForm";
            this.Text = "Stok Yönetimi";
            ((System.ComponentModel.ISupportInitialize)(this.dgvUrunler)).EndInit();
            this.pnlSagPanel.ResumeLayout(false);
            this.pnlUrunEkle.ResumeLayout(false);
            this.pnlUrunEkle.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}