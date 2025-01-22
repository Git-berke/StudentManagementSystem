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

namespace AnaKodYazılımAkademisi
{
    public partial class EskiEmailler : Form
    {
        private Panel panelDesktop;
        private Form activeForm;
        private string connectionString = "Data Source=DESKTOP-ECJJPMC\\SQLEXPRESS;Initial Catalog=deneme3;Integrated Security=True;";

        public EskiEmailler(Panel panelDesktop)
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
        private void LoadSentEmails()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Stored procedure çağrısı
                    using (SqlCommand command = new SqlCommand("GetSentEmails", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        // DataGridView'a veriyi yükle
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veritabanı hatası: " + ex.Message);
            }
        }



        private void EskiEmailler_Load(object sender, EventArgs e)
        {
            LoadSentEmails();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new MailYonetim(panelDesktop), sender);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                int emailNotificationId = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["email_notification_id"].Value);

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Stored procedure çağrısı
                        using (SqlCommand command = new SqlCommand("DeleteEmailNotification", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@EmailNotificationId", emailNotificationId);
                            command.ExecuteNonQuery();
                        }
                    }

                    // DataGridView'den satırı sil
                    dataGridView1.Rows.RemoveAt(selectedRowIndex);
                    MessageBox.Show("Email successfully deleted.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {



        }
    }
}
