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
using System.Net;
using System.Net.Mail;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace AnaKodYazılımAkademisi
{
    public partial class MailYonetim : Form
    {
        private string connectionString = "Data Source=DESKTOP-ECJJPMC\\SQLEXPRESS;Initial Catalog=deneme3;Integrated Security=True";
        private Panel panelDesktop;
        private Form activeForm;

        public MailYonetim(Panel panelDesktop)
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
        private void DataGridViewaGoster()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Öğrenci ve veli bilgilerini çekecek SQL sorgusu
                    string query = @"
                    SELECT s.student_id, s.first_name, s.last_name, s.email AS StudentEmail, 
                           g.name AS GuardianName, g.contact_number AS GuardianContact, g.email AS Gemail
                    FROM Students s
                    INNER JOIN Guardians g ON s.student_id = g.student_id";

                    // SQL komutunu oluşturuyoruz
                    SqlCommand command = new SqlCommand(query, connection);

                    // Verileri DataTable'a çekiyoruz
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    // DataGridView'a verileri bağlıyoruz
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanı hatası: " + ex.Message);
                }
            }
        }
        private void SendEmail(List<string> toEmails, string subject, string body)
        {
            try
            {
                // SMTP ayarları (Gmail örneği)
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("denemeanakod@gmail.com", "tamisvxvsdbrispv"),
                    EnableSsl = true
                };

                // MailMessage oluşturma
                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("anakoddeneme@gmail.com"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                // To adreslerini ekleyelim (toplu mail)
                foreach (var email in toEmails)
                {
                    mailMessage.To.Add(email);
                }

                // Mail gönderme
                smtpClient.Send(mailMessage);

                // Mail gönderildikten sonra veritabanına kaydedelim
                SaveEmailToDatabase(toEmails, subject, body);
                MessageBox.Show("Mail başarıyla gönderildi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void SaveEmailToDatabase(List<string> toEmails, string subject, string body)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    foreach (var toEmail in toEmails)
                    {
                        // Öğrenci ve veli ID'lerini al
                        int studentId = GetStudentIdByEmail(toEmail);
                        int guardianId = GetGuardianIdByEmail(toEmail);

                        // Gönderim türünü belirle
                        bool isStudentEmail = studentId > 0;
                        bool isGuardianEmail = guardianId > 0;

                        // Debug: Hangi e-posta hangi ID'lere eşleşiyor
                        Console.WriteLine($"Email: {toEmail}, Student ID: {studentId}, Guardian ID: {guardianId}");

                        // SQL sorgusunu oluştur
                        string query = @"
                    INSERT INTO EmailNotifications (student_id, guardian_id, subject, body, send_date, status, notification_type)
                    VALUES (@student_id, @guardian_id, @subject, @body, GETDATE(), 'Sent', 'Email')";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // Parametreleri belirle
                            command.Parameters.AddWithValue("@subject", subject);
                            command.Parameters.AddWithValue("@body", body);

                            if (isStudentEmail && isGuardianEmail)
                            {
                                // Hem öğrenci hem veli bilgisi var
                                command.Parameters.AddWithValue("@student_id", studentId);
                                command.Parameters.AddWithValue("@guardian_id", guardianId);
                            }
                            else if (isStudentEmail)
                            {
                                // Sadece öğrenci bilgisi var
                                command.Parameters.AddWithValue("@student_id", studentId);
                                command.Parameters.AddWithValue("@guardian_id", DBNull.Value);
                            }
                            else if (isGuardianEmail)
                            {
                                // Sadece veli bilgisi var
                                command.Parameters.AddWithValue("@student_id", DBNull.Value);
                                command.Parameters.AddWithValue("@guardian_id", guardianId);
                            }
                            else
                            {
                                // Hiçbiri eşleşmiyor
                                MessageBox.Show($"E-posta adresi {toEmail} ile eşleşen öğrenci veya veli bulunamadı.");
                                continue;
                            }

                            // Veritabanına kaydet
                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanı hatası: " + ex.Message);
                }
            }
        }





        private int GetStudentIdByEmail(string email)
        {
            // Bu metot, e-posta adresine göre öğrenci ID'sini döndürecek
            int studentId = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT student_id FROM Students WHERE email = @Email";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);

                var result = command.ExecuteScalar();
                if (result != null)
                {
                    studentId = Convert.ToInt32(result);
                }
            }
            return studentId;
        }

        private int GetGuardianIdByEmail(string email)
        {
            int guardianId = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT guardian_id FROM Guardians WHERE email = @Email";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);

                var result = command.ExecuteScalar();
                if (result != null)
                {
                    guardianId = Convert.ToInt32(result);
                }
            }
            return guardianId;
        }




        // Mail Gönder Butonuna Tıklanması

        private void MailYonetim_Load(object sender, EventArgs e)
        {
            DataGridViewaGoster();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string subject = textBox2.Text; // Mailin konusu
            string body = textBox1.Text; // Mailin içeriği

            // DataGridView'dan seçilen satırları alalım
            List<string> toEmails = new List<string>();

            // Seçilen satırları alıyoruz
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                // Öğrencinin e-posta adresini alıyoruz
                if (row.Cells["StudentEmail"].Value != null)
                {
                    string studentEmail = row.Cells["StudentEmail"].Value.ToString();
                    toEmails.Add(studentEmail);
                }

                // Velisinin e-posta adresini alıyoruz
                if (row.Cells["GEmail"].Value != null)
                {
                    string guardianEmail = row.Cells["GEmail"].Value.ToString();
                    toEmails.Add(guardianEmail);
                }
            }

            // Eğer e-posta adresi yoksa kullanıcıyı uyarıyoruz
            if (toEmails.Count == 0)
            {
                MessageBox.Show("Hiçbir e-posta adresi seçilmedi.");
                return;
            }

            // Mail gönderme fonksiyonunu çağırıyoruz
            SendEmail(toEmails, subject, body);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.SelectAll();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string subject = textBox2.Text; // Mailin konusu
            string body = textBox1.Text; // Mailin içeriği

            // Öğrencilere ait e-posta adreslerini alacağız
            List<string> studentEmails = new List<string>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Öğrencinin e-posta adresini alıyoruz
                if (row.Cells["StudentEmail"].Value != null)
                {
                    string studentEmail = row.Cells["StudentEmail"].Value.ToString();
                    studentEmails.Add(studentEmail);
                }
            }

            // Eğer e-posta adresi yoksa kullanıcıyı uyarıyoruz
            if (studentEmails.Count == 0)
            {
                MessageBox.Show("Hiçbir öğrenci e-posta adresi bulunamadı.");
                return;
            }

            // Öğrencilere mail gönderme fonksiyonunu çağırıyoruz
            SendEmail(studentEmails, subject, body);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string subject = textBox2.Text; // Mailin konusu
            string body = textBox1.Text; // Mailin içeriği

            // Velilere ait e-posta adreslerini alacağız
            List<string> guardianEmails = new List<string>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Velisinin e-posta adresini alıyoruz
                if (row.Cells["Gemail"].Value != null) // "Gemail" alanının veli e-posta olduğunu varsayıyorum
                {
                    string guardianEmail = row.Cells["Gemail"].Value.ToString();
                    guardianEmails.Add(guardianEmail);
                }
            }

            // Eğer e-posta adresi yoksa kullanıcıyı uyarıyoruz
            if (guardianEmails.Count == 0)
            {
                MessageBox.Show("Hiçbir veli e-posta adresi bulunamadı.");
                return;
            }

            // Velilere mail gönderme fonksiyonunu çağırıyoruz
            SendEmail(guardianEmails, subject, body);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string subject = textBox2.Text; // Mailin konusu
            string body = textBox1.Text; // Mailin içeriği

            // Seçilen satırlardaki öğrencilere ait e-posta adreslerini alacağız
            List<string> studentEmails = new List<string>();

            // Seçilen satırlarda işlem yap
            if (dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow selectedRow in dataGridView1.SelectedRows)
                {
                    // Öğrencinin e-posta adresini alıyoruz
                    if (selectedRow.Cells["StudentEmail"].Value != null)
                    {
                        string studentEmail = selectedRow.Cells["StudentEmail"].Value.ToString();
                        studentEmails.Add(studentEmail);
                    }
                }

                // Eğer hiç e-posta adresi bulunamadıysa kullanıcıyı uyarıyoruz
                if (studentEmails.Count == 0)
                {
                    MessageBox.Show("Seçilen öğrencilerin e-posta adresi bulunamadı.");
                    return;
                }

                // Öğrencilere mail gönderme fonksiyonunu çağırıyoruz
                SendEmail(studentEmails, subject, body);
            }
            else
            {
                MessageBox.Show("Lütfen bir veya birden fazla öğrenci seçin.");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string subject = textBox2.Text; // Mailin konusu
            string body = textBox1.Text; // Mailin içeriği

            // Seçilen satırlardaki velilere ait e-posta adreslerini alacağız
            List<string> guardianEmails = new List<string>();

            // Seçilen satırlarda işlem yap
            if (dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow selectedRow in dataGridView1.SelectedRows)
                {
                    // Velisinin e-posta adresini alıyoruz
                    if (selectedRow.Cells["Gemail"].Value != null)
                    {
                        string guardianEmail = selectedRow.Cells["Gemail"].Value.ToString();
                        guardianEmails.Add(guardianEmail);
                    }
                }

                // Eğer hiç e-posta adresi bulunamadıysa kullanıcıyı uyarıyoruz
                if (guardianEmails.Count == 0)
                {
                    MessageBox.Show("Seçilen velilerin e-posta adresi bulunamadı.");
                    return;
                }

                // Velilere mail gönderme fonksiyonunu çağırıyoruz
                SendEmail(guardianEmails, subject, body);
            }
            else
            {
                MessageBox.Show("Lütfen bir veya birden fazla öğrenci seçin.");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string subject = textBox2.Text; // Mailin konusu
            string body = textBox1.Text; // Mailin içeriği

            // Öğrencilerin ve velilerin e-posta adreslerini alalım
            List<string> allEmails = new List<string>();

            // DataGridView'daki tüm satırları gezelim
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Öğrencinin e-posta adresini alıyoruz
                if (row.Cells["StudentEmail"].Value != null)
                {
                    string studentEmail = row.Cells["StudentEmail"].Value.ToString();
                    allEmails.Add(studentEmail);
                }

                // Velisinin e-posta adresini alıyoruz
                if (row.Cells["Gemail"].Value != null)
                {
                    string guardianEmail = row.Cells["Gemail"].Value.ToString();
                    allEmails.Add(guardianEmail);
                }
            }

            // Eğer e-posta adresi yoksa kullanıcıyı uyarıyoruz
            if (allEmails.Count == 0)
            {
                MessageBox.Show("Hiçbir e-posta adresi bulunamadı.");
                return;
            }

            // Mail gönderme fonksiyonunu çağırıyoruz
            SendEmail(allEmails, subject, body);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenChildForm(new EskiEmailler(panelDesktop), sender);
        }
    }
}
