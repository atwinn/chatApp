namespace ChatApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void userPn_MouseLeave(object sender, EventArgs e)
        {
            userPn.FillColor = Color.Transparent;
        }

        private void userPn_MouseEnter(object sender, EventArgs e)
        {
            userPn.FillColor = Color.FromArgb(193, 20, 137);
        }

        private void closeBox_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}