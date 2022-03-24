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

namespace _03DTP_Contacts_List
{
    public partial class Form1 : Form   
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ListViewItem lvi = new ListViewItem(txtName.Text);
            lvi.SubItems.Add(txtAge.Text);
            lvi.SubItems.Add(txtPhone.Text);
            listView1.Items.Add(lvi);

            txtName.Text = "";
            txtAge.Text = "";
            txtPhone.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string filePath = @"H:/contacts-storage.txt";
            List<string> lines = File.ReadAllLines(filePath).ToList();

            foreach(string line in lines)
            {
                string[] sections = line.Split(',');
                //make into method
                ListViewItem lvi = new ListViewItem(sections[0]);
                lvi.SubItems.Add(sections[1]);
                lvi.SubItems.Add(sections[2]);
                listView1.Items.Add(lvi);
                //
            }
        }
    }
}
