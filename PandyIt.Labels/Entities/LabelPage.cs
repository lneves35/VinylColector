using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using PandyIT.VinylOrganizer.DAL.Model.Entities;

namespace PandyIt.VinylOrganizer.Labels.Entities
{
    public class LabelPage
    {
        private readonly int rows;
        private readonly int columns;
        private List<LabelSlot> labelSlots;

        public LabelPage(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;

            InitializeLabelSlots();
        }

        private void InitializeLabelSlots()
        {
            labelSlots = new List<LabelSlot>();
            for (var row = 0; row < this.rows; row++)
            {
                for (var column = 0; column < this.columns; column++)
                {
                    labelSlots.Add(new LabelSlot(row, column));
                }
            }
        }

        public void AddLabel(LocationVinyl locationVinyl)
        {
            this.GetFirstEmptyLabelSlot().LocationVinyl = locationVinyl;
        }

        private LabelSlot GetFirstEmptyLabelSlot()
        {
            return labelSlots.First(ls => ls.LocationVinyl == null);
        }

        public void Print(PrintPageEventArgs ev)
        {
            var width = ev.PageSettings.PaperSize.Width / this.columns;
            var height = ev.PageSettings.PaperSize.Height / this.rows;

            labelSlots
                .Where(ls => ls.LocationVinyl != null)
                .ToList()
                .ForEach(ls => ls.Draw(ev.Graphics, width, height));
        }
    }
}
