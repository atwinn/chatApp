using Guna.UI2.AnimatorNS;
using Guna.UI2.Material.Animation;
using Guna.UI2.WinForms;
using MESSAGE;
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
    }
}