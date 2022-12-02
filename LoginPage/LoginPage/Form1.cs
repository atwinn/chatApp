using Guna.UI2.AnimatorNS;
using Guna.UI2.Material.Animation;
using Guna.UI2.WinForms;
using MESSAGE;
using ChatApplication;
namespace LoginPage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
            msg_pn.Visible = true;
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            pn_login.Visible = true;
            msg_pn.Visible = false;
            guna2Transition1.ShowSync(pn_login);
        }

        private void login_Click(object sender, EventArgs e)
        {
            ChatApplication.Form1 form = new ChatApplication.Form1();
            this.Hide();
            guna2Transition1.Show(form);
            form.Show();
        }
    }
}