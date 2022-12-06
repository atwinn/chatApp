using Guna.UI2.WinForms;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Text.Json;
using System.Text;
using System.Net.Sockets;
using MESSAGE;
using System.Net;
using System.Windows.Forms;
using Microsoft.VisualBasic.Logging;
using System.Diagnostics;
using System.Timers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace ChatApplication
{

    public partial class Form1 : Form
    {
        public IPEndPoint iep;
        public Socket server;
        public Socket client;
        public bool thoat;
        public Thread trd;
        public string all_user;
        public string pathGui;
        public string nameFileGui;

        private Guna.UI2.WinForms.Guna2Panel userPn;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2CirclePictureBox guna2CirclePictureBox1;
        private Guna.UI2.WinForms.Guna2Panel groupPn;
        private System.Windows.Forms.Label groupName;
        private Guna.UI2.WinForms.Guna2CirclePictureBox groupPic;
        private Guna.UI2.WinForms.Guna2Panel PanelTong;
        private Guna.UI2.WinForms.Guna2Shapes circle;
        private Guna.UI2.WinForms.Guna2Shapes circle2;
        private Guna.UI2.WinForms.Guna2Panel sendContainer;
        private Guna.UI2.WinForms.Guna2GradientPanel sendPn;
        private Guna.UI2.WinForms.Guna2Panel recvContainer;
        private Guna.UI2.WinForms.Guna2GradientPanel recvPn;
        private System.Windows.Forms.Label recvLabel;
        private System.Windows.Forms.Label recvUnLabel;
        private Guna.UI2.WinForms.Guna2Panel recvGrContainer;
        private Guna.UI2.WinForms.Guna2GradientPanel recvGrPn;
        private System.Windows.Forms.Label recvGrLabel;
        private System.Windows.Forms.Label sendChatLabel;
        private Guna.UI2.WinForms.Guna2Panel imageContainer;
        private Guna.UI2.WinForms.Guna2PictureBox imageSend;
        private Guna.UI2.WinForms.Guna2Panel imageContainer2;
        private Guna.UI2.WinForms.Guna2PictureBox imageRecv;
        public Form1()
        {
            InitializeComponent();
            PanelContainer();
            //createUserPanel();
        }
        private void sendJson(object obj)
        {
            byte[] jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes(obj);
            client.Send(jsonUtf8Bytes, jsonUtf8Bytes.Length, SocketFlags.None);
        }

        private void createSendView(string mess)
        {
            sendContainer = new Guna2Panel();
            sendPn = new Guna2GradientPanel();
            sendChatLabel = new System.Windows.Forms.Label();

            //this.Controls.Add(sendContainer);
            this.Invoke((MethodInvoker)delegate
            {
                this.Controls.Add(sendContainer);
                chatBoxPn.Controls.Add(sendContainer);
                sendContainer.Controls.Add(sendPn);
                sendContainer.AutoSize = true;
                sendContainer.BringToFront();
                guna2Transition1.SetDecoration(sendContainer, Guna.UI2.AnimatorNS.DecorationType.None);
                sendContainer.Dock = System.Windows.Forms.DockStyle.Top;
                sendContainer.Location = new System.Drawing.Point(0, 0);
                sendContainer.Name = "sendContainer";
                sendContainer.Size = new System.Drawing.Size(761, 61);
                sendContainer.TabIndex = 0;
                sendContainer.UseTransparentBackground = true;

                sendPn.BorderRadius = 6;
                sendPn.Controls.Add(sendChatLabel);
                guna2Transition1.SetDecoration(sendPn, Guna.UI2.AnimatorNS.DecorationType.None);
                sendPn.AutoSize = true;
                sendPn.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(103)))), ((int)(((byte)(228)))));
                sendPn.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(43)))));
                sendPn.ForeColor = System.Drawing.Color.White;
                sendPn.Location = new System.Drawing.Point(455, 8);
                sendPn.Padding = new Padding(0, 0, 0, 15);
                sendPn.Name = "sendPn";
                sendPn.Size = new System.Drawing.Size(290, 36);
                sendPn.TabIndex = 1;
                sendPn.UseTransparentBackground = true;
                
                guna2Transition1.SetDecoration(sendChatLabel, Guna.UI2.AnimatorNS.DecorationType.None);
                sendChatLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                sendChatLabel.Location = new System.Drawing.Point(20, 13);
                sendChatLabel.Name = "sendChatLabel";
                sendChatLabel.Size = new System.Drawing.Size(44, 21);
                sendChatLabel.TabIndex = 0;
                sendChatLabel.MaximumSize = new System.Drawing.Size(255, 0);
                sendChatLabel.AutoSize = true;
                sendChatLabel.Text = mess;

                txtchatbox.Text = "";

            });

            //chatBoxPn.Controls.Add(sendContainer);
            //this.Invoke((MethodInvoker)(() => chatBoxPn.Controls.Add(sendContainer)));

            //sendContainer.Controls.Add(sendPn);
            // this.Invoke((MethodInvoker)(() => chatBoxPn.Controls.Add(sendContainer)));



        }

        private void createRecvView(string mess)
        {
            recvContainer = new Guna2Panel();
            recvPn = new Guna2GradientPanel();
            recvLabel = new System.Windows.Forms.Label();
            recvUnLabel = new System.Windows.Forms.Label();
            this.Invoke((MethodInvoker)delegate
            {
                this.Controls.Add(recvContainer);
                chatBoxPn.Controls.Add(recvContainer);
                recvContainer.Controls.Add(recvPn);

                recvContainer.BringToFront();
                recvContainer.AutoSize = true;
                guna2Transition1.SetDecoration(recvContainer, Guna.UI2.AnimatorNS.DecorationType.None);
                recvContainer.Dock = System.Windows.Forms.DockStyle.Top;
                recvContainer.Location = new System.Drawing.Point(0, 61);
                recvContainer.Name = "recvContainer";
                recvContainer.Size = new System.Drawing.Size(761, 61);
                recvContainer.TabIndex = 1;
                recvContainer.UseTransparentBackground = true;

                recvPn.BorderRadius = 6;
                recvPn.Controls.Add(recvLabel);
                guna2Transition1.SetDecoration(recvPn, Guna.UI2.AnimatorNS.DecorationType.None);
                recvPn.AutoSize = true;
                recvPn.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
                recvPn.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(103)))), ((int)(((byte)(228)))));
                recvPn.ForeColor = System.Drawing.Color.White;
                recvPn.Location = new System.Drawing.Point(14, 8);
                recvPn.Padding = new Padding(0, 0, 0, 15);
                recvPn.Name = "recvPn";
                recvPn.Size = new System.Drawing.Size(290, 36);
                recvPn.TabIndex = 1;
                recvPn.UseTransparentBackground = true;

                guna2Transition1.SetDecoration(recvLabel, Guna.UI2.AnimatorNS.DecorationType.None);
                recvLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                recvLabel.ForeColor = System.Drawing.Color.Black;
                recvLabel.Location = new System.Drawing.Point(19, 12);
                recvLabel.Name = "recvLabel";
                recvLabel.Size = new System.Drawing.Size(44, 21);
                recvLabel.MaximumSize = new System.Drawing.Size(255, 0);
                recvLabel.AutoSize = true;
                recvLabel.TabIndex = 0;
                recvLabel.Text = mess;
            });

        }

        private void createGroupRecvView(string mess)
        {
            recvGrContainer = new Guna2Panel();
            recvGrPn = new Guna2GradientPanel();
            recvGrLabel = new System.Windows.Forms.Label();
            this.Invoke((MethodInvoker)delegate
            {
                this.Controls.Add(recvGrContainer);
                chatBoxPn.Controls.Add(recvGrContainer);
                recvGrContainer.Controls.Add(recvGrPn);
                recvGrContainer.Controls.Add(recvUnLabel);

                recvUnLabel.AutoSize = true;
                guna2Transition1.SetDecoration(recvUnLabel, Guna.UI2.AnimatorNS.DecorationType.None);
                recvUnLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                recvUnLabel.ForeColor = System.Drawing.Color.White;
                recvUnLabel.Location = new System.Drawing.Point(14, 0);
                recvUnLabel.Name = "recvUnLabel";
                recvUnLabel.Size = new System.Drawing.Size(44, 21);
                recvUnLabel.TabIndex = 0;
                recvUnLabel.Text = "Hello";

                recvGrContainer.BringToFront();
                guna2Transition1.SetDecoration(recvGrContainer, Guna.UI2.AnimatorNS.DecorationType.None);
                recvGrContainer.AutoSize = true;
                recvGrContainer.Dock = System.Windows.Forms.DockStyle.Top;
                recvGrContainer.Location = new System.Drawing.Point(0, 61);
                recvGrContainer.Name = "recvContainer";
                recvGrContainer.Size = new System.Drawing.Size(761, 81);
                recvGrContainer.TabIndex = 1;
                recvGrContainer.UseTransparentBackground = true;

                recvGrPn.BorderRadius = 6;
                recvGrPn.Controls.Add(recvLabel);
                guna2Transition1.SetDecoration(recvGrLabel, Guna.UI2.AnimatorNS.DecorationType.None);
                recvGrPn.AutoSize = true;
                recvGrPn.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
                recvGrPn.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(103)))), ((int)(((byte)(228)))));
                recvGrPn.ForeColor = System.Drawing.Color.White;
                recvGrPn.Location = new System.Drawing.Point(14, 20);
                recvGrPn.Name = "recvPn";
                recvGrPn.Padding = new Padding(0, 0, 0, 15);
                recvGrPn.Size = new System.Drawing.Size(290, 36);
                recvGrPn.TabIndex = 1;
                recvGrPn.UseTransparentBackground = true;

                guna2Transition1.SetDecoration(recvGrLabel, Guna.UI2.AnimatorNS.DecorationType.None);
                recvGrLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                recvGrLabel.ForeColor = System.Drawing.Color.Black;
                recvGrLabel.Location = new System.Drawing.Point(19, 12);
                recvGrLabel.Name = "recvLabel";
                recvGrLabel.Size = new System.Drawing.Size(44, 21);
                recvGrLabel.MaximumSize = new System.Drawing.Size(255, 0);
                recvGrLabel.AutoSize = true;
                recvGrLabel.TabIndex = 0;
                recvGrLabel.Text = mess;
            });

        }

        public void vonglap()
        {
            while (!thoat)
            {

                byte[] data = new byte[1024 * 5000];


                int recv = client.Receive(data);
                if (thoat == true) break;
                string jsonString = Encoding.ASCII.GetString(data, 0, recv);
                jsonString.Replace("\\u0022", "\"");
                MESSAGE.COMMON? com = JsonSerializer.Deserialize<MESSAGE.COMMON>(jsonString);

                if (com != null)
                {
                    switch (com.kind)
                    {
                        case 2:
                            {
                                MESSAGE.MESSAGE? mes = JsonSerializer.Deserialize<MESSAGE.MESSAGE>(com.content);
                                if (all_user == mes.usernameSender)
                                {
                                    createSendView(mes.content);
                                    this.Invoke((MethodInvoker)(() => chatBoxPn.ScrollControlIntoView(sendContainer)));
                                }
                                else
                                {
                                    createRecvView(mes.content);
                                    this.Invoke((MethodInvoker)(() => chatBoxPn.ScrollControlIntoView(recvContainer)));
                                }
                            }

                            //createRecvView()
                            //AppendTextBox(mes.usernameSender + ":" + mes.content);
                            break;
                        case 8:

                            if (com.content == "OK")
                                MessageBox.Show("Create Group success!");
                            else MessageBox.Show("Create Group fail!");
                            break;
                        case 9:

                            if (com.content == "OK")
                                MessageBox.Show("Add members success!");
                            else MessageBox.Show("Add members fail!");
                            break;
                        case 10:

                            if (com.content == "CANCEL")
                                MessageBox.Show("User not in group!");
                            break;
                        case 11:

                            if (com.content == "CANCEL")
                                MessageBox.Show("No group!");
                            break;
                        case 12:
                            {
                                MESSAGE.DAU? obj = JsonSerializer.Deserialize<MESSAGE.DAU>(com.content);
                                createUserPanel_thread(obj.user, obj.DSClient);
                            }

                            break;
                        case 13:
                            {
                                MESSAGE.FILE? obj2 = JsonSerializer.Deserialize<MESSAGE.FILE>(com.content);
                                if (all_user == obj2.usernameSender)
                                {
                                    createImageSend(pathGui);
                                    chatBoxPn.Invoke((MethodInvoker)(() => chatBoxPn.ScrollControlIntoView(imageContainer)));
                                    pathGui = "";
                                }
                                if (all_user == obj2.usernameReceiver)
                                {
                                    if (MessageBox.Show(obj2.usernameSender + " đã gửi một file cho bạn , bạn có muốn nhận không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {



                                        string path = "C:/Users/ad/Desktop/nhanFile_Client";
                                        byte[] clientData = new byte[1024 * 5000];
                                        clientData = obj2.file;
                                        int receivedBytesLen = clientData.Length;
                                        int fileNameLen = BitConverter.ToInt32(clientData, 0);
                                        string fileName = Encoding.ASCII.GetString(clientData, 4, fileNameLen);
                                        fileName = fileName.Replace("\\", "/");
                                        while (fileName.IndexOf("/") > -1)
                                        {
                                            fileName = fileName.Substring(fileName.IndexOf("/") + 1);
                                        }
                                        string link = path + "/" + fileName;
                                        BinaryWriter bWrite = new BinaryWriter(File.Open(link, FileMode.Create));
                                        bWrite.Write(clientData, 4 + fileNameLen, receivedBytesLen - 4 - fileNameLen);
                                        bWrite.Close();
                                        createRecvView(obj2.usernameSender + " đã gửi cho bạn 1 file ảnh");
                                        createImageRecv(link);
                                        chatBoxPn.Invoke((MethodInvoker)(() => chatBoxPn.ScrollControlIntoView(imageContainer2)));
                                        //chatBoxPn.ScrollControlIntoView(imageContainer2);
                                        //createSendView(obj2.usernameSender + " send a file to " + obj2.usernameReceiver + " Path: " + path + "/" + fileName);


                                    }

                                }
                            }

                            break;
                        case 14:
                            {
                                MESSAGE.FILE? obj2 = JsonSerializer.Deserialize<MESSAGE.FILE>(com.content);
                                if (all_user == obj2.usernameSender)
                                {
                                    createSendView(nameFileGui);
                                    //createImageSend(pathGui);
                                    //chatBoxPn.Invoke((MethodInvoker)(() => chatBoxPn.ScrollControlIntoView(imageContainer)));
                                    nameFileGui = "";
                                }
                                if (all_user == obj2.usernameReceiver)
                                {
                                    if (MessageBox.Show(obj2.usernameSender + " đã gửi một file cho bạn , bạn có muốn nhận không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {



                                        string path = "C:/Users/ad/Desktop/nhanFile_Client";
                                        byte[] clientData = new byte[1024 * 5000];
                                        clientData = obj2.file;
                                        int receivedBytesLen = clientData.Length;
                                        int fileNameLen = BitConverter.ToInt32(clientData, 0);
                                        string fileName = Encoding.ASCII.GetString(clientData, 4, fileNameLen);
                                        fileName = fileName.Replace("\\", "/");
                                        while (fileName.IndexOf("/") > -1)
                                        {
                                            fileName = fileName.Substring(fileName.IndexOf("/") + 1);
                                        }
                                        string link = path + "/" + fileName;
                                        BinaryWriter bWrite = new BinaryWriter(File.Open(link, FileMode.Create));
                                        bWrite.Write(clientData, 4 + fileNameLen, receivedBytesLen - 4 - fileNameLen);
                                        bWrite.Close();
                                        createRecvView(obj2.usernameSender + " đã gửi cho bạn 1 file ");
                                        createRecvView("File được lưu vào đường dẫn:"+path);


                                    }

                                }
                            }

                            break;

                    }

                }



            }

        }

        private void PanelContainer()
        {
            PanelTong = new Guna.UI2.WinForms.Guna2Panel();

            this.Controls.Add(PanelTong);
            PanelTong.BringToFront();
            PanelTong.AutoScroll = true;
            guna2Transition1.SetDecoration(PanelTong, Guna.UI2.AnimatorNS.DecorationType.None);
            PanelTong.Location = new System.Drawing.Point(3, 190);
            PanelTong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(43)))));
            PanelTong.Name = "panelTong";
            PanelTong.Size = new System.Drawing.Size(258, 410);
            PanelTong.TabIndex = 4;
        }

        public void createUserPanel(List<string> DS_member, List<string> DSClient)
        {

            int x = 0;
            foreach (string s in DS_member)
            {
                if (DSClient.Contains(s))
                {
                    userPn = new Guna.UI2.WinForms.Guna2Panel();

                    this.Controls.Add(userPn);
                    PanelTong.Controls.Add(userPn);
                    label2 = new System.Windows.Forms.Label();
                    guna2CirclePictureBox1 = new Guna.UI2.WinForms.Guna2CirclePictureBox();
                    circle = new Guna.UI2.WinForms.Guna2Shapes();
                    //Hình tròn trạng thái
                    circle.BorderThickness = 0;
                    guna2Transition1.SetDecoration(circle, Guna.UI2.AnimatorNS.DecorationType.None);
                    circle.BackColor = System.Drawing.Color.Transparent;
                    circle.FillColor = System.Drawing.Color.Lime;
                    circle.Location = new System.Drawing.Point(50, 7);
                    circle.Name = "guna2Shapes1" + s;
                    circle.PolygonSkip = 1;
                    circle.Rotate = 0F;
                    circle.Shape = Guna.UI2.WinForms.Enums.ShapeType.Ellipse;
                    circle.Size = new System.Drawing.Size(18, 18);
                    circle.TabIndex = 4;
                    circle.Text = "guna2Shapes1";
                    circle.Zoom = 80;
                    circle.UseTransparentBackground = true;
                    //Tên hiển thị
                    label2.AutoSize = true;
                    label2.BackColor = System.Drawing.Color.Transparent;
                    guna2Transition1.SetDecoration(label2, Guna.UI2.AnimatorNS.DecorationType.None);
                    label2.Font = new System.Drawing.Font("Yu Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                    label2.ForeColor = System.Drawing.Color.White;
                    label2.Location = new System.Drawing.Point(80, 20);
                    label2.Name = "label2" + s;
                    label2.Size = new System.Drawing.Size(116, 25);
                    label2.TabIndex = 0;
                    label2.Text = s;
                    label2.Click += new System.EventHandler((sender, e) => this.userPn_Click(sender, e, s));
                    //label2.Click += new System.EventHandler(this.userPn_Click);
                    //Hình hiển thị
                    guna2CirclePictureBox1.BackColor = System.Drawing.Color.Transparent;
                    guna2Transition1.SetDecoration(guna2CirclePictureBox1, Guna.UI2.AnimatorNS.DecorationType.None);
                    guna2CirclePictureBox1.Image = global::ChatApplication.Properties.Resources._274242879_916713445688196_4178666346407724582_n;
                    guna2CirclePictureBox1.ImageRotate = 0F;
                    guna2CirclePictureBox1.Location = new System.Drawing.Point(17, 9);
                    guna2CirclePictureBox1.Name = "guna2CirclePictureBox1" + s;
                    guna2CirclePictureBox1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
                    guna2CirclePictureBox1.Size = new System.Drawing.Size(47, 46);
                    guna2CirclePictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    guna2CirclePictureBox1.TabIndex = 1;
                    guna2CirclePictureBox1.TabStop = false;
                    guna2CirclePictureBox1.UseTransparentBackground = true;
                    guna2CirclePictureBox1.Click += new System.EventHandler((sender, e) => this.userPn_Click(sender, e, s));
                    //guna2CirclePictureBox1.Click += new System.EventHandler(this.userPn_Click);
                    //Bộ khung
                    //userPn.BringToFront();
                    userPn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(43)))));
                    userPn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(20)))), ((int)(((byte)(137)))));
                    userPn.BorderRadius = 15;
                    userPn.BorderThickness = 2;
                    userPn.Controls.Add(label2);
                    userPn.Controls.Add(circle);
                    userPn.Controls.Add(guna2CirclePictureBox1);
                    guna2Transition1.SetDecoration(userPn, Guna.UI2.AnimatorNS.DecorationType.None);
                    userPn.Location = new System.Drawing.Point(18, x);
                    x += 80;
                    userPn.Name = "userPn" + s;
                    userPn.Size = new System.Drawing.Size(215, 64);
                    userPn.TabIndex = 5;
                    userPn.Click += new System.EventHandler((sender, e) => this.userPn_Click(sender, e, s));
                    //userPn.Click += new System.EventHandler(this.userPn_Click);
                    //chattingUN.Text = s;
                }
                else
                {
                    userPn = new Guna.UI2.WinForms.Guna2Panel();
                    this.Controls.Add(userPn);
                    PanelTong.Controls.Add(userPn);
                    label2 = new System.Windows.Forms.Label();
                    guna2CirclePictureBox1 = new Guna.UI2.WinForms.Guna2CirclePictureBox();
                    circle = new Guna.UI2.WinForms.Guna2Shapes();
                    //Hình tròn trạng thái
                    circle.BorderThickness = 0;
                    guna2Transition1.SetDecoration(circle, Guna.UI2.AnimatorNS.DecorationType.None);
                    circle.BackColor = System.Drawing.Color.Transparent;
                    circle.FillColor = System.Drawing.Color.Gray;
                    circle.Location = new System.Drawing.Point(50, 7);
                    circle.Name = "guna2Shapes1" + s;
                    circle.PolygonSkip = 1;
                    circle.Rotate = 0F;
                    circle.Shape = Guna.UI2.WinForms.Enums.ShapeType.Ellipse;
                    circle.Size = new System.Drawing.Size(18, 18);
                    circle.TabIndex = 4;
                    circle.Text = "guna2Shapes1";
                    circle.Zoom = 80;
                    circle.UseTransparentBackground = true;
                    //Tên hiển thị
                    label2.AutoSize = true;
                    label2.BackColor = System.Drawing.Color.Transparent;
                    guna2Transition1.SetDecoration(label2, Guna.UI2.AnimatorNS.DecorationType.None);
                    label2.Font = new System.Drawing.Font("Yu Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                    label2.ForeColor = System.Drawing.Color.White;
                    label2.Location = new System.Drawing.Point(80, 20);
                    label2.Name = "label2" + s;
                    label2.Size = new System.Drawing.Size(116, 25);
                    label2.TabIndex = 0;
                    label2.Text = s;
                    //label2.Click += new System.EventHandler(this.userPn_Click);
                    label2.Click += new System.EventHandler((sender, e) => this.userPn_Click(sender, e, s));
                    //Hình hiển thị
                    guna2CirclePictureBox1.BackColor = System.Drawing.Color.Transparent;
                    guna2Transition1.SetDecoration(guna2CirclePictureBox1, Guna.UI2.AnimatorNS.DecorationType.None);
                    guna2CirclePictureBox1.Image = global::ChatApplication.Properties.Resources._274242879_916713445688196_4178666346407724582_n;
                    guna2CirclePictureBox1.ImageRotate = 0F;
                    guna2CirclePictureBox1.Location = new System.Drawing.Point(17, 9);
                    guna2CirclePictureBox1.Name = "guna2CirclePictureBox1" + s;
                    guna2CirclePictureBox1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
                    guna2CirclePictureBox1.Size = new System.Drawing.Size(47, 46);
                    guna2CirclePictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    guna2CirclePictureBox1.TabIndex = 1;
                    guna2CirclePictureBox1.TabStop = false;
                    guna2CirclePictureBox1.UseTransparentBackground = true;
                    guna2CirclePictureBox1.Click += new System.EventHandler((sender, e) => this.userPn_Click(sender, e, s));
                    //Bộ khung
                    //userPn.BringToFront();
                    userPn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(43)))));
                    userPn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(20)))), ((int)(((byte)(137)))));
                    userPn.BorderRadius = 15;
                    userPn.BorderThickness = 2;
                    userPn.Controls.Add(label2);
                    userPn.Controls.Add(circle);
                    userPn.Controls.Add(guna2CirclePictureBox1);
                    guna2Transition1.SetDecoration(userPn, Guna.UI2.AnimatorNS.DecorationType.None);
                    userPn.Location = new System.Drawing.Point(18, x);
                    x += 80;
                    userPn.Name = "userPn" + s;
                    userPn.Size = new System.Drawing.Size(215, 64);
                    userPn.TabIndex = 5;
                    //userPn.Click += new System.EventHandler(this.userPn_Click);
                    userPn.Click += new System.EventHandler((sender, e) => this.userPn_Click(sender, e, s));
                    //chattingUN.Text = s;
                }

            }
        }

        public void createUserPanel_thread(List<string> DS_member, List<string> DSClient)
        {

            int x = 0;
            foreach (string s in DS_member)
            {
                if (DSClient.Contains(s))
                {
                    userPn = new Guna.UI2.WinForms.Guna2Panel();


                    this.Invoke((MethodInvoker)delegate
                    {

                        this.Controls.Add(userPn);
                        PanelTong.Controls.Add(userPn);
                        label2 = new System.Windows.Forms.Label();
                        guna2CirclePictureBox1 = new Guna.UI2.WinForms.Guna2CirclePictureBox();
                        circle = new Guna.UI2.WinForms.Guna2Shapes();
                        //Hình tròn trạng thái
                        circle.BorderThickness = 0;
                        guna2Transition1.SetDecoration(circle, Guna.UI2.AnimatorNS.DecorationType.None);
                        circle.BackColor = System.Drawing.Color.Transparent;
                        circle.FillColor = System.Drawing.Color.Lime;
                        circle.Location = new System.Drawing.Point(50, 7);
                        circle.Name = "guna2Shapes1" + s;
                        circle.PolygonSkip = 1;
                        circle.Rotate = 0F;
                        circle.Shape = Guna.UI2.WinForms.Enums.ShapeType.Ellipse;
                        circle.Size = new System.Drawing.Size(18, 18);
                        circle.TabIndex = 4;
                        circle.Text = "guna2Shapes1";
                        circle.Zoom = 80;
                        circle.UseTransparentBackground = true;
                        //Tên hiển thị
                        label2.AutoSize = true;
                        label2.BackColor = System.Drawing.Color.Transparent;
                        guna2Transition1.SetDecoration(label2, Guna.UI2.AnimatorNS.DecorationType.None);
                        label2.Font = new System.Drawing.Font("Yu Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                        label2.ForeColor = System.Drawing.Color.White;
                        label2.Location = new System.Drawing.Point(80, 20);
                        label2.Name = "label2" + s;
                        label2.Size = new System.Drawing.Size(116, 25);
                        label2.TabIndex = 0;
                        label2.Text = s;
                        label2.Click += new System.EventHandler((sender, e) => this.userPn_Click(sender, e, s));
                        //label2.Click += new System.EventHandler(this.userPn_Click);
                        //Hình hiển thị
                        guna2CirclePictureBox1.BackColor = System.Drawing.Color.Transparent;
                        guna2Transition1.SetDecoration(guna2CirclePictureBox1, Guna.UI2.AnimatorNS.DecorationType.None);
                        guna2CirclePictureBox1.Image = global::ChatApplication.Properties.Resources._274242879_916713445688196_4178666346407724582_n;
                        guna2CirclePictureBox1.ImageRotate = 0F;
                        guna2CirclePictureBox1.Location = new System.Drawing.Point(17, 9);
                        guna2CirclePictureBox1.Name = "guna2CirclePictureBox1" + s;
                        guna2CirclePictureBox1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
                        guna2CirclePictureBox1.Size = new System.Drawing.Size(47, 46);
                        guna2CirclePictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                        guna2CirclePictureBox1.TabIndex = 1;
                        guna2CirclePictureBox1.TabStop = false;
                        guna2CirclePictureBox1.UseTransparentBackground = true;
                        guna2CirclePictureBox1.Click += new System.EventHandler((sender, e) => this.userPn_Click(sender, e, s));
                        //guna2CirclePictureBox1.Click += new System.EventHandler(this.userPn_Click);
                        //Bộ khung
                        //userPn.BringToFront();
                        userPn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(43)))));
                        userPn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(20)))), ((int)(((byte)(137)))));
                        userPn.BorderRadius = 15;
                        userPn.BorderThickness = 2;
                        userPn.Controls.Add(label2);
                        userPn.Controls.Add(circle);
                        userPn.Controls.Add(guna2CirclePictureBox1);
                        guna2Transition1.SetDecoration(userPn, Guna.UI2.AnimatorNS.DecorationType.None);
                        userPn.Location = new System.Drawing.Point(18, x);
                        x += 80;
                        userPn.Name = "userPn" + s;
                        userPn.Size = new System.Drawing.Size(215, 64);
                        userPn.TabIndex = 5;
                        userPn.Click += new System.EventHandler((sender, e) => this.userPn_Click(sender, e, s));
                        //userPn.Click += new System.EventHandler(this.userPn_Click);
                        //chattingUN.Text = s;
                    });


                }
                else
                {
                    this.Invoke((MethodInvoker)delegate
                    {

                        userPn = new Guna.UI2.WinForms.Guna2Panel();
                        this.Controls.Add(userPn);
                        PanelTong.Controls.Add(userPn);
                        label2 = new System.Windows.Forms.Label();
                        guna2CirclePictureBox1 = new Guna.UI2.WinForms.Guna2CirclePictureBox();
                        circle = new Guna.UI2.WinForms.Guna2Shapes();
                        //Hình tròn trạng thái
                        circle.BorderThickness = 0;
                        guna2Transition1.SetDecoration(circle, Guna.UI2.AnimatorNS.DecorationType.None);
                        circle.BackColor = System.Drawing.Color.Transparent;
                        circle.FillColor = System.Drawing.Color.Gray;
                        circle.Location = new System.Drawing.Point(50, 7);
                        circle.Name = "guna2Shapes1" + s;
                        circle.PolygonSkip = 1;
                        circle.Rotate = 0F;
                        circle.Shape = Guna.UI2.WinForms.Enums.ShapeType.Ellipse;
                        circle.Size = new System.Drawing.Size(18, 18);
                        circle.TabIndex = 4;
                        circle.Text = "guna2Shapes1";
                        circle.Zoom = 80;
                        circle.UseTransparentBackground = true;
                        //Tên hiển thị
                        label2.AutoSize = true;
                        label2.BackColor = System.Drawing.Color.Transparent;
                        guna2Transition1.SetDecoration(label2, Guna.UI2.AnimatorNS.DecorationType.None);
                        label2.Font = new System.Drawing.Font("Yu Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                        label2.ForeColor = System.Drawing.Color.White;
                        label2.Location = new System.Drawing.Point(80, 20);
                        label2.Name = "label2" + s;
                        label2.Size = new System.Drawing.Size(116, 25);
                        label2.TabIndex = 0;
                        label2.Text = s;
                        //label2.Click += new System.EventHandler(this.userPn_Click);
                        label2.Click += new System.EventHandler((sender, e) => this.userPn_Click(sender, e, s));
                        //Hình hiển thị
                        guna2CirclePictureBox1.BackColor = System.Drawing.Color.Transparent;
                        guna2Transition1.SetDecoration(guna2CirclePictureBox1, Guna.UI2.AnimatorNS.DecorationType.None);
                        guna2CirclePictureBox1.Image = global::ChatApplication.Properties.Resources._274242879_916713445688196_4178666346407724582_n;
                        guna2CirclePictureBox1.ImageRotate = 0F;
                        guna2CirclePictureBox1.Location = new System.Drawing.Point(17, 9);
                        guna2CirclePictureBox1.Name = "guna2CirclePictureBox1" + s;
                        guna2CirclePictureBox1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
                        guna2CirclePictureBox1.Size = new System.Drawing.Size(47, 46);
                        guna2CirclePictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                        guna2CirclePictureBox1.TabIndex = 1;
                        guna2CirclePictureBox1.TabStop = false;
                        guna2CirclePictureBox1.UseTransparentBackground = true;
                        guna2CirclePictureBox1.Click += new System.EventHandler((sender, e) => this.userPn_Click(sender, e, s));
                        //Bộ khung
                        //userPn.BringToFront();
                        userPn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(43)))));
                        userPn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(20)))), ((int)(((byte)(137)))));
                        userPn.BorderRadius = 15;
                        userPn.BorderThickness = 2;
                        userPn.Controls.Add(label2);
                        userPn.Controls.Add(circle);
                        userPn.Controls.Add(guna2CirclePictureBox1);
                        guna2Transition1.SetDecoration(userPn, Guna.UI2.AnimatorNS.DecorationType.None);
                        userPn.Location = new System.Drawing.Point(18, x);
                        x += 80;
                        userPn.Name = "userPn" + s;
                        userPn.Size = new System.Drawing.Size(215, 64);
                        userPn.TabIndex = 5;
                        //userPn.Click += new System.EventHandler(this.userPn_Click);
                        userPn.Click += new System.EventHandler((sender, e) => this.userPn_Click(sender, e, s));
                        //chattingUN.Text = s;
                    });
                }

            }
        }

        public void createGroupPanel_thread(List<string> DS_member, List<string> DSClient)
        {

            int x = 0;
            foreach (string s in DS_member)
            {
                if (DSClient.Contains(s))
                {
                    groupPn = new Guna.UI2.WinForms.Guna2Panel();


                    this.Invoke((MethodInvoker)delegate
                    {

                        this.Controls.Add(groupPn);
                        PanelTong.Controls.Add(groupPn);
                        groupName = new System.Windows.Forms.Label();
                        groupPic = new Guna.UI2.WinForms.Guna2CirclePictureBox();
                        circle2 = new Guna.UI2.WinForms.Guna2Shapes();
                        //Hình tròn trạng thái
                        circle2.BorderThickness = 0;
                        guna2Transition1.SetDecoration(circle2, Guna.UI2.AnimatorNS.DecorationType.None);
                        circle2.BackColor = System.Drawing.Color.Transparent;
                        circle2.FillColor = System.Drawing.Color.Lime;
                        circle2.Location = new System.Drawing.Point(50, 7);
                        circle2.Name = "guna2Shapes1" + s;
                        circle2.PolygonSkip = 1;
                        circle2.Rotate = 0F;
                        circle2.Shape = Guna.UI2.WinForms.Enums.ShapeType.Ellipse;
                        circle2.Size = new System.Drawing.Size(18, 18);
                        circle2.TabIndex = 4;
                        circle2.Text = "guna2Shapes1";
                        circle2.Zoom = 80;
                        circle2.UseTransparentBackground = true;
                        //Tên hiển thị
                        groupName.AutoSize = true;
                        groupName.BackColor = System.Drawing.Color.Transparent;
                        guna2Transition1.SetDecoration(groupName, Guna.UI2.AnimatorNS.DecorationType.None);
                        groupName.Font = new System.Drawing.Font("Yu Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                        groupName.ForeColor = System.Drawing.Color.White;
                        groupName.Location = new System.Drawing.Point(80, 20);
                        groupName.Name = "label2" + s;
                        groupName.Size = new System.Drawing.Size(116, 25);
                        groupName.TabIndex = 0;
                        groupName.Text = s;
                        groupName.Click += new System.EventHandler((sender, e) => this.groupPn_Click(sender, e, s));
                        //label2.Click += new System.EventHandler(this.userPn_Click);
                        //Hình hiển thị
                        groupPic.BackColor = System.Drawing.Color.Transparent;
                        guna2Transition1.SetDecoration(groupPic, Guna.UI2.AnimatorNS.DecorationType.None);
                        groupPic.Image = global::ChatApplication.Properties.Resources._274242879_916713445688196_4178666346407724582_n;
                        groupPic.ImageRotate = 0F;
                        groupPic.Location = new System.Drawing.Point(17, 9);
                        groupPic.Name = "guna2CirclePictureBox1" + s;
                        groupPic.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
                        groupPic.Size = new System.Drawing.Size(47, 46);
                        groupPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                        groupPic.TabIndex = 1;
                        groupPic.TabStop = false;
                        groupPic.UseTransparentBackground = true;
                        groupPic.Click += new System.EventHandler((sender, e) => this.groupPn_Click(sender, e, s));
                        //guna2CirclePictureBox1.Click += new System.EventHandler(this.userPn_Click);
                        //Bộ khung
                        //userPn.BringToFront();
                        groupPn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(43)))));
                        groupPn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(20)))), ((int)(((byte)(137)))));
                        groupPn.BorderRadius = 15;
                        groupPn.BorderThickness = 2;
                        groupPn.Controls.Add(groupName);
                        groupPn.Controls.Add(circle2);
                        groupPn.Controls.Add(guna2CirclePictureBox1);
                        guna2Transition1.SetDecoration(groupPn, Guna.UI2.AnimatorNS.DecorationType.None);
                        groupPn.Location = new System.Drawing.Point(18, x);
                        x += 80;
                        groupPn.Name = "groupPn" + s;
                        groupPn.Size = new System.Drawing.Size(215, 64);
                        groupPn.TabIndex = 5;
                        groupPn.Click += new System.EventHandler((sender, e) => this.groupPn_Click(sender, e, s));
                        //userPn.Click += new System.EventHandler(this.userPn_Click);
                        //chattingUN.Text = s;
                    });


                }
                else
                {
                    this.Invoke((MethodInvoker)delegate
                    {

                        this.Controls.Add(groupPn);
                        PanelTong.Controls.Add(groupPn);
                        groupName = new System.Windows.Forms.Label();
                        groupPic = new Guna.UI2.WinForms.Guna2CirclePictureBox();
                        circle2 = new Guna.UI2.WinForms.Guna2Shapes();
                        //Hình tròn trạng thái
                        circle2.BorderThickness = 0;
                        guna2Transition1.SetDecoration(circle2, Guna.UI2.AnimatorNS.DecorationType.None);
                        circle2.BackColor = System.Drawing.Color.Transparent;
                        circle2.FillColor = System.Drawing.Color.Lime;
                        circle2.Location = new System.Drawing.Point(50, 7);
                        circle2.Name = "guna2Shapes1" + s;
                        circle2.PolygonSkip = 1;
                        circle2.Rotate = 0F;
                        circle2.Shape = Guna.UI2.WinForms.Enums.ShapeType.Ellipse;
                        circle2.Size = new System.Drawing.Size(18, 18);
                        circle2.TabIndex = 4;
                        circle2.Text = "guna2Shapes1";
                        circle2.Zoom = 80;
                        circle2.UseTransparentBackground = true;
                        //Tên hiển thị
                        groupName.AutoSize = true;
                        groupName.BackColor = System.Drawing.Color.Transparent;
                        guna2Transition1.SetDecoration(groupName, Guna.UI2.AnimatorNS.DecorationType.None);
                        groupName.Font = new System.Drawing.Font("Yu Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                        groupName.ForeColor = System.Drawing.Color.White;
                        groupName.Location = new System.Drawing.Point(80, 20);
                        groupName.Name = "label2" + s;
                        groupName.Size = new System.Drawing.Size(116, 25);
                        groupName.TabIndex = 0;
                        groupName.Text = s;
                        groupName.Click += new System.EventHandler((sender, e) => this.groupPn_Click(sender, e, s));
                        //label2.Click += new System.EventHandler(this.userPn_Click);
                        //Hình hiển thị
                        groupPic.BackColor = System.Drawing.Color.Transparent;
                        guna2Transition1.SetDecoration(groupPic, Guna.UI2.AnimatorNS.DecorationType.None);
                        groupPic.Image = global::ChatApplication.Properties.Resources._274242879_916713445688196_4178666346407724582_n;
                        groupPic.ImageRotate = 0F;
                        groupPic.Location = new System.Drawing.Point(17, 9);
                        groupPic.Name = "guna2CirclePictureBox1" + s;
                        groupPic.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
                        groupPic.Size = new System.Drawing.Size(47, 46);
                        groupPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                        groupPic.TabIndex = 1;
                        groupPic.TabStop = false;
                        groupPic.UseTransparentBackground = true;
                        groupPic.Click += new System.EventHandler((sender, e) => this.groupPn_Click(sender, e, s));
                        //guna2CirclePictureBox1.Click += new System.EventHandler(this.userPn_Click);
                        //Bộ khung
                        //userPn.BringToFront();
                        groupPn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(43)))));
                        groupPn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(20)))), ((int)(((byte)(137)))));
                        groupPn.BorderRadius = 15;
                        groupPn.BorderThickness = 2;
                        groupPn.Controls.Add(groupName);
                        groupPn.Controls.Add(circle2);
                        groupPn.Controls.Add(guna2CirclePictureBox1);
                        guna2Transition1.SetDecoration(groupPn, Guna.UI2.AnimatorNS.DecorationType.None);
                        groupPn.Location = new System.Drawing.Point(18, x);
                        x += 80;
                        groupPn.Name = "groupPn" + s;
                        groupPn.Size = new System.Drawing.Size(215, 64);
                        groupPn.TabIndex = 5;
                        groupPn.Click += new System.EventHandler((sender, e) => this.groupPn_Click(sender, e, s));
                        //userPn.Click += new System.EventHandler(this.userPn_Click);
                        //chattingUN.Text = s;
                    });
                }

            }
        }


        private void userPn_Click(object sender, System.EventArgs e, string aa)
        {
            chattingUN.Text = aa;
            chatPanel.Visible = true;
            chatBoxPn.Controls.Clear();
            chattingUnPn.Visible = true;
        }

        private void groupPn_Click(object sender, System.EventArgs e, string aa)
        {
            chattingUN.Text = aa;
            chatPanel.Visible = true;
            chatBoxPn.Controls.Clear();
            chattingUnPn.Visible = true;
        }

        private void closeBox_Click(object sender, EventArgs e)
        {
            Form frm = Application.OpenForms["Form1"];
            frm.Invoke((MethodInvoker)(() => frm.Close()));
        }

        private void logout_Click(object sender, EventArgs e)
        {
            //thoat = true;


            MESSAGE.COMMON common = new MESSAGE.COMMON(4, all_user);
            sendJson(common);
            thoat = true;
            this.Close();
            Form frm = Application.OpenForms["Form1"];
            frm.Invoke((MethodInvoker)(() => frm.Show()));




        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            addGroupPanel.Visible = false;
        }

        private void createImageSend(string path)
        {
            this.Invoke((MethodInvoker)delegate
            {
                imageContainer = new Guna.UI2.WinForms.Guna2Panel();
            imageSend = new Guna.UI2.WinForms.Guna2PictureBox();
            this.Controls.Add(imageContainer);
            chatBoxPn.Controls.Add(imageContainer);

            imageContainer.Controls.Add(imageSend);
                imageContainer.BringToFront();
                guna2Transition1.SetDecoration(imageContainer, Guna.UI2.AnimatorNS.DecorationType.None);

            imageContainer.Dock = System.Windows.Forms.DockStyle.Top;
            imageContainer.Location = new System.Drawing.Point(0, 0);
            imageContainer.Name = "imageContainer";
            imageContainer.Size = new System.Drawing.Size(761, 187);
            imageContainer.TabIndex = 0;

            guna2Transition1.SetDecoration(imageSend, Guna.UI2.AnimatorNS.DecorationType.None);
            imageSend.ImageRotate = 0F;
            imageSend.SizeMode = PictureBoxSizeMode.Zoom;
            imageSend.Location = new System.Drawing.Point(461, 7);
            imageSend.Name = "imageSend";
            imageSend.Size = new System.Drawing.Size(262, 171);
            imageSend.TabIndex = 0;
            imageSend.TabStop = false;
                imageSend.Image =Image.FromFile(path);
            });
        }

        private void createImageRecv(string path)
        {
            imageContainer2 = new Guna.UI2.WinForms.Guna2Panel();
            imageRecv = new Guna.UI2.WinForms.Guna2PictureBox();
            this.Invoke((MethodInvoker)delegate
            {
          
            this.Controls.Add(imageContainer2);
            chatBoxPn.Controls.Add(imageContainer2);

            imageContainer2.Controls.Add(imageRecv);
            imageContainer2.BringToFront();
            guna2Transition1.SetDecoration(imageContainer2, Guna.UI2.AnimatorNS.DecorationType.None);
            imageContainer2.Dock = System.Windows.Forms.DockStyle.Top;
            imageContainer2.Location = new System.Drawing.Point(0, 187);
            imageContainer2.Name = "imageContainer2";
            imageContainer2.Size = new System.Drawing.Size(761, 187);
            imageContainer2.TabIndex = 1;

            guna2Transition1.SetDecoration(imageRecv, Guna.UI2.AnimatorNS.DecorationType.None);
            imageRecv.ImageRotate = 0F;
            imageRecv.SizeMode = PictureBoxSizeMode.Zoom;
            imageRecv.Location = new System.Drawing.Point(35, 8);
            imageRecv.Name = "imageRecv";
            imageRecv.Size = new System.Drawing.Size(262, 171);
            imageRecv.TabIndex = 0;
            imageRecv.TabStop = false;
                imageRecv.Image = Image.FromFile(path);
            });
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {

            MESSAGE.MESSAGE mes = new MESSAGE.MESSAGE(all_user, chattingUN.Text, txtchatbox.Text);
            //var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(mes);
            MESSAGE.COMMON common = new MESSAGE.COMMON(2, jsonString);
            sendJson(common);


            //createSendView();
            //createRecvView("hello");

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           // vonglap();
            Thread trd1 = new Thread(new ThreadStart(this.vonglap));
            trd1.IsBackground = true;
            trd1.Start();
        }

        private void chattingUN_Click(object sender, EventArgs e)
        {

        }

        private void btnReload_Click(object sender, EventArgs e)
        {

            PanelTong.Controls.Clear();
            MESSAGE.COMMON common = new MESSAGE.COMMON(11, "reload");
            sendJson(common);
        }
        int originalExStyle = -1;
        bool enableFormLevelDoubleBuffering = true;
        protected override CreateParams CreateParams
        {
            get
            {
                if (originalExStyle == -1)
                    originalExStyle = base.CreateParams.ExStyle;

                CreateParams cp = base.CreateParams;
                if (enableFormLevelDoubleBuffering)
                    cp.ExStyle |= 0x02000000;  // WS_EX_COMPOSITED
                else
                    cp.ExStyle = originalExStyle;

                return cp;
            }
        }

        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            Thread t = new Thread((ThreadStart)(() =>
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    //namepath.Text = filePath;
                    //textBox2.Text = Path.GetFileName(openFileDialog.FileName);
                    var fileStream = openFileDialog.OpenFile();
                    string path = "";
                    filePath = filePath.Replace("\\", "/");
                    while (filePath.IndexOf("/") > -1)
                    {
                        path += filePath.Substring(0, filePath.IndexOf("/") + 1);
                        filePath = filePath.Substring(filePath.IndexOf("/") + 1);
                    }
                    string fileName = filePath;// "c:\\filetosend.txt";
                    byte[] fileNameByte = Encoding.ASCII.GetBytes(fileName);
                    byte[] fileNameLen = BitConverter.GetBytes(fileNameByte.Length);
                    byte[] fileData = File.ReadAllBytes(path + fileName);
                    byte[] clientData = new byte[4 + fileNameByte.Length + fileData.Length];
                    nameFileGui = fileName;
                    if (clientData.Length > 1024 * 5000)
                    {
                        new Thread(() =>
                        {
                            MessageBox.Show(" Kích thước file không được lớn hơn 5mb ");
                        }).Start();
                    }
                    else
                    {
                        fileNameLen.CopyTo(clientData, 0);
                        fileNameByte.CopyTo(clientData, 4);
                        fileData.CopyTo(clientData, 4 + fileNameByte.Length);
                        new Thread(() =>
                        {
                            MESSAGE.FILE file = new MESSAGE.FILE(all_user, chattingUN.Text, clientData);
                            string jsonString = JsonSerializer.Serialize(file);
                            MESSAGE.COMMON common = new MESSAGE.COMMON(9, jsonString);
                            sendJson(common);
                        }).Start();




                    }
                }
            }));

            // Run your code from a thread that joins the STA Thread
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
        }
        private void guna2PictureBox4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            Thread t = new Thread((ThreadStart)(() =>
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    //namepath.Text = filePath;
                    //textBox2.Text = Path.GetFileName(openFileDialog.FileName);
                    var fileStream = openFileDialog.OpenFile();
                    string path = "";
                    filePath = filePath.Replace("\\", "/");
                    while (filePath.IndexOf("/") > -1)
                    {
                        path += filePath.Substring(0, filePath.IndexOf("/") + 1);
                        filePath = filePath.Substring(filePath.IndexOf("/") + 1);
                    }
                    string fileName = filePath;// "c:\\filetosend.txt";
                    byte[] fileNameByte = Encoding.ASCII.GetBytes(fileName);
                    byte[] fileNameLen = BitConverter.GetBytes(fileNameByte.Length);
                    byte[] fileData = File.ReadAllBytes(path + fileName);
                    byte[] clientData = new byte[4 + fileNameByte.Length + fileData.Length];
                    
                    pathGui = path + fileName;
                    if (clientData.Length > 1024 * 5000)
                    {
                        new Thread(() =>
                        {
                            MessageBox.Show(" Kích thước file không được lớn hơn 5mb ");
                        }).Start();
                    }
                    else
                    {
                        fileNameLen.CopyTo(clientData, 0);
                        fileNameByte.CopyTo(clientData, 4);
                        fileData.CopyTo(clientData, 4 + fileNameByte.Length);
                        new Thread(() =>
                        {
                            MESSAGE.FILE file = new MESSAGE.FILE(all_user, chattingUN.Text, clientData);
                            string jsonString = JsonSerializer.Serialize(file);
                            MESSAGE.COMMON common = new MESSAGE.COMMON(8, jsonString);
                            sendJson(common);
                        }).Start();




                    }
                }
            }));

            // Run your code from a thread that joins the STA Thread
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
        }
        private void guna2PictureBox5_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("a");
            if (listView1.Visible == true)
            {
                listView1.Visible = false;
            }
            else
            {
                listView1.Visible = true;
                string[] icons = { "D83DDE01", "D83DDE02", "D83DDE03", "D83DDE04", "D83DDE05", "D83DDE06", "D83DDE07", "D83DDE08", "D83DDE09", "D83DDE10", "D83DDE11", "D83DDE12", "D83DDE13", "D83DDE14", "D83DDE15", "D83DDE16", "D83DDE17", "D83DDE18", "D83DDE19", "D83DDE20" };
                for (int i = 0; i < icons.Length; i++)
                {
                    listView1.Items[i].Text = ParseUnicodeHex(icons[i]);
                }

            }
        }
        string ParseUnicodeHex(string hex)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < hex.Length; i += 4)
            {
                string temp = hex.Substring(i, 4);
                char character = (char)Convert.ToInt16(temp, 16);
                sb.Append(character);
            }
            return sb.ToString();
        }

        private void groupChatBtn_Click(object sender, EventArgs e)
        {
            if (groupChatBtn.FillColor == Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(20)))), ((int)(((byte)(137))))))
            {
                groupChatBtn.FillColor = Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(103)))), ((int)(((byte)(228)))));
                PanelTong.Controls.Clear();
                //createGroupPanel_thread();
            }
            else
            {
                groupChatBtn.FillColor = Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(20)))), ((int)(((byte)(137)))));
                PanelTong.Controls.Clear();
            }
        }

        private void addNewGroupBtn_Click(object sender, EventArgs e)
        {
            addNewGr.Visible = false;
        }

        private void addGroupBtn_Click(object sender, EventArgs e)
        {
            if (addNewGr.Visible == true)
            {
                addNewGr.Visible = false;
            }
            else if (chatPanel.Visible == true)
            {
                chatPanel.Visible = false;
                chattingUnPn.Visible = false;
                addNewGr.Visible = true;
            }
            else
            {
                addNewGr.Visible = true;
            }
            addNewGr.BringToFront();
        }

        private void addMemberBtn_Click(object sender, EventArgs e)
        {
            if (addGroupPanel.Visible == true)
            {
                addGroupPanel.Visible = false;
            }
            else if (chatPanel.Visible == true)
            {
                chatPanel.Visible = false;
                chattingUnPn.Visible = false;
                addGroupPanel.Visible = true;
            }
            else
            {
                addGroupPanel.Visible = true;
            }
            addGroupPanel.BringToFront();
        }

        private void chatBoxPn_Paint(object sender, PaintEventArgs e)
        {

        }

        private void emoji_list_MouseClick(object sender, MouseEventArgs e)
        {
            string i = listView1.SelectedItems[0].Text;
            txtchatbox.Text += i;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && chatPanel.Visible == true)
                sendBtn.PerformClick();
        }
    }
}