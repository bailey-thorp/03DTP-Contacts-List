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
            string filePath = @"H:/contacts-storage-2.txt";
            List<string> lines = File.ReadAllLines(filePath).ToList();

            int i = 0;
            ListViewItem lvi = new ListViewItem();
            foreach (string line in lines)
            {
                switch (i)
                {
                    case 0:
                        lvi.SubItems.Add(line);
                        lvi.SubItems.RemoveAt(0);
                        i++;
                        break;
                    case 1:
                        lvi.SubItems.Add(line);
                        i++;
                        break;
                    case 2:
                        lvi.SubItems.Add(line);
                        listView1.Items.Add(lvi);
                        lvi.SubItems.Clear();
                        i = 0;
                        break;
                }

                    
            }

        }

    }
}
