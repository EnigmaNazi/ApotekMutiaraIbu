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
    public partial class FRM_PENJUALAN : Form
    {
        SqlConnection con = new SqlConnection("Server=DESKTOP-6PC0I83;Database=ApotekMutiaraIbu;Integrated Security=true;");
        SqlCommand cmd;
        SqlDataAdapter adapt;
        SqlDataReader rd;

        private void DisplayData()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from tbl_penjualan", con);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void DisplayDataBarang()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from tbl_barang", con);
            adapt.Fill(dt);
            dg_barang.DataSource = dt;
            con.Close();
        }

        private void otomatis()
        {
            long hitung;
            string urut;

            con.Open();
            cmd = new SqlCommand("select no_penjualan from tbl_penjualan_detail where no_penjualan in(select max(no_penjualan) from tbl_penjualan_detail) order by no_penjualan desc", con);
            rd = cmd.ExecuteReader();
            rd.Read();
            if (rd.HasRows)
            {
                hitung = Convert.ToInt64(rd[0].ToString().Substring(rd["no_penjualan"].ToString().Length - 4, 4)) + 1;

                string joinstr = "0000" + hitung;



                urut = "AMI" + joinstr.Substring(joinstr.Length - 4, 4);

            }
            else
            {
                urut = "AMI0001";
            }
            rd.Close();
            txt_no_penjualan.Text = urut;
            con.Close();
        }

        private void cleartext()
        {
            txt_kode_barang.Text = "";
            txt_nama_barang.Text = "";
            txt_harga.Text = "0";
            txt_stock.Text = "0";
            txt_satuan.Text = "";
            txt_jenis_obat.Text = "";
            txt_jumlah_beli.Text = "";
            txt_subtotal.Text = "";
            txt_keterangan.Text = "";
            cmb_penjual.Text = "";
            txt_no_penjualan.Enabled = false;
            txt_kode_barang.Enabled = false;
            txt_stock.Enabled = false;
            txt_subtotal.Enabled = false;
        }

        public FRM_PENJUALAN()
        {
            InitializeComponent();
            DisplayData();;
            DisplayDataBarang();
            cleartext();
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker1.Enabled = false;
            txt_no_penjualan.Enabled = false;
            cleartext();
            otomatis();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("insert into tbl_penjualan_detail select * from tbl_penjualan where no_penjualan=@no_penjualan", con);
            con.Open();
            cmd.Parameters.AddWithValue("@no_penjualan", txt_no_penjualan.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Berhasil simpan");
            cmd = new SqlCommand("delete tbl_penjualan", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            DisplayData();
            cleartext();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                int stock, jumlah_beli, stock_akhir;
                stock = int.Parse(txt_stock.Text);
                jumlah_beli = int.Parse(txt_jumlah_beli.Text);
                stock_akhir = stock - jumlah_beli;

                if (int.Parse(txt_stock.Text) >= int.Parse(txt_jumlah_beli.Text))
                {
                    try
                    {
                        if (txt_no_penjualan.Text != "" && txt_kode_barang.Text != "" && txt_nama_barang.Text != "" && txt_jumlah_beli.Text != "" && txt_subtotal.Text != "" && txt_keterangan.Text != "")
                        {
                            cmd = new SqlCommand("update tbl_barang set stock=@stock_akhir where kode_barang=@kode_barang", con);
                            con.Open();
                            cmd.Parameters.AddWithValue("@stock_akhir", stock_akhir);
                            cmd.Parameters.AddWithValue("@kode_barang", txt_kode_barang.Text);
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        else
                        {
                            MessageBox.Show("gagal simpan");
                        }

                        if (txt_no_penjualan.Text != "" && txt_kode_barang.Text != "" && txt_nama_barang.Text != "" && txt_jumlah_beli.Text != "" && txt_subtotal.Text != "" && txt_keterangan.Text != "")
                        {
                            cmd = new SqlCommand("insert into tbl_penjualan(no_penjualan,tgl_penjualan,kode_barang,nama_barang,jumlah_beli,subtotal,keterangan,penjual) values(@no_penjualan,@tgl_penjualan,@kode_barang,@nama_barang,@jumlah_beli,@subtotal,@keterangan,@penjual)", con);
                            con.Open();
                            cmd.Parameters.AddWithValue("@no_penjualan", txt_no_penjualan.Text);
                            cmd.Parameters.Add("@tgl_penjualan", SqlDbType.DateTime).Value = dateTimePicker1.Value.Date;
                            cmd.Parameters.AddWithValue("@kode_barang", txt_kode_barang.Text);
                            cmd.Parameters.AddWithValue("@nama_barang", txt_nama_barang.Text);
                            cmd.Parameters.AddWithValue("@jumlah_beli", txt_jumlah_beli.Text);
                            cmd.Parameters.AddWithValue("@subtotal", txt_subtotal.Text);
                            cmd.Parameters.AddWithValue("@keterangan", txt_keterangan.Text);
                            cmd.Parameters.AddWithValue("@penjual", cmb_penjual.Text);
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
                    catch (Exception)
                    {
                        MessageBox.Show("Kode barang tidak boleh sama, atau hapus dahulu data yang telah ditambahkan jika ingin diubah!");
                    }
                }
                else
                {
                    MessageBox.Show("stock kurang");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Kolom masih kosong!");
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (txt_kode_barang.Text != "" && txt_nama_barang.Text != "")
            {
                cmd = new SqlCommand("delete tbl_penjualan where kode_barang=@kode_barang", con);
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

        private void txt_jumlah_beli_TextChanged(object sender, EventArgs e)
        {
            if (txt_jumlah_beli.Text != "") txt_subtotal.Text = (int.Parse(txt_harga.Text) * int.Parse(txt_jumlah_beli.Text)).ToString();
            else txt_subtotal.Text = "0";
        }

        private void pilih_barang_Click(object sender, EventArgs e)
        {
            group_cari_barang.Visible = true;
        }

        private void button_batal_barang_Click(object sender, EventArgs e)
        {
            group_cari_barang.Visible = false;
        }

        private void dg_barang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txt_kode_barang.Text = dg_barang.Rows[e.RowIndex].Cells[0].Value.ToString();
                txt_nama_barang.Text = dg_barang.Rows[e.RowIndex].Cells[1].Value.ToString();
                txt_harga.Text = dg_barang.Rows[e.RowIndex].Cells[2].Value.ToString();
                txt_stock.Text = dg_barang.Rows[e.RowIndex].Cells[3].Value.ToString();
                txt_satuan.Text = dg_barang.Rows[e.RowIndex].Cells[6].Value.ToString();
                txt_jenis_obat.Text = dg_barang.Rows[e.RowIndex].Cells[7].Value.ToString();
                DisplayDataBarang();
                group_cari_barang.Visible = false;
            }
            catch (Exception)
            {
                MessageBox.Show("Klik isi tabel kode barang atau nama barang!");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txt_no_penjualan.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                dateTimePicker1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txt_kode_barang.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                txt_nama_barang.Text = dg_barang.Rows[e.RowIndex].Cells[3].Value.ToString();
                txt_jumlah_beli.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                txt_subtotal.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                txt_keterangan.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                cmb_penjual.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                dg_barang_CellContentClick(sender, e);
            }
            catch (Exception)
            {
                MessageBox.Show("Klik isi tabel!");
            }
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            string query = "SELECT * FROM tbl_barang WHERE kode_barang like '%" + textBox14.Text + "%'";
            cmd = new SqlCommand(query, con);
            adapt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            dg_barang.DataSource = dt;
        }
    }
}
