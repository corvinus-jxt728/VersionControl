using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VerkoForm.Entities;
using System.IO;

namespace VerkoForm
{
    public partial class Form1 : Form
    {
        BindingList<user> users = new BindingList<user>();
        public Form1()
        {
            InitializeComponent();
            label1.Text = Resource1.FullName;
            label2.Text = Resource1.placeholder;
            button1.Text = Resource1.Add;
            button2.Text = Resource1.WriteToFile;

            listBox1.DataSource = users;
            listBox1.ValueMember = "ID";
            listBox1.DisplayMember = "FullName";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var u = new user()
            {
                FullName = textBox1.Text
               // FirstName = textBox2.Text
            };
            users.Add(u);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = ".csv";
            sfd.ShowDialog();
            StreamWriter sw = new StreamWriter(sfd.FileName, false, encoding: Encoding.UTF8);
            for (int i = 0; i < users.Count; i++)
            {
                sw.Write(users[i].ID);
                sw.Write(";");
                sw.Write(users[i].FullName);
                sw.WriteLine();
            }
           
            sw.Close();
           
        }
    }
}
