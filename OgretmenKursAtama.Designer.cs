namespace AnaKodYazılımAkademisi
{
    partial class OgretmenKursAtama
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cmb_Egitmenler = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_Kurslar = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_update = new System.Windows.Forms.Button();
            this.btn_del = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_OgrenciGoster = new System.Windows.Forms.Button();
            this.btn_OgretmenGoster = new System.Windows.Forms.Button();
            this.btn_kursEklemeGoster = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(48, 118);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Honeydew;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Teal;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(850, 320);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // cmb_Egitmenler
            // 
            this.cmb_Egitmenler.FormattingEnabled = true;
            this.cmb_Egitmenler.Location = new System.Drawing.Point(148, 485);
            this.cmb_Egitmenler.Name = "cmb_Egitmenler";
            this.cmb_Egitmenler.Size = new System.Drawing.Size(315, 31);
            this.cmb_Egitmenler.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(44, 493);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Eğitmenler";
            // 
            // cmb_Kurslar
            // 
            this.cmb_Kurslar.FormattingEnabled = true;
            this.cmb_Kurslar.Location = new System.Drawing.Point(583, 485);
            this.cmb_Kurslar.Name = "cmb_Kurslar";
            this.cmb_Kurslar.Size = new System.Drawing.Size(315, 31);
            this.cmb_Kurslar.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Location = new System.Drawing.Point(499, 493);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 23);
            this.label2.TabIndex = 4;
            this.label2.Text = "Kurslar";
            // 
            // btn_update
            // 
            this.btn_update.BackColor = System.Drawing.Color.Gold;
            this.btn_update.Location = new System.Drawing.Point(806, 588);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(92, 30);
            this.btn_update.TabIndex = 15;
            this.btn_update.Text = "Güncelle";
            this.btn_update.UseVisualStyleBackColor = false;
            this.btn_update.Click += new System.EventHandler(this.btn_update_Click);
            // 
            // btn_del
            // 
            this.btn_del.BackColor = System.Drawing.Color.Crimson;
            this.btn_del.Location = new System.Drawing.Point(699, 588);
            this.btn_del.Name = "btn_del";
            this.btn_del.Size = new System.Drawing.Size(92, 30);
            this.btn_del.TabIndex = 14;
            this.btn_del.Text = "Sil";
            this.btn_del.UseVisualStyleBackColor = false;
            this.btn_del.Click += new System.EventHandler(this.btn_del_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.BackColor = System.Drawing.Color.LimeGreen;
            this.btn_Add.Location = new System.Drawing.Point(590, 588);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(92, 30);
            this.btn_Add.TabIndex = 13;
            this.btn_Add.Text = "Ekle";
            this.btn_Add.UseVisualStyleBackColor = false;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(158, 595);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 30);
            this.dateTimePicker1.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label4.Location = new System.Drawing.Point(44, 601);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 23);
            this.label4.TabIndex = 18;
            this.label4.Text = "Kayıt Tarihi";
            // 
            // btn_OgrenciGoster
            // 
            this.btn_OgrenciGoster.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_OgrenciGoster.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_OgrenciGoster.Location = new System.Drawing.Point(336, 39);
            this.btn_OgrenciGoster.Name = "btn_OgrenciGoster";
            this.btn_OgrenciGoster.Size = new System.Drawing.Size(181, 37);
            this.btn_OgrenciGoster.TabIndex = 21;
            this.btn_OgrenciGoster.Text = "Öğrenci Kurs Kayıt";
            this.btn_OgrenciGoster.UseVisualStyleBackColor = false;
            this.btn_OgrenciGoster.Click += new System.EventHandler(this.btn_OgrenciGoster_Click);
            // 
            // btn_OgretmenGoster
            // 
            this.btn_OgretmenGoster.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_OgretmenGoster.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_OgretmenGoster.Location = new System.Drawing.Point(158, 39);
            this.btn_OgretmenGoster.Name = "btn_OgretmenGoster";
            this.btn_OgretmenGoster.Size = new System.Drawing.Size(172, 37);
            this.btn_OgretmenGoster.TabIndex = 20;
            this.btn_OgretmenGoster.Text = "Eğitmen Kurs Kayıt";
            this.btn_OgretmenGoster.UseVisualStyleBackColor = false;
            // 
            // btn_kursEklemeGoster
            // 
            this.btn_kursEklemeGoster.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_kursEklemeGoster.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_kursEklemeGoster.Location = new System.Drawing.Point(51, 39);
            this.btn_kursEklemeGoster.Name = "btn_kursEklemeGoster";
            this.btn_kursEklemeGoster.Size = new System.Drawing.Size(101, 37);
            this.btn_kursEklemeGoster.TabIndex = 19;
            this.btn_kursEklemeGoster.Text = "Kurs ekleme";
            this.btn_kursEklemeGoster.UseVisualStyleBackColor = false;
            this.btn_kursEklemeGoster.Click += new System.EventHandler(this.btn_kursEklemeGoster_Click);
            // 
            // OgretmenKursAtama
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.ClientSize = new System.Drawing.Size(1045, 781);
            this.Controls.Add(this.btn_OgrenciGoster);
            this.Controls.Add(this.btn_OgretmenGoster);
            this.Controls.Add(this.btn_kursEklemeGoster);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.btn_update);
            this.Controls.Add(this.btn_del);
            this.Controls.Add(this.btn_Add);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmb_Kurslar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmb_Egitmenler);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "OgretmenKursAtama";
            this.Text = "OgretmenKursAtama";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cmb_Egitmenler;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_Kurslar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_update;
        private System.Windows.Forms.Button btn_del;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_OgrenciGoster;
        private System.Windows.Forms.Button btn_OgretmenGoster;
        private System.Windows.Forms.Button btn_kursEklemeGoster;
    }
}