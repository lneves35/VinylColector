﻿using System.Drawing.Printing;
using System.Linq;
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
            var pdfPrinter = PrinterSettings.InstalledPrinters.Cast<string>().First(p => p.ToLower().Contains("4500"));

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
            //ev.PageSettings.Margins.Bottom = 0;
            //ev.PageSettings.Margins.Top = 0;
            this.page.Print(ev);                   
            ev.HasMorePages = false;
        }        
    }
}
