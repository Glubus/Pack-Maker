using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace OsuPackGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool OG=checkBox1.Checked;
            bool difficulty = checkBox2.Checked;
            string PackName = textBox2.Text;
            string MapperName = textBox1.Text;
            string ArtistsName = textBox3.Text;
            string Tags = textBox4.Text;
            Program.OsuPack(OG,difficulty,PackName,MapperName,ArtistsName,Tags);
            MessageBox.Show("Operation was successful", "Success");
        }
    }
}
