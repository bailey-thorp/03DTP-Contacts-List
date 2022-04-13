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
    public partial class frm1 : Form   
    {   
        public static string editName = string.Empty;
        public static string editAge = string.Empty;
        public static string editPhone = string.Empty;
        public static bool editConfirm = false;

        string filePath = @"H:/contacts-storage.txt";

        public frm1()
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

        public void EditContact()
        {
            string[] allLines = File.ReadAllLines(filePath);
            allLines[listView1.FocusedItem.Index] = $"{editName},{editAge},{editPhone}";
            File.WriteAllLines(filePath, allLines);
        }

        public void Swap(int a)
        {

            int selectedIndex = listView1.FocusedItem.Index;
            string[] allLines = File.ReadAllLines(filePath);

            (allLines[selectedIndex], allLines[selectedIndex + a]) = (allLines[selectedIndex + a], allLines[selectedIndex]);
            File.WriteAllLines(filePath, allLines);

            LoadContacts();
            listView1.Items[selectedIndex + a].Focused = true;
            listView1.Items[selectedIndex + a].Selected = true;
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
            File.AppendAllText(filePath, $"\n{txtName.Text},{txtAge.Text},{txtPhone.Text}");

            LoadContacts();
            ClearTextBoxes();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            editName = listView1.FocusedItem.SubItems[0].Text;
            editAge = listView1.FocusedItem.SubItems[1].Text;
            editPhone = listView1.FocusedItem.SubItems[2].Text;

            frm2 editingForm = new frm2();

            editingForm.ShowDialog();

            if (editConfirm)
            {
                EditContact();
                LoadContacts();
                editConfirm = false;
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (listView1.FocusedItem.Index > 0)
            {
                Swap(-1);
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (listView1.FocusedItem.Index < listView1.Items.Count - 1)
            {
                Swap(1);
            }
        }


    }
}
