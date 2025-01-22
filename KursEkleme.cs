using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AnaKodYazılımAkademisi
{
    public partial class KursEkleme : Form
    {
        public KursEkleme(Panel panelDesktop)
        {
            InitializeComponent();
            this.panelDesktop = panelDesktop;
        }

        private Panel panelDesktop;
        private Form activeForm;

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

       
        private void btn_kursEklemeGoster_Click(object sender, EventArgs e)
        {
            OpenChildForm(new KursEkleme(panelDesktop), sender);
        }

        private void btn_OgretmenGoster_Click(object sender, EventArgs e)
        {
            OpenChildForm(new OgretmenKursAtama(panelDesktop), sender);
        }

        private void btn_OgrenciGoster_Click(object sender, EventArgs e)
        {
            OpenChildForm(new OgrenciKursAtama(panelDesktop), sender);
        }
        /// <summary>
        /// /////////////////////////////////////////////////
        /// </summary>
        /// 
        // SQL bağlantı dizesi
        // Veritabanı bağlantı dizesi


        private string connectionString = @"Server=DESKTOP-ECJJPMC\SQLEXPRESS;Database=deneme3;Trusted_Connection=True;";
        private int selectedCourseId = 0; // Seçili kursun ID'si
        // Kursları listeleme metodu
        private void KurslarıListele()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Courses";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }

        }


        // ComboBox içine Levels tablosundaki verileri doldurur
        private void LoadLevelsIntoComboBox()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT level_name FROM Levels";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            comboBox1.Items.Add(reader["level_name"].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        // Seçili level_name'e göre level_id'yi alır
        private int GetLevelId(string levelName)
        {
            int levelId = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT level_id FROM Levels WHERE level_name = @level_name";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@level_name", levelName);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            levelId = reader.GetInt32(0);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
            return levelId;
        }


        // Kurs ekleme butonu
        private void btn_Add_Click(object sender, EventArgs e)
        {
            // Formdan gelen değerleri al
            string courseName = txt_KursAd.Text;
            string courseType = txt_CourseType.Text;
            string courseLevel = comboBox1.SelectedItem?.ToString(); // Null kontrolü
            string description = txt_Acıklama.Text;
            int duration = (int)numericUpDown1.Value;
            string courseDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            int maxCapacity;

            // Boş alan ve kapasite kontrolü
            if (string.IsNullOrWhiteSpace(courseName) || string.IsNullOrWhiteSpace(courseType) || string.IsNullOrWhiteSpace(courseLevel) || string.IsNullOrWhiteSpace(description))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.");
                return;
            }

            if (!int.TryParse(txt_Kapasite.Text, out maxCapacity))
            {
                MessageBox.Show("Kapasite bir sayı olmalıdır.");
                return;
            }

            // Seviye ID'sini al
            int levelId = GetLevelId(courseLevel);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Stored Procedure çağrısı
                    using (SqlCommand command = new SqlCommand("sp_AddCourse", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Parametreleri ekle
                        command.Parameters.AddWithValue("@course_name", courseName);
                        command.Parameters.AddWithValue("@course_type", courseType);
                        command.Parameters.AddWithValue("@course_level", courseLevel);
                        command.Parameters.AddWithValue("@description", description);
                        command.Parameters.AddWithValue("@level_id", levelId);
                        command.Parameters.AddWithValue("@duration", duration);
                        command.Parameters.AddWithValue("@course_date", courseDate);
                        command.Parameters.AddWithValue("@max_capacity", maxCapacity);

                        // Sorguyu çalıştır
                        command.ExecuteNonQuery();
                        MessageBox.Show("Kurs başarıyla eklendi!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }

            // Kursları listeleme fonksiyonunu çağır
            KurslarıListele();
        }


        private void KursEkleme_Load(object sender, EventArgs e)
        {
            // Form yüklendiğinde kursları listele
            KurslarıListele();
        }

        private void btn_Del_Click(object sender, EventArgs e)
        {
            if (selectedCourseId == 0)
            {
                MessageBox.Show("Lütfen silmek için bir kurs seçin.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM Courses WHERE course_id = @course_id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@course_id", selectedCourseId);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Kurs başarıyla silindi!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
            KurslarıListele();
        }

        private void btn_Upd_Click(object sender, EventArgs e)
        {
            // Güncellenecek kursun seçilip seçilmediğini kontrol et
            if (selectedCourseId == 0)
            {
                MessageBox.Show("Lütfen güncellemek için bir kurs seçin.");
                return;
            }

            // Formdan gelen değerleri al
            string courseName = txt_KursAd.Text;
            string courseType = txt_CourseType.Text;
            string courseLevel = comboBox1.SelectedItem?.ToString(); // Null kontrolü
            string description = txt_Acıklama.Text;
            int duration = (int)numericUpDown1.Value;
            string courseDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            int maxCapacity;

            // Boş alan ve kapasite kontrolü
            if (string.IsNullOrWhiteSpace(courseName) || string.IsNullOrWhiteSpace(courseType) || string.IsNullOrWhiteSpace(courseLevel) || string.IsNullOrWhiteSpace(description))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.");
                return;
            }

            if (!int.TryParse(txt_Kapasite.Text, out maxCapacity))
            {
                MessageBox.Show("Kapasite bir sayı olmalıdır.");
                return;
            }

            // Seviye ID'sini al
            int levelId = GetLevelId(courseLevel);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Stored Procedure çağrısı
                    using (SqlCommand command = new SqlCommand("sp_UpdateCourse", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Parametreleri ekle
                        command.Parameters.AddWithValue("@course_id", selectedCourseId);
                        command.Parameters.AddWithValue("@course_name", courseName);
                        command.Parameters.AddWithValue("@course_type", courseType);
                        command.Parameters.AddWithValue("@course_level", courseLevel);
                        command.Parameters.AddWithValue("@description", description);
                        command.Parameters.AddWithValue("@level_id", levelId);
                        command.Parameters.AddWithValue("@duration", duration);
                        command.Parameters.AddWithValue("@course_date", courseDate);
                        command.Parameters.AddWithValue("@max_capacity", maxCapacity);

                        // Sorguyu çalıştır
                        command.ExecuteNonQuery();
                        MessageBox.Show("Kurs başarıyla güncellendi!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }

            // Kursları listeleme fonksiyonunu çağır
            KurslarıListele();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                selectedCourseId = Convert.ToInt32(row.Cells["course_id"].Value);
                txt_KursAd.Text = row.Cells["course_name"].Value.ToString();
                txt_CourseType.Text = row.Cells["course_type"].Value.ToString();
                comboBox1.SelectedItem = row.Cells["course_level"].Value.ToString();
                txt_Acıklama.Text = row.Cells["description"].Value.ToString();
                numericUpDown1.Value = Convert.ToInt32(row.Cells["duration"].Value);
                dateTimePicker1.Value = DateTime.Parse(row.Cells["course_Date"].Value.ToString());
                txt_Kapasite.Text = row.Cells["MaxCapacity"].Value.ToString();
            }
        }

        private void btn_Ara_Click(object sender, EventArgs e)
        {
            // txt_Search alanına girilen değerler datagridviewda bulunur ve listelenir 
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Courses WHERE course_name LIKE @course_name";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@course_name", "%" + txt_Search.Text + "%");
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }
    }
}
