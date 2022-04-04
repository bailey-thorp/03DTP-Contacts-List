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
    public partial class Form2 : Form
    {
        //make it so Form1 cannot be used while Form2 is open
        //how to do something (write to file) in Form1 when Confirm is pressed?
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            txtName.Text = Form1.editName;
            txtAge.Text = Form1.editAge;
            txtPhone.Text = Form1.editPhone;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            Form1.editName = txtName.Text;
            Form1.editAge = txtAge.Text;
            Form1.editPhone = txtPhone.Text;
        }
    }
}
