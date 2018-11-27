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
    public partial class FRM_BARANG : Form
    {
        SqlConnection con = new SqlConnection("Server=DESKTOP-6PC0I83;Database=ApotekMutiaraIbu;Integrated Security=true;");
        SqlCommand cmd;
        SqlDataAdapter adapt;
        SqlDataReader rd;

        private void DisplayData()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from tbl_barang", con);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

        }

        private void cleartext()
        {
            txt_kode_barang.Text = "";
            txt_nama_barang.Text = "";
            txt_harga.Text = "";
            txt_stock.Text = "";
            dateTimePicker1.Text = "";
            dateTimePicker2.Text = "";
            cmb_satuan.Text = "";
            cmb_jenis_obat.Text = "";
        }

        public FRM_BARANG()
        {
            InitializeComponent();
            cleartext();
            DisplayData();
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            if (txt_kode_barang.Text != "" && txt_nama_barang.Text != "" && txt_harga.Text != "" && txt_stock.Text != "" && cmb_satuan.Text != "" && cmb_jenis_obat.Text != "")
            {
                cmd = new SqlCommand("insert into tbl_barang(kode_barang,nama_barang,harga,stock,tgl_dibuat,exp,satuan,jenis_obat) values(@kode_barang,@nama_barang,@harga,@stock,@tgl_dibuat,@exp,@satuan,@jenis_obat)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@kode_barang", txt_kode_barang.Text);
                cmd.Parameters.AddWithValue("@nama_barang", txt_nama_barang.Text);
                cmd.Parameters.AddWithValue("@harga", txt_harga.Text);
                cmd.Parameters.AddWithValue("@stock", txt_stock.Text);
                cmd.Parameters.Add("@tgl_dibuat", SqlDbType.DateTime).Value = dateTimePicker1.Value.Date;
                cmd.Parameters.Add("@exp", SqlDbType.DateTime).Value = dateTimePicker2.Value.Date;
                cmd.Parameters.AddWithValue("@satuan", cmb_satuan.Text);
                cmd.Parameters.AddWithValue("@jenis_obat", cmb_jenis_obat.Text);
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

        private void button_new_Click(object sender, EventArgs e)
        {
            cleartext();
        }

        private void button_edit_Click(object sender, EventArgs e)
        {
            if (txt_kode_barang.Text != "" && txt_nama_barang.Text != "" && txt_harga.Text != "" && txt_stock.Text != "" && cmb_satuan.Text != "" && cmb_jenis_obat.Text != "")
            {
                cmd = new SqlCommand("update tbl_barang set nama_barang=@nama_barang,harga=@harga,stock=@stock,tgl_dibuat=@tgl_dibuat,exp=@exp,satuan=@satuan,jenis_obat=@jenis_obat where kode_barang=@kode_barang", con);
                con.Open();
                cmd.Parameters.AddWithValue("@kode_barang", txt_kode_barang.Text);
                cmd.Parameters.AddWithValue("@nama_barang", txt_nama_barang.Text);
                cmd.Parameters.AddWithValue("@harga", txt_harga.Text);
                cmd.Parameters.AddWithValue("@stock", txt_stock.Text);
                cmd.Parameters.Add("@tgl_dibuat", SqlDbType.DateTime).Value = dateTimePicker1.Value.Date;
                cmd.Parameters.Add("@exp", SqlDbType.DateTime).Value = dateTimePicker2.Value.Date;
                cmd.Parameters.AddWithValue("@satuan", cmb_satuan.Text);
                cmd.Parameters.AddWithValue("@jenis_obat", cmb_jenis_obat.Text);
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

        private void button_refresh_Click(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (txt_kode_barang.Text != "")
            {
                cmd = new SqlCommand("delete tbl_barang where kode_barang=@kode_barang", con);
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

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            string query = "SELECT * FROM tbl_barang WHERE kode_barang like '%" + txt_search.Text + "%'";
            cmd = new SqlCommand(query, con);
            adapt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txt_kode_barang.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                txt_nama_barang.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txt_harga.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                txt_stock.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                dateTimePicker1.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                dateTimePicker2.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                cmb_satuan.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                cmb_jenis_obat.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Klik isi tabel!");
            }
        }
    }
}
