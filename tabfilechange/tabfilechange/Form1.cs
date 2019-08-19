using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace tabfilechange
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items.AddRange(openFileDialog1.FileNames);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (string item in listBox1.Items)
            {
                ChangeOneExt(item);
            }
        }

        private void ChangeOneExt(string item)
        {
            FileStream fs = new FileStream(item, FileMode.Open);
            byte[] data = new byte[fs.Length];
            fs.Read(data, 0, (int)fs.Length);
            fs.Close();
            
            string ss = ASCIIEncoding.Default.GetString(data);
            int cc= ss.IndexOf("File");

            if (cc == -1)
            {
                MessageBox.Show("No file ref find "+item);
                return;
            }
            int cc2=ss.IndexOf("\"",cc+4);
            int cc3 = ss.IndexOf("\"", cc2 + 1);
            string fname = ss.Substring(cc2 + 1, cc3 - cc2 - 1);
            int exb=fname.LastIndexOf(".");
            string nfname = fname.Substring(0, exb+1) + comboBox1.SelectedItem;
            string ss2 = ss.Substring(0, cc2+1) + nfname + ss.Substring(cc3);
            listBox2.Items.Add(ss2);
            FileStream fout = new FileStream(item, FileMode.Create);
            fout.Write(ASCIIEncoding.Default.GetBytes(ss2), 0, ASCIIEncoding.Default.GetByteCount(ss2));
            fout.Close();
        }
    }
}
