using ClosedXML.Excel;
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
using System.IO;



namespace AnaKodYazılımAkademisi
{
    public partial class DashBoard : Form
    {
        public DashBoard()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // ınstagram hesabına yönlendirme 
            System.Diagnostics.Process.Start("https://www.instagram.com/anakodakademi/");


        }




        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // web sitesine yönlendirme 
            System.Diagnostics.Process.Start("https://anakodakademi.com/");
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        SqlConnection SqlConnection = new SqlConnection("Data Source=DESKTOP-ECJJPMC\\SQLEXPRESS;Initial Catalog=deneme3;Integrated Security=True;");
        private int ToplamOgrenci()
        {
            int toplam = 0;
            SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-ECJJPMC\\SQLEXPRESS;Initial Catalog=deneme3;Integrated Security=True;");
            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT COUNT(*) FROM Students", baglanti);
            toplam = Convert.ToInt32(komut.ExecuteScalar());
            baglanti.Close();
            return toplam;
        }

        private int ToplamKurs()
        {
            int toplam = 0;
            SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-ECJJPMC\\SQLEXPRESS;Initial Catalog=deneme3;Integrated Security=True;");
            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT COUNT(*) FROM Courses", baglanti);
            toplam = Convert.ToInt32(komut.ExecuteScalar());
            baglanti.Close();
            return toplam;
        }

