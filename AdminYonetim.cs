using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Text.RegularExpressions;


namespace AnaKodYazılımAkademisi
{
    public partial class AdminYonetim : Form
    {
        private string connectionString = "Data Source=DESKTOP-ECJJPMC\\SQLEXPRESS;Initial Catalog=deneme3;Integrated Security=True;";

        public AdminYonetim()
        {
            InitializeComponent();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (!IsValidEmail(txt_Email.Text))
            {
                MessageBox.Show("Geçersiz email formatı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Check if the username already exists
                    SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE username = @username", conn);
                    checkCmd.Parameters.AddWithValue("@username", txt_Username.Text);
                    int count = (int)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("Bu isimde bir kullanıcı zaten kayıtlı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Add new user
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Users (username, password, user_type_id, email) VALUES (@username, @password, (SELECT user_type_id FROM UserTypes WHERE user_type_name = @userTypeName), @email)", conn);
                    cmd.Parameters.AddWithValue("@username", txt_Username.Text);
                    cmd.Parameters.AddWithValue("@password", txt_password.Text);
                    cmd.Parameters.AddWithValue("@userTypeName", cmb_UserType.Text);
                    cmd.Parameters.AddWithValue("@email", txt_Email.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Yeni kullanıcı başarıyla eklendi!", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    KullanıcılarıListele();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        private void KullanıcılarıListele()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("sp_KullanıcılarıListele", conn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure; // Stored Procedure olduğunu belirtiyoruz.
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }


        private void btn_del_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen bir satır seçin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Silinecek kullanıcı ID'sini alıyoruz
                    int userId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["user_id"].Value);

                    // Kullanıcıyı silme sorgusu
                    SqlCommand cmd = new SqlCommand("DELETE FROM Users WHERE user_id = @userId", conn);
                    cmd.Parameters.AddWithValue("@userId", userId);

                    // Silme işlemi
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Kullanıcı başarılı bir şekilde silindi
                        MessageBox.Show("Kullanıcı başarıyla silindi!", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Log tablosunu kontrol edebilirsiniz (isteğe bağlı)
                        SqlCommand logCheckCmd = new SqlCommand("SELECT COUNT(*) FROM DeletedUsersLog WHERE user_id = @userId", conn);
                        logCheckCmd.Parameters.AddWithValue("@userId", userId);

                        int logCount = (int)logCheckCmd.ExecuteScalar();
                        if (logCount > 0)
                        {
                            MessageBox.Show("Silinen kullanıcı loglandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Silinen kullanıcı loglanamadı!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        // Listeyi yenile
                        KullanıcılarıListele();
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı silinemedi!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }


        private void btn_updt_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen bir satır seçin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsValidEmail(txt_Email.Text))
            {
                MessageBox.Show("Geçersiz email formatı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE Users SET username = @username, password = @password, user_type_id = (SELECT user_type_id FROM UserTypes WHERE user_type_name = @userTypeName), email = @email WHERE user_id = @userId", conn);
                    cmd.Parameters.AddWithValue("@username", txt_Username.Text);
                    cmd.Parameters.AddWithValue("@password", txt_password.Text);
                    cmd.Parameters.AddWithValue("@userTypeName", cmb_UserType.Text);
                    cmd.Parameters.AddWithValue("@email", txt_Email.Text);
                    cmd.Parameters.AddWithValue("@userId", Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["user_id"].Value));

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Kullanıcı başarıyla güncellendi!", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    KullanıcılarıListele();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txt_Username.Text = row.Cells["username"].Value.ToString();
                txt_Email.Text = row.Cells["email"].Value.ToString();
                cmb_UserType.Text = row.Cells["user_type_name"].Value.ToString();
                // Password is not displayed for security purposes.
            }
        }

        private void AdminYonetim_Load(object sender, EventArgs e)
        {
            KullanıcılarıListele();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT user_type_name FROM UserTypes", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        cmb_UserType.Items.Add(reader["user_type_name"].ToString());
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
