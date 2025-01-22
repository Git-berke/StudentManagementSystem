using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Mail;
using System.Linq;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth;
using Google.Apis.Util.Store;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using DocumentFormat.OpenXml.Spreadsheet;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
namespace AnaKodYazılımAkademisi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox_password.UseSystemPasswordChar = true; // Hide password initially
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox_password.UseSystemPasswordChar = false; // Show password
                checkBox1.Text = "Parolayı Gizle";
            }
            else
            {
                textBox_password.UseSystemPasswordChar = true; // Hide password
                checkBox1.Text = "Parolayı Göster";
            }
        }

        private void button_login_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-ECJJPMC\\SQLEXPRESS;Initial Catalog=deneme3;Integrated Security=True"))
            {
                try
                {
                    conn.Open();

                    // Kullanıcı adı ve şifre doğrulaması
                    string query = @"SELECT u.user_id, u.username, ut.user_type_name 
                                     FROM Users u 
                                     INNER JOIN UserTypes ut ON u.user_type_id = ut.user_type_id 
                                     WHERE u.username = @username AND u.password = @password";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", textBox_userName.Text);
                    cmd.Parameters.AddWithValue("@password", textBox_password.Text); // Şifreyi hash'lemek önemlidir.

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        int userId = reader.GetInt32(0);
                        string userName = reader.GetString(1);
                        string userType = reader.GetString(2);

                        // Başarılı giriş
                        MessageBox.Show("Giriş başarılı!", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Form2'yi uygun parametrelerle aç
                        Form2 form2 = new Form2(userId, userName, userType);
                        form2.Show();
                        this.Hide();
                    }
                    else
                    {
                        // Giriş başarısız
                        MessageBox.Show("Kullanıcı adı veya şifre yanlış!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buton_cıkıs_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Çıkmak istediğinize emin misiniz?", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        private void SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                using (System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587))
                {
                    client.Credentials = new System.Net.NetworkCredential("anakoddeneme@gmail.com", "xxryjgzuejuzmyfg"); // Kendi e-posta ve şifrenizi kullanın
                    client.EnableSsl = true;

                    using (System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage())
                    {
                        mail.From = new System.Net.Mail.MailAddress("anakoddeneme@gmail.com");
                        mail.To.Add(toEmail);
                        mail.Subject = subject;
                        mail.Body = body;

                        client.Send(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("E-posta gönderilirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string GenerateRandomPassword(int length)
        {
            const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(validChars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private void SendResetPassword(string email)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-ECJJPMC\\SQLEXPRESS;Initial Catalog=deneme3;Integrated Security=True"))
            {
                try
                {
                    conn.Open();

                    // Kullanıcı e-posta adresini kontrol et
                    SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE email = @Email", conn);
                    cmd.Parameters.AddWithValue("@Email", email);

                    int emailExists = (int)cmd.ExecuteScalar();

                    if (emailExists > 0)
                    {
                        // Rastgele bir şifre oluştur
                        string newPassword = GenerateRandomPassword(8);

                        // Kullanıcının şifresini güncelle
                        SqlCommand updateCmd = new SqlCommand("UPDATE Users SET password = @Password WHERE email = @Email", conn);
                        updateCmd.Parameters.AddWithValue("@Password", newPassword); // Şifreyi hash'lemek önemlidir.
                        updateCmd.Parameters.AddWithValue("@Email", email);

                        updateCmd.ExecuteNonQuery();

                        // Yeni şifreyi kullanıcıya mail gönder
                        SendEmail(email, "Şifre Sıfırlama", $"Yeni şifreniz: {newPassword}");

                        MessageBox.Show("Şifreniz sıfırlandı. Lütfen e-postanızı kontrol edin.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Bu e-posta adresi sistemde kayıtlı değil.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            linkLabel1.Visible = false;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            // Yeni bir form elemanı göstererek kullanıcıdan e-posta isteyin
            string userEmail = textBox1.Text;

            if (!string.IsNullOrEmpty(userEmail))
            {
                // Kullanıcı e-posta girdiyse işleme devam
                SendResetPassword(userEmail);
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            textBox1.Visible = true;
            linkLabel1.Visible = true;
        }
        private async Task<bool> CheckUserInDatabase(string email)
        {
            string connectionString = "Data Source=DESKTOP-ECJJPMC\\SQLEXPRESS;Initial Catalog=deneme3;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();

                string query = "SELECT COUNT(*) FROM Users WHERE email = @Email";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);

                    int userCount = (int)await cmd.ExecuteScalarAsync();
                    return userCount > 0; // Kullanıcı varsa true döner
                }
            }
        }
        private async Task<(int UserId, string UserName, string UserType)> GetUserFromDatabase(string email)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-ECJJPMC\\SQLEXPRESS;Initial Catalog=deneme3;Integrated Security=True"))
            {
                await conn.OpenAsync();

                string query = @"SELECT u.user_id, u.username, ut.user_type_name 
                         FROM Users u 
                         INNER JOIN UserTypes ut ON u.user_type_id = ut.user_type_id 
                         WHERE u.email = @Email";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);

                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                if (reader.Read())
                {
                    int userId = reader.GetInt32(0);
                    string userName = reader.GetString(1);
                    string userType = reader.GetString(2);

                    return (userId, userName, userType);  // Kullanıcı bilgilerini döndür
                }
                else
                {
                    return (0, null, null);  // Kullanıcı bulunamazsa (0, null, null) döndür
                }
            }
        }
        private async void button1_Click(object sender, EventArgs e)
        {

            try
            {
                string clientId = "106666550402-nttbi4no0jfaj0gplelhfj3m3qbuu9n9.apps.googleusercontent.com";
                string clientSecret = "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";
                string dataStorePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), ".credentials/GoogleAuth");

                var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    new ClientSecrets
                    {
                        ClientId = clientId,
                        ClientSecret = clientSecret
                    },
                    new[] { "email", "profile" },
                    "user",
                    CancellationToken.None,
                    new NullDataStore()); // Yetkilendirme bilgilerini saklamaz

                // Kullanıcı bilgilerini al
                var payload = await GoogleJsonWebSignature.ValidateAsync(credential.Token.IdToken);

                // Veritabanında kontrol et
                string userEmail = payload.Email;
                var userData = await GetUserFromDatabase(userEmail);  // Kullanıcı bilgilerini veritabanından al

                // userData'daki UserId'yi kontrol edin, null değilse işlem yapın
                if (userData.UserId != 0 && !string.IsNullOrEmpty(userData.UserName) && !string.IsNullOrEmpty(userData.UserType))
                {
                    // Kullanıcıyı bulduk, bilgilerini al
                    int userId = userData.UserId;
                    string userName = userData.UserName;
                    string userType = userData.UserType;

                    MessageBox.Show($"Hoş geldiniz, {userName} ({userEmail})!");

                    // Form2'yi uygun parametrelerle aç
                    Form2 form2 = new Form2(userId, userName, userType);
                    form2.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Bu email ile kayıtlı bir kullanıcı bulunamadı. Lütfen kaydolun.");
                    // Kullanıcıyı kayıt ekranına yönlendirin veya yeni kullanıcı oluşturun
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Giriş sırasında bir hata oluştu: {ex.Message}");
            }
        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                string clientId = "106666550402-nttbi4no0jfaj0gplelhfj3m3qbuu9n9.apps.googleusercontent.com";
                string clientSecret = "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";
                string dataStorePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), ".credentials/GoogleAuth");

                var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    new ClientSecrets
                    {
                        ClientId = clientId,
                        ClientSecret = clientSecret
                    },
                    new[] { "email", "profile" },
                    "user",
                    CancellationToken.None,
                    new NullDataStore()); // Yetkilendirme bilgilerini saklamaz

                // Kullanıcı bilgilerini al
                var payload = await GoogleJsonWebSignature.ValidateAsync(credential.Token.IdToken);

                // Veritabanında kontrol et
                string userEmail = payload.Email;
                var userData = await GetUserFromDatabase(userEmail);  // Kullanıcı bilgilerini veritabanından al

                // userData'daki UserId'yi kontrol edin, null değilse işlem yapın
                if (userData.UserId != 0 && !string.IsNullOrEmpty(userData.UserName) && !string.IsNullOrEmpty(userData.UserType))
                {
                    // Kullanıcıyı bulduk, bilgilerini al
                    int userId = userData.UserId;
                    string userName = userData.UserName;
                    string userType = userData.UserType;

                    MessageBox.Show($"Hoş geldiniz, {userName} ({userEmail})!");

                    // Form2'yi uygun parametrelerle aç
                    Form2 form2 = new Form2(userId, userName, userType);
                    form2.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Bu email ile kayıtlı bir kullanıcı bulunamadı. Lütfen kaydolun.");
                    // Kullanıcıyı kayıt ekranına yönlendirin veya yeni kullanıcı oluşturun
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Giriş sırasında bir hata oluştu: {ex.Message}");
            }
        }
    }
}
