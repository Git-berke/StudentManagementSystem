using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using ClosedXML.Excel;
using System.Globalization;
using System.Linq;
using System.IO;



namespace AnaKodYazılımAkademisi
{
    public partial class StudentYonetim : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-ECJJPMC\\SQLEXPRESS;Initial Catalog=deneme3;Integrated Security=True");

        public StudentYonetim()
        {
            InitializeComponent();
        }

        private void StudentYonetim_Load(object sender, EventArgs e)
        {
            OgrenciListele();
        }

        private void OgrenciListele()
        {
            try
            {
                if (baglanti.State != ConnectionState.Open)
                {
                    baglanti.Open();
                }

                SqlDataAdapter da = new SqlDataAdapter("sp_OgrenciListele", baglanti);
                da.SelectCommand.CommandType = CommandType.StoredProcedure; // Stored Procedure olduğunu belirtiyoruz.
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                }
            }
        }





        private void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State != ConnectionState.Open)
                {
                    baglanti.Open();
                }

                // Stored Procedure çağrısı
                SqlCommand cmd = new SqlCommand("sp_AddStudentAndGuardian", baglanti);
                cmd.CommandType = CommandType.StoredProcedure;

                // Öğrenci bilgileri parametreleri
                cmd.Parameters.AddWithValue("@first_name", txt_Ad.Text);
                cmd.Parameters.AddWithValue("@last_name", txt_Soy.Text);
                cmd.Parameters.AddWithValue("@date_of_birth", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@contact_number", txt_tel.Text);
                cmd.Parameters.AddWithValue("@email", txt_ema.Text);
                cmd.Parameters.AddWithValue("@gender", radioButton1.Checked ? "Kadın" : "Erkek");
                cmd.Parameters.AddWithValue("@level", comboBox1.Text);
                cmd.Parameters.AddWithValue("@school", comboBox2.Text);
                cmd.Parameters.AddWithValue("@coding_experience", checkBox1.Checked);
                cmd.Parameters.AddWithValue("@status", "Aktif");
                cmd.Parameters.AddWithValue("@enrollment_date", DateTime.Now);

                // Veli bilgileri parametreleri
                cmd.Parameters.AddWithValue("@guardian_name", txt_veliAd.Text);
                cmd.Parameters.AddWithValue("@guardian_contact", txt_veliTel.Text);
                cmd.Parameters.AddWithValue("@guardian_email", txt_veliEmail.Text);

                // Stored Procedure'ü çalıştır
                cmd.ExecuteNonQuery();

                MessageBox.Show("Öğrenci ve veli bilgileri başarıyla eklendi!");
                OgrenciListele();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                }
            }
        }



        private void btn_Upd_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State != ConnectionState.Open)
                {
                    baglanti.Open();
                }

                // Seçili satırdaki öğrenci ID'sini al
                int selectedRow = dataGridView1.CurrentRow.Index;
                int studentId = Convert.ToInt32(dataGridView1.Rows[selectedRow].Cells["ID"].Value);

                // Stored Procedure çağrısı
                SqlCommand cmd = new SqlCommand("sp_UpdateStudentAndGuardian", baglanti);
                cmd.CommandType = CommandType.StoredProcedure;

                // Öğrenci bilgileri parametreleri
                cmd.Parameters.AddWithValue("@student_id", studentId);
                cmd.Parameters.AddWithValue("@first_name", txt_Ad.Text);
                cmd.Parameters.AddWithValue("@last_name", txt_Soy.Text);
                cmd.Parameters.AddWithValue("@date_of_birth", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@contact_number", txt_tel.Text);
                cmd.Parameters.AddWithValue("@email", txt_ema.Text);
                cmd.Parameters.AddWithValue("@gender", radioButton1.Checked ? "Kadın" : "Erkek");
                cmd.Parameters.AddWithValue("@level", comboBox1.Text);
                cmd.Parameters.AddWithValue("@school", comboBox2.Text);
                cmd.Parameters.AddWithValue("@coding_experience", checkBox1.Checked);
                cmd.Parameters.AddWithValue("@status", "Aktif");

                // Veli bilgileri parametreleri
                cmd.Parameters.AddWithValue("@guardian_name", txt_veliAd.Text);
                cmd.Parameters.AddWithValue("@guardian_contact", txt_veliTel.Text);
                cmd.Parameters.AddWithValue("@guardian_email", txt_veliEmail.Text);

                // Stored Procedure'ü çalıştır
                cmd.ExecuteNonQuery();

                MessageBox.Show("Öğrenci ve veli bilgileri başarıyla güncellendi!");
                OgrenciListele();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                }
            }
        }



        private void btn_Del_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State != ConnectionState.Open)
                {
                    baglanti.Open();
                }

                // Seçili satırdaki öğrenci ID'sini al
                int selectedRow = dataGridView1.CurrentRow.Index;
                int studentId = Convert.ToInt32(dataGridView1.Rows[selectedRow].Cells["ID"].Value);

                // Stored Procedure çağrısı
                SqlCommand cmd = new SqlCommand("sp_DeleteStudentAndGuardian", baglanti);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@student_id", studentId);

                // Stored Procedure'ü çalıştır
                cmd.ExecuteNonQuery();

                MessageBox.Show("Öğrenci ve veli bilgileri başarıyla silindi!");
                OgrenciListele();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                }
            }
        }



        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int selectedRow = e.RowIndex;
            if (selectedRow >= 0)
            {
                txt_Ad.Text = dataGridView1.Rows[selectedRow].Cells["Ad"].Value.ToString();
                txt_Soy.Text = dataGridView1.Rows[selectedRow].Cells["Soyad"].Value.ToString();
                txt_tel.Text = dataGridView1.Rows[selectedRow].Cells["Telefon"].Value.ToString();
                txt_ema.Text = dataGridView1.Rows[selectedRow].Cells["Email"].Value.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.Rows[selectedRow].Cells["Doğum Tarihi"].Value);
                comboBox1.SelectedItem = dataGridView1.Rows[selectedRow].Cells["Seviye"].Value.ToString();
                txt_veliAd.Text = dataGridView1.Rows[selectedRow].Cells["Veli Adı"].Value.ToString();
                txt_veliTel.Text = dataGridView1.Rows[selectedRow].Cells["Veli Telefon"].Value.ToString();
                txt_veliEmail.Text = dataGridView1.Rows[selectedRow].Cells["Veli Email"].Value.ToString();

                if (dataGridView1.Rows[selectedRow].Cells["Cinsiyet"].Value.ToString() == "Kadın")
                    radioButton1.Checked = true;
                else if (dataGridView1.Rows[selectedRow].Cells["Cinsiyet"].Value.ToString() == "Erkek")
                    radioButton2.Checked = true;

                checkBox1.Checked = (dataGridView1.Rows[selectedRow].Cells["Kodlama Geçmişi"].Value.ToString() == "Evet");
            }



        }

        private void button1_Click(object sender, EventArgs e)
        {

            //temizleme kodları burada
            txt_Ad.Text = "";
            txt_Soy.Text = "";
            txt_tel.Text = "";
            txt_ema.Text = "";
            txt_veliAd.Text = "";
            txt_veliTel.Text = "";
            txt_veliEmail.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            checkBox1.Checked = false;
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now;



        }

        private void btn_Src_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State != ConnectionState.Open)
                {
                    baglanti.Open();
                }

                // DataAdapter kullanarak Stored Procedure çağır
                SqlDataAdapter da = new SqlDataAdapter("sp_SearchStudents", baglanti);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                // Arama metnini parametre olarak gönder
                da.SelectCommand.Parameters.AddWithValue("@searchText", txt_src.Text);

                DataTable dt = new DataTable();
                da.Fill(dt);

                // Arama sonuçlarını DataGridView'e aktar
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                }
            }
        }


        private void radioButton_KadınlarıGöster_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State != ConnectionState.Open)
                {
                    baglanti.Open();
                }

                SqlDataAdapter da;
                if (radioButton_KadınlarıGöster.Checked)
                {
                    da = new SqlDataAdapter(@"
                        SELECT 
                            s.student_id AS 'ID',
                            s.first_name AS 'Ad',
                            s.last_name AS 'Soyad',
                            s.contact_number AS 'Telefon',
                            s.email AS 'Email',
                            s.gender AS 'Cinsiyet',
                            s.date_of_birth AS 'Doğum Tarihi',
                            s.level AS 'Seviye',
                            s.status AS 'Durum',
                            s.school AS 'Okul',
                            CASE WHEN s.coding_experience = 1 THEN 'Evet' ELSE 'Hayır' END AS 'Kodlama Geçmişi',
                            g.name AS 'Veli Adı',
                            g.contact_number AS 'Veli Telefon',
                            g.email AS 'Veli Email'
                        FROM Students s
                        LEFT JOIN Guardians g ON s.student_id = g.student_id
                        WHERE s.gender = 'Kadın'", baglanti);
                }
                else
                {
                    da = new SqlDataAdapter(@"
                        SELECT 
                            s.student_id AS 'ID',
                            s.first_name AS 'Ad',
                            s.last_name AS 'Soyad',
                            s.contact_number AS 'Telefon',
                            s.email AS 'Email',
                            s.gender AS 'Cinsiyet',
                            s.date_of_birth AS 'Doğum Tarihi',
                            s.level AS 'Seviye',
                            s.status AS 'Durum',
                            s.school AS 'Okul',
                            CASE WHEN s.coding_experience = 1 THEN 'Evet' ELSE 'Hayır' END AS 'Kodlama Geçmişi',
                            g.name AS 'Veli Adı',
                            g.contact_number AS 'Veli Telefon',
                            g.email AS 'Veli Email'
                        FROM Students s
                        LEFT JOIN Guardians g ON s.student_id = g.student_id", baglanti);
                }

                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                }
            }
        }

        private void radioButton_ErkekleriGöster_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State != ConnectionState.Open)
                {
                    baglanti.Open();
                }

                SqlDataAdapter da;
                if (radioButton_ErkekleriGöster.Checked)
                {
                    da = new SqlDataAdapter(@"
                SELECT 
                    s.student_id AS 'ID',
                    s.first_name AS 'Ad',
                    s.last_name AS 'Soyad',
                    s.contact_number AS 'Telefon',
                    s.email AS 'Email',
                    s.gender AS 'Cinsiyet',
                    s.date_of_birth AS 'Doğum Tarihi',
                    s.level AS 'Seviye',
                    s.status AS 'Durum',
                    s.school AS 'Okul',
                    CASE WHEN s.coding_experience = 1 THEN 'Evet' ELSE 'Hayır' END AS 'Kodlama Geçmişi',
                    g.name AS 'Veli Adı',
                    g.contact_number AS 'Veli Telefon',
                    g.email AS 'Veli Email'
                FROM Students s
                LEFT JOIN Guardians g ON s.student_id = g.student_id
                WHERE s.gender = 'Erkek'", baglanti);
                }
                else
                {
                    da = new SqlDataAdapter(@"
                SELECT 
                    s.student_id AS 'ID',
                    s.first_name AS 'Ad',
                    s.last_name AS 'Soyad',
                    s.contact_number AS 'Telefon',
                    s.email AS 'Email',
                    s.gender AS 'Cinsiyet',
                    s.date_of_birth AS 'Doğum Tarihi',
                    s.level AS 'Seviye',
                    s.status AS 'Durum',
                    s.school AS 'Okul',
                    CASE WHEN s.coding_experience = 1 THEN 'Evet' ELSE 'Hayır' END AS 'Kodlama Geçmişi',
                    g.name AS 'Veli Adı',
                    g.contact_number AS 'Veli Telefon',
                    g.email AS 'Veli Email'
                FROM Students s
                LEFT JOIN Guardians g ON s.student_id = g.student_id", baglanti);
                }

                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                }
            }
        }

        private void radioButton_İlkOkulÖğrencileriniGöster_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State != ConnectionState.Open)
                {
                    baglanti.Open();
                }

                SqlDataAdapter da;
                if (radioButton_İlkOkulÖğrencileriniGöster.Checked)
                {
                    da = new SqlDataAdapter(@"
                SELECT 
                    s.student_id AS 'ID',
                    s.first_name AS 'Ad',
                    s.last_name AS 'Soyad',
                    s.contact_number AS 'Telefon',
                    s.email AS 'Email',
                    s.gender AS 'Cinsiyet',
                    s.date_of_birth AS 'Doğum Tarihi',
                    s.level AS 'Seviye',
                    s.status AS 'Durum',
                    s.school AS 'Okul',
                    CASE WHEN s.coding_experience = 1 THEN 'Evet' ELSE 'Hayır' END AS 'Kodlama Geçmişi',
                    g.name AS 'Veli Adı',
                    g.contact_number AS 'Veli Telefon',
                    g.email AS 'Veli Email'
                FROM Students s
                LEFT JOIN Guardians g ON s.student_id = g.student_id
                GROUP BY 
                    s.student_id, 
                    s.first_name, 
                    s.last_name, 
                    s.contact_number, 
                    s.email, 
                    s.gender, 
                    s.date_of_birth, 
                    s.level, 
                    s.status, 
                    s.school, 
                    s.coding_experience, 
                    g.name, 
                    g.contact_number, 
                    g.email
                HAVING s.level = 'İlkokul'", baglanti);
                }
                else
                {
                    da = new SqlDataAdapter(@"
                SELECT 
                    s.student_id AS 'ID',
                    s.first_name AS 'Ad',
                    s.last_name AS 'Soyad',
                    s.contact_number AS 'Telefon',
                    s.email AS 'Email',
                    s.gender AS 'Cinsiyet',
                    s.date_of_birth AS 'Doğum Tarihi',
                    s.level AS 'Seviye',
                    s.status AS 'Durum',
                    s.school AS 'Okul',
                    CASE WHEN s.coding_experience = 1 THEN 'Evet' ELSE 'Hayır' END AS 'Kodlama Geçmişi',
                    g.name AS 'Veli Adı',
                    g.contact_number AS 'Veli Telefon',
                    g.email AS 'Veli Email'
                FROM Students s
                LEFT JOIN Guardians g ON s.student_id = g.student_id
                GROUP BY 
                    s.student_id, 
                    s.first_name, 
                    s.last_name, 
                    s.contact_number, 
                    s.email, 
                    s.gender, 
                    s.date_of_birth, 
                    s.level, 
                    s.status, 
                    s.school, 
                    s.coding_experience, 
                    g.name, 
                    g.contact_number, 
                    g.email", baglanti);
                }

                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                }
            }
        }


        private void radioButton_OrtaOkulÖrencileriniGöster_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State != ConnectionState.Open)
                {
                    baglanti.Open();
                }

                SqlDataAdapter da;
                if (radioButton_OrtaOkulÖrencileriniGöster.Checked)
                {
                    da = new SqlDataAdapter(@"
                        SELECT 
                            s.student_id AS 'ID',
                            s.first_name AS 'Ad',
                            s.last_name AS 'Soyad',
                            s.contact_number AS 'Telefon',
                            s.email AS 'Email',
                            s.gender AS 'Cinsiyet',
                            s.date_of_birth AS 'Doğum Tarihi',
                            s.level AS 'Seviye',
                            s.status AS 'Durum',
                            s.school AS 'Okul',
                            CASE WHEN s.coding_experience = 1 THEN 'Evet' ELSE 'Hayır' END AS 'Kodlama Geçmişi',
                            g.name AS 'Veli Adı',
                            g.contact_number AS 'Veli Telefon',
                            g.email AS 'Veli Email'
                        FROM Students s
                        LEFT JOIN Guardians g ON s.student_id = g.student_id
                        WHERE s.level = 'Ortaokul'", baglanti);
                }
                else
                {
                    da = new SqlDataAdapter(@"
                        SELECT 
                            s.student_id AS 'ID',
                            s.first_name AS 'Ad',
                            s.last_name AS 'Soyad',
                            s.contact_number AS 'Telefon',
                            s.email AS 'Email',
                            s.gender AS 'Cinsiyet',
                            s.date_of_birth AS 'Doğum Tarihi',
                            s.level AS 'Seviye',
                            s.status AS 'Durum',
                            s.school AS 'Okul',
                            CASE WHEN s.coding_experience = 1 THEN 'Evet' ELSE 'Hayır' END AS 'Kodlama Geçmişi',
                            g.name AS 'Veli Adı',
                            g.contact_number AS 'Veli Telefon',
                            g.email AS 'Veli Email'
                        FROM Students s
                        LEFT JOIN Guardians g ON s.student_id = g.student_id", baglanti);
                }

                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                }
            }
        }

        private void radioButton_ListeÖğrencileriniGöster_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State != ConnectionState.Open)
                {
                    baglanti.Open();
                }

                SqlDataAdapter da;
                if (radioButton_ListeÖğrencileriniGöster.Checked)
                {
                    da = new SqlDataAdapter(@"
                        SELECT 
                            s.student_id AS 'ID',
                            s.first_name AS 'Ad',
                            s.last_name AS 'Soyad',
                            s.contact_number AS 'Telefon',
                            s.email AS 'Email',
                            s.gender AS 'Cinsiyet',
                            s.date_of_birth AS 'Doğum Tarihi',
                            s.level AS 'Seviye',
                            s.status AS 'Durum',
                            s.school AS 'Okul',
                            CASE WHEN s.coding_experience = 1 THEN 'Evet' ELSE 'Hayır' END AS 'Kodlama Geçmişi',
                            g.name AS 'Veli Adı',
                            g.contact_number AS 'Veli Telefon',
                            g.email AS 'Veli Email'
                        FROM Students s
                        LEFT JOIN Guardians g ON s.student_id = g.student_id
                        WHERE s.level = 'Lise'", baglanti);
                }
                else
                {
                    da = new SqlDataAdapter(@"
                        SELECT 
                            s.student_id AS 'ID',
                            s.first_name AS 'Ad',
                            s.last_name AS 'Soyad',
                            s.contact_number AS 'Telefon',
                            s.email AS 'Email',
                            s.gender AS 'Cinsiyet',
                            s.date_of_birth AS 'Doğum Tarihi',
                            s.level AS 'Seviye',
                            s.status AS 'Durum',
                            s.school AS 'Okul',
                            CASE WHEN s.coding_experience = 1 THEN 'Evet' ELSE 'Hayır' END AS 'Kodlama Geçmişi',
                            g.name AS 'Veli Adı',
                            g.contact_number AS 'Veli Telefon',
                            g.email AS 'Veli Email'
                        FROM Students s
                        LEFT JOIN Guardians g ON s.student_id = g.student_id", baglanti);
                }

                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                }
            }
        }

        private void radioButton_TumOgrencileriGoster_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State != ConnectionState.Open)
                {
                    baglanti.Open();
                }

                SqlDataAdapter da = new SqlDataAdapter(@"
            SELECT 
                s.student_id AS 'ID',
                s.first_name AS 'Ad',
                s.last_name AS 'Soyad',
                s.contact_number AS 'Telefon',
                s.email AS 'Email',
                s.gender AS 'Cinsiyet',
                s.date_of_birth AS 'Doğum Tarihi',
                s.level AS 'Seviye',
                s.status AS 'Durum',
                s.school AS 'Okul',
                CASE WHEN s.coding_experience = 1 THEN 'Evet' ELSE 'Hayır' END AS 'Kodlama Geçmişi',
                g.name AS 'Veli Adı',
                g.contact_number AS 'Veli Telefon',
                g.email AS 'Veli Email'
            FROM Students s
            LEFT JOIN Guardians g ON s.student_id = g.student_id", baglanti);

                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                }
            }
        }

        private void txt_tel_TextChanged(object sender, EventArgs e)
        {
            // 11 haneden fazla giriş yapılmasını engelle
            if (txt_tel.Text.Length > 11)
            {
                txt_tel.Text = txt_tel.Text.Substring(0, 11);
                txt_tel.SelectionStart = txt_tel.Text.Length; // İmleci sona taşı
            }
        }

        private void txt_tel_KeyPress(object sender, KeyPressEventArgs e)
        {

            // Sadece rakam ve kontrol tuşlarına izin ver
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void txt_veliTel_TextChanged(object sender, EventArgs e)
        {
            // 11 haneden fazla giriş yapılmasını engelle
            if (txt_veliTel.Text.Length > 11)
            {
                txt_veliTel.Text = txt_veliTel.Text.Substring(0, 11);
                txt_veliTel.SelectionStart = txt_veliTel.Text.Length; // İmleci sona taşı
            }
        }

        private void txt_veliTel_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Sadece rakam ve kontrol tuşlarına izin ver
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            try
            {
                // Veritabanı bağlantısı
                string connectionString = "Data Source=DESKTOP-ECJJPMC\\SQLEXPRESS;Initial Catalog=deneme3;Integrated Security=True";
                string query = "SELECT * FROM Students";


                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Excel dosyası oluşturma
                    using (XLWorkbook workbook = new XLWorkbook())
                    {
                        workbook.Worksheets.Add(dt, "Sheet1");
                        SaveFileDialog saveFileDialog = new SaveFileDialog
                        {
                            Filter = "Excel Files|*.xlsx",
                            Title = "Save an Excel File",
                            FileName = "ExportedData.xlsx"
                        };

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            workbook.SaveAs(saveFileDialog.FileName);
                            MessageBox.Show("Veriler başarıyla Excel'e aktarıldı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            // Create an instance of OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select Excel File", // Window title
                Filter = "Excel Files|.xls;.xlsx|All Files|.", // File filters
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) // Default directory
            };

            // Show the dialog and check if the user selected a file
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the selected file path
                string selectedFilePath = openFileDialog.FileName;

                // Call the ImportFromExcel method
                ImportFromExcel(selectedFilePath);
            }

            OgrenciListele();




        }



        public void ImportFromExcel(string filePath)
        {
            // Check if the file exists
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found!");
                return;
            }


            try
            {
                using (var workbook = new XLWorkbook(filePath))
                {
                    // Assume the first sheet contains the data
                    var worksheet = workbook.Worksheet(1);

                    using (var connection = new SqlConnection("Data Source=DESKTOP-ECJJPMC\\SQLEXPRESS;Initial Catalog=deneme3;Integrated Security=True"))
                    {
                        connection.Open();

                        // Loop through rows in the worksheet (assuming headers are in the first row)
                        foreach (var row in worksheet.RowsUsed().Skip(1)) // Skip header row
                        {
                            // Read the data from Excel
                            string firstName = row.Cell(1).GetValue<string>();
                            string lastName = row.Cell(2).GetValue<string>();

                            string dateOfBirthString = row.Cell(3).GetValue<string>();
                            DateTime dateOfBirth;
                            string dateFormat = "dd.MM.yyyy";  // European date format (day.month.year)

                            if (!DateTime.TryParseExact(dateOfBirthString, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOfBirth))
                            {
                                Console.WriteLine($"Invalid date format for row {row.RowNumber()} in the 'Date of Birth' column.");
                                continue; // Skip this row
                            }


                            string contactNumber = row.Cell(4).GetValue<string>();
                            string email = row.Cell(5).GetValue<string>();
                            string gender = row.Cell(6).GetValue<string>();
                            string level = row.Cell(7).GetValue<string>();
                            string school = row.Cell(8).GetValue<string>();

                            // Convert coding_experience to BIT (0 or 1)
                            bool codingExperienceBool = row.Cell(9).GetValue<bool>();
                            int codingExperience = codingExperienceBool ? 1 : 0;

                            string status = row.Cell(10).GetValue<string>();

                            string enrollmentDateString = row.Cell(11).GetValue<string>();
                            DateTime enrollmentDate;

                            if (!DateTime.TryParseExact(enrollmentDateString, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out enrollmentDate))
                            {
                                Console.WriteLine($"Invalid date format for row {row.RowNumber()} in the 'Date of Birth' column.");
                                continue; // Skip this row
                            }

                            // Insert into the Students table
                            string studentInsertQuery = @"
                INSERT INTO Students (first_name, last_name, date_of_birth, contact_number, email, gender, level, School, coding_experience, status, enrollment_date)
                VALUES (@first_name, @last_name, @date_of_birth, @contact_number, @email, @gender, @level, @school, @coding_experience, @status, @enrollment_date);
                SELECT SCOPE_IDENTITY();";

                            using (SqlCommand studentCmd = new SqlCommand(studentInsertQuery, connection))
                            {
                                studentCmd.Parameters.AddWithValue("@first_name", firstName);
                                studentCmd.Parameters.AddWithValue("@last_name", lastName);
                                studentCmd.Parameters.AddWithValue("@date_of_birth", dateOfBirth);
                                studentCmd.Parameters.AddWithValue("@contact_number", contactNumber);
                                studentCmd.Parameters.AddWithValue("@email", email);
                                studentCmd.Parameters.AddWithValue("@gender", gender);
                                studentCmd.Parameters.AddWithValue("@level", level);
                                studentCmd.Parameters.AddWithValue("@school", school);
                                studentCmd.Parameters.AddWithValue("@coding_experience", codingExperience); // Insert as BIT (0 or 1)
                                studentCmd.Parameters.AddWithValue("@status", status);
                                studentCmd.Parameters.AddWithValue("@enrollment_date", enrollmentDate);

                                // Execute the insert and retrieve the generated student ID
                                int studentId = Convert.ToInt32(studentCmd.ExecuteScalar());

                                // Read guardian details from the Excel file
                                string guardianName = row.Cell(12).GetValue<string>();
                                string guardianContact = row.Cell(13).GetValue<string>();
                                string guardianEmail = row.Cell(14).GetValue<string>();

                                // Insert into the Guardians table
                                string guardianInsertQuery = @"
                    INSERT INTO Guardians (student_id, name, contact_number, email)
                    VALUES (@student_id, @name, @contact_number, @email)";

                                using (SqlCommand guardianCmd = new SqlCommand(guardianInsertQuery, connection))
                                {
                                    guardianCmd.Parameters.AddWithValue("@student_id", studentId);
                                    guardianCmd.Parameters.AddWithValue("@name", guardianName);
                                    guardianCmd.Parameters.AddWithValue("@contact_number", guardianContact);
                                    guardianCmd.Parameters.AddWithValue("@email", guardianEmail);

                                    guardianCmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }

                Console.WriteLine("Data imported successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
























    }
}
