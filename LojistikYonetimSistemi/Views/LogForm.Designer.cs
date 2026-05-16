namespace LojistikYonetimSistemi.Views
{
    partial class LogForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblBaslik;
        private System.Windows.Forms.Label lblToplamLog;
        private System.Windows.Forms.Label lblArama;
        private System.Windows.Forms.TextBox txtArama;
        private System.Windows.Forms.ListBox lstLoglar;
        private System.Windows.Forms.Button btnYenile;
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
            this.lblToplamLog = new System.Windows.Forms.Label();
            this.lblArama = new System.Windows.Forms.Label();
            this.txtArama = new System.Windows.Forms.TextBox();
            this.lstLoglar = new System.Windows.Forms.ListBox();
            this.btnYenile = new System.Windows.Forms.Button();
            this.btnTemizle = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // lblBaslik
            this.lblBaslik.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblBaslik.ForeColor = System.Drawing.Color.FromArgb(33, 97, 140);
            this.lblBaslik.Location = new System.Drawing.Point(10, 10);
            this.lblBaslik.Size = new System.Drawing.Size(200, 30);
            this.lblBaslik.Text = "Sistem Logları";

            // lblToplamLog
            this.lblToplamLog.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblToplamLog.ForeColor = System.Drawing.Color.Gray;
            this.lblToplamLog.Location = new System.Drawing.Point(220, 16);
            this.lblToplamLog.Name = "lblToplamLog";
            this.lblToplamLog.Size = new System.Drawing.Size(180, 20);
            this.lblToplamLog.Text = "Toplam: 0 kayıt";

            // lblArama
            this.lblArama.AutoSize = true;
            this.lblArama.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblArama.Location = new System.Drawing.Point(10, 50);
            this.lblArama.Text = "Log Ara:";

            // txtArama
            this.txtArama.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtArama.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtArama.Location = new System.Drawing.Point(70, 47);
            this.txtArama.Name = "txtArama";
            this.txtArama.Size = new System.Drawing.Size(350, 26);
            this.txtArama.TextChanged += new System.EventHandler(this.txtArama_TextChanged);

            // lstLoglar
            this.lstLoglar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstLoglar.Font = new System.Drawing.Font("Consolas", 9F);
            this.lstLoglar.HorizontalScrollbar = true;
            this.lstLoglar.Location = new System.Drawing.Point(10, 85);
            this.lstLoglar.Name = "lstLoglar";
            this.lstLoglar.Size = new System.Drawing.Size(1020, 440);

            // btnYenile
            this.btnYenile.BackColor = System.Drawing.Color.FromArgb(33, 97, 140);
            this.btnYenile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYenile.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnYenile.ForeColor = System.Drawing.Color.White;
            this.btnYenile.Location = new System.Drawing.Point(10, 540);
            this.btnYenile.Size = new System.Drawing.Size(120, 34);
            this.btnYenile.Text = "Yenile";
            this.btnYenile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnYenile.Click += new System.EventHandler(this.btnYenile_Click);

            // btnTemizle
            this.btnTemizle.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.btnTemizle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTemizle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnTemizle.ForeColor = System.Drawing.Color.White;
            this.btnTemizle.Location = new System.Drawing.Point(140, 540);
            this.btnTemizle.Size = new System.Drawing.Size(140, 34);
            this.btnTemizle.Text = "Logları Temizle";
            this.btnTemizle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTemizle.Click += new System.EventHandler(this.btnTemizle_Click);

            // LogForm
            this.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.ClientSize = new System.Drawing.Size(1040, 590);
            this.Controls.Add(this.lblBaslik);
            this.Controls.Add(this.lblToplamLog);
            this.Controls.Add(this.lblArama);
            this.Controls.Add(this.txtArama);
            this.Controls.Add(this.lstLoglar);
            this.Controls.Add(this.btnYenile);
            this.Controls.Add(this.btnTemizle);
            this.Name = "LogForm";
            this.Text = "Sistem Logları";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}