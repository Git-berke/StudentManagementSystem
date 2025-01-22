using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AnaKodYazılımAkademisi
{
    public partial class Form2 : Form
    {
        private Form activeForm;
        private int userId;          // Kullanıcının ID'si
        private string userName;     // Kullanıcının adı
        private string userType;     // Kullanıcının tipi (Admin, Eğitmen, vb.)

        // Parametreli constructor
        public Form2(int userId, string userName, string userType)
        {
            InitializeComponent();

            this.userId = userId;
            this.userName = userName;
            this.userType = userType;

            // Kullanıcı tipine göre erişim kontrolü
            CheckUserAccess();
        }

        private void CheckUserAccess()
        {
            if (userType == "admin")
            {
                // Admin tüm butonlara erişebilir
            }
            else if (userType == "eğitmen")
            {
                button2.Enabled = false; //  admin yönetimi kapalı
                button7.Enabled = false; // Ana Ekran kapalı
                button5.Enabled = false; //  Ödeme yönetimi kapalı
                button3.Enabled = false; // Eğitmen yönetimi kapalı
            }
            else if (userType == "muhasebe")
            {
                button3.Enabled = false; // Eğitmen yönetimi kapalı
                button7.Enabled = false; // Ana Ekran kapalı
                button2.Enabled = false; // Kullanıcı yönetimi kapalı
                button1.Enabled = false; // Öğrenci yönetimi kapalı
                button4.Enabled = false; // Kurs yönetimi kapalı
                button8.Enabled = false; // kurs öneri kapalı
            }
            else if (userType == "sekreter")
            {
                button4.Enabled = false; // Kurs ekleme kapalı
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
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            OpenChildForm(new AdminYonetim(), sender);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new StudentYonetim(), sender);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenChildForm(new EgıtmenYonetim(), sender);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenChildForm(new KursEkleme(panelDesktop), sender);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenChildForm(new DashBoard(), sender);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenChildForm(new MailYonetim(panelDesktop), sender);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

            label2.Text = $"Hoşgeldiniz, {userName}";
            OpenChildForm(new DashBoard(), sender);
        }

        private void panelDesktop_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Odeme(panelDesktop), sender);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            // buna basıldığı zaman uygulama minimze olacak yani alta geçecek
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form3(), sender);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
