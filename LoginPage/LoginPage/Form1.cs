using Guna.UI2.AnimatorNS;
using Guna.UI2.Material.Animation;
using Guna.UI2.WinForms;
using MESSAGE;
using ChatApplication;
using System.Net.Sockets;
using System.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Text.Json;
using System.Text;
using System;
using System.Xml.Linq;


namespace LoginPage
{
    public partial class Form1 : Form
    {
        IPEndPoint iep;
        Socket server;
        Socket client;
        bool thoat = false;
        Thread trd;
        private string ipAdd = "192.168.31.108";
        public Form1()
        {
            InitializeComponent();
        }
        private void sendJson(object obj)
        {
            byte[] jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes(obj);
            client.Send(jsonUtf8Bytes, jsonUtf8Bytes.Length, SocketFlags.None);
        }
        private void ThreadTask()
        {
            byte[] data = new byte[1024];
            MESSAGE.LOGIN login = new MESSAGE.LOGIN(UserName.Text, Pass.Text);

            string jsonString = JsonSerializer.Serialize(login);

            MESSAGE.COMMON common = new MESSAGE.COMMON(1, jsonString);
            sendJson(common);
            int recv = client.Receive(data);

            jsonString = Encoding.ASCII.GetString(data, 0, recv);
            jsonString.Replace("\\u0022", "\"");
            MESSAGE.COMMON? com = JsonSerializer.Deserialize<MESSAGE.COMMON>(jsonString);
            try
            {
                if (com != null && com.kind == 3)
                {
                    if (com.content != null)
                    {
                        if (com.content == "CANCEL")
                        {
                            MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác");
                            //msg_pn.Visible = true;
                           // msg_pn.Text = "Tài khoản hoặc mật khẩu không chính xác";
                           
                            return;
                        }    
                       // MessageBox.Show("Login success!");
                        ChatApplication.Form1 form = new ChatApplication.Form1();
                        this.Invoke((MethodInvoker)(() => this.Hide()));
                        form.label_ten.Text = UserName.Text;
                        form.client = client;
                        form.trd = trd;
                        form.thoat = thoat;
                        form.all_user = UserName.Text;
                        MESSAGE.DAU? obj = JsonSerializer.Deserialize<MESSAGE.DAU> (com.content);
                        form.createUserPanel(obj.user,obj.DSClient, UserName.Text);
                        //MessageBox.Show(com.content);
                        //form.vonglap();
                        //form.Invoke((MethodInvoker)(() => form.Show()));
                        Application.Run(form);

                        //while (!thoat)
                        //{
                        //    data = new byte[1024];
                        //    recv = client.Receive(data);

                        //    jsonString = Encoding.ASCII.GetString(data, 0, recv);
                        //    //jsonString=jsonString.Replace("\\u0022", "\"");
                        //    //jsonString = jsonString.Replace("\0", "");
                        //    com = JsonSerializer.Deserialize<MESSAGE.COMMON>(jsonString);

                        //    if (com != null)
                        //    {
                        //        switch (com.kind)
                        //        {
                        //            case 2:
                        //                {
                        //                    MESSAGE.MESSAGE? mes = JsonSerializer.Deserialize<MESSAGE.MESSAGE>(com.content);
                        //                    if (UserName.Text == mes.usernameSender)
                        //                    {
                        //                        createSendView(mes.content);
                        //                    }
                        //                    else
                        //                    {
                        //                        createRecvView(mes.content);
                        //                    }
                        //                }
                        //                //MESSAGE.MESSAGE? mes = JsonSerializer.Deserialize<MESSAGE.MESSAGE>(com.content);
                        //                //AppendTextBox(mes.usernameSender + ":" + mes.content);
                        //                break;
                        //            case 8:

                        //                if (com.content == "OK")
                        //                    MessageBox.Show("Create Group success!");
                        //                else MessageBox.Show("Create Group fail!");
                        //                break;
                        //            case 9:

                        //                if (com.content == "OK")
                        //                    MessageBox.Show("Add members success!");
                        //                else MessageBox.Show("Add members fail!");
                        //                break;
                        //            case 10:

                        //                if (com.content == "CANCEL")
                        //                    MessageBox.Show("User not in group!");
                        //                break;
                        //            case 11:

                        //                if (com.content == "CANCEL")
                        //                    MessageBox.Show("No group!");
                        //                break;
                        //        }

                        //    }



                        //}
                    }
                    else MessageBox.Show("Login fail!");
                }
                client.Disconnect(true);
                client.Close();
            }
            catch (Exception)
            {

            }

        }
        private void ThreadRegister()
        {
            byte[] data = new byte[1024 * 5000];
            MESSAGE.USER login = new MESSAGE.USER(txtUser_re.Text, pass_re.Text, txtname.Text);

            string jsonString = JsonSerializer.Serialize(login);

            MESSAGE.COMMON common = new MESSAGE.COMMON(5, jsonString);
            sendJson(common);
            int recv = client.Receive(data);

            jsonString = Encoding.ASCII.GetString(data, 0, recv);
            //jsonString.Replace("\\u0022", "\"");
            MESSAGE.COMMON? com = JsonSerializer.Deserialize<MESSAGE.COMMON>(jsonString);

            try
            {
                if (com != null && com.kind == 3)
                {
                    if (com.content == "OK")
                    {
                        //MessageBox.Show("Register success!");
                        msg_pn.Visible = true;
                    }
                    else MessageBox.Show("Register fail!");
                }
                //client.Disconnect(true);
                //client.Close();
            }
            catch (Exception)
            {

            }
        }
        private void label1_Click(object sender, EventArgs e)
        {
            pn_login.Visible = true;
            guna2Transition1.ShowSync(pn_login);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            
            pn_login.Visible = false;
            guna2Transition1.HideSync(pn_login);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            // msg_pn.Visible = true;//////
            iep = new IPEndPoint(IPAddress.Parse(ipAdd), int.Parse("2008"));
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(iep);
            ThreadRegister();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            pn_login.Visible = true;
            msg_pn.Visible = false;
            guna2Transition1.ShowSync(pn_login);
        }

        private void login_Click(object sender, EventArgs e)
        {
            iep = new IPEndPoint(IPAddress.Parse(ipAdd), int.Parse("2008"));
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(iep);
            trd = new Thread(new ThreadStart(this.ThreadTask));
            trd.IsBackground = true;
            trd.Start();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                login.PerformClick();
        }
    }
}