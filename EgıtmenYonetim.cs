using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;
using System.Text.RegularExpressions;


namespace AnaKodYazılımAkademisi
{
    public partial class EgıtmenYonetim : Form
    {
        public EgıtmenYonetim()
        {
            InitializeComponent();
        }

        SqlConnection SqlConnection = new SqlConnection("Data Source=DESKTOP-ECJJPMC\\SQLEXPRESS;Initial Catalog=deneme3;Integrated Security=True");

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // buraya excele aktarım kodları gelecek
        }

        private void EgitmenleriListele()
        {
            try
            {
                // Bağlantının açık olup olmadığını kontrol et
                if (SqlConnection.State != ConnectionState.Open)
                {
                    SqlConnection.Open();
                }

                // Stored Procedure çağrısı
                using (SqlCommand sqlCommand = new SqlCommand("sp_GetInstructors", SqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure; // Stored Procedure olarak ayarla

                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable; // DataGridView'i doldur
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                // Bağlantıyı kapat
                if (SqlConnection.State == ConnectionState.Open)
                {
                    SqlConnection.Close();
                }
            }
        }


        private void btn_add_Click(object sender, EventArgs e)
        {
            string ad = txt_ad.Text;
            string telefon = txt_telefon.Text;
            string email = txt_email.Text;

            if (string.IsNullOrEmpty(ad) || string.IsNullOrEmpty(telefon))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.");
                return;
            }

            if (!IsValidEmail(email))
            {
                MessageBox.Show("Geçersiz email formatı.");
                return;
            }

            try
            {
                // Bağlantının açık olup olmadığını kontrol et
                if (SqlConnection.State != ConnectionState.Open)
                {
                    SqlConnection.Open();
                }

                // using bloğu ile SqlCommand yönetimi
                using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Instructors (name, contact_number, email) VALUES (@name, @contact_number, @email)", SqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@name", ad);
                    sqlCommand.Parameters.AddWithValue("@contact_number", telefon);
                    sqlCommand.Parameters.AddWithValue("@email", email);

                    int result = sqlCommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Eğitmen başarıyla eklendi.");
                        EgitmenleriListele(); // Başarılıysa listeyi güncelle
                    }
                    else
                    {
                        MessageBox.Show("Eğitmen eklenirken bir hata oluştu.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                // Bağlantıyı kapat
                if (SqlConnection.State == ConnectionState.Open)
                {
                    SqlConnection.Close();
                }
            }
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Lütfen silmek istediğiniz eğitmeni seçin.");
                return;
            }

            string ad = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            string telefon = dataGridView1.CurrentRow.Cells[1].Value.ToString();

            try
            {
                SqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("DELETE FROM Instructors WHERE name = @name AND contact_number = @contact_number", SqlConnection);
                sqlCommand.Parameters.AddWithValue("@name", ad);
                sqlCommand.Parameters.AddWithValue("@contact_number", telefon);

                int result = sqlCommand.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show("Eğitmen başarıyla silindi.");
                    EgitmenleriListele();
                }
                else
                {
                    MessageBox.Show("Eğitmen silinirken bir hata oluştu.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                SqlConnection.Close();
            }
            EgitmenleriListele();
        }

        private void btn_upd_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Lütfen güncellemek istediğiniz eğitmeni seçin.");
                return;
            }

            string eskiAd = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            string eskiTelefon = dataGridView1.CurrentRow.Cells[1].Value.ToString();

            string yeniAd = txt_ad.Text;
            string yeniTelefon = txt_telefon.Text;
            string yeniEmail = txt_email.Text;

            if (string.IsNullOrEmpty(yeniAd) || string.IsNullOrEmpty(yeniTelefon))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.");
                return;
            }

            if (!IsValidEmail(yeniEmail))
            {
                MessageBox.Show("Geçersiz email formatı.");
                return;
            }

            try
            {
                SqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("UPDATE Instructors SET name = @newName, contact_number = @newContactNumber, email = @newEmail WHERE name = @oldName AND contact_number = @oldContactNumber", SqlConnection);
                sqlCommand.Parameters.AddWithValue("@newName", yeniAd);
                sqlCommand.Parameters.AddWithValue("@newContactNumber", yeniTelefon);
                sqlCommand.Parameters.AddWithValue("@newEmail", yeniEmail);
                sqlCommand.Parameters.AddWithValue("@oldName", eskiAd);
                sqlCommand.Parameters.AddWithValue("@oldContactNumber", eskiTelefon);

                int result = sqlCommand.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show("Eğitmen bilgileri başarıyla güncellendi.");
                    EgitmenleriListele();
                }
                else
                {
                    MessageBox.Show("Eğitmen bilgileri güncellenirken bir hata oluştu.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                SqlConnection.Close();
            }
            EgitmenleriListele();
        }

        private void btn_temizle_Click(object sender, EventArgs e)
        {
            //textboxların içinin temizlenmesi
            txt_ad.Text = "";
            txt_telefon.Text = "";
            txt_email.Text = "";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // datagridviewda seçili olab hücreyi textboxlarımın içine taşısın
            txt_ad.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txt_telefon.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txt_email.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }

        private void EgıtmenYonetim_Load(object sender, EventArgs e)
        {
            EgitmenleriListele();
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
       

      

        private void txt_telefon_TextChanged_1(object sender, EventArgs e)
        {
            // 11 haneden fazla giriş yapılmasını engelle
            if (txt_telefon.Text.Length > 11)
            {
                txt_telefon.Text = txt_telefon.Text.Substring(0, 11);
                txt_telefon.SelectionStart = txt_telefon.Text.Length; // İmleci sona taşı
            }
        }

        private void txt_telefon_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            // Sadece rakam ve kontrol tuşlarına izin ver
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
