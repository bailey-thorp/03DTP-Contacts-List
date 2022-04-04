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
        public static string editName = string.Empty;
        public static string editAge = string.Empty;
        public static string editPhone = string.Empty;
        string filePath = @"H:/contacts-storage.txt";
        public Form1()
        {
            InitializeComponent();
        }
        //
        //Methods
        //
        public void ClearTextBoxes()
        {
            txtName.Text = "";
            txtAge.Text = "";
            txtPhone.Text = "";
        }

        public void LoadContacts()
        {
            listView1.Items.Clear();

            List<string> lines = File.ReadAllLines(filePath).ToList();

            foreach (string line in lines)
            {
                string[] sections = line.Split(',');

                ListViewItem lvi = new ListViewItem(sections[0]);
                
                lvi.SubItems.Add(sections[1]);
                lvi.SubItems.Add(sections[2]);
                listView1.Items.Add(lvi);
            }
        }
        //
        //Events
        //
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadContacts();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ListViewItem lvi = new ListViewItem(txtName.Text);
            lvi.SubItems.Add(txtAge.Text);
            lvi.SubItems.Add(txtPhone.Text);
            listView1.Items.Add(lvi);

            File.AppendAllText(filePath, $"\n{txtName.Text},{txtAge.Text},{txtPhone.Text}");

            LoadContacts();
            
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //MessageBox.Show($"{listView1.FocusedItem.SubItems[0].Text} {listView1.FocusedItem.SubItems[1].Text} {listView1.FocusedItem.SubItems[2].Text}");
            editName = listView1.FocusedItem.SubItems[0].Text;
            editAge = listView1.FocusedItem.SubItems[1].Text;
            editPhone = listView1.FocusedItem.SubItems[2].Text;

            Form2 editingForm = new Form2();
            editingForm.Show();
        }
    }
}
