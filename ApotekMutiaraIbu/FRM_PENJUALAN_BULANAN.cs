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
    public partial class FRM_PENJUALAN_BULANAN : Form
    {
        SqlConnection con = new SqlConnection("Server=DESKTOP-6PC0I83;Database=ApotekMutiaraIbu;Integrated Security=true;");
        SqlCommand cmd;
        SqlDataAdapter adapt;
        SqlDataReader rd;

        private void DisplayData()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from tbl_penjualan_detail", con);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        public FRM_PENJUALAN_BULANAN()
        {
            InitializeComponent();
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            CETAK_PENJUALAN_BULANAN CPB = new CETAK_PENJUALAN_BULANAN();
            CPB.Show();
        }

        private void FRM_PENJUALAN_BULANAN_Load(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("insert into tbl_penjualan_tahunan select * from tbl_penjualan_detail", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Berhasil simpan");
            cmd = new SqlCommand("delete tbl_penjualan_detail", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            DisplayData();
        }
    }
}
