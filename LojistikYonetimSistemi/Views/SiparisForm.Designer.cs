namespace LojistikYonetimSistemi.Views
{
    partial class SiparisForm
    {
        private System.ComponentModel.IContainer components = null;

        // Üst panel — sipariş listesi
        private System.Windows.Forms.Label lblSiparisListesi;
        private System.Windows.Forms.DataGridView dgvSiparisler;
        private System.Windows.Forms.Button btnDurumIlerlet;
        private System.Windows.Forms.Button btnIptalEt;
        private System.Windows.Forms.Button btnYenile;

        // Alt panel — yeni sipariş oluştur
        private System.Windows.Forms.Panel pnlYeniSiparis;
        private System.Windows.Forms.Label lblYeniSiparis;
        private System.Windows.Forms.Label lblUrunler;
        private System.Windows.Forms.ListBox lstUrunler;
        private System.Windows.Forms.Label lblMiktar;
        private System.Windows.Forms.TextBox txtMiktar;
        private System.Windows.Forms.Button btnSepeteEkle;
        private System.Windows.Forms.Label lblSepet;
        private System.Windows.Forms.ListBox lstSepet;
        private System.Windows.Forms.Button btnSepettenCikar;
        private System.Windows.Forms.Label lblOdeme;
        private System.Windows.Forms.ComboBox cmbOdemeYontemi;
        private System.Windows.Forms.Label lblToplam;
        private System.Windows.Forms.Button btnSiparisOlustur;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblSiparisListesi = new System.Windows.Forms.Label();
            this.dgvSiparisler = new System.Windows.Forms.DataGridView();
            this.btnDurumIlerlet = new System.Windows.Forms.Button();
            this.btnIptalEt = new System.Windows.Forms.Button();
            this.btnYenile = new System.Windows.Forms.Button();
            this.pnlYeniSiparis = new System.Windows.Forms.Panel();
            this.lblYeniSiparis = new System.Windows.Forms.Label();
            this.lblUrunler = new System.Windows.Forms.Label();
            this.lstUrunler = new System.Windows.Forms.ListBox();
            this.lblMiktar = new System.Windows.Forms.Label();
            this.txtMiktar = new System.Windows.Forms.TextBox();
            this.btnSepeteEkle = new System.Windows.Forms.Button();
            this.lblSepet = new System.Windows.Forms.Label();
            this.lstSepet = new System.Windows.Forms.ListBox();
            this.btnSepettenCikar = new System.Windows.Forms.Button();
            this.lblOdeme = new System.Windows.Forms.Label();
            this.cmbOdemeYontemi = new System.Windows.Forms.ComboBox();
            this.lblToplam = new System.Windows.Forms.Label();
            this.btnSiparisOlustur = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSiparisler)).BeginInit();
            this.pnlYeniSiparis.SuspendLayout();
            this.SuspendLayout();

            // lblSiparisListesi
            this.lblSiparisListesi.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblSiparisListesi.ForeColor = System.Drawing.Color.FromArgb(33, 97, 140);
            this.lblSiparisListesi.Location = new System.Drawing.Point(10, 10);
            this.lblSiparisListesi.Size = new System.Drawing.Size(200, 28);
            this.lblSiparisListesi.Text = "Sipariş Listesi";

            // dgvSiparisler
            this.dgvSiparisler.AllowUserToAddRows = false;
            this.dgvSiparisler.AllowUserToDeleteRows = false;
            this.dgvSiparisler.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSiparisler.BackgroundColor = System.Drawing.Color.White;
            this.dgvSiparisler.ColumnCount = 7;
            this.dgvSiparisler.Columns[0].Name = "colId";
            this.dgvSiparisler.Columns[0].HeaderText = "ID";
            this.dgvSiparisler.Columns[1].Name = "colTarih";
            this.dgvSiparisler.Columns[1].HeaderText = "Tarih";
            this.dgvSiparisler.Columns[2].Name = "colDurum";
            this.dgvSiparisler.Columns[2].HeaderText = "Durum";
            this.dgvSiparisler.Columns[3].Name = "colTutar";
            this.dgvSiparisler.Columns[3].HeaderText = "Tutar";
            this.dgvSiparisler.Columns[4].Name = "colOdeme";
            this.dgvSiparisler.Columns[4].HeaderText = "Ödeme";
            this.dgvSiparisler.Columns[5].Name = "colKargoFirmasi";
            this.dgvSiparisler.Columns[5].HeaderText = "Kargo Firması";
            this.dgvSiparisler.Columns[6].Name = "colTakipNo";
            this.dgvSiparisler.Columns[6].HeaderText = "Takip No";
            this.dgvSiparisler.Location = new System.Drawing.Point(10, 45);
            this.dgvSiparisler.MultiSelect = false;
            this.dgvSiparisler.Name = "dgvSiparisler";
            this.dgvSiparisler.ReadOnly = true;
            this.dgvSiparisler.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSiparisler.Size = new System.Drawing.Size(1040, 220);

            // btnDurumIlerlet
            this.btnDurumIlerlet.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnDurumIlerlet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDurumIlerlet.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnDurumIlerlet.ForeColor = System.Drawing.Color.White;
            this.btnDurumIlerlet.Location = new System.Drawing.Point(10, 275);
            this.btnDurumIlerlet.Size = new System.Drawing.Size(160, 34);
            this.btnDurumIlerlet.Text = "Durumu İlerlet";
            this.btnDurumIlerlet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDurumIlerlet.Click += new System.EventHandler(this.btnDurumIlerlet_Click);

            // btnIptalEt
            this.btnIptalEt.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.btnIptalEt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIptalEt.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnIptalEt.ForeColor = System.Drawing.Color.White;
            this.btnIptalEt.Location = new System.Drawing.Point(180, 275);
            this.btnIptalEt.Size = new System.Drawing.Size(120, 34);
            this.btnIptalEt.Text = "İptal Et";
            this.btnIptalEt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIptalEt.Click += new System.EventHandler(this.btnIptalEt_Click);

            // btnYenile
            this.btnYenile.BackColor = System.Drawing.Color.FromArgb(33, 97, 140);
            this.btnYenile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYenile.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnYenile.ForeColor = System.Drawing.Color.White;
            this.btnYenile.Location = new System.Drawing.Point(310, 275);
            this.btnYenile.Size = new System.Drawing.Size(100, 34);
            this.btnYenile.Text = "Yenile";
            this.btnYenile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnYenile.Click += new System.EventHandler(this.btnYenile_Click);

            // pnlYeniSiparis
            this.pnlYeniSiparis.BackColor = System.Drawing.Color.White;
            this.pnlYeniSiparis.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlYeniSiparis.Controls.Add(this.lblYeniSiparis);
            this.pnlYeniSiparis.Controls.Add(this.lblUrunler);
            this.pnlYeniSiparis.Controls.Add(this.lstUrunler);
            this.pnlYeniSiparis.Controls.Add(this.lblMiktar);
            this.pnlYeniSiparis.Controls.Add(this.txtMiktar);
            this.pnlYeniSiparis.Controls.Add(this.btnSepeteEkle);
            this.pnlYeniSiparis.Controls.Add(this.lblSepet);
            this.pnlYeniSiparis.Controls.Add(this.lstSepet);
            this.pnlYeniSiparis.Controls.Add(this.btnSepettenCikar);
            this.pnlYeniSiparis.Controls.Add(this.lblOdeme);
            this.pnlYeniSiparis.Controls.Add(this.cmbOdemeYontemi);
            this.pnlYeniSiparis.Controls.Add(this.lblToplam);
            this.pnlYeniSiparis.Controls.Add(this.btnSiparisOlustur);
            this.pnlYeniSiparis.Location = new System.Drawing.Point(10, 320);
            this.pnlYeniSiparis.Name = "pnlYeniSiparis";
            this.pnlYeniSiparis.Size = new System.Drawing.Size(1040, 280);

            // lblYeniSiparis
            this.lblYeniSiparis.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblYeniSiparis.ForeColor = System.Drawing.Color.FromArgb(33, 97, 140);
            this.lblYeniSiparis.Location = new System.Drawing.Point(10, 10);
            this.lblYeniSiparis.Size = new System.Drawing.Size(220, 28);
            this.lblYeniSiparis.Text = "Yeni Sipariş Oluştur";

            // lblUrunler
            this.lblUrunler.AutoSize = true;
            this.lblUrunler.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUrunler.Location = new System.Drawing.Point(10, 50);
            this.lblUrunler.Text = "Ürünler";

            // lstUrunler
            this.lstUrunler.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lstUrunler.Location = new System.Drawing.Point(10, 70);
            this.lstUrunler.Name = "lstUrunler";
            this.lstUrunler.Size = new System.Drawing.Size(300, 160);

            // lblMiktar
            this.lblMiktar.AutoSize = true;
            this.lblMiktar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMiktar.Location = new System.Drawing.Point(320, 50);
            this.lblMiktar.Text = "Miktar";

            // txtMiktar
            this.txtMiktar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMiktar.Location = new System.Drawing.Point(320, 70);
            this.txtMiktar.Name = "txtMiktar";
            this.txtMiktar.Size = new System.Drawing.Size(80, 26);
            this.txtMiktar.Text = "1";

            // btnSepeteEkle
            this.btnSepeteEkle.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnSepeteEkle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSepeteEkle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSepeteEkle.ForeColor = System.Drawing.Color.White;
            this.btnSepeteEkle.Location = new System.Drawing.Point(320, 106);
            this.btnSepeteEkle.Size = new System.Drawing.Size(120, 34);
            this.btnSepeteEkle.Text = "Sepete Ekle";
            this.btnSepeteEkle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSepeteEkle.Click += new System.EventHandler(this.btnSepeteEkle_Click);

            // lblSepet
            this.lblSepet.AutoSize = true;
            this.lblSepet.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSepet.Location = new System.Drawing.Point(460, 50);
            this.lblSepet.Text = "Sepet";

            // lstSepet
            this.lstSepet.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lstSepet.Location = new System.Drawing.Point(460, 70);
            this.lstSepet.Name = "lstSepet";
            this.lstSepet.Size = new System.Drawing.Size(340, 160);

            // btnSepettenCikar
            this.btnSepettenCikar.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.btnSepettenCikar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSepettenCikar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSepettenCikar.ForeColor = System.Drawing.Color.White;
            this.btnSepettenCikar.Location = new System.Drawing.Point(460, 240);
            this.btnSepettenCikar.Size = new System.Drawing.Size(140, 30);
            this.btnSepettenCikar.Text = "Sepetten Çıkar";
            this.btnSepettenCikar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSepettenCikar.Click += new System.EventHandler(this.btnSepettenCikar_Click);

            // lblOdeme
            this.lblOdeme.AutoSize = true;
            this.lblOdeme.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblOdeme.Location = new System.Drawing.Point(820, 50);
            this.lblOdeme.Text = "Ödeme Yöntemi";

            // cmbOdemeYontemi
            this.cmbOdemeYontemi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOdemeYontemi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbOdemeYontemi.Location = new System.Drawing.Point(820, 70);
            this.cmbOdemeYontemi.Name = "cmbOdemeYontemi";
            this.cmbOdemeYontemi.Size = new System.Drawing.Size(200, 26);

            // lblToplam
            this.lblToplam.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblToplam.ForeColor = System.Drawing.Color.FromArgb(33, 97, 140);
            this.lblToplam.Location = new System.Drawing.Point(820, 120);
            this.lblToplam.Size = new System.Drawing.Size(200, 28);
            this.lblToplam.Name = "lblToplam";
            this.lblToplam.Text = "Toplam: 0,00 TL";

            // btnSiparisOlustur
            this.btnSiparisOlustur.BackColor = System.Drawing.Color.FromArgb(33, 97, 140);
            this.btnSiparisOlustur.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSiparisOlustur.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSiparisOlustur.ForeColor = System.Drawing.Color.White;
            this.btnSiparisOlustur.Location = new System.Drawing.Point(820, 200);
            this.btnSiparisOlustur.Size = new System.Drawing.Size(200, 40);
            this.btnSiparisOlustur.Text = "Siparişi Tamamla";
            this.btnSiparisOlustur.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSiparisOlustur.Click += new System.EventHandler(this.btnSiparisOlustur_Click);

            // SiparisForm
            this.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.ClientSize = new System.Drawing.Size(1060, 620);
            this.Controls.Add(this.lblSiparisListesi);
            this.Controls.Add(this.dgvSiparisler);
            this.Controls.Add(this.btnDurumIlerlet);
            this.Controls.Add(this.btnIptalEt);
            this.Controls.Add(this.btnYenile);
            this.Controls.Add(this.pnlYeniSiparis);
            this.Name = "SiparisForm";
            this.Text = "Sipariş Yönetimi";
            ((System.ComponentModel.ISupportInitialize)(this.dgvSiparisler)).EndInit();
            this.pnlYeniSiparis.ResumeLayout(false);
            this.pnlYeniSiparis.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}