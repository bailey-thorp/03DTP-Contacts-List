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
            validAge = Regex.IsMatch(txtAge.Text, @"^[\d\s]+$");
            validPhone = Regex.IsMatch(txtPhone.Text, @"^[\d\s\-]+$");
        }
        public void ResetTextBoxColors()
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
        public void ClearTextBoxes()
        {
            txtName.Text = "";
            txtAge.Text = "";
            txtPhone.Text = "";
        }


        //
        //Methods
        //


        public void LoadContacts()
        {   
            listView1.Items.Clear();
            ReadFile();
            ConvertFromFileToListView();
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

        //Here, almost every line of code is replaced by a small method
        //Each method is self-explanatory as to what the method does.
        //This is an alternative to putting comments throughout the code


        private void Form1_Load(object sender, EventArgs e)
        {
            LoadContacts();
        }   

        private void btnAdd_Click(object sender, EventArgs e)
        {
            CheckTextBoxValidity();
            ResetTextBoxColors();

            if (AllTextBoxesAreValid())
            {
                AppendToFile();
                LoadContacts();
                ClearTextBoxes();
            }
            else
            {
                //These three if statements set the error message based on which textboxes are invalid
                //If a textbox is empty, there will not be an error message for it, but it will still turn red.
                if (!validName)
                { 
                    if (!String.IsNullOrWhiteSpace(txtName.Text)) error = error +"The name must only contain letters\n"; 
                    txtName.BackColor = Color.Pink; 
                }
                if (!validAge)
                {
                    if (!String.IsNullOrWhiteSpace(txtAge.Text)) error = error + "The age must only contain numbers\n";
                    txtAge.BackColor = Color.Pink;
                }
                if (!validPhone)
                {
                    if (!String.IsNullOrWhiteSpace(txtPhone.Text)) error = error + "The Phone No. must only contain numbers\n";
                    txtPhone.BackColor = Color.Pink;
                }
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
                //Swaps the selected contact with the one above or below it, repsectively
                //This method also contains other methods
                Swap(-1);
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (SelectedContactIsNotAtBottom())
            {
                //Same as above
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
