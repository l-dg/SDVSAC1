using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAC1Task1
{   
    public partial class Form1 : Form
    {
        // define variable for later use
        decimal totalValue = 0;
        public Form1()
        {
            InitializeComponent();
            // allow for any value to be entered in NumericUpDown
            numPricePaid.Maximum = decimal.MaxValue;
            numAge.Maximum = decimal.MaxValue;
        }

        // function calculates value given price paid and age, returns value as decimal
        private decimal calculate(decimal purchasePrice, decimal age)
        {
            decimal deprecation = Decimal.Multiply(0.2m, Decimal.Multiply(purchasePrice, age));
            if (deprecation > purchasePrice)
            {
                return 0;
            }
            else
            {
                return purchasePrice - deprecation;
            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            // extracting values from NumericUpDown
            decimal age = numAge.Value; 
            decimal purchasePrice = numPricePaid.Value;
            // displays value of current item
            lblValue.Text = "It is worth $" + calculate(purchasePrice, age).ToString();
            // calculate total by adding new value to old values
            totalValue += calculate(purchasePrice, age);
            // display total value of items
            lblTotalValue.Text = "The collection so far is worth $" + totalValue.ToString();
            // clear inputs
            numAge.Value = 0;
            numPricePaid.Value = 0;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // clear inputs
            numAge.Value = 0;
            numPricePaid.Value = 0;
            totalValue = 0;
            // clear labels displaying current and total values
            lblValue.Text = string.Empty;
            lblTotalValue.Text = string.Empty;
        }
    }
}
