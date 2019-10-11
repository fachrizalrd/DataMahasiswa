using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AplikasiInputDataMahasiswa
{
    public partial class Form1 : Form
    {

        //deklarasi object collection
        private List<Mahasiswa> list = new List<Mahasiswa>();
        // constructor
        public Form1()
        {
            InitializeComponent();
            InisialisasiListView();
        }

     private void InisialisasiListView()
        {
            lvwMahasiswa.View = View.Details;
            lvwMahasiswa.FullRowSelect = true;
            lvwMahasiswa.GridLines = true;

            lvwMahasiswa.Columns.Add("No.", 30, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.Add("NIM", 91, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.Add("Nama", 200, HorizontalAlignment.Left);
            lvwMahasiswa.Columns.Add("Kelas", 70, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.Add("Nilai", 50, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.Add("Nilai Huruf", 70, HorizontalAlignment.Center);
        }

        private void ResetForm()
        {
            txtNim.Clear();
            txtNama.Clear();
            txtKelas.Clear();
            txtNilai.Text = "0";
            txtNim.Focus();
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private bool NumericOnly(KeyPressEventArgs e)
        {
            var strValid = "0123456789";

            if (!(e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                // inputan selain angka
                if (strValid.IndexOf(e.KeyChar) < 0)
                {
                    return true;
                }

                return false;
            }

            else
                return false;
        }

        private void txtNilai_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = NumericOnly(e);
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            Mahasiswa mhs = new Mahasiswa();

            mhs.Nama = txtNama.Text;
            mhs.NIM = txtNim.Text;
            mhs.Kelas = txtKelas.Text;
            mhs.Nilai = int.Parse(txtNilai.Text);
          
            list.Add(mhs);

            var msg = "Data Mahasiswa Berhasil Disimpan";

            MessageBox.Show(msg, "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ResetForm();

            if (mhs.Nilai <= 20)
            {
                mhs.Huruf = "E";
            }
            else if (mhs.Nilai <= 40)
            {
                mhs.Huruf = "D";
            }
            else if (mhs.Nilai <= 60)
            {
                mhs.Huruf = "C";
            }
            else if (mhs.Nilai <= 80)
            {
                mhs.Huruf = "B";
            }
            else if (mhs.Nilai <= 100)
            {
                mhs.Huruf = "A";
            }
        }

        private void TampilkanData() 
        {
            lvwMahasiswa.Items.Clear();
            
            foreach (var mhs in list)
            {
                var noUrut = lvwMahasiswa.Items.Count + 1;

                var item = new ListViewItem(noUrut.ToString());
                item.SubItems.Add(mhs.NIM);
                item.SubItems.Add(mhs.Nama);
                item.SubItems.Add(mhs.Kelas);
                item.SubItems.Add(mhs.Nilai.ToString());
                item.SubItems.Add(mhs.Huruf);

                lvwMahasiswa.Items.Add(item);
            }
        }

        private void btnTampilkanData_Click(object sender, EventArgs e)
        {
            TampilkanData();
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (lvwMahasiswa.SelectedItems.Count > 0)
            {
                var konfirmasi = MessageBox.Show("Apakah Data Mahasiswa Ingin Dihapus ?", "Konfirmasi",
                    MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation);

                if (konfirmasi == DialogResult.Yes)
                {
                    var index = lvwMahasiswa.SelectedIndices[0];
                    list.RemoveAt(index);
                    TampilkanData();
                }
            }

            else
            {
                MessageBox.Show("Data Mahasiswa Belum Dipilih", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        

    }

   
}
