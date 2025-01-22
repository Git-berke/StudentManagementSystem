using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AnaKodYazılımAkademisi
{
    public partial class EskiDekont : Form
    {
        private string connectionString = "Data Source=DESKTOP-ECJJPMC\\SQLEXPRESS;Initial Catalog=deneme3;Integrated Security=True;";

        private Panel panelDesktop;
        private Form activeForm;
        public EskiDekont(Panel panelDesktop)
        {
            InitializeComponent();
            dataGridView1.CellClick += new DataGridViewCellEventHandler(dataGridView1_CellClick);
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
        private void LoadReceiptData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // SQL sorgusu
                    string query = @"
                SELECT 
                    R.receipt_id AS [Dekont ID],
                    S.student_id AS [Öğrenci ID],
                    CONCAT(S.first_name, ' ', S.last_name) AS [Ad Soyad],
                    R.receipt_date AS [Dekont Tarihi],
                    R.file_path AS [Dosya Yolu]
                FROM Receipts R
                INNER JOIN Payments P ON R.payment_id = P.payment_id
                INNER JOIN Students S ON P.student_id = S.student_id;
            ";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // DataGridView'e veri bağlama
                    dataGridView1.DataSource = dataTable;

                    // DataGridView sütun genişliklerini otomatik ayarla
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veriler yüklenirken bir hata oluştu: " + ex.Message);
            }
        }

        private void EskiDekont_Load(object sender, EventArgs e)
        {
            LoadReceiptData();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Odeme(panelDesktop), sender);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Sadece geçerli bir hücre tıklandığında işlem yapılır
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    string columnHeader = dataGridView1.Columns[e.ColumnIndex].HeaderText; // Başlık kontrolü
                                                                                           // Sütun başlığı "Dosya Yolu" ise
                    if (columnHeader == "Dosya Yolu")
                    {
                        string filePath = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();

                        if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                        {
                            System.Diagnostics.Process.Start(filePath);
                        }
                        else
                        {
                            MessageBox.Show("Dekont dosyası bulunamadı veya geçersiz.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
    }
}
