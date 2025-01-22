namespace AnaKodYazılımAkademisi
{
    partial class KursEkleme
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KursEkleme));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txt_Search = new System.Windows.Forms.TextBox();
            this.btn_Ara = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_CourseType = new System.Windows.Forms.TextBox();
            this.txt_Kapasite = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btn_Upd = new System.Windows.Forms.Button();
            this.btn_Del = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.txt_Acıklama = new System.Windows.Forms.TextBox();
            this.txt_KursAd = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_kursEklemeGoster = new System.Windows.Forms.Button();
            this.btn_OgretmenGoster = new System.Windows.Forms.Button();
            this.btn_OgrenciGoster = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.txt_Search);
            this.panel1.Controls.Add(this.btn_Ara);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Location = new System.Drawing.Point(13, 59);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1020, 398);
            this.panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(952, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(52, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // txt_Search
            // 
            this.txt_Search.Location = new System.Drawing.Point(745, 130);
            this.txt_Search.Name = "txt_Search";
            this.txt_Search.Size = new System.Drawing.Size(161, 27);
            this.txt_Search.TabIndex = 2;
            this.txt_Search.Text = " ";
            // 
            // btn_Ara
            // 
            this.btn_Ara.BackColor = System.Drawing.Color.Gold;
            this.btn_Ara.Location = new System.Drawing.Point(912, 128);
            this.btn_Ara.Name = "btn_Ara";
            this.btn_Ara.Size = new System.Drawing.Size(92, 30);
            this.btn_Ara.TabIndex = 1;
            this.btn_Ara.Text = "ARA";
            this.btn_Ara.UseVisualStyleBackColor = false;
            this.btn_Ara.Click += new System.EventHandler(this.btn_Ara_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dataGridView1.Location = new System.Drawing.Point(26, 50);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Honeydew;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Teal;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(713, 314);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Info;
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.txt_CourseType);
            this.panel2.Controls.Add(this.txt_Kapasite);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.comboBox1);
            this.panel2.Controls.Add(this.btn_Upd);
            this.panel2.Controls.Add(this.btn_Del);
            this.panel2.Controls.Add(this.btn_Add);
            this.panel2.Controls.Add(this.txt_Acıklama);
            this.panel2.Controls.Add(this.txt_KursAd);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.dateTimePicker1);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.numericUpDown1);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(13, 463);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1020, 306);
            this.panel2.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(296, 63);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 20);
            this.label7.TabIndex = 17;
            this.label7.Text = "Kurs Türü";
            // 
            // txt_CourseType
            // 
            this.txt_CourseType.Location = new System.Drawing.Point(383, 56);
            this.txt_CourseType.Name = "txt_CourseType";
            this.txt_CourseType.Size = new System.Drawing.Size(181, 27);
            this.txt_CourseType.TabIndex = 16;
            // 
            // txt_Kapasite
            // 
            this.txt_Kapasite.Location = new System.Drawing.Point(383, 14);
            this.txt_Kapasite.Name = "txt_Kapasite";
            this.txt_Kapasite.Size = new System.Drawing.Size(42, 27);
            this.txt_Kapasite.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(294, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 20);
            this.label6.TabIndex = 14;
            this.label6.Text = "Kapasite";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.comboBox1.Location = new System.Drawing.Point(130, 59);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(149, 28);
            this.comboBox1.TabIndex = 13;
            // 
            // btn_Upd
            // 
            this.btn_Upd.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btn_Upd.Location = new System.Drawing.Point(711, 207);
            this.btn_Upd.Name = "btn_Upd";
            this.btn_Upd.Size = new System.Drawing.Size(92, 30);
            this.btn_Upd.TabIndex = 12;
            this.btn_Upd.Text = "Güncelle";
            this.btn_Upd.UseVisualStyleBackColor = false;
            this.btn_Upd.Click += new System.EventHandler(this.btn_Upd_Click);
            // 
            // btn_Del
            // 
            this.btn_Del.BackColor = System.Drawing.Color.Crimson;
            this.btn_Del.Location = new System.Drawing.Point(604, 207);
            this.btn_Del.Name = "btn_Del";
            this.btn_Del.Size = new System.Drawing.Size(92, 30);
            this.btn_Del.TabIndex = 11;
            this.btn_Del.Text = "Sil";
            this.btn_Del.UseVisualStyleBackColor = false;
            this.btn_Del.Click += new System.EventHandler(this.btn_Del_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.BackColor = System.Drawing.Color.LimeGreen;
            this.btn_Add.Location = new System.Drawing.Point(495, 207);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(92, 30);
            this.btn_Add.TabIndex = 10;
            this.btn_Add.Text = "Ekle";
            this.btn_Add.UseVisualStyleBackColor = false;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // txt_Acıklama
            // 
            this.txt_Acıklama.Location = new System.Drawing.Point(130, 95);
            this.txt_Acıklama.Multiline = true;
            this.txt_Acıklama.Name = "txt_Acıklama";
            this.txt_Acıklama.Size = new System.Drawing.Size(295, 75);
            this.txt_Acıklama.TabIndex = 9;
            // 
            // txt_KursAd
            // 
            this.txt_KursAd.Location = new System.Drawing.Point(130, 14);
            this.txt_KursAd.Name = "txt_KursAd";
            this.txt_KursAd.Size = new System.Drawing.Size(100, 27);
            this.txt_KursAd.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 239);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "Kurs Tarihi";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(130, 232);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 27);
            this.dateTimePicker1.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 176);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Kurs Saati";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(130, 176);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 27);
            this.numericUpDown1.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Açıklama";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Kurs Seviyesi";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Kurs Adı";
            // 
            // btn_kursEklemeGoster
            // 
            this.btn_kursEklemeGoster.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_kursEklemeGoster.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_kursEklemeGoster.Location = new System.Drawing.Point(13, 12);
            this.btn_kursEklemeGoster.Name = "btn_kursEklemeGoster";
            this.btn_kursEklemeGoster.Size = new System.Drawing.Size(101, 29);
            this.btn_kursEklemeGoster.TabIndex = 2;
            this.btn_kursEklemeGoster.Text = "Kurs ekleme";
            this.btn_kursEklemeGoster.UseVisualStyleBackColor = false;
            this.btn_kursEklemeGoster.Click += new System.EventHandler(this.btn_kursEklemeGoster_Click);
            // 
            // btn_OgretmenGoster
            // 
            this.btn_OgretmenGoster.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_OgretmenGoster.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_OgretmenGoster.Location = new System.Drawing.Point(120, 12);
            this.btn_OgretmenGoster.Name = "btn_OgretmenGoster";
            this.btn_OgretmenGoster.Size = new System.Drawing.Size(172, 29);
            this.btn_OgretmenGoster.TabIndex = 3;
            this.btn_OgretmenGoster.Text = "Eğitmen Kurs Kayıt";
            this.btn_OgretmenGoster.UseVisualStyleBackColor = false;
            this.btn_OgretmenGoster.Click += new System.EventHandler(this.btn_OgretmenGoster_Click);
            // 
            // btn_OgrenciGoster
            // 
            this.btn_OgrenciGoster.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_OgrenciGoster.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_OgrenciGoster.Location = new System.Drawing.Point(298, 12);
            this.btn_OgrenciGoster.Name = "btn_OgrenciGoster";
            this.btn_OgrenciGoster.Size = new System.Drawing.Size(181, 29);
            this.btn_OgrenciGoster.TabIndex = 4;
            this.btn_OgrenciGoster.Text = "Öğrenci Kurs Kayıt";
            this.btn_OgrenciGoster.UseVisualStyleBackColor = false;
            this.btn_OgrenciGoster.Click += new System.EventHandler(this.btn_OgrenciGoster_Click);
            // 
            // KursEkleme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Highlight;
            this.ClientSize = new System.Drawing.Size(1045, 781);
            this.Controls.Add(this.btn_OgrenciGoster);
            this.Controls.Add(this.btn_OgretmenGoster);
            this.Controls.Add(this.btn_kursEklemeGoster);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "KursEkleme";
            this.Text = "KursEkleme";
            this.Load += new System.EventHandler(this.KursEkleme_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txt_Search;
        private System.Windows.Forms.Button btn_Ara;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Upd;
        private System.Windows.Forms.Button btn_Del;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.TextBox txt_Acıklama;
        private System.Windows.Forms.TextBox txt_KursAd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_Kapasite;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_kursEklemeGoster;
        private System.Windows.Forms.Button btn_OgretmenGoster;
        private System.Windows.Forms.Button btn_OgrenciGoster;
        private System.Windows.Forms.TextBox txt_CourseType;
        private System.Windows.Forms.Label label7;
    }
}