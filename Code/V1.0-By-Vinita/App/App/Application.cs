using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    
    public partial class Application : Form
    {

        
        public Application()
        {
            InitializeComponent();
        }

        private void Application_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {


        }



        private void bt_login_Click(object sender, EventArgs e)
        {
            //Variable.gusername = "qwe";
            this.Hide();
            Login frm2 = new Login();
            frm2.Show();

        }

        private void bt_register_Click(object sender, EventArgs e)
        {
            this.Hide();
            Register frm1 = new Register();
            frm1.Show();
        }
    }
}
