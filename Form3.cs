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
    public partial class Form3 : Form
    {

        string connectionString = "Data Source=DESKTOP-ECJJPMC\\SQLEXPRESS;Initial Catalog=deneme3;Integrated Security=True;";

        public Form3()
        {
            InitializeComponent();
            LoadCoursesIntoComboBox();
            LoadSuggestionsToGrid();
            LoadSuggestionsIntoComboBox();
        }

        private void LoadCoursesIntoComboBox()
        {
            try
            {
                string query = "SELECT course_id, course_name FROM Courses";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            comboBox1.Items.Clear();
                            comboBox2.Items.Clear();

                            while (reader.Read())
                            {
                                // Add items as KeyValuePairs (optional: for ID and Name binding)
                                comboBox1.Items.Add(new KeyValuePair<int, string>(
                                    reader.GetInt32(0), // CourseId
                                    reader.GetString(1) // CourseName
                                ));
                                comboBox2.Items.Add(new KeyValuePair<int, string>(
                                    reader.GetInt32(0), // CourseId
                                    reader.GetString(1) // CourseName
                                ));
                            }
                        }
                    }
                }

                // Optional: Set display member (if using KeyValuePair)
                comboBox1.DisplayMember = "Value";
                comboBox1.ValueMember = "Key";
                comboBox2.DisplayMember = "Value";
                comboBox2.ValueMember = "Key";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching courses: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSuggestionsIntoComboBox()
        {
            try
            {
                string query = "SELECT id, finished_course_id, suggested_course_id FROM Suggestions";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            comboBox3.Items.Clear();

                            while (reader.Read())
                            {
                                int finishedCourseId = reader.GetInt32(1);
                                int suggestedCourseId = reader.GetInt32(2);

                                // Get the course names using the GetCourseNameById method
                                string finishedCourseName = GetCourseNameById(finishedCourseId);
                                string suggestedCourseName = GetCourseNameById(suggestedCourseId);

                                // Construct the display string in the format "id: finished_course_name -> suggested_course_name"
                                string suggestionText = $"{reader.GetInt32(0)}: {finishedCourseName} -> {suggestedCourseName}";

                                // Add the formatted string to the comboBox
                                comboBox3.Items.Add(suggestionText);
                            }
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching suggestions: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetCourseNameById(int courseId)
        {
            try
            {
                string query = "SELECT course_name FROM Courses WHERE course_id = @CourseId";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CourseId", courseId);
                        return cmd.ExecuteScalar() as string; // Returns the course name
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching course name: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }


        private void LoadSuggestionsToGrid()
        {
            try
            {
                // Define the query to fetch all suggestions
                string query = "SELECT id AS id, finished_course_id AS bitirilen_kurs, suggested_course_id AS onerilen_kurs, " +
                               "mail_sent AS mail_gonderildi, created_at AS tarih FROM Suggestions";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            // Fill a DataTable with the query results
                            DataTable suggestionsTable = new DataTable();
                            adapter.Fill(suggestionsTable);

                            // Disable auto-generating columns
                            dataGridView1.AutoGenerateColumns = false;

                            // Map DataGridView columns to DataTable columns
                            dataGridView1.Columns["id"].DataPropertyName = "id";
                            dataGridView1.Columns["bitirilen_kurs"].DataPropertyName = "bitirilen_kurs";
                            dataGridView1.Columns["onerilen_kurs"].DataPropertyName = "onerilen_kurs";
                            dataGridView1.Columns["mail_gonderildi"].DataPropertyName = "mail_gonderildi";
                            dataGridView1.Columns["tarih"].DataPropertyName = "tarih";

                            // Bind the DataTable to the DataGridView
                            dataGridView1.DataSource = suggestionsTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Lütfen bir bitirilen kurs seçin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Lütfen bir önerilen kurs seçin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int finishedCourseId = ((KeyValuePair<int, string>)comboBox1.SelectedItem).Key;
            int suggestedCourseId = ((KeyValuePair<int, string>)comboBox2.SelectedItem).Key;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Öneriyi ekle
                    string insertQuery = "INSERT INTO Suggestions (finished_course_id, suggested_course_id, created_at, mail_sent) " +
                                         "VALUES (@finishedCourseId, @suggestedCourseId, GETDATE(), 0)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@finishedCourseId", finishedCourseId);
                        cmd.Parameters.AddWithValue("@suggestedCourseId", suggestedCourseId);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Öneri başarıyla kursa atandı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Öneri kaydı başarısız oldu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    // Tetikleyici oluşturma adımı kaldırıldı veya pasifleştirildi
                    // Eğer tetikleyici SQL'de hazırsa, buraya kod eklemenize gerek yok.

                }

                // UI'yi yenile
                LoadSuggestionsToGrid();
                LoadSuggestionsIntoComboBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int suggestionId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id"].Value);
                    string finishedCourseName = dataGridView1.SelectedRows[0].Cells["bitirilen_kurs"].Value.ToString();
                    string suggestedCourseName = dataGridView1.SelectedRows[0].Cells["onerilen_kurs"].Value.ToString();

                    DialogResult result = MessageBox.Show("Bu kaydı silmek istediğinizden emin misiniz?", "Onay",
                                                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            conn.Open();

                            // Step 1: Delete the trigger
                            string triggerName = $"TR_Suggestion_{finishedCourseName}_To_{suggestedCourseName}"
                                            .Replace(" ", "_")  // Replace spaces with underscores
                                            .Replace("-", "_");  // Replace hyphens with underscores
                            string dropTriggerQuery = $"IF OBJECT_ID('{triggerName}', 'TR') IS NOT NULL DROP TRIGGER {triggerName}";

                            using (SqlCommand dropTriggerCmd = new SqlCommand(dropTriggerQuery, conn))
                            {
                                dropTriggerCmd.ExecuteNonQuery();
                            }

                            // Step 2: Delete the related records from StudentSuggestions
                            string deleteStudentSuggestionsQuery = @"
                        DELETE FROM StudentSuggestions
                        WHERE finished_course_id = (SELECT finished_course_id FROM Suggestions WHERE id = @suggestionId)
                          AND suggested_course_id = (SELECT suggested_course_id FROM Suggestions WHERE id = @suggestionId)";

                            using (SqlCommand deleteStudentSuggestionsCmd = new SqlCommand(deleteStudentSuggestionsQuery, conn))
                            {
                                deleteStudentSuggestionsCmd.Parameters.AddWithValue("@suggestionId", suggestionId);
                                deleteStudentSuggestionsCmd.ExecuteNonQuery();
                            }

                            // Step 3: Delete the suggestion from the Suggestions table
                            string deleteQuery = "DELETE FROM Suggestions WHERE id = @suggestionId";
                            using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn))
                            {
                                deleteCmd.Parameters.AddWithValue("@suggestionId", suggestionId);

                                int rowsAffected = deleteCmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Kayıt başarıyla silindi.", "Başarılı",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Kayıt silinemedi.", "Hata",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }

                        // Refresh the UI
                        LoadSuggestionsToGrid();
                        LoadSuggestionsIntoComboBox();
                    }
                }
                else
                {
                    MessageBox.Show("Silmek için bir kayıt seçin.", "Uyarı",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Ensure an item is selected
                if (comboBox3.SelectedItem == null)
                {
                    return;
                }

                // Extract the suggestion ID from the selected text (format: "id: finishedCourse -> suggestedCourse")
                string selectedSuggestion = comboBox3.SelectedItem.ToString();
                int suggestionId = int.Parse(selectedSuggestion.Split(':')[0]);

                // Clear the ListBox
                Sonuçlar.Items.Clear();

                // Query to get suggested students
                string query = @"
                SELECT s.student_id, s.first_name
                FROM Students s
                INNER JOIN StudentSuggestions ss ON ss.student_id = s.student_id
                INNER JOIN Suggestions sug ON ss.suggestion_id = sug.id
                WHERE ss.suggestion_id = @suggestionId;";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@suggestionId", suggestionId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int studentId = reader.GetInt32(0);
                                string studentName = reader.GetString(1);

                                // Add to ListBox in the format: "ID: StudentName"
                                Sonuçlar.Items.Add($"ID: {studentId} - {studentName}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void Sonuçlar_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
