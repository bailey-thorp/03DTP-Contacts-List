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
        public static int selectedIndex;
        List<string> allLines = new List<string>();
        public static string error = "";

        public static bool validName;
        public static bool validAge;
        public static bool validPhone;

        string filePath = @"contacts-storage.txt";

        public frm1()
        {
            InitializeComponent();
            CreateFileIfNeccecary();
        }


        //
        //Small Methods
        //

        public void CreateFileIfNeccecary()
        {
            if (!File.Exists(filePath)) { using (File.Create("Contacts-storage.txt")) { } }
        }

        public void ReadFile()
        {
            allLines = File.ReadAllLines(filePath).ToList();
        }

        public void WriteFile()
        {
            File.WriteAllLines(filePath, allLines);
        }

        public void ConvertFromFileToListView()
        {
            foreach (string line in allLines)
            {
                string[] sections = line.Split(',');

                ListViewItem lvi = new ListViewItem(sections[0]);
                lvi.SubItems.Add(sections[1]);
                lvi.SubItems.Add(sections[2]);
                listView1.Items.Add(lvi);
            }
        }
        public void FindSelectedIndex()
        {
            selectedIndex = listView1.FocusedItem.Index;
        }
        public void SwapLines(int a)
        {
            (allLines[selectedIndex], allLines[selectedIndex + a]) = (allLines[selectedIndex + a], allLines[selectedIndex]);
        }

        public void SelectNewlySwappedLine(int a)
        {
            listView1.Items[selectedIndex + a].Focused = true;
            listView1.Items[selectedIndex + a].Selected = true;
        }

        public void CheckTextBoxValidity()
        {
            validName = Regex.IsMatch(txtName.Text, @"^[a-zA-Z\s]+$");
            validAge = Regex.IsMatch(txtAge.Text, @"^\d+$");
            validPhone = Regex.IsMatch(txtPhone.Text, @"^\d+$");
        }
        public void SetDefaultColors()
        {
            txtName.BackColor = SystemColors.Window;
            txtAge.BackColor = SystemColors.Window;
            txtPhone.BackColor = SystemColors.Window;
        }
        public void AppendToFile()
        {
            File.AppendAllText(filePath, $"{txtName.Text},{txtAge.Text},{txtPhone.Text}\n");
        }
        public bool AllTextBoxesAreValid()
        {
            bool a = (validName && validAge && validPhone);
            return a;
        }
        public void ShowErrorThenClearVariable()
        {
            lblError.Text = error;
            error = "";
        }
        public void SetEditVariables()
        {
            editName = listView1.FocusedItem.SubItems[0].Text;
            editAge = listView1.FocusedItem.SubItems[1].Text;
            editPhone = listView1.FocusedItem.SubItems[2].Text;
        }
        public void ShowEditingForm()
        {
            frm2 editingForm = new frm2();
            editingForm.ShowDialog();
        }
        public void EditSelectedLine()
        {
            allLines[listView1.FocusedItem.Index] = $"{editName},{editAge},{editPhone}";
        }
        public bool SelectedContactIsNotAtTop()
        {
            bool a = (selectedIndex > 0);
            return a;
        }
        public bool SelectedContactIsNotAtBottom()
        {
            bool a = (selectedIndex < listView1.Items.Count - 1);
            return a;
        }
        public void ReselectContact()
        {
            listView1.Items[selectedIndex].Focused = true;
            listView1.Items[selectedIndex].Selected = true;
        }
        public void RemoveSelectedContact()
        {
            allLines.RemoveAt(selectedIndex);
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
            ReadFile();
            ConvertFromFileToListView();
        }

        public void Swap(int a)
        {

            ReadFile();
            SwapLines(a);
            WriteFile();

            LoadContacts();
            SelectNewlySwappedLine(a);
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
            CheckTextBoxValidity();
            SetDefaultColors();

            if (AllTextBoxesAreValid())
            {
                AppendToFile();
                LoadContacts();
                ClearTextBoxes();
            }
            else
            {
                //set error message based on which textboxes are invalid
                if (!validName) { error = error +"The name must only contain letters\n"; txtName.BackColor = Color.Pink; }
                if (!validAge) { error = error + "The age must only contain numbers\n"; txtAge.BackColor = Color.Pink; }
                if (!validPhone) { error = error + "The Phone No. must only contain numbers\n"; txtPhone.BackColor = Color.Pink; }
            }
            ShowErrorThenClearVariable();

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            SetEditVariables();

            ShowEditingForm();

            if (editConfirm)
            {
                ReadFile();
                EditSelectedLine();
                WriteFile();
                LoadContacts();
                editConfirm = false;
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (SelectedContactIsNotAtTop())
            {
                Swap(-1);
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (SelectedContactIsNotAtBottom())
            {
                Swap(1);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ReadFile();
            RemoveSelectedContact();
            WriteFile();
            LoadContacts();

            if (SelectedContactIsNotAtBottom())
            {
                ReselectContact();
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FindSelectedIndex();
        }
    }
}
