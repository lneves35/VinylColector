using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using PandyIt.VinylOrganizer.Labels.Entities;

namespace PandyIt.VinylOrganizer.Labels
{
    public class LabelPrinter
    {
        private IEnumerable<LabelPage> labelPages;

        public LabelPrinter(IEnumerable<LabelPage> labelPages)
        {
            this.labelPages = labelPages;
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
            this.labelPages.First().Draw(ev.Graphics);
            ev.HasMorePages = false;
        }        
    }
}
