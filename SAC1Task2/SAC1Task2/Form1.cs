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

namespace SAC1Task2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click_1(object sender, EventArgs e)
        {
            // opens file dialog for user to select file
            OpenFileDialog openFileDialog = new OpenFileDialog();
            // if user selected a file
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                List<string> lines = new List<string>();
                lines = File.ReadAllLines(filePath).ToList();
                decimal total = 0m;

                // 
                foreach (string line in lines.Skip(1))
                {
                    List<string> items = line.Split(',').ToList();
                    decimal profit;
                    decimal purchasePrice = decimal.Parse(items[4]);
                    string salePrice = items[5];

                    if (salePrice == "NA")
                    {
                        profit = purchasePrice * -1;
                    }
                    else
                    {
                        profit = decimal.Parse(salePrice) - purchasePrice;
                    }
                    total += profit;
                    items.Add(profit.ToString());
                    dgvSalesData.Rows.Add(items.ToArray());
                }
                lblTotalProfit.Text = "Total profit is $" + total.ToString();
                dgvSalesData.Rows.Add();
                dgvSalesData.Rows[dgvSalesData.Rows.Count - 1].Cells[5].Value = "Total";
                dgvSalesData.Rows[dgvSalesData.Rows.Count - 1].Cells[6].Value = $"${total.ToString()}";
            }
        }

    }
}
