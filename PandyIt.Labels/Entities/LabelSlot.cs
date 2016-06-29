using System.Drawing;
using PandyIT.VinylOrganizer.DAL.Model.Entities;

namespace PandyIt.VinylOrganizer.Labels.Entities
{
    public class LabelSlot
    {
        public int Row { get; set; }

        public int Column { get; set; }

        public LocationVinyl LocationVinyl { get; set; }

        public LabelSlot(int row, int column, LocationVinyl locationViny)
        {
            this.Row = row;
            this.Column = column;
            this.LocationVinyl = locationViny;
        }

        public LabelSlot(int row, int column) : this(row, column, null)
        {
        }

        public void Draw(Graphics graphics)
        {
            using (var pen = new Pen(Color.Black))
            {
                var rectangle = new Rectangle(this.Column * 10, this.Row * 10, 50, 50);
                graphics.DrawRectangle(pen, rectangle);
            }
        }

    }
}
