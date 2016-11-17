using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;

namespace App
{
    public partial class Login : Form
    {
        String username = "";
        System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();

        StreamWriter STR_W;
        StreamReader STR_R;
        //Socket sender = NewMethod();

        //Socket skt;
        //EndPoint eplocal, 
        EndPoint epremote;
        //String SR_IP = "10.0.0.2";
        byte[] buffer; 
        public Login()
        {
            InitializeComponent();
            //txt_username.Text=Variable.gusername;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            

        }

        private void bt_reset_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login frm = new Login();
            frm.Show();
            txt_username.Clear();
            txt_passwd.Clear();
        }

        
        private void bt_login_Click(object sender, EventArgs e)
        {
            //byte[] bytes = new byte[1024];
            
            username = txt_username.Text;
            MessageBox.Show(username);

            //binding socket

            // eplocal = new IPEndPoint(IPAddress.Parse(SR_IP), Convert.ToInt32("12000"));
            //skt.Bind(eplocal);
            /*
            try
            {
                epremote = new IPEndPoint(IPAddress.Parse("10.0.0.2"), Convert.ToInt32("12000"));
                //skt.Connect("10.0.0.2",12000);
                skt.Connect(epremote);
                byte[] msg = Encoding.ASCII.GetBytes("Login");
                skt.Send(msg);
                byte[] msg1 = Encoding.ASCII.GetBytes(txt_username.Text);
                skt.Send(msg1);
                byte[] msg2 = Encoding.ASCII.GetBytes(txt_passwd.Text);
                skt.Send(msg2);

                buffer = new byte[1500];
                skt.Receive(buffer);
                string returndata = System.Text.Encoding.ASCII.GetString(buffer);
                MessageBox.Show(returndata);
            }
            catch (Exception e1)
            {
                MessageBox.Show("ArgumentNullException : {0}", e1.ToString());
            }*/

            /*// start
            byte[] bytes = new byte[1024];

            // Connect to a remote device.
            try
            {
                // Establish the remote endpoint for the socket.
                // This example uses port 11000 on the local computer.
                // IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName())
                //IPAddress ipAddress = ipHostInfo.AddressList[0];
                //IPEndPoint remoteEP = new IPEndPoint('10.0.0.2', 12000);

                // Create a TCP/IP  socket.

                // Connect the socket to the remote endpoint. Catch any errors.
                try
                {
                    sender.Connect("10.0.0.2", 12000);

                    // Console.WriteLine("Socket connected to {0}",
                    //   new Socket(AddressFamily.InterNetwork,
                    //SocketType.Stream, ProtocolType.Tcp).RemoteEndPoint.ToString());

                    // Encode the data string into a byte array.
                    byte[] msg = Encoding.ASCII.GetBytes("Login");

                    // Send the data through the socket.
                    int bytesSent = sender.Send(msg);

                    msg = Encoding.ASCII.GetBytes(txt_username.Text);
                    bytesSent = sender.Send(msg);

                    msg = Encoding.ASCII.GetBytes(txt_passwd.Text);
                    bytesSent = sender.Send(msg);

                    // Receive the response from the remote device.
                    int bytesRec = sender.Receive(bytes);
                    Console.WriteLine("Echoed test = {0}",
                        Encoding.ASCII.GetString(bytes, 0, bytesRec));
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e3)
                {
                    Console.WriteLine("Unexpected exception : {0}", e3.ToString());
                }

            }
            catch (Exception e4)
            {
                Console.WriteLine(e4.ToString());
            }
            // end*/

            try
            {
                //epremote = new IPEndPoint(IPAddress.Parse("10.0.0.2"), Convert.ToInt32("12000"));
                clientSocket.Connect("10.0.0.2",12000);
                STR_W = new StreamWriter(clientSocket.GetStream());
                STR_R = new StreamReader(clientSocket.GetStream());
                STR_W.AutoFlush = true;
                if (clientSocket.Connected)
                {
                    STR_W.WriteLine("Login|"+ txt_username.Text+"|"+ txt_passwd.Text);

                    //STR_W.WriteLine(txt_username.Text);

                    //STR_W.WriteLine(txt_passwd.Text);

                    string returndata;

                    returndata = STR_R.ReadLine();
                    if(returndata== "Failed")
                    {
                        MessageBox.Show("Username or Password is incorrect");
                    } 
                    else
                    {
                        clientSocket.Close();
                        Variable.gusername = txt_username.Text;
                        Variable.gusertype = returndata;
                        MessageBox.Show("Login Successful.........!", returndata);
                        if(returndata=="admin")
                        {
                            this.Hide();
                            AdminHome frm = new AdminHome();
                            frm.Show();
                        }
                        if (returndata == "enduser")
                        {
                            this.Hide();
                            UserHome frm1 = new UserHome();
                            frm1.Show();
                        }
                        if (returndata == "secmgr")
                        {
                            this.Hide();
                            SecurityManagerHome frm2 = new SecurityManagerHome();
                            frm2.Show();
                        }
                    }
                    
                    /* NetworkStream serverStream = clientSocket.GetStream();
                     byte[] outStream = System.Text.Encoding.ASCII.GetBytes("Login");
                     serverStream.Write(outStream, 0, outStream.Length);
                     //serverStream.Flush();
                     NetworkStream serverStream1 = clientSocket.GetStream();
                     byte[] outStream2 = System.Text.Encoding.ASCII.GetBytes(username);
                     serverStream1.Write(outStream, 0, outStream2.Length);
                     // serverStream.Flush();
                     NetworkStream serverStream2 = clientSocket.GetStream();
                     byte[] outStream3 = System.Text.Encoding.ASCII.GetBytes(txt_passwd.Text);
                     serverStream2.Write(outStream, 0, outStream3.Length);
                     serverStream.Flush();

                                        byte[] inStream = new byte[1024];
                     serverStream.Read(inStream, 0, inStream.Length);
                     string returndata = System.Text.Encoding.ASCII.GetString(inStream);
                     MessageBox.Show(returndata);*/
                }
            }


            catch (Exception e4)
            {
                Console.WriteLine(e4.ToString());
            }

        }

        private static Socket NewMethod()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        private void Login_Load(object sender, EventArgs e)
        {
            //MessageBox.Show("Client Started");

            //label1.Text = "Client Socket Program - Server Connected ...";
            try
            {
              //  skt = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Tcp);
               // skt.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            }
            catch (Exception e4)
            {

            }
        }
    }
}
