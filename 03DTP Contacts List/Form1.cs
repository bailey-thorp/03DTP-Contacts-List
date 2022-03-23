using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _03DTP_Contacts_List
{
    public partial class Form1 : Form
    {
        int count = 0;
        public static string[] names = { "bail", "dan", "sam" };
        public static string[] ages = { "17", "17", "18" };
        public static string[] phones = { "548264", "791236", "7862469" };
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (count < 3)
            {
                ListViewItem lvi = new ListViewItem(names[count]);
                lvi.SubItems.Add(ages[count]);
                lvi.SubItems.Add(phones[count]);
                listView1.Items.Add(lvi);
                count++;
            }
        }
    }
}
