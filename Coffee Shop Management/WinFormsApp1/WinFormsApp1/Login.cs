using MaterialSkin;
using MaterialSkin.Controls;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;

namespace WinFormsApp1
{
    public partial class Login : MaterialForm

    {
        public Login()
        {
            InitializeComponent();
            this.ActiveControl = txtUsername;
            txtUsername.Focus();
            DataAccess da = new DataAccess();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Grey900, Primary.Grey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    

        private void materialCheckbox1_CheckedChanged(object sender, EventArgs e)
        {
            if(materialCheckbox1.Checked == true)
            {
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '*';
            }
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            if(txtUsername.Text == "")
            {
                label2.Text = "Enter your username";
                label2.ForeColor = Color.Red;
            }
            else
            {
                label2.Text = "";
            }
            if(txtPassword.Text == "")
            {
                label3.Text = "Enter your password";
                label3.ForeColor = Color.Red;
            }
            else
            {
                label3.Text = "";
            }
           

            // Server 
            string sql = "SELECT * FROM Users Where username = '" + this.txtUsername.Text + "' AND password='" + this.txtPassword.Text + "';";
            DataAccess da = new DataAccess();
            DataSet ds = da.ExecuteQuery(sql);

            if(ds.Tables[0].Rows.Count == 1)
            {
                AdminDashboard admin = new AdminDashboard();
                admin.Show();
                this.Hide();
            }
            else
            {

                MessageBox.Show("username or password is invalid, try again");
                txtUsername.Clear();
                txtPassword.Clear();
                txtUsername.Focus();
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {
            txtUsername.Clear();
            txtPassword.Clear();
            label2.Text = "";
            label3.Text = "";
            txtUsername.Focus();
        }
    }
}