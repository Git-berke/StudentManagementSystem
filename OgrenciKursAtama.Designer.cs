namespace AnaKodYazılımAkademisi
{
    partial class OgrenciKursAtama
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
            this.btn_del = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.txt_Search = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_MevcutKurslarıGoruntule = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox_Status = new System.Windows.Forms.ComboBox();
            this.btn_OgrenciGoster = new System.Windows.Forms.Button();
            this.btn_OgretmenGoster = new System.Windows.Forms.Button();
            this.btn_kursEklemeGoster = new System.Windows.Forms.Button();
            this.comboBox_KursAtamaları = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_PaymentAmount = new System.Windows.Forms.TextBox();
            this.cmb_PaymentType = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmb_InstallmentCount = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_del
            // 
            this.btn_del.BackColor = System.Drawing.Color.Crimson;
            this.btn_del.Location = new System.Drawing.Point(708, 677);
            this.btn_del.Name = "btn_del";
            this.btn_del.Size = new System.Drawing.Size(92, 30);
            this.btn_del.TabIndex = 23;
            this.btn_del.Text = "Sil";
            this.btn_del.UseVisualStyleBackColor = false;
            this.btn_del.Click += new System.EventHandler(this.btn_del_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.BackColor = System.Drawing.Color.LimeGreen;
            this.btn_Add.Location = new System.Drawing.Point(599, 677);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(92, 30);
            this.btn_Add.TabIndex = 22;
            this.btn_Add.Text = "Ekle";
            this.btn_Add.UseVisualStyleBackColor = false;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // txt_Search
            // 
            this.txt_Search.Location = new System.Drawing.Point(156, 517);
            this.txt_Search.Name = "txt_Search";
            this.txt_Search.Size = new System.Drawing.Size(277, 30);
            this.txt_Search.TabIndex = 26;
            this.txt_Search.TextChanged += new System.EventHandler(this.txt_Search_TextChanged_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 520);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 23);
            this.label1.TabIndex = 27;
            this.label1.Text = "Öğrenci Arama";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 23;
            this.listBox1.Location = new System.Drawing.Point(517, 532);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(468, 96);
            this.listBox1.TabIndex = 30;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 559);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 23);
            this.label2.TabIndex = 31;
            this.label2.Text = "Mevcut Kurslar";
            // 
            // comboBox_MevcutKurslarıGoruntule
            // 
            this.comboBox_MevcutKurslarıGoruntule.FormattingEnabled = true;
            this.comboBox_MevcutKurslarıGoruntule.Location = new System.Drawing.Point(156, 559);
            this.comboBox_MevcutKurslarıGoruntule.Name = "comboBox_MevcutKurslarıGoruntule";
            this.comboBox_MevcutKurslarıGoruntule.Size = new System.Drawing.Size(277, 31);
            this.comboBox_MevcutKurslarıGoruntule.TabIndex = 32;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(156, 608);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(277, 30);
            this.dateTimePicker1.TabIndex = 33;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 615);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 23);
            this.label4.TabIndex = 34;
            this.label4.Text = "Atama Tarihi";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 646);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 23);
            this.label5.TabIndex = 35;
            this.label5.Text = "durum";
            // 
            // comboBox_Status
            // 
            this.comboBox_Status.FormattingEnabled = true;
            this.comboBox_Status.Items.AddRange(new object[] {
            "tamamlandi",
            "tamamlanmadi"});
            this.comboBox_Status.Location = new System.Drawing.Point(156, 644);
            this.comboBox_Status.Name = "comboBox_Status";
            this.comboBox_Status.Size = new System.Drawing.Size(121, 31);
            this.comboBox_Status.TabIndex = 36;
            // 
            // btn_OgrenciGoster
            // 
            this.btn_OgrenciGoster.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_OgrenciGoster.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_OgrenciGoster.Location = new System.Drawing.Point(334, 23);
            this.btn_OgrenciGoster.Name = "btn_OgrenciGoster";
            this.btn_OgrenciGoster.Size = new System.Drawing.Size(189, 34);
            this.btn_OgrenciGoster.TabIndex = 39;
            this.btn_OgrenciGoster.Text = "Öğrenci Kurs Kayıt";
            this.btn_OgrenciGoster.UseVisualStyleBackColor = false;
            this.btn_OgrenciGoster.Click += new System.EventHandler(this.btn_OgrenciGoster_Click);
            // 
            // btn_OgretmenGoster
            // 
            this.btn_OgretmenGoster.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_OgretmenGoster.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_OgretmenGoster.Location = new System.Drawing.Point(156, 23);
            this.btn_OgretmenGoster.Name = "btn_OgretmenGoster";
            this.btn_OgretmenGoster.Size = new System.Drawing.Size(172, 34);
            this.btn_OgretmenGoster.TabIndex = 38;
            this.btn_OgretmenGoster.Text = "Eğitmen Kurs Kayıt";
            this.btn_OgretmenGoster.UseVisualStyleBackColor = false;
            this.btn_OgretmenGoster.Click += new System.EventHandler(this.btn_OgretmenGoster_Click);
            // 
            // btn_kursEklemeGoster
            // 
            this.btn_kursEklemeGoster.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_kursEklemeGoster.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_kursEklemeGoster.Location = new System.Drawing.Point(49, 23);
            this.btn_kursEklemeGoster.Name = "btn_kursEklemeGoster";
            this.btn_kursEklemeGoster.Size = new System.Drawing.Size(101, 34);
            this.btn_kursEklemeGoster.TabIndex = 37;
            this.btn_kursEklemeGoster.Text = "Kurs ekleme";
            this.btn_kursEklemeGoster.UseVisualStyleBackColor = false;
            this.btn_kursEklemeGoster.Click += new System.EventHandler(this.btn_kursEklemeGoster_Click);
            // 
            // comboBox_KursAtamaları
            // 
            this.comboBox_KursAtamaları.FormattingEnabled = true;
            this.comboBox_KursAtamaları.Location = new System.Drawing.Point(676, 36);
            this.comboBox_KursAtamaları.Name = "comboBox_KursAtamaları";
            this.comboBox_KursAtamaları.Size = new System.Drawing.Size(277, 31);
            this.comboBox_KursAtamaları.TabIndex = 41;
            this.comboBox_KursAtamaları.SelectedIndexChanged += new System.EventHandler(this.comboBox_KursAtamaları_SelectedIndexChanged_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(588, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 23);
            this.label3.TabIndex = 40;
            this.label3.Text = "Kurs Görüntüle";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dataGridView1.Location = new System.Drawing.Point(49, 151);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Honeydew;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Teal;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(936, 360);
            this.dataGridView1.TabIndex = 42;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.Location = new System.Drawing.Point(45, 100);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(111, 31);
            this.label6.TabIndex = 43;
            this.label6.Text = "KURS ADI";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(34, 684);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 23);
            this.label7.TabIndex = 45;
            this.label7.Text = "Öğrenci Ücreti";
            // 
            // txt_PaymentAmount
            // 
            this.txt_PaymentAmount.Location = new System.Drawing.Point(156, 681);
            this.txt_PaymentAmount.Name = "txt_PaymentAmount";
            this.txt_PaymentAmount.Size = new System.Drawing.Size(277, 30);
            this.txt_PaymentAmount.TabIndex = 44;
            // 
            // cmb_PaymentType
            // 
            this.cmb_PaymentType.FormattingEnabled = true;
            this.cmb_PaymentType.Items.AddRange(new object[] {
            "Taksitli",
            "Peşin"});
            this.cmb_PaymentType.Location = new System.Drawing.Point(156, 719);
            this.cmb_PaymentType.Name = "cmb_PaymentType";
            this.cmb_PaymentType.Size = new System.Drawing.Size(121, 31);
            this.cmb_PaymentType.TabIndex = 47;
            this.cmb_PaymentType.SelectedIndexChanged += new System.EventHandler(this.cmb_PaymentType_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(32, 721);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 23);
            this.label8.TabIndex = 46;
            this.label8.Text = "Ödeme Tipi";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // cmb_InstallmentCount
            // 
            this.cmb_InstallmentCount.FormattingEnabled = true;
            this.cmb_InstallmentCount.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.cmb_InstallmentCount.Location = new System.Drawing.Point(434, 719);
            this.cmb_InstallmentCount.Name = "cmb_InstallmentCount";
            this.cmb_InstallmentCount.Size = new System.Drawing.Size(121, 31);
            this.cmb_InstallmentCount.TabIndex = 48;
            this.cmb_InstallmentCount.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(330, 722);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(98, 23);
            this.label9.TabIndex = 49;
            this.label9.Text = "Taksit Sayısı";
            // 
            // OgrenciKursAtama
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Highlight;
            this.ClientSize = new System.Drawing.Size(1045, 781);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cmb_InstallmentCount);
            this.Controls.Add(this.cmb_PaymentType);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txt_PaymentAmount);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.comboBox_KursAtamaları);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_OgrenciGoster);
            this.Controls.Add(this.btn_OgretmenGoster);
            this.Controls.Add(this.btn_kursEklemeGoster);
            this.Controls.Add(this.comboBox_Status);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.comboBox_MevcutKurslarıGoruntule);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_Search);
            this.Controls.Add(this.btn_del);
            this.Controls.Add(this.btn_Add);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "OgrenciKursAtama";
            this.Text = "OgrenciKursAtama";
            this.Load += new System.EventHandler(this.OgrenciKursAtama_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_del;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.TextBox txt_Search;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_MevcutKurslarıGoruntule;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox_Status;
        private System.Windows.Forms.Button btn_OgrenciGoster;
        private System.Windows.Forms.Button btn_OgretmenGoster;
        private System.Windows.Forms.Button btn_kursEklemeGoster;
        private System.Windows.Forms.ComboBox comboBox_KursAtamaları;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_PaymentAmount;
        private System.Windows.Forms.ComboBox cmb_PaymentType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmb_InstallmentCount;
        private System.Windows.Forms.Label label9;
    }
}