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

namespace SAC1Task3
{
    public partial class Form1 : Form
    {
        string filter;
        List<Sale> sales = new List<Sale> ();
        BindingSource bs = new BindingSource ();
        public Form1()
        {
            InitializeComponent();
            // loads and reads csv file then displays it to datagridview
            LoadCSV();
            bs.DataSource = sales;
            dgvSales.DataSource = bs;
        }
        private void LoadCSV()
        {
            // has user select csv file on launch
            OpenFileDialog openFileDialog = new OpenFileDialog ();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // reads csv to list "sales"
                string filePath = openFileDialog.FileName;
                List<string> lines = new List<string>();
                lines = File.ReadAllLines(filePath).ToList();
                foreach (string line in lines.Skip(1))
                {
                    List<string> fields = line.Split(',').ToList();
                    Sale s = new Sale();
                    s.textbook = fields[0];
                    s.subject = fields[1];
                    s.purchase = decimal.Parse(fields[4]);
                    s.sale = fields[5];
                    s.rating = fields[6];
                    sales.Add(s);
                }
            }
            
        }

        private void comFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            // update variable when filter changes, if filter is rating then run sorting algorithm
            filter = comFilter.Text;
            if (filter == "Rating")
            {
                List<Sale> fixedList = sales;
                foreach(var entry in fixedList)
                {
                    if(entry.rating == "none")
                    {
                        entry.rating = "0";
                    }
                }
                sortByRating(fixedList);
            }
            dgvSales.DataSource = bs;
            bs.ResetBindings(false);
        }
        // selection sort algorithm to sort by rating, descending
        private void sortByRating(List<Sale> _sales)
        {
            int minVal;
            string temp;
            for (int i = 0; i < _sales.Count - 1; i++)
            {
                minVal = i;
                for (int j = i + 1; j < _sales.Count; j++)
                {

                    if (int.TryParse(_sales[j].rating, out int ratingJ))
                    {
                        if (int.TryParse(_sales[minVal].rating, out int ratingMin))
                        { if (ratingJ < ratingMin) minVal = j; }
                        else
                        {
                            minVal = j;
                        }
                    }
                }                
                temp = _sales[minVal].rating;
                _sales[minVal].rating = _sales[i].rating;
                _sales[i].rating = temp;
            }
        }
        private List<Sale> Search(string target, string filter)
        {
            List<Sale> results = new List<Sale>();
            // creates list containing only entries containing search prompt
            foreach(Sale s in sales)
            {
                if(filter == "Rating")
                {
                    if (s.rating.ToLower() == target.ToLower()) results.Add(s);
                }
                if (filter == "Subject")
                {
                    if (s.subject.ToLower().Contains(target.ToLower())) { results.Add(s); Console.WriteLine(target); }
                }
                if (filter == "Textbook")
                {
                    if (s.textbook.ToLower().Contains(target.ToLower())) { results.Add(s); Console.WriteLine(target); }
                }
            }
            return results;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // whenever searchbox input is changed, searches and displays results
            List<Sale> r = Search(txtSearch.Text, filter);
            bs.DataSource = r;
            dgvSales.DataSource = r;
            bs.ResetBindings(false);
        }
    }
}
