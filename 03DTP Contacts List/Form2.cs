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
    public partial class frm2 : Form
    {
        public frm2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            txtName.Text = frm1.editName;
            txtAge.Text = frm1.editAge;
            txtPhone.Text = frm1.editPhone;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            bool validName = Regex.IsMatch(txtName.Text, @"^[a-zA-Z]+$");
            bool validAge = Regex.IsMatch(txtAge.Text, @"^\d+$");
            bool validPhone = Regex.IsMatch(txtPhone.Text, @"^\d+$");
            string error = "";

            txtName.BackColor = SystemColors.Window;
            txtAge.BackColor = SystemColors.Window;
            txtPhone.BackColor = SystemColors.Window;

            if (validName && validAge && validPhone)
            {
                lblError.Text = error;
                frm1.editName = txtName.Text;
                frm1.editAge = txtAge.Text;
                frm1.editPhone = txtPhone.Text;
                frm1.editConfirm = true;

                this.Close();
            }
            else
            {
                if (!validName) { error = error + "The name must only contain letters\n"; txtName.BackColor = Color.Pink; }
                if (!validAge) { error = error + "The age must only contain numbers\n"; txtAge.BackColor = Color.Pink; }
                if (!validPhone) { error = error + "The Phone Number must only contain numbers\n"; txtPhone.BackColor = Color.Pink; }
            }
            lblError.Text = error;
        }
    }
}
