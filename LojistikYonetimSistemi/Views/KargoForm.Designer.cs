namespace LojistikYonetimSistemi.Views
{
    partial class KargoForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblBaslik;
        private System.Windows.Forms.DataGridView dgvSiparisler;
        private System.Windows.Forms.Button btnDurumSorgula;
        private System.Windows.Forms.Button btnTeslimEdildi;
        private System.Windows.Forms.Button btnYenile;
        private System.Windows.Forms.Panel pnlKargoAta;
        private System.Windows.Forms.Label lblKargoAta;
        private System.Windows.Forms.Label lblFirma;
        private System.Windows.Forms.ComboBox cmbFirma;
        private System.Windows.Forms.Label lblAgirlik;
        private System.Windows.Forms.TextBox txtAgirlik;
        private System.Windows.Forms.Label lblMesafe;
        private System.Windows.Forms.TextBox txtMesafe;
        private System.Windows.Forms.CheckBox chkSigorta;
        private System.Windows.Forms.Label lblUrunDegeri;
        private System.Windows.Forms.TextBox txtUrunDegeri;
        private System.Windows.Forms.CheckBox chkKirilgan;
        private System.Windows.Forms.Label lblFiyatOnizleme;
        private System.Windows.Forms.Button btnKargoAta;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblBaslik = new System.Windows.Forms.Label();
            this.dgvSiparisler = new System.Windows.Forms.DataGridView();
            this.btnDurumSorgula = new System.Windows.Forms.Button();
            this.btnTeslimEdildi = new System.Windows.Forms.Button();
            this.btnYenile = new System.Windows.Forms.Button();
            this.pnlKargoAta = new System.Windows.Forms.Panel();
            this.lblKargoAta = new System.Windows.Forms.Label();
            this.lblFirma = new System.Windows.Forms.Label();
            this.cmbFirma = new System.Windows.Forms.ComboBox();
            this.lblAgirlik = new System.Windows.Forms.Label();
            this.txtAgirlik = new System.Windows.Forms.TextBox();
            this.lblMesafe = new System.Windows.Forms.Label();
            this.txtMesafe = new System.Windows.Forms.TextBox();
            this.chkSigorta = new System.Windows.Forms.CheckBox();
            this.lblUrunDegeri = new System.Windows.Forms.Label();
            this.txtUrunDegeri = new System.Windows.Forms.TextBox();
            this.chkKirilgan = new System.Windows.Forms.CheckBox();
            this.lblFiyatOnizleme = new System.Windows.Forms.Label();
            this.btnKargoAta = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSiparisler)).BeginInit();
            this.pnlKargoAta.SuspendLayout();
            this.SuspendLayout();

            // lblBaslik
            this.lblBaslik.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblBaslik.ForeColor = System.Drawing.Color.FromArgb(33, 97, 140);
            this.lblBaslik.Location = new System.Drawing.Point(10, 10);
            this.lblBaslik.Size = new System.Drawing.Size(200, 30);
            this.lblBaslik.Text = "Kargo Yönetimi";

            // dgvSiparisler
            this.dgvSiparisler.AllowUserToAddRows = false;
            this.dgvSiparisler.AllowUserToDeleteRows = false;
            this.dgvSiparisler.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSiparisler.BackgroundColor = System.Drawing.Color.White;
            this.dgvSiparisler.ColumnCount = 6;
            this.dgvSiparisler.Columns[0].Name = "colId";
            this.dgvSiparisler.Columns[0].HeaderText = "ID";
            this.dgvSiparisler.Columns[0].FillWeight = 40;
            this.dgvSiparisler.Columns[1].Name = "colTarih";
            this.dgvSiparisler.Columns[1].HeaderText = "Tarih";
            this.dgvSiparisler.Columns[1].FillWeight = 120;
            this.dgvSiparisler.Columns[2].Name = "colDurum";
            this.dgvSiparisler.Columns[2].HeaderText = "Durum";
            this.dgvSiparisler.Columns[2].FillWeight = 100;
            this.dgvSiparisler.Columns[3].Name = "colTutar";
            this.dgvSiparisler.Columns[3].HeaderText = "Tutar";
            this.dgvSiparisler.Columns[3].FillWeight = 80;
            this.dgvSiparisler.Columns[4].Name = "colFirma";
            this.dgvSiparisler.Columns[4].HeaderText = "Kargo Firması";
            this.dgvSiparisler.Columns[4].FillWeight = 100;
            this.dgvSiparisler.Columns[5].Name = "colTakip";
            this.dgvSiparisler.Columns[5].HeaderText = "Takip No";
            this.dgvSiparisler.Columns[5].FillWeight = 120;
            this.dgvSiparisler.Location = new System.Drawing.Point(10, 50);
            this.dgvSiparisler.MultiSelect = false;
            this.dgvSiparisler.Name = "dgvSiparisler";
            this.dgvSiparisler.ReadOnly = true;
            this.dgvSiparisler.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSiparisler.Size = new System.Drawing.Size(740, 300);
            this.dgvSiparisler.SelectionChanged += new System.EventHandler(this.dgvSiparisler_SelectionChanged);

            // btnDurumSorgula
            this.btnDurumSorgula.BackColor = System.Drawing.Color.FromArgb(33, 97, 140);
            this.btnDurumSorgula.Enabled = false;
            this.btnDurumSorgula.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDurumSorgula.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnDurumSorgula.ForeColor = System.Drawing.Color.White;
            this.btnDurumSorgula.Location = new System.Drawing.Point(10, 360);
            this.btnDurumSorgula.Size = new System.Drawing.Size(160, 34);
            this.btnDurumSorgula.Text = "Kargo Durumu Sorgula";
            this.btnDurumSorgula.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDurumSorgula.Click += new System.EventHandler(this.btnDurumSorgula_Click);

            // btnTeslimEdildi
            this.btnTeslimEdildi.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnTeslimEdildi.Enabled = false;
            this.btnTeslimEdildi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTeslimEdildi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnTeslimEdildi.ForeColor = System.Drawing.Color.White;
            this.btnTeslimEdildi.Location = new System.Drawing.Point(180, 360);
            this.btnTeslimEdildi.Size = new System.Drawing.Size(160, 34);
            this.btnTeslimEdildi.Text = "Teslim Edildi";
            this.btnTeslimEdildi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTeslimEdildi.Click += new System.EventHandler(this.btnTeslimEdildi_Click);

            // btnYenile
            this.btnYenile.BackColor = System.Drawing.Color.Gray;
            this.btnYenile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYenile.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnYenile.ForeColor = System.Drawing.Color.White;
            this.btnYenile.Location = new System.Drawing.Point(350, 360);
            this.btnYenile.Size = new System.Drawing.Size(100, 34);
            this.btnYenile.Text = "Yenile";
            this.btnYenile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnYenile.Click += new System.EventHandler(this.btnYenile_Click);

            // pnlKargoAta
            this.pnlKargoAta.BackColor = System.Drawing.Color.White;
            this.pnlKargoAta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlKargoAta.Controls.Add(this.lblKargoAta);
            this.pnlKargoAta.Controls.Add(this.lblFirma);
            this.pnlKargoAta.Controls.Add(this.cmbFirma);
            this.pnlKargoAta.Controls.Add(this.lblAgirlik);
            this.pnlKargoAta.Controls.Add(this.txtAgirlik);
            this.pnlKargoAta.Controls.Add(this.lblMesafe);
            this.pnlKargoAta.Controls.Add(this.txtMesafe);
            this.pnlKargoAta.Controls.Add(this.chkSigorta);
            this.pnlKargoAta.Controls.Add(this.lblUrunDegeri);
            this.pnlKargoAta.Controls.Add(this.txtUrunDegeri);
            this.pnlKargoAta.Controls.Add(this.chkKirilgan);
            this.pnlKargoAta.Controls.Add(this.lblFiyatOnizleme);
            this.pnlKargoAta.Controls.Add(this.btnKargoAta);
            this.pnlKargoAta.Location = new System.Drawing.Point(765, 50);
            this.pnlKargoAta.Name = "pnlKargoAta";
            this.pnlKargoAta.Size = new System.Drawing.Size(270, 400);

            // lblKargoAta
            this.lblKargoAta.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblKargoAta.ForeColor = System.Drawing.Color.FromArgb(33, 97, 140);
            this.lblKargoAta.Location = new System.Drawing.Point(10, 10);
            this.lblKargoAta.Size = new System.Drawing.Size(240, 22);
            this.lblKargoAta.Text = "Kargo Ata";

            // lblFirma
            this.lblFirma.AutoSize = true;
            this.lblFirma.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFirma.Location = new System.Drawing.Point(10, 42);
            this.lblFirma.Text = "Kargo Firması";

            // cmbFirma
            this.cmbFirma.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFirma.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbFirma.Location = new System.Drawing.Point(10, 60);
            this.cmbFirma.Name = "cmbFirma";
            this.cmbFirma.Size = new System.Drawing.Size(245, 26);

            // lblAgirlik
            this.lblAgirlik.AutoSize = true;
            this.lblAgirlik.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblAgirlik.Location = new System.Drawing.Point(10, 96);
            this.lblAgirlik.Text = "Ağırlık (kg)";

            // txtAgirlik
            this.txtAgirlik.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtAgirlik.Location = new System.Drawing.Point(10, 114);
            this.txtAgirlik.Name = "txtAgirlik";
            this.txtAgirlik.Size = new System.Drawing.Size(115, 26);
            this.txtAgirlik.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // lblMesafe
            this.lblMesafe.AutoSize = true;
            this.lblMesafe.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMesafe.Location = new System.Drawing.Point(135, 96);
            this.lblMesafe.Text = "Mesafe (km)";

            // txtMesafe
            this.txtMesafe.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMesafe.Location = new System.Drawing.Point(135, 114);
            this.txtMesafe.Name = "txtMesafe";
            this.txtMesafe.Size = new System.Drawing.Size(115, 26);
            this.txtMesafe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // chkSigorta
            this.chkSigorta.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkSigorta.Location = new System.Drawing.Point(10, 150);
            this.chkSigorta.Name = "chkSigorta";
            this.chkSigorta.Size = new System.Drawing.Size(130, 22);
            this.chkSigorta.Text = "Sigortalı Gönderim";

            // lblUrunDegeri
            this.lblUrunDegeri.AutoSize = true;
            this.lblUrunDegeri.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUrunDegeri.Location = new System.Drawing.Point(10, 180);
            this.lblUrunDegeri.Text = "Ürün Değeri (sigorta için)";

            // txtUrunDegeri
            this.txtUrunDegeri.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUrunDegeri.Location = new System.Drawing.Point(10, 198);
            this.txtUrunDegeri.Name = "txtUrunDegeri";
            this.txtUrunDegeri.Size = new System.Drawing.Size(245, 26);
            this.txtUrunDegeri.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // chkKirilgan
            this.chkKirilgan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkKirilgan.Location = new System.Drawing.Point(10, 232);
            this.chkKirilgan.Name = "chkKirilgan";
            this.chkKirilgan.Size = new System.Drawing.Size(160, 22);
            this.chkKirilgan.Text = "Kırılgan Koruma (+25 TL)";

            // lblFiyatOnizleme
            this.lblFiyatOnizleme.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFiyatOnizleme.ForeColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.lblFiyatOnizleme.Location = new System.Drawing.Point(10, 265);
            this.lblFiyatOnizleme.Name = "lblFiyatOnizleme";
            this.lblFiyatOnizleme.Size = new System.Drawing.Size(245, 22);
            this.lblFiyatOnizleme.Text = "Tahmini Kargo Ücreti: —";

            // btnKargoAta
            this.btnKargoAta.BackColor = System.Drawing.Color.FromArgb(33, 97, 140);
            this.btnKargoAta.Enabled = false;
            this.btnKargoAta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKargoAta.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnKargoAta.ForeColor = System.Drawing.Color.White;
            this.btnKargoAta.Location = new System.Drawing.Point(10, 310);
            this.btnKargoAta.Name = "btnKargoAta";
            this.btnKargoAta.Size = new System.Drawing.Size(245, 40);
            this.btnKargoAta.Text = "Kargo Ata";
            this.btnKargoAta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKargoAta.Click += new System.EventHandler(this.btnKargoAta_Click);

            // KargoForm
            this.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.ClientSize = new System.Drawing.Size(1050, 420);
            this.Controls.Add(this.lblBaslik);
            this.Controls.Add(this.dgvSiparisler);
            this.Controls.Add(this.btnDurumSorgula);
            this.Controls.Add(this.btnTeslimEdildi);
            this.Controls.Add(this.btnYenile);
            this.Controls.Add(this.pnlKargoAta);
            this.Name = "KargoForm";
            this.Text = "Kargo Yönetimi";
            ((System.ComponentModel.ISupportInitialize)(this.dgvSiparisler)).EndInit();
            this.pnlKargoAta.ResumeLayout(false);
            this.pnlKargoAta.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}