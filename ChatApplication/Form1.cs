using Guna.UI2.WinForms;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;

namespace ChatApplication
{
    public partial class Form1 : Form
    {
        private Guna.UI2.WinForms.Guna2Panel userPn;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2CirclePictureBox guna2CirclePictureBox1;
        private Guna.UI2.WinForms.Guna2Panel PanelTong;
        private Guna.UI2.WinForms.Guna2VScrollBar Scroll;
        public Form1()
        {
            InitializeComponent();

            //userPn = new Guna.UI2.WinForms.Guna2Panel();
            //this.Controls.Add(userPn);
            //label2 = new System.Windows.Forms.Label();
            //guna2CirclePictureBox1 = new Guna.UI2.WinForms.Guna2CirclePictureBox();

            //label2.AutoSize = true;
            //label2.BackColor = System.Drawing.Color.Transparent;
            //guna2Transition1.SetDecoration(label2, Guna.UI2.AnimatorNS.DecorationType.None);
            //label2.Font = new System.Drawing.Font("Yu Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            //label2.ForeColor = System.Drawing.Color.White;
            //label2.Location = new System.Drawing.Point(80, 20);
            //label2.Name = "label2";
            //label2.Size = new System.Drawing.Size(116, 25);
            //label2.TabIndex = 0;
            //label2.Text = "Vy Vũ Luân";

            //guna2CirclePictureBox1.BackColor = System.Drawing.Color.Transparent;
            //guna2Transition1.SetDecoration(guna2CirclePictureBox1, Guna.UI2.AnimatorNS.DecorationType.None);
            //guna2CirclePictureBox1.Image = global::ChatApplication.Properties.Resources._274242879_916713445688196_4178666346407724582_n;
            //guna2CirclePictureBox1.ImageRotate = 0F;
            //guna2CirclePictureBox1.Location = new System.Drawing.Point(17, 9);
            //guna2CirclePictureBox1.Name = "guna2CirclePictureBox1";
            //guna2CirclePictureBox1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            //guna2CirclePictureBox1.Size = new System.Drawing.Size(47, 46);
            //guna2CirclePictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            //guna2CirclePictureBox1.TabIndex = 1;
            //guna2CirclePictureBox1.TabStop = false;
            //guna2CirclePictureBox1.UseTransparentBackground = true;

            //userPn.BringToFront();
            //userPn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(43)))));
            //userPn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(20)))), ((int)(((byte)(137)))));
            //userPn.BorderRadius = 15;
            //userPn.BorderThickness = 2;
            //userPn.Controls.Add(label2);
            //userPn.Controls.Add(guna2CirclePictureBox1);
            //guna2Transition1.SetDecoration(userPn, Guna.UI2.AnimatorNS.DecorationType.None);
            //userPn.Location = new System.Drawing.Point(18, 165);
            //userPn.Name = "userPn";
            //userPn.Size = new System.Drawing.Size(229, 64);
            //userPn.TabIndex = 0;
            PanelTong = new Guna.UI2.WinForms.Guna2Panel();
            Scroll = new Guna.UI2.WinForms.Guna2VScrollBar();

            this.Controls.Add(PanelTong);
            PanelTong.BringToFront();
            guna2Transition1.SetDecoration(PanelTong, Guna.UI2.AnimatorNS.DecorationType.None);
            PanelTong.Location = new System.Drawing.Point(3, 174);
            PanelTong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(43)))));
            PanelTong.Name = "panelTong";
            PanelTong.Size = new System.Drawing.Size(258, 432);
            PanelTong.TabIndex = 4;
            guna2Transition1.SetDecoration(Scroll, Guna.UI2.AnimatorNS.DecorationType.None);
            Scroll.Dock = System.Windows.Forms.DockStyle.Right;
            Scroll.FillColor = System.Drawing.Color.Transparent;
            Scroll.InUpdate = false;
            Scroll.LargeChange = 10;
            Scroll.Location = new System.Drawing.Point(248, 0);
            Scroll.Name = "scrollBar";
            Scroll.ScrollbarSize = 10;
            Scroll.Size = new System.Drawing.Size(10, 432);
            Scroll.TabIndex = 0;
            Scroll.ThumbColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(20)))), ((int)(((byte)(137)))));
            createUserPanel();
        }

        private void createUserPanel()
        {
            
            int x = 0;
            for(int i=1; i<10; i++)
            {
                userPn = new Guna.UI2.WinForms.Guna2Panel();
                this.Controls.Add(userPn);
                PanelTong.Controls.Add(userPn);
                PanelTong.Controls.Add(Scroll);
                label2 = new System.Windows.Forms.Label();
                guna2CirclePictureBox1 = new Guna.UI2.WinForms.Guna2CirclePictureBox();
                //Tên hiển thị
                label2.AutoSize = true;
                label2.BackColor = System.Drawing.Color.Transparent;
                guna2Transition1.SetDecoration(label2, Guna.UI2.AnimatorNS.DecorationType.None);
                label2.Font = new System.Drawing.Font("Yu Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                label2.ForeColor = System.Drawing.Color.White;
                label2.Location = new System.Drawing.Point(80, 20);
                label2.Name = "label2" + i.ToString();
                label2.Size = new System.Drawing.Size(116, 25);
                label2.TabIndex = 0;
                label2.Text = "Vy Vũ Luân";
                //Hình hiển thị
                guna2CirclePictureBox1.BackColor = System.Drawing.Color.Transparent;
                guna2Transition1.SetDecoration(guna2CirclePictureBox1, Guna.UI2.AnimatorNS.DecorationType.None);
                guna2CirclePictureBox1.Image = global::ChatApplication.Properties.Resources._274242879_916713445688196_4178666346407724582_n;
                guna2CirclePictureBox1.ImageRotate = 0F;
                guna2CirclePictureBox1.Location = new System.Drawing.Point(17, 9);
                guna2CirclePictureBox1.Name = "guna2CirclePictureBox1" + i.ToString();
                guna2CirclePictureBox1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
                guna2CirclePictureBox1.Size = new System.Drawing.Size(47, 46);
                guna2CirclePictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                guna2CirclePictureBox1.TabIndex = 1;
                guna2CirclePictureBox1.TabStop = false;
                guna2CirclePictureBox1.UseTransparentBackground = true;
                //Bộ khung
                //userPn.BringToFront();
                userPn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(43)))));
                userPn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(20)))), ((int)(((byte)(137)))));
                userPn.BorderRadius = 15;
                userPn.BorderThickness = 2;
                userPn.Controls.Add(label2);
                userPn.Controls.Add(guna2CirclePictureBox1);
                guna2Transition1.SetDecoration(userPn, Guna.UI2.AnimatorNS.DecorationType.None);
                userPn.Location = new System.Drawing.Point(18, x);
                x += 80;
                userPn.Name = "userPn" + i.ToString();
                userPn.Size = new System.Drawing.Size(229, 64);
                userPn.TabIndex = 5;
            }
        }



        //private void userPn_MouseLeave(object sender, EventArgs e)
        //{
        //    userPn.FillColor = Color.Transparent;
        //}

        //private void userPn_MouseEnter(object sender, EventArgs e)
        //{
        //    userPn.FillColor = Color.FromArgb(193, 20, 137);
        //}

        private void closeBox_Click(object sender, EventArgs e)
        {
            Form frm = Application.OpenForms["Form1"];
            frm.Invoke((MethodInvoker)(() => frm.Close()));
        }

        private void logout_Click(object sender, EventArgs e)
        {
            this.Close();
            Form frm = Application.OpenForms["Form1"];
            frm.Invoke((MethodInvoker)(() => frm.Show()));
        }
    }
}