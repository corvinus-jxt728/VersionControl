using jxt728_irf1.Osztaly;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace jxt728_irf1
{
    public partial class Form1 : Form
    {
        DVD_RentalEntities context = new DVD_RentalEntities();
        List<Category> categories;
        List<CategorySummary> catsums;
        Excel.Application xlApp;
        Excel.Workbook xlWb;
        Excel.Worksheet xlSheet;
        public Form1()
        {
            InitializeComponent();
            FillList();
            CatGo();
        }
        public void FillList()
        {
            var vk = (from k in context.Categories orderby k.Name select k).Distinct();
            categories = vk.ToList();
        }
        public void CatGo()
        {
            decimal szum = 0;
            int db = 0;
            string nev = "";
            foreach (var item in categories)
            {
                // var adottkat = from c in context.DVDs where c.CategoryFK == item.CategorySK select c.NetPrice;
                var akat = (from c in context.Rentals where c.DVDFK == c.DVD.DVDSK select c.DVD.NetPrice).FirstOrDefault();
                db++;
                szum = (decimal)(szum + akat);
                nev = item.Name;

            }
            CategorySummary cs = new CategorySummary();
            cs.Name = nev;
            cs.NbrOfSales = db;
            cs.TotalSales = szum;

          //  catsums.Add(cs);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                xlApp = new Excel.Application();
                xlWb = xlApp.Workbooks.Add(Missing.Value);
                xlSheet = xlWb.ActiveSheet;

                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception ex)
            {


            }
            string[] Headers = new string[]
            {
                "Kategória",
                "Eladott mennyiség",
                "Teljes bevétel",
                "Átlagá"
            };
            xlSheet.Cells[1, 1] = Headers[0];
            xlSheet.Cells[1, 2] = Headers[1];
            xlSheet.Cells[1, 3] = Headers[2];
            xlSheet.Cells[1, 4] = Headers[3];
        }
    } }