        private int ToplamEgitmen()
        {
            int toplam = 0;
            using (SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-ECJJPMC\\SQLEXPRESS;Initial Catalog=deneme3;Integrated Security=True;"))
            {
                baglanti.Open();
                using (SqlCommand komut = new SqlCommand("GetTotalInstructors", baglanti))
                {
                    komut.CommandType = CommandType.StoredProcedure;
                    toplam = Convert.ToInt32(komut.ExecuteScalar());
                }
            }
            return toplam;
        }

        private void BugunKaydolan()
        {
            string connectionString = "Data Source=DESKTOP-ECJJPMC\\SQLEXPRESS;Initial Catalog=deneme3;Integrated Security=True;";

            try
            {
                // SqlConnection nesnesi ile bağlantıyı aç
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // SqlCommand nesnesi ile stored procedure'ü çalıştır
                    using (SqlCommand command = new SqlCommand("GetTodayEnrollmentCount", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Stored procedure sonucunu al
                        int todayEnrollmentCount = (int)command.ExecuteScalar();

                        // Sonucu TextBox veya Label ile kullanıcıya göster
                        label10.Text = $" {todayEnrollmentCount}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void level1sayisi()
        {
            {
                // Veritabanı bağlantı dizesi
                string connectionString = "Data Source=DESKTOP-ECJJPMC\\SQLEXPRESS;Initial Catalog=deneme3;Integrated Security=True;";

                // SQL Sorgusu: Level 1 öğrenci sayısını al
                string query = "SELECT COUNT(*) FROM Students WHERE level = 'İLKOKUL'";

                try
                {
                    // SqlConnection nesnesi ile bağlantıyı aç
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // SqlCommand nesnesi ile SQL sorgusunu çalıştır
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // ExecuteScalar ile sorgu sonucunu al
                            int level1StudentCount = (int)command.ExecuteScalar();

                            // Sonucu Label ile göster
                            label11.Text = $"{level1StudentCount}";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }
        private void level2sayisi()
        {
            {
                // Veritabanı bağlantı dizesi
                string connectionString = "Data Source=DESKTOP-ECJJPMC\\SQLEXPRESS;Initial Catalog=deneme3;Integrated Security=True;";

                // SQL Sorgusu: Level 1 öğrenci sayısını al
                string query = "SELECT COUNT(*) FROM Students WHERE level = 'ORTAOKUL'";

                try
                {
                    // SqlConnection nesnesi ile bağlantıyı aç
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // SqlCommand nesnesi ile SQL sorgusunu çalıştır
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // ExecuteScalar ile sorgu sonucunu al
                            int level1StudentCount = (int)command.ExecuteScalar();

                            // Sonucu Label ile göster
                            label15.Text = $" {level1StudentCount}";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }
        private void level3sayisi()
        {
            {
                // Veritabanı bağlantı dizesi
                string connectionString = "Data Source=DESKTOP-ECJJPMC\\SQLEXPRESS;Initial Catalog=deneme3;Integrated Security=True;";

                // SQL Sorgusu: Level 1 öğrenci sayısını al
                string query = "SELECT COUNT(*) FROM Students WHERE level = 'LİSE'";

                try
                {
                    // SqlConnection nesnesi ile bağlantıyı aç
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // SqlCommand nesnesi ile SQL sorgusunu çalıştır
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // ExecuteScalar ile sorgu sonucunu al
                            int level1StudentCount = (int)command.ExecuteScalar();

                            // Sonucu Label ile göster
                            label16.Text = $" {level1StudentCount}";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }
        private void OdemeYapmayan()
        {
            string connectionString = "Data Source=DESKTOP-ECJJPMC\\SQLEXPRESS;Initial Catalog=deneme3;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(
                    "SELECT COUNT(DISTINCT student_id) AS UnpaidStudentCount " +
                    "FROM Payments WHERE payment_status = 'Ödenmedi'", connection);

                int unpaidStudentCount = (int)command.ExecuteScalar();

                label9.Text = $" {unpaidStudentCount}";
            }
        }
        public void ExportEachTableToSeperateExcel()
        {
            try
            {
                // Veritabanı bağlantı bilgisi
                string connectionString = "Data Source=DESKTOP-ECJJPMC\\SQLEXPRESS;Initial Catalog=deneme3;Integrated Security=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Tüm tablo isimlerini almak için sorgu
                    SqlCommand getTablesCommand = new SqlCommand(
                        "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'", connection);

                    SqlDataReader reader = getTablesCommand.ExecuteReader();
                    List<string> tableNames = new List<string>();

                    while (reader.Read())
                    {
                        string tableName = reader.GetString(0);
                        tableNames.Add(tableName);
                    }
                    reader.Close();

                    // Her tablo için ayrı Excel dosyası oluştur
                    foreach (string tableName in tableNames)
                    {
                        // Tablo verisini çekmek için sorgu
                        string query = tableName == "Users"
                            ? "SELECT username, email FROM Users" // Users tablosunda password hariç sütunları seç
                            : $"SELECT * FROM [{tableName}]"; // Diğer tabloların tüm verilerini çek

                        SqlCommand command = new SqlCommand(query, connection);
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Her tablo için ayrı bir Excel dosyası oluştur
                        using (XLWorkbook workbook = new XLWorkbook())
                        {
                            workbook.Worksheets.Add(dt, tableName);

                            // SaveFileDialog her tablo için dosya adını alır
                            SaveFileDialog saveFileDialog = new SaveFileDialog
                            {
                                Filter = "Excel Files|*.xlsx",
                                Title = $"Save {tableName} to Excel",
                                FileName = $"{tableName}.xlsx"
                            };

                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                workbook.SaveAs(saveFileDialog.FileName);
                                MessageBox.Show($"{tableName} başarıyla Excel dosyasına kaydedildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DashBoard_Load(object sender, EventArgs e)
        {
            BugunKaydolan();
            level1sayisi();
            level2sayisi();
            level3sayisi();
            label2.Text = ToplamOgrenci().ToString();
            label4.Text = ToplamKurs().ToString();
            label6.Text = ToplamEgitmen().ToString();
            OdemeYapmayan();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ExportEachTableToSeperateExcel();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-ECJJPMC\\SQLEXPRESS;Initial Catalog=deneme3;Integrated Security=True;";
            string query = @"
                SELECT 
                    s.first_name + ' ' + s.last_name AS Ogrenci,
                    COUNT(e.course_id) AS KursSayisi
                FROM 
                    Students s
                JOIN 
                    Enrollments e ON s.student_id = e.student_id
                GROUP BY 
                    s.first_name, s.last_name
                HAVING 
                    COUNT(e.course_id) > 1
                ORDER BY 
                    KursSayisi DESC;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            StringBuilder result = new StringBuilder();
                            while (reader.Read())
                            {
                                string ogrenci = reader["Ogrenci"].ToString();
                                int kursSayisi = (int)reader["KursSayisi"];
                                result.AppendLine($"{ogrenci} : {kursSayisi}");
                            }
                            MessageBox.Show(result.ToString(), "Öğrenci Kurs Bilgileri", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }


        // import işlemleri bu click uzerınden gerceklestırılmektedir 
        public void ImportExcelToTable()
        {
            try
            {
                // Veritabanı bağlantı bilgisi
                string connectionString = "Data Source=DESKTOP-ECJJPMC\\SQLEXPRESS;Initial Catalog=deneme3;Integrated Security=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Kullanıcıdan Excel dosyasını seçmesini isteyin
                    OpenFileDialog openFileDialog = new OpenFileDialog
                    {
                        Filter = "Excel Files|*.xlsx",
                        Title = "Bir Excel Dosyası Seçin"
                    };

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = openFileDialog.FileName;

                        // Dosya adını tablo adı olarak kullan
                        string tableName = Path.GetFileNameWithoutExtension(filePath);

                        // Excel dosyasını oku
                        using (XLWorkbook workbook = new XLWorkbook(filePath))
                        {
                            var worksheet = workbook.Worksheet(1); // İlk sayfa okunur
                            DataTable dataTable = new DataTable();

                            // Sütun başlıklarını al
                            bool isFirstRow = true;
                            foreach (var row in worksheet.Rows())
                            {
                                if (isFirstRow)
                                {
                                    foreach (var cell in row.Cells())
                                    {
                                        dataTable.Columns.Add(cell.Value.ToString());
                                    }
                                    isFirstRow = false;
                                }
                                else
                                {
                                    // Satır verilerini ekle
                                    DataRow dataRow = dataTable.NewRow();
                                    for (int i = 0; i < dataTable.Columns.Count; i++)
                                    {
                                        dataRow[i] = row.Cell(i + 1).Value;
                                    }
                                    dataTable.Rows.Add(dataRow);
                                }
                            }

                            // Veritabanına ekleme işlemi
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                            {
                                bulkCopy.DestinationTableName = tableName;

                                try
                                {
                                    bulkCopy.WriteToServer(dataTable);
                                    MessageBox.Show($"{tableName} tablosuna veriler başarıyla eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show($"Veri eklenirken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            ImportExcelToTable();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ShowAgeDistribution();
        }



        private void ShowAgeDistribution()
        {
            string connectionString = "Data Source=DESKTOP-ECJJPMC\\SQLEXPRESS;Initial Catalog=deneme3;Integrated Security=True;";
            string query = @"
        SELECT 
            CASE 
                WHEN DATEDIFF(YEAR, date_of_birth, GETDATE()) BETWEEN 7 AND 10 THEN '7-10'
                WHEN DATEDIFF(YEAR, date_of_birth, GETDATE()) BETWEEN 11 AND 15 THEN '11-15'
                WHEN DATEDIFF(YEAR, date_of_birth, GETDATE()) BETWEEN 16 AND 18 THEN '16-18'
                WHEN DATEDIFF(YEAR, date_of_birth, GETDATE()) BETWEEN 18 AND 25 THEN '18-25'
            END AS YasGrubu,
            COUNT(student_id) AS OgrenciSayisi
        FROM 
            Students
        GROUP BY 
            CASE 
                WHEN DATEDIFF(YEAR, date_of_birth, GETDATE()) BETWEEN 7 AND 10 THEN '7-10'
                WHEN DATEDIFF(YEAR, date_of_birth, GETDATE()) BETWEEN 11 AND 15 THEN '11-15'
                WHEN DATEDIFF(YEAR, date_of_birth, GETDATE()) BETWEEN 16 AND 18 THEN '16-18'
                WHEN DATEDIFF(YEAR, date_of_birth, GETDATE()) BETWEEN 18 AND 25 THEN '18-25'
            END
        ORDER BY 
            YasGrubu;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            StringBuilder result = new StringBuilder();
                            while (reader.Read())
                            {
                                string yasGrubu = reader["YasGrubu"].ToString();
                                int ogrenciSayisi = (int)reader["OgrenciSayisi"];
                                result.AppendLine($"{yasGrubu} : {ogrenciSayisi}");
                            }
                            MessageBox.Show(result.ToString(), "Yaş Dağılımı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-ECJJPMC\\SQLEXPRESS;Initial Catalog=deneme3;Integrated Security=True;";
            string query = @"
                SELECT 
                    s.first_name + ' ' + s.last_name AS ogrenci_adi,
                    s.email AS ogrenci_mail,
                    g.name AS veli_adi,
                    g.contact_number AS veli_telefon,
                    SUM(p.amount) AS toplam_borc, 
                    SUM(p.total_paid) AS odenen_tutar,
                    (SUM(p.amount) - SUM(p.total_paid)) AS kalan_borc
                FROM 
                    Students s
                LEFT JOIN Payments p ON s.student_id = p.student_id
                LEFT JOIN Guardians g ON s.student_id = g.student_id
                GROUP BY 
                    s.student_id, s.first_name, s.last_name, s.email, g.name, g.contact_number
                HAVING 
                    SUM(p.amount) > SUM(p.total_paid);";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            StringBuilder result = new StringBuilder();
                            while (reader.Read())
                            {
                                string ogrenciAdi = reader["ogrenci_adi"].ToString();
                                string ogrenciMail = reader["ogrenci_mail"].ToString();
                                string veliAdi = reader["veli_adi"].ToString();
                                string veliTelefon = reader["veli_telefon"].ToString();
                                decimal toplamBorc = (decimal)reader["toplam_borc"];
                                decimal odenenTutar = (decimal)reader["odenen_tutar"];
                                decimal kalanBorc = (decimal)reader["kalan_borc"];

                                result.AppendLine($"Öğrenci Adı: {ogrenciAdi}");
                                result.AppendLine($"Öğrenci Mail: {ogrenciMail}");
                                result.AppendLine($"Veli Adı: {veliAdi}");
                                result.AppendLine($"Veli Telefon: {veliTelefon}");
                                result.AppendLine($"Toplam Borç: {toplamBorc}");
                                result.AppendLine($"Ödenen Tutar: {odenenTutar}");
                                result.AppendLine($"Kalan Borç: {kalanBorc}");
                                result.AppendLine(new string('-', 50));
                            }
                            MessageBox.Show(result.ToString(), "Öğrenci Borç Bilgileri", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-ECJJPMC\\SQLEXPRESS;Initial Catalog=deneme3;Integrated Security=True;";
            string query = @"
                SELECT 
                    i.name AS Egitmen,
                    COUNT(DISTINCT ia.course_id) AS KursSayisi,
                    SUM(CASE WHEN e.student_id IS NOT NULL THEN 1 ELSE 0 END) AS ToplamOgrenciSayisi
                FROM 
                    Instructors i
                LEFT JOIN 
                    InstructorAssignments ia ON i.instructor_id = ia.instructor_id
                LEFT JOIN 
                    Enrollments e ON ia.course_id = e.course_id
                GROUP BY 
                    i.name;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            StringBuilder result = new StringBuilder();
                            while (reader.Read())
                            {
                                string egitmen = reader["Egitmen"].ToString();
                                int kursSayisi = (int)reader["KursSayisi"];
                                int toplamOgrenciSayisi = (int)reader["ToplamOgrenciSayisi"];
                                result.AppendLine($"Eğitmen: {egitmen}, Kurs Sayısı: {kursSayisi}, Toplam Öğrenci Sayısı: {toplamOgrenciSayisi}");
                            }
                            MessageBox.Show(result.ToString(), "Eğitmen Kurs ve Öğrenci Bilgileri", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}



