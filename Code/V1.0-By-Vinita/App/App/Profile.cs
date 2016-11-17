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
    public partial class Profile : Form
    {
        public Profile()
        {
            InitializeComponent();
        }

        private void bt_Register_Click(object sender, EventArgs e)
        {

        }

        private void Profile_Load(object sender, EventArgs e)
        {
            lb_hello_usr.Text = "Hi " + Variable.gusername;
        }
    }
}
