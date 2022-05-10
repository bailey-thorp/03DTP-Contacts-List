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
using System.Text.RegularExpressions;

namespace _03DTP_Contacts_List
{
    public partial class frm1 : Form   
    {   
        public static string editName = string.Empty;
        public static string editAge = string.Empty;
        public static string editPhone = string.Empty;
        public static bool editConfirm = false;

        string filePath = @"contacts-storage.txt";

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
            string[] allLines = File.ReadAllLines(filePath);
            foreach (string line in allLines)
            {
                string[] sections = line.Split(',');

                //put another foreach loop here
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
            bool validName = Regex.IsMatch(txtName.Text, @"^[a-zA-Z\s]+$");
            bool validAge = Regex.IsMatch(txtAge.Text, @"^\d+$");
            bool validPhone = Regex.IsMatch(txtPhone.Text, @"^\d+$");
            string error = "";

            txtName.BackColor = SystemColors.Window;
            txtAge.BackColor = SystemColors.Window;
            txtPhone.BackColor = SystemColors.Window;

            if (validName && validAge && validPhone)
            {
                File.AppendAllText(filePath, $"{txtName.Text},{txtAge.Text},{txtPhone.Text}\n");
                LoadContacts();
                ClearTextBoxes();
            }
            else
            {
                if (!validName) { error = error +"The name must only contain letters\n"; txtName.BackColor = Color.Pink; }
                if (!validAge) { error = error + "The age must only contain numbers\n"; txtAge.BackColor = Color.Pink; }
                if (!validPhone) { error = error + "The Phone No. must only contain numbers\n"; txtPhone.BackColor = Color.Pink; }
            }
            lblError.Text = error;
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
            if (listView1.FocusedItem.Index < listView1.Items.Count -1)
            {
                Swap(1);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int selectedIndex = listView1.FocusedItem.Index;
            List<string> allLines = File.ReadAllLines(filePath).ToList();

            allLines.RemoveAt(selectedIndex);

            File.WriteAllLines(filePath, allLines);
            LoadContacts();
        }

    }
}
