using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CinemaCourse1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (textBoxNameFilm.Text == "")
            {
                MessageBox.Show("Error!");
            } 
            else
            {
                int n  = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = textBoxNameFilm.Text;
                dataGridView1.Rows[n].Cells[1].Value = comboBoxCinema.Text;
                dataGridView1.Rows[n].Cells[2].Value = comboBoxGenre.Text;
                dataGridView1.Rows[n].Cells[3].Value = textBoxDate.Text;
                dataGridView1.Rows[n].Cells[4].Value = textBoxBeginning.Text;
                dataGridView1.Rows[n].Cells[5].Value = numericUpDownDuration.Text;
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int n = dataGridView1.SelectedRows[0].Index;
                dataGridView1.Rows[n].Cells[0].Value = textBoxNameFilm.Text;
                dataGridView1.Rows[n].Cells[1].Value = comboBoxCinema.Text;
                dataGridView1.Rows[n].Cells[2].Value = comboBoxGenre.Text;
                dataGridView1.Rows[n].Cells[3].Value = textBoxDate.Text;
                dataGridView1.Rows[n].Cells[4].Value = textBoxBeginning.Text;
                dataGridView1.Rows[n].Cells[5].Value = numericUpDownDuration.Text;
            } 
            else
            {
                MessageBox.Show("Error!");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Error!");
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            textBoxNameFilm.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            comboBoxCinema.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            comboBoxGenre.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBoxDate.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textBoxBeginning.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            int n = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[5].Value);
            numericUpDownDuration.Value = n;
        }

        private void buttonSaveAsXml_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet dataSet = new DataSet();
                DataTable dataTable = new DataTable();
                dataTable.TableName = "Movie Show";
                dataTable.Columns.Add("Film");
                dataTable.Columns.Add("Cinema");
                dataTable.Columns.Add("Genre");
                dataTable.Columns.Add("Date");
                dataTable.Columns.Add("Beginnig");
                dataTable.Columns.Add("Duration");
                
                dataSet.Tables.Add(dataTable);

                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    DataRow row = dataSet.Tables["Movie Show"].NewRow();
                    row["Film"] = r.Cells[0].Value;
                    row["Cinema"] = r.Cells[1].Value;
                    row["Genre"] = r.Cells[2].Value;
                    row["Date"] = r.Cells[3].Value;
                    row["Beginnig"] = r.Cells[4].Value;
                    row["Duration"] = r.Cells[5].Value;

                    dataSet.Tables["Movie Show"].Rows.Add(row);
                }
                dataSet.WriteXml("D:\\Data.xml");
                MessageBox.Show("XML-file is saved!", "Done!");
            } 
            catch 
            {
                MessageBox.Show("Could not save XML-file", "Error!");
            }
        }

        private void buttonLoadXml_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                MessageBox.Show("Please, clear the field before loading new XML-file!", "Error!");
            }
            else
            {
                if(File.Exists("D:\\Data.xml"))
                {
                    DataSet dataSet = new DataSet();
                    dataSet.ReadXml("D:\\Data.xml");

                    foreach (DataRow item in dataSet.Tables["Movie Show"].Rows)
                    {
                        int n = dataGridView1.Rows.Add();
                        dataGridView1.Rows[n].Cells[0].Value = item["Film"];
                        dataGridView1.Rows[n].Cells[1].Value = item["Cinema"];
                        dataGridView1.Rows[n].Cells[2].Value = item["Genre"];
                        dataGridView1.Rows[n].Cells[3].Value = item["Date"];
                        dataGridView1.Rows[n].Cells[4].Value = item["Beginnig"];
                        dataGridView1.Rows[n].Cells[5].Value = item["Duration"];
                    }
                }
                else
                {
                    MessageBox.Show("XML-file not found!", "Error!");
                }
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                dataGridView1.Rows.Clear();
            }
            else
            {
                MessageBox.Show("Table is empty!", "Error!");
            }
        }
    }
}
