using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetPhoto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        MySqlConnection con = new MySqlConnection("Server=localhost;Database=example_webcam;Uid=root;Pwd=;");
        private void button1_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand("select * from pictures",con);
            con.Open();
            MySqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            pictureBox1.Image = new Bitmap(new MemoryStream(Convert.FromBase64String(read["image"].ToString())));
            con.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Welcome");
        }
    }
}
