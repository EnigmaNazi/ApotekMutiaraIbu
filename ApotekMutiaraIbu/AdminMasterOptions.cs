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
    public partial class AdminMasterOptions : Form
    {
        SqlConnection con = new SqlConnection("Server=DESKTOP-6PC0I83;Database=ApotekMutiaraIbu;Integrated Security=true;");
        SqlCommand cmd;
        SqlDataAdapter adapt;
        SqlDataReader rd;

        private void DisplayData()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from tbl_login", con);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

        }

        private void cleartext()
        {
            txt_username.Text = "";
            txt_password.Text = "";
        }

        public AdminMasterOptions()
        {
            InitializeComponent();
            cleartext();
            DisplayData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            FRM_LOGIN FLOGIN = new FRM_LOGIN();
            FLOGIN.Show();
        }

        private void button_new_Click(object sender, EventArgs e)
        {
            cleartext();
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            if (txt_username.Text != "" && txt_password.Text != "")
            {
                cmd = new SqlCommand("insert into tbl_login(username,password) values(@username,@password)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@username", txt_username.Text);
                cmd.Parameters.AddWithValue("@password", txt_password.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Data berhasil disimpan");
                DisplayData();
                cleartext();
            }
            else
            {
                MessageBox.Show("gagal simpan");
            }
        }

        private void button_edit_Click(object sender, EventArgs e)
        {
            if (txt_username.Text != "" && txt_password.Text != "")
            {
                cmd = new SqlCommand("UPDATE tbl_login SET password=@password WHERE username=@username", con);
                con.Open();
                cmd.Parameters.AddWithValue("@username", txt_username.Text);
                cmd.Parameters.AddWithValue("@password", txt_password.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Data berhasil diperbarui");
                DisplayData();
                cleartext();
            }
            else
            {
                MessageBox.Show("gagal memperbarui data");
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (txt_username.Text != "admin" && txt_username.Text != "")
            {
                cmd = new SqlCommand("delete tbl_login where username=@username", con);
                con.Open();
                cmd.Parameters.AddWithValue("@username", txt_username.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("berhasil hapus");
                DisplayData();
                cleartext();
            }
            else
            {
                MessageBox.Show("gagal hapus");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_username.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txt_password.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            string query = "SELECT * FROM tbl_login WHERE username like '%" + txt_search.Text + "%'";
            cmd = new SqlCommand(query, con);
            adapt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
