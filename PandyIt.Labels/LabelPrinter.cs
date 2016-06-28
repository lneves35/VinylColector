using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandyIt.Labels
{
    public class LabelPrinter
    {
        public void Print()
        {
            var pdfPrinter = PrinterSettings.InstalledPrinters.Cast<string>().First(p => p.ToLower().Contains("pdf"));

            var printDocument = new PrintDocument
            {
                PrinterSettings = {PrinterName = pdfPrinter}
            };

            printDocument.PrintPage += this.pd_PrintPage;
            if (printDocument.PrinterSettings.IsValid)
            {
                printDocument.Print();
            }
        }

        private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            string stringToPrint = "SOME TEXT TO PRINT";

            // Create font and brush.
            Font drawFont = new Font("Arial", 16);
            SolidBrush drawBrush = new SolidBrush(System.Drawing.Color.Black);

            System.Drawing.Point pos = new System.Drawing.Point(100, 100);

            ev.Graphics.DrawString(stringToPrint, drawFont, drawBrush, pos);

            ev.HasMorePages = false;
        }
    }
}
