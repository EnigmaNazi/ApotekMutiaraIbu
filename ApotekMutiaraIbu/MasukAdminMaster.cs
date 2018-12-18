using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ApotekMutiaraIbu
{
    public partial class MasukAdminMaster : Form
    {
        SqlConnection con = new SqlConnection("Server=DESKTOP-6PC0I83;Database=ApotekMutiaraIbu;Integrated Security=true;");

        public MasukAdminMaster()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand com = new SqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            string strSQL = " select * from tbl_login where username ='Admin' and password='" + txt_password.Text + "'";

            com.CommandText = strSQL;
            SqlDataReader datareader = com.ExecuteReader();
            if (datareader.Read())
            {
                MessageBox.Show("Login berhasil", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AdminMasterOptions AMO = new AdminMasterOptions();
                AMO.ShowDialog();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Username dan Password Salah", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_password.Text = "";
            }
            con.Close();
        }

        private void btn_batal_Click(object sender, EventArgs e)
        {
            FRM_LOGIN LG = new FRM_LOGIN();
            LG.Show();
            this.Hide();
        }
    }
}
