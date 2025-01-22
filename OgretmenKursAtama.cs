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
    public partial class OgretmenKursAtama : Form
    {
        private SqlConnection connection;
        private SqlDataAdapter adapter;
        private DataTable instructorsTable;
        private DataTable coursesTable;
        private DataTable assignmentsTable;
        private Panel panelDesktop;
        private Form activeForm;

        public OgretmenKursAtama(Panel panelDesktop)
        {
            InitializeComponent();
            this.panelDesktop = panelDesktop;
            connection = new SqlConnection("Server=DESKTOP-ECJJPMC\\SQLEXPRESS;Database=deneme3;Trusted_Connection=True");
            LoadInstructors();
            LoadCourses();
            LoadAssignments();
        }

        private void LoadInstructors()
        {
            string query = "SELECT instructor_id, name FROM Instructors";
            adapter = new SqlDataAdapter(query, connection);
            instructorsTable = new DataTable();
            adapter.Fill(instructorsTable);
            cmb_Egitmenler.DataSource = instructorsTable;
            cmb_Egitmenler.DisplayMember = "name";
            cmb_Egitmenler.ValueMember = "instructor_id";
        }

        private void LoadCourses()
        {
            string query = "SELECT course_id, course_name + ' - ' + course_level AS course_info FROM Courses";
            adapter = new SqlDataAdapter(query, connection);
            coursesTable = new DataTable();
            adapter.Fill(coursesTable);
            cmb_Kurslar.DataSource = coursesTable;
            cmb_Kurslar.DisplayMember = "course_info";
            cmb_Kurslar.ValueMember = "course_id";
        }

        private void LoadAssignments()
        {
            string query = @"SELECT ia.instructor_id, i.name AS instructor_name, ia.course_id, c.course_name, ia.assignment_date 
                                 FROM InstructorAssignments ia
                                 JOIN Instructors i ON ia.instructor_id = i.instructor_id
                                 JOIN Courses c ON ia.course_id = c.course_id";
            adapter = new SqlDataAdapter(query, connection);
            assignmentsTable = new DataTable();
            adapter.Fill(assignmentsTable);
            dataGridView1.DataSource = assignmentsTable;
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            int instructorId = (int)cmb_Egitmenler.SelectedValue;
            int courseId = (int)cmb_Kurslar.SelectedValue;
            DateTime assignmentDate = dateTimePicker1.Value;

            string query = "INSERT INTO InstructorAssignments (instructor_id, course_id, assignment_date) VALUES (@instructorId, @courseId, @assignmentDate)";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@instructorId", instructorId);
                command.Parameters.AddWithValue("@courseId", courseId);
                command.Parameters.AddWithValue("@assignmentDate", assignmentDate);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            LoadAssignments();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                cmb_Egitmenler.SelectedValue = row.Cells["instructor_id"].Value;
                cmb_Kurslar.SelectedValue = row.Cells["course_id"].Value;
                dateTimePicker1.Value = (DateTime)row.Cells["assignment_date"].Value;
            }
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int instructorId = (int)dataGridView1.SelectedRows[0].Cells["instructor_id"].Value;
                int courseId = (int)dataGridView1.SelectedRows[0].Cells["course_id"].Value;

                string query = "DELETE FROM InstructorAssignments WHERE instructor_id = @instructorId AND course_id = @courseId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@instructorId", instructorId);
                    command.Parameters.AddWithValue("@courseId", courseId);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                LoadAssignments();
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int instructorId = (int)dataGridView1.SelectedRows[0].Cells["instructor_id"].Value;
                int courseId = (int)dataGridView1.SelectedRows[0].Cells["course_id"].Value;
                DateTime assignmentDate = (DateTime)dataGridView1.SelectedRows[0].Cells["assignment_date"].Value;

                int newInstructorId = (int)cmb_Egitmenler.SelectedValue;
                int newCourseId = (int)cmb_Kurslar.SelectedValue;
                DateTime newAssignmentDate = dateTimePicker1.Value;

                string query = "UPDATE InstructorAssignments SET instructor_id = @newInstructorId, course_id = @newCourseId, assignment_date = @newAssignmentDate WHERE instructor_id = @instructorId AND course_id = @courseId AND assignment_date = @assignmentDate";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@newInstructorId", newInstructorId);
                    command.Parameters.AddWithValue("@newCourseId", newCourseId);
                    command.Parameters.AddWithValue("@newAssignmentDate", newAssignmentDate);
                    command.Parameters.AddWithValue("@instructorId", instructorId);
                    command.Parameters.AddWithValue("@courseId", courseId);
                    command.Parameters.AddWithValue("@assignmentDate", assignmentDate);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                LoadAssignments();
            }
            else
            {
                MessageBox.Show("Please select a row to update.");
            }
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

        private void btn_kursEklemeGoster_Click(object sender, EventArgs e)
        {
            OpenChildForm(new KursEkleme(panelDesktop), sender);
        }

        private void btn_OgrenciGoster_Click(object sender, EventArgs e)
        {
            OpenChildForm(new OgrenciKursAtama(panelDesktop), sender);
        }
    }
}
