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
    public partial class FRM_BARANG_DETAIL : Form
    {
        SqlConnection con = new SqlConnection("Server=DESKTOP-6PC0I83;Database=ApotekMutiaraIbu;Integrated Security=true;");
        SqlCommand cmd;
        SqlDataAdapter adapt;
        SqlDataReader rd;

        private void DisplayData()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from tbl_barang_detail", con);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

        }

        private void otomatis()
        {
            long hitung;
            string urut;

            con.Open();
            cmd = new SqlCommand("select kode_barang from tbl_barang_detail where kode_barang in(select max(kode_barang) from tbl_barang_detail) order by kode_barang desc", con);
            rd = cmd.ExecuteReader();
            rd.Read();
            if (rd.HasRows)
            {
                hitung = Convert.ToInt64(rd[0].ToString().Substring(rd["kode_barang"].ToString().Length - 4, 4)) + 1;

                string joinstr = "0000" + hitung;



                urut = "BRG" + joinstr.Substring(joinstr.Length - 4, 4);

            }
            else
            {
                urut = "BRG0001";
            }
            rd.Close();
            txt_kode_barang.Text = urut;
            con.Close();

        }
        private void cleartext()
        {
            txt_kode_barang.Text = "";
            txt_nama_barang.Text = "";
            txt_stock.Text = "";
            txt_kode_barang.Enabled = false;
        }

        public FRM_BARANG_DETAIL()
        {
            InitializeComponent();
            cleartext();
            DisplayData();
        }

        private void button_new_Click(object sender, EventArgs e)
        {
            otomatis();
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            if (txt_kode_barang.Text != "" && txt_nama_barang.Text != "" && txt_stock.Text != "")
            {
                cmd = new SqlCommand("insert into tbl_barang_detail(kode_barang,nama_barang,stok) values(@kode_barang,@nama_barang,@stok)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@kode_barang", txt_kode_barang.Text);
                cmd.Parameters.AddWithValue("@nama_barang", txt_nama_barang.Text);
                cmd.Parameters.AddWithValue("@stok", txt_stock.Text);
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
            if (txt_kode_barang.Text != "" && txt_nama_barang.Text != "" && txt_stock.Text != "")
            {
                cmd = new SqlCommand("update tbl_barang_detail set nama_barang=@nama_barang,stok=@stok where kode_barang=@kode_barang", con);
                con.Open();
                cmd.Parameters.AddWithValue("@kode_barang", txt_kode_barang.Text);
                cmd.Parameters.AddWithValue("@nama_barang", txt_nama_barang.Text);
                cmd.Parameters.AddWithValue("@stok", txt_stock.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Edit Berhasil");
                con.Close();
                DisplayData();
                cleartext();
            }
            else
            {
                MessageBox.Show("gagal edit");
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (txt_kode_barang.Text != "")
            {
                cmd = new SqlCommand("delete tbl_barang_detail where kode_barang=@kode_barang", con);
                con.Open();
                cmd.Parameters.AddWithValue("@kode_barang", txt_kode_barang.Text);
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
            try
            {
                txt_kode_barang.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                txt_nama_barang.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txt_stock.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                txt_kode_barang.Enabled = false;
            }
            catch (Exception)
            {
                MessageBox.Show("Harus klik isi tabel!");
            }
        }

        private void button_refresh_Click(object sender, EventArgs e)
        {
            DisplayData();
        }
    }
}
