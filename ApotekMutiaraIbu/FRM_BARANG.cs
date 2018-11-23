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
            txt_no_batch.Text = "";
            txt_qr_kode.Text = "";
            txt_nama_barang.Text = "";
            txt_harga.Text = "";
            dateTimePicker1.Text = "";
            cmb_satuan.Text = "";
            txt_nama_barang.Enabled = true;
        }
        public FRM_BARANG()
        {
            InitializeComponent();
            cleartext();
            DisplayData();
        }

        private void button_new_Click(object sender, EventArgs e)
        {
            cleartext();
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            if (txt_nama_barang.Text != "" && txt_no_batch.Text != "" && txt_qr_kode.Text != "" && txt_kode_barang.Text != "" && txt_harga.Text != "" && cmb_satuan.Text != "" && cmb_jenis_obat.Text != "")
            {
                cmd = new SqlCommand("insert into tbl_barang(nama_barang,no_batch,qr_kode,kode_barang,harga,exp,satuan,jenis_obat) values(@nama_barang,@no_batch,@qr_kode,@kode_barang,@harga,@exp,@satuan,@jenis_obat)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@nama_barang", txt_nama_barang.Text);
                cmd.Parameters.AddWithValue("@no_batch", txt_no_batch.Text);
                cmd.Parameters.AddWithValue("@qr_kode", txt_qr_kode.Text);
                cmd.Parameters.AddWithValue("@kode_barang", txt_kode_barang.Text);
                cmd.Parameters.AddWithValue("@harga", txt_harga.Text);
                cmd.Parameters.Add("@exp", SqlDbType.DateTime).Value = dateTimePicker1.Value.Date;
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

        private void button_edit_Click(object sender, EventArgs e)
        {
            if (txt_nama_barang.Text != "" && txt_no_batch.Text != "" && txt_qr_kode.Text != "" && txt_kode_barang.Text != "" && txt_harga.Text != "" && cmb_satuan.Text != "" && cmb_jenis_obat.Text != "")
            {
                cmd = new SqlCommand("update tbl_barang set nama_barang=@nama_barang,no_batch=@no_batch,kode_barang=@kode_barang,harga=@harga,exp=@exp,satuan=@satuan,jenis_obat=@jenis_obat where qr_kode=@qr_kode", con);
                con.Open();
                cmd.Parameters.AddWithValue("@nama_barang", txt_nama_barang.Text);
                cmd.Parameters.AddWithValue("@no_batch", txt_no_batch.Text);
                cmd.Parameters.AddWithValue("@qr_kode", txt_qr_kode.Text);
                cmd.Parameters.AddWithValue("@kode_barang", txt_kode_barang.Text);
                cmd.Parameters.AddWithValue("@harga", txt_harga.Text);
                cmd.Parameters.Add("@exp", SqlDbType.DateTime).Value = dateTimePicker1.Value.Date;
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

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (txt_qr_kode.Text != "")
            {
                cmd = new SqlCommand("delete tbl_barang where qr_kode=@qr_kode", con);
                con.Open();
                cmd.Parameters.AddWithValue("@qr_kode", txt_qr_kode.Text);
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
                txt_no_batch.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txt_qr_kode.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                txt_nama_barang.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                cmb_satuan.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                txt_harga.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                dateTimePicker1.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                cmb_jenis_obat.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Harus klik isi tabel!");
            }
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            DisplayData();
        }
    }
}
