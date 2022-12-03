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
        private Guna.UI2.WinForms.Guna2Shapes circle;
        public Form1()
        {
            InitializeComponent();
            PanelContainer();
            createUserPanel();
        }

        private void PanelContainer()
        {
            PanelTong = new Guna.UI2.WinForms.Guna2Panel();

            this.Controls.Add(PanelTong);
            PanelTong.BringToFront();
            PanelTong.AutoScroll = true;
            guna2Transition1.SetDecoration(PanelTong, Guna.UI2.AnimatorNS.DecorationType.None);
            PanelTong.Location = new System.Drawing.Point(3, 174);
            PanelTong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(43)))));
            PanelTong.Name = "panelTong";
            PanelTong.Size = new System.Drawing.Size(258, 432);
            PanelTong.TabIndex = 4;
        }

        private void createUserPanel()
        {
            
            int x = 0;
            for(int i=1; i<12; i++)
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
                circle.Name = "guna2Shapes1" + i.ToString();
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
                userPn.Controls.Add(circle);
                userPn.Controls.Add(guna2CirclePictureBox1);
                guna2Transition1.SetDecoration(userPn, Guna.UI2.AnimatorNS.DecorationType.None);
                userPn.Location = new System.Drawing.Point(18, x);
                x += 80;
                userPn.Name = "userPn" + i.ToString();
                userPn.Size = new System.Drawing.Size(215, 64);
                userPn.TabIndex = 5;
            }
        }

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