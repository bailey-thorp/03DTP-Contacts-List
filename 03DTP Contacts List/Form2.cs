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
            frm1.editName = txtName.Text;
            frm1.editAge = txtAge.Text;
            frm1.editPhone = txtPhone.Text;
            frm1.editConfirm = true;

            this.Close();
        }
    }
}
