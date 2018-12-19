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
    public partial class FRM_STOCK_OPNAME : Form
    {
        SqlConnection con = new SqlConnection("Server=DESKTOP-6PC0I83;Database=ApotekMutiaraIbu;Integrated Security=true;");
        SqlCommand cmd;
        SqlDataAdapter adapt;
        SqlDataReader rd;

        private void DisplayData()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from DataStock;", con);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        public FRM_STOCK_OPNAME()
        {
            InitializeComponent();
            DisplayData();
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void FRM_LIHAT_DATA_Load(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            string query = "SELECT * FROM DataStock WHERE tgl_masuk like '%" + txt_search.Text + "%'";
            cmd = new SqlCommand(query, con);
            adapt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
