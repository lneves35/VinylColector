using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using PandyIT.VinylOrganizer.DAL.Model.Entities;

namespace PandyIt.VinylOrganizer.Labels.Entities
{
    public class LabelPage
    {
        public int Rows { get; set; }

        public int Columns { get; set; }

        public int Width { get; set; }

        public int Heigth { get; set; }

        private List<LabelSlot> labelSlots = new List<LabelSlot>();

        public LabelPage(int rows, int columns, int width, int heigth) 
        {
            this.Rows = rows;
            this.Columns = columns;
            this.Width = width;
            this.Heigth = heigth;
        }

        public void AddLabel(int row, int column, LocationVinyl locationVinyl)
        {
            var labelSlot = new LabelSlot(this, locationVinyl, row, column);
            labelSlots.Add(labelSlot);
        }

        public void Draw(Graphics graphics)
        {
            //using (var pen = new Pen(Color.Black))
            //{
            //    graphics.DrawRectangle(pen, 0, 0, this.Width, this.Heigth);
            //}

            labelSlots
                .ToList()
                .ForEach(ls =>
                {
                    ls.Draw(graphics);
                });
        }

    }
}
