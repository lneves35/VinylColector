using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PandyIt.VinylOrganizer.Labels.Entities;

namespace PandyIt.VinylOrganizer.Labels
{
    public class LabelPrinter
    {
        private readonly LabelPage page;

        public LabelPrinter(LabelPage page)
        {
            this.page = page;
        }

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
            this.page.Draw(ev.Graphics);                   
            ev.HasMorePages = false;
        }        
    }
}
