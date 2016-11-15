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

namespace App
{
    public partial class Register : Form
    {
   //     String username = "";
        System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
        //System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
        String Data_send = "";

        StreamWriter STR_W;
        StreamReader STR_R;
        public Register()
        {
            InitializeComponent();
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }



        private void bt_reset_Click(object sender, EventArgs e)
        {
            txt_full_name.Clear();
            txt_username.Clear();
            txt_Contact_no.Clear();
            txt_passwd.Clear();
            txt_passwd_confirm.Clear();
            txt_ans.Clear();
            txt_location.Clear();
            //cb_gender.SelectedIndex = 0;
            //cb_security_qus.DataSource = 0;
        }

        private void txt_Contact_no_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_Contact_no_key_press(object sender, KeyPressEventArgs e)
        {
            if(!char.IsNumber(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void bt_Register_Click(object sender, EventArgs e)
        {

            if (txt_username.Text != "" && txt_passwd.Text != "" && txt_passwd_confirm.Text != "" && txt_full_name.Text != "" && cb_gender.Text != "" && txt_location.Text != "" && txt_Contact_no.Text != "" && cb_security_qus.Text != "" && txt_ans.Text != "")
            {


                if (txt_Contact_no.Text.Length<10)
                {
                    MessageBox.Show("Contact no. should be 10 digit......!");

                }
                else
                {
                    if (txt_passwd.Text != txt_passwd_confirm.Text)
                    {
                        txt_passwd.Clear();
                        txt_passwd_confirm.Clear();

                    }
                    else
                    {
                        Data_send = "Register|" + txt_username.Text + "|" + txt_passwd.Text + "|" + txt_full_name.Text + "|" + cb_gender.Text + "|" + txt_location.Text + "|" + txt_Contact_no.Text + "|" + cb_security_qus.Text + "|" + txt_ans.Text;
                        MessageBox.Show(Data_send);
                        try
                        {
                            //epremote = new IPEndPoint(IPAddress.Parse("10.0.0.2"), Convert.ToInt32("12000"));
                            clientSocket.Connect("10.100.220.33", 12000);
                            STR_W = new StreamWriter(clientSocket.GetStream());
                            STR_R = new StreamReader(clientSocket.GetStream());
                            STR_W.AutoFlush = true;
                            Data_send = Data_send = "Register|" + txt_username.Text + "|" + txt_passwd.Text + "|" + txt_full_name.Text + "|" + cb_gender.Text + "|" + txt_location.Text + "|" + txt_Contact_no.Text + "|" + cb_security_qus.Text + "|" + txt_ans.Text;
                            if (clientSocket.Connected)
                            {
                                STR_W.WriteLine(Data_send);

                                //STR_W.WriteLine(txt_username.Text);

                                //STR_W.WriteLine(txt_passwd.Text);

                                string returndata;

                                returndata = STR_R.ReadLine();
                                MessageBox.Show(returndata);
                                if (returndata == "Success")
                                {
                                    this.Hide();
                                    Login frm1 = new Login();
                                    frm1.Show();
                                }
                                if (returndata == "User already present")
                                {
                                    txt_passwd.Clear();
                                    txt_passwd_confirm.Clear();
                                    txt_username.Clear();
                                    clientSocket.Close();
                                    MessageBox.Show("Username is already present");
                                    this.Hide();
                                    Register frm1 = new Register();
                                    frm1.Show();
                                }
                            }
                        }
                        catch (Exception e1)
                        {
                            MessageBox.Show(e1.ToString());
                        }






                    }
                }
            }
            else
            {
                MessageBox.Show("Must need to fill each field......!");

            }

            /* try
             {
                 //epremote = new IPEndPoint(IPAddress.Parse("10.0.0.2"), Convert.ToInt32("12000"));
                 clientSocket.Connect("10.0.0.2", 12000);
                 STR_W = new StreamWriter(clientSocket.GetStream());
                 STR_R = new StreamReader(clientSocket.GetStream());
                 STR_W.AutoFlush = true;
                 Data_send = "Register|" + txt_username.Text + "|" + txt_passwd + "|" + txt_full_name.Text + "|" + cb_gender.Text + "|" + txt_location.Text + "|" + txt_Contact_no.Text + "|" + cb_security_qus.Text + "|" + txt_ans.Text;
                  if (clientSocket.Connected)
                 {
                     STR_W.WriteLine("Login|" + txt_username.Text + "|" + txt_passwd.Text);

                     //STR_W.WriteLine(txt_username.Text);

                     //STR_W.WriteLine(txt_passwd.Text);

                     string returndata;

                     returndata = STR_R.ReadLine();
                 }
             }
             catch (Exception e1)
             {

             }*/
        }
    }
}
