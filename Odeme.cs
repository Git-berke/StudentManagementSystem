using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AnaKodYazılımAkademisi
{
    public partial class Odeme : Form
    {
        private string connectionString = "Data Source=DESKTOP-ECJJPMC\\SQLEXPRESS;Initial Catalog=deneme3;Integrated Security=True;";
        private Panel panelDesktop;
        private Form activeForm;
        public Odeme(Panel panelDesktop)
        {
            InitializeComponent();
            this.panelDesktop = panelDesktop;
        }
        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        private void LoadPaymentData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
            SELECT 
                S.student_id AS [Öğrenci ID], 
                CONCAT(S.first_name, ' ', S.last_name) AS [Ad Soyad], 
                P.amount AS [Toplam Tutar],
                ISNULL(P.total_paid, 0) AS [Ödenen Tutar],
                (P.amount - ISNULL(P.total_paid, 0)) AS [Kalan Tutar],
                P.payment_status AS [Ödeme Durumu],
                P.payment_type AS [Ödeme Tipi],
                P.installment_count AS [Taksit Sayısı]
            FROM Students S
            LEFT JOIN Payments P ON S.student_id = P.student_id
            WHERE (P.amount - ISNULL(P.total_paid, 0)) > 0";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // DataGridView'i güncelle
                    dataGridView1.DataSource = null; // Önce bağlamayı temizle
                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri yüklenirken bir hata oluştu: " + ex.Message);
            }
        }




        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = checkBox1.Checked;
            label3.Visible = isChecked;
            label4.Visible = isChecked;
            labelRemainingInstallments.Visible = isChecked;
            label9.Visible = isChecked;
            label12.Visible = isChecked;
            label13.Visible = isChecked;
        }

        private void Odeme_Load(object sender, EventArgs e)
        {
            label3.Visible = false;
            label4.Visible = false;
            labelRemainingInstallments.Visible = false;
            label9.Visible = false;
            label12.Visible = false;
            label13.Visible = false;

            // Verileri yükle
            LoadPaymentData();  // Ödeme yapmamış öğrencileri yükler
            LoadPaymentDetailsData();  // Eski ödeme bilgilerini yükler
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Seçili satırdaki değerleri al
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                // Ödeme Tipini al
                string odemeTipi = selectedRow.Cells["Ödeme Tipi"].Value?.ToString();

                // Kalan ödeme ve toplam tutar bilgilerini al
                decimal kalanOdeme;
                decimal toplamTutar;
                int taksitSayisi;
                if (odemeTipi == "Taksitli")
                {
                    // Taksit bilgilerini doldur
                    if (decimal.TryParse(selectedRow.Cells["Kalan Tutar"].Value?.ToString(), out kalanOdeme) &&
                        decimal.TryParse(selectedRow.Cells["Toplam Tutar"].Value?.ToString(), out toplamTutar) &&
                        int.TryParse(selectedRow.Cells["Taksit Sayısı"].Value?.ToString(), out taksitSayisi))
                    {
                        // Eğer ödeme tipi "Taksitli" ise, kalan taksit sayısını ve son ödemeyi hesapla
                        if (odemeTipi == "Taksitli")
                        {
                            decimal taksitTutari = toplamTutar / taksitSayisi; // Bir taksit tutarı
                            int kalanTaksitSayisi = (int)(kalanOdeme / taksitTutari); // Tam taksit sayısı
                            decimal sonTaksit = kalanOdeme % taksitTutari; // Son taksit miktarı
                            label12.Text = taksitTutari.ToString();
                            // Eğer tam bölünüyorsa, son taksit miktarını sıfır yerine taksit tutarına eşitle


                            // Kalan taksit ve son ödeme bilgilerini label'a yaz
                            labelRemainingInstallments.Text = $"Kalan Taksit: {kalanTaksitSayisi} + {sonTaksit:F2} TL";
                        }
                        else
                        {
                            // Taksitli ödeme değilse, label'ı temizle
                            labelRemainingInstallments.Text = "Kalan Taksit: N/A";
                        }
                    }
                    else
                    {
                        // Eğer bilgiler alınamazsa label'ı temizle
                        labelRemainingInstallments.Text = "Kalan Taksit: N/A";
                    }
                }
                // CheckBox ve ComboBox güncelleme
                if (odemeTipi == "Taksitli")
                {
                    checkBox1.Checked = true;
                }
                else
                {
                    checkBox1.Checked = false;
                }

                // ComboBox'ta taksit sayısını göster
                if (int.TryParse(selectedRow.Cells["Taksit Sayısı"].Value?.ToString(), out taksitSayisi))
                {
                    label4.Text = taksitSayisi.ToString();
                }
                else
                {
                    label4.Visible = false;
                }

                // Kalan ödeme miktarını ve toplam tutarı TextBox'lara yaz
                label11.Text = selectedRow.Cells["Kalan Tutar"].Value?.ToString();
                label10.Text = selectedRow.Cells["Toplam Tutar"].Value?.ToString();
            }
        }
        private void UpdatePaymentInDatabase(string studentId, decimal totalPaid, decimal remainingAmount, string paymentType)
        {
            string query = @"
        UPDATE Payments
        SET total_paid = @TotalPaid,
            payment_status = CASE 
                WHEN @RemainingAmount = 0 THEN 'Ödendi' 
                ELSE payment_status 
            END,
            payment_type = CASE 
                WHEN @RemainingAmount = 0 THEN 'Peşin' 
                ELSE payment_type 
            END
        WHERE student_id = @StudentId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TotalPaid", totalPaid);
                cmd.Parameters.AddWithValue("@RemainingAmount", remainingAmount);
                cmd.Parameters.AddWithValue("@StudentId", studentId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void SavePaymentDetail(int studentId, decimal paymentAmount, string paymentMethod, string paymentType)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Öğrenciye ait bir payment_id olup olmadığını kontrol et
                    string checkQuery = "SELECT payment_id FROM Payments WHERE student_id = @StudentId";
                    int paymentId;

                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@StudentId", studentId);
                        object result = checkCmd.ExecuteScalar();

                        if (result == null)
                        {
                            throw new InvalidOperationException("Bu öğrenci için bir ödeme kaydı bulunamadı. Lütfen önce Payments tablosunda bir kayıt oluşturun.");
                        }

                        paymentId = Convert.ToInt32(result);
                    }

                    // PaymentDetails tablosuna ödeme detaylarını ekle
                    string paymentDetailQuery = @"
            INSERT INTO PaymentDetails (payment_id, paid_amount, payment_method, description, payment_date)
            VALUES (@PaymentId, @PaidAmount, @PaymentMethod, @Description, @PaymentDate)";

                    using (SqlCommand detailCmd = new SqlCommand(paymentDetailQuery, connection))
                    {
                        detailCmd.Parameters.AddWithValue("@PaymentId", paymentId);
                        detailCmd.Parameters.AddWithValue("@PaidAmount", paymentAmount);
                        detailCmd.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                        detailCmd.Parameters.AddWithValue("@Description", $"{paymentType} ödeme"); // Açıklama kısmına ödeme tipi ekle
                        detailCmd.Parameters.AddWithValue("@PaymentDate", DateTime.Now);

                        detailCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ödeme detayları kaydedilirken bir hata oluştu: " + ex.Message);
            }
        }





        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                if (decimal.TryParse(selectedRow.Cells["Kalan Tutar"].Value?.ToString(), out decimal kalanOdeme) &&
                    decimal.TryParse(selectedRow.Cells["Ödenen Tutar"].Value?.ToString(), out decimal totalPaid))
                {
                    if (decimal.TryParse(textBox2.Text, out decimal odemeMiktari))
                    {
                        if (odemeMiktari > 0 && odemeMiktari <= kalanOdeme)
                        {
                            totalPaid += odemeMiktari;
                            kalanOdeme -= odemeMiktari;

                            // Öğrenci ID'sini al
                            string studentId = selectedRow.Cells["Öğrenci ID"].Value?.ToString();
                            string odemeTipi = selectedRow.Cells["Ödeme Tipi"].Value?.ToString(); // Ödeme tipini kontrol et

                            if (!string.IsNullOrEmpty(studentId))
                            {
                                // 1. Ödeme tablosunu güncelle
                                UpdatePaymentInDatabase(studentId, totalPaid, kalanOdeme, odemeTipi);

                                // 2. Ödeme detaylarını kaydet
                                SavePaymentDetail(int.Parse(studentId), odemeMiktari, odemeTipi, odemeTipi);

                                // 3. Veri tablosunu yenile
                                LoadPaymentData();
                                LoadPaymentDetailsData();

                                MessageBox.Show("Ödeme başarıyla gerçekleştirildi.");
                            }
                            else
                            {
                                MessageBox.Show("Geçerli bir Öğrenci ID'si bulunamadı.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Geçerli bir ödeme miktarı giriniz. Ödeme kalan tutardan fazla olamaz.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Lütfen geçerli bir ödeme miktarı giriniz.");
                    }
                }
                else
                {
                    MessageBox.Show("Seçili satırdaki ödeme bilgileri eksik veya hatalı.");
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir satır seçiniz.");
            }
        }
        private void SaveReceiptToDatabase(string studentId, string filePath)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                INSERT INTO Receipts (payment_id, receipt_date, file_path)
                VALUES (
                    (SELECT payment_id FROM Payments WHERE student_id = @StudentId), 
                    @ReceiptDate, 
                    @FilePath
                )";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@StudentId", studentId);
                        cmd.Parameters.AddWithValue("@ReceiptDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@FilePath", filePath);

                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dekont kaydedilirken bir hata oluştu: " + ex.Message);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Seçili satırı al
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string studentId = selectedRow.Cells["Öğrenci ID"].Value?.ToString();

                // OpenFileDialog'u başlat
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "PDF Dosyaları|*.pdf|Tüm Dosyalar|*.*";
                openFileDialog.Title = "Dekont Seçiniz";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Dosya yolunu al
                    string filePath = openFileDialog.FileName;

                    // Veritabanına kaydet
                    SaveReceiptToDatabase(studentId, filePath);

                    MessageBox.Show("Dekont başarıyla eklendi.");
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir öğrenci seçiniz.");
            }
        }
        private void LoadPaymentDetailsData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // SQL Sorgusu
                    string query = @"
                SELECT 
                    PD.payment_id AS [Ödeme ID], 
                    S.first_name + ' ' + S.last_name AS [Öğrenci Adı], 
                    PD.paid_amount AS [Ödenen Tutar], 
                    PD.payment_method AS [Ödeme Yöntemi], 
                    PD.description AS [Açıklama], 
                    PD.payment_date AS [Ödeme Tarihi]
                FROM PaymentDetails PD
                INNER JOIN Payments P ON PD.payment_id = P.payment_id
                INNER JOIN Students S ON P.student_id = S.student_id
            ";

                    // SqlDataAdapter ile veri çekme
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // DataGridView2'ye veri bağlama
                    dataGridView2.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eski ödeme verileri yüklenirken bir hata oluştu: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenChildForm(new EskiDekont(panelDesktop), sender);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Seçili satırdaki ödeme bilgilerini al
                DataGridViewRow selectedRow = dataGridView2.Rows[e.RowIndex];

                // Ödeme bilgilerini işle
                string paymentId = selectedRow.Cells["Ödeme ID"].Value?.ToString();
                string studentName = selectedRow.Cells["Öğrenci Adı"].Value?.ToString();
                decimal paidAmount = Convert.ToDecimal(selectedRow.Cells["Ödenen Tutar"].Value);
                string paymentMethod = selectedRow.Cells["Ödeme Yöntemi"].Value?.ToString();

                // Ödeme detaylarını göstermek için işlem yapabilirsiniz.
            }
        }
    }
}
