using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AnaKodYazılımAkademisi
{
    public partial class OgrenciKursAtama : Form
    {
        string connectionString = "Data Source=DESKTOP-ECJJPMC\\SQLEXPRESS;Initial Catalog=deneme3;Integrated Security=True;";

        private Panel panelDesktop;
        private Form activeForm;

        public OgrenciKursAtama(Panel panelDesktop)
        {
            InitializeComponent();
            this.panelDesktop = panelDesktop;
            comboBox_Status.SelectedItem = "tamamlanmadi";
            LoadCourses();
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

        private void LoadCourses()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT course_id, course_name + ' - ' + course_level AS CourseInfo FROM Courses", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    comboBox_MevcutKurslarıGoruntule.Items.Add(new
                    {
                        CourseID = reader.GetInt32(0),
                        CourseInfo = reader.GetString(1)
                    });
                }
            }
        }



        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null && comboBox_MevcutKurslarıGoruntule.SelectedItem != null)
            {
                var selectedStudent = listBox1.SelectedItem;
                var selectedCourse = comboBox_MevcutKurslarıGoruntule.SelectedItem;
                DateTime enrollmentDate = dateTimePicker1.Value;

                if (string.IsNullOrWhiteSpace(txt_PaymentAmount.Text) || cmb_PaymentType.SelectedItem == null)
                {
                    MessageBox.Show("Lütfen ödeme miktarını ve ödeme tipini giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal paymentAmount;
                if (!decimal.TryParse(txt_PaymentAmount.Text, out paymentAmount))
                {
                    MessageBox.Show("Geçerli bir ödeme miktarı giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string paymentType = cmb_PaymentType.SelectedItem.ToString();
                int? installmentCount = null;

                if (paymentType == "Taksitli")
                {
                    if (cmb_InstallmentCount.SelectedItem == null)
                    {
                        MessageBox.Show("Lütfen taksit sayısını seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    installmentCount = int.Parse(cmb_InstallmentCount.SelectedItem.ToString());
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // 1. Kapasite kontrolü
                    SqlCommand capacityCommand = new SqlCommand(
                        "SELECT (SELECT COUNT(*) FROM Enrollments WHERE course_id = @courseID) AS CurrentEnrollment, " +
                        "MaxCapacity FROM Courses WHERE course_id = @courseID", connection);
                    capacityCommand.Parameters.AddWithValue("@courseID", ((dynamic)selectedCourse).CourseID);

                    using (SqlDataReader reader = capacityCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int currentEnrollment = reader.GetInt32(0);
                            int maxCapacity = reader.GetInt32(1);

                            if (currentEnrollment >= maxCapacity)
                            {
                                MessageBox.Show("Bu kursun kapasitesi dolmuş durumda!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return; // Kapasite dolu olduğu için işlem durdurulur
                            }
                        }
                    }

                    // 2. Öğrencinin kursa kayıtlı olup olmadığını kontrol et
                    SqlCommand checkCommand = new SqlCommand(
                        "SELECT COUNT(*) FROM Enrollments WHERE student_id = @studentID AND course_id = @courseID", connection);
                    checkCommand.Parameters.AddWithValue("@studentID", ((dynamic)selectedStudent).StudentID);
                    checkCommand.Parameters.AddWithValue("@courseID", ((dynamic)selectedCourse).CourseID);

                    int enrollmentCount = (int)checkCommand.ExecuteScalar();

                    if (enrollmentCount > 0)
                    {
                        MessageBox.Show("Bu öğrenci zaten bu kursa kayıtlı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return; // İşlemi durdur
                    }

                    // 3. Öğrenci kursa eklenir
                    SqlCommand command = new SqlCommand(
                        "INSERT INTO Enrollments (student_id, course_id, enrollment_date, completion_status) " +
                        "VALUES (@studentID, @courseID, @enrollmentDate, 'tamamlandi')", connection);
                    command.Parameters.AddWithValue("@studentID", ((dynamic)selectedStudent).StudentID);
                    command.Parameters.AddWithValue("@courseID", ((dynamic)selectedCourse).CourseID);
                    command.Parameters.AddWithValue("@enrollmentDate", enrollmentDate);

                    command.ExecuteNonQuery();

                    // 4. Ödeme detaylarını ekle
                    SqlCommand paymentCommand = new SqlCommand(
                        "INSERT INTO Payments (student_id, amount, payment_date, payment_status, payment_type, notes, installment_count, installment_amount, total_paid) " +
                        "VALUES (@studentID, @amount, @paymentDate, @paymentStatus, @paymentType, @notes, @installmentCount, @installmentAmount, @totalPaid)", connection);
                    paymentCommand.Parameters.AddWithValue("@studentID", ((dynamic)selectedStudent).StudentID);
                    paymentCommand.Parameters.AddWithValue("@amount", paymentAmount);
                    paymentCommand.Parameters.AddWithValue("@paymentDate", DBNull.Value);
                    paymentCommand.Parameters.AddWithValue("@paymentStatus", "Ödenmedi");
                    paymentCommand.Parameters.AddWithValue("@paymentType", paymentType);
                    paymentCommand.Parameters.AddWithValue("@notes", paymentType == "Taksitli" ? "Taksit ödeme planı" : "Peşin ödeme");
                    paymentCommand.Parameters.AddWithValue("@installmentCount", paymentType == "Taksitli" ? installmentCount : (object)DBNull.Value);
                    paymentCommand.Parameters.AddWithValue("@installmentAmount", paymentType == "Taksitli" ? paymentAmount / installmentCount : (object)DBNull.Value);
                    paymentCommand.Parameters.AddWithValue("@totalPaid", 0);

                    paymentCommand.ExecuteNonQuery();

                    MessageBox.Show("Öğrenci başarıyla kursa ve ödeme planına kaydedildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir öğrenci ve bir kurs seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // DataGridView'i güncelle
            comboBox_KursAtamaları_SelectedIndexChanged_1(sender, e);
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

        private void LoadAvailableCourses()
        {
            comboBox_KursAtamaları.Items.Clear();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT course_id, course_name + ' - ' + course_level AS CourseInfo FROM Courses", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    comboBox_KursAtamaları.Items.Add(new
                    {
                        CourseID = reader.GetInt32(0),
                        CourseInfo = reader.GetString(1)
                    });
                }
            }
            comboBox_KursAtamaları.DisplayMember = "CourseInfo";
            comboBox_KursAtamaları.ValueMember = "CourseID";
        }

        private void comboBox_KursAtamaları_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear(); // DataGridView'i temizle
            dataGridView1.Columns.Clear(); // Sütunları temizle

            // DataGridView için yeni sütunları oluştur
            dataGridView1.Columns.Add("EnrollmentID", "Enrollment ID");
            dataGridView1.Columns.Add("StudentName", "Ad Soyad");
            dataGridView1.Columns.Add("Email", "E-posta");
            dataGridView1.Columns.Add("Level", "Seviye");
            dataGridView1.Columns.Add("EnrollmentDate", "Kayıt Tarihi");

            var selectedCourse = comboBox_KursAtamaları.SelectedItem;
            if (selectedCourse != null)
            {
                dynamic course = selectedCourse;
                label6.Text = $"Kurs Adı: {course.CourseInfo.Split('-')[0].Trim()}, Seviye: {course.CourseInfo.Split('-')[1].Trim()}";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(
                        "SELECT e.enrollment_id, " +
                        "       s.first_name + ' ' + s.last_name AS StudentName, " +
                        "       s.email, " +
                        "       s.level, " +
                        "       e.enrollment_date " +
                        "FROM Enrollments e " +
                        "JOIN Students s ON e.student_id = s.student_id " +
                        "WHERE e.course_id = @courseID", connection);
                    command.Parameters.AddWithValue("@courseID", course.CourseID);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add(
                            reader.GetInt32(0), // Enrollment ID
                            reader.GetString(1), // Ad Soyad
                            reader.GetString(2), // Email
                            reader.GetString(3), // Seviye
                            reader.GetDateTime(4).ToShortDateString() // Kayıt Tarihi
                        );
                    }
                }
            }
            else
            {
                label6.Text = "Seçilen kurs bilgisi yok.";
            }
        }


        private void InitializeDataGridView()
        {
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add("EnrollmentID", "Enrollment ID");
            dataGridView1.Columns.Add("StudentName", "Student Name");
            dataGridView1.Columns.Add("EnrollmentDate", "Enrollment Date");

            dataGridView1.Columns["EnrollmentID"].Visible = false; // ID'yi gizleyebilirsiniz
        }

        private void txt_Search_TextChanged_1(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // Level bilgisini de çekiyoruz
                SqlCommand command = new SqlCommand(
                    "SELECT student_id, first_name + ' ' + last_name AS FullName, level " +
                    "FROM Students " +
                    "WHERE first_name LIKE @searchText OR last_name LIKE @searchText", connection);
                command.Parameters.AddWithValue("@searchText", $"%{txt_Search.Text}%");

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    // Ad Soyad ve Level bilgisini birleştirerek ekliyoruz
                    listBox1.Items.Add(new
                    {
                        StudentID = reader.GetInt32(0),
                        FullName = $"{reader.GetString(1)} - Seviye: {reader.GetString(2)}"
                    });
                }
            }
        }


        private void OgrenciKursAtama_Load(object sender, EventArgs e)
        {
            InitializeDataGridView();
            LoadAvailableCourses();
            cmb_InstallmentCount.Enabled = false; // Başlangıçta pasif
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Seçili kayıtları silmek istediğinizden emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        int enrollmentID = Convert.ToInt32(row.Cells["EnrollmentID"].Value);
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            // Get student_id before deleting enrollment
                            SqlCommand getStudentIDCommand = new SqlCommand("SELECT student_id FROM Enrollments WHERE enrollment_id = @enrollmentID", connection);
                            getStudentIDCommand.Parameters.AddWithValue("@enrollmentID", enrollmentID);
                            int studentID = (int)getStudentIDCommand.ExecuteScalar();

                            // Delete from Enrollments
                            SqlCommand deleteEnrollmentCommand = new SqlCommand("DELETE FROM Enrollments WHERE enrollment_id = @enrollmentID", connection);
                            deleteEnrollmentCommand.Parameters.AddWithValue("@enrollmentID", enrollmentID);
                            deleteEnrollmentCommand.ExecuteNonQuery();

                            // Delete from Payments
                            SqlCommand deletePaymentCommand = new SqlCommand("DELETE FROM Payments WHERE student_id = @studentID", connection);
                            deletePaymentCommand.Parameters.AddWithValue("@studentID", studentID);
                            deletePaymentCommand.ExecuteNonQuery();
                        }
                        dataGridView1.Rows.Remove(row); // DataGridView'den de sil
                    }
                    MessageBox.Show("Seçili kayıtlar başarıyla silindi.");
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek için bir veya daha fazla satır seçin.");
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void cmb_PaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_PaymentType.SelectedItem != null && cmb_PaymentType.SelectedItem.ToString() == "Taksitli")
            {
                cmb_InstallmentCount.Enabled = true;
            }
            else
            {
                cmb_InstallmentCount.Enabled = false;
                cmb_InstallmentCount.SelectedItem = null; // Varsayılan olarak temizle
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
