using System.Drawing;
using System.Drawing.Printing;
using PandyIT.VinylOrganizer.DAL.Model.Entities;

namespace PandyIt.VinylOrganizer.Labels.Entities
{
    public class LabelSlot
    {
        public int Row { get; set; }

        public int Column { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

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

        public void Draw(Graphics graphics, int width, int heigth)
        {
            using (var pen = new Pen(Color.Black))
            {
                var x = this.Column*width;
                var y = this.Row*heigth;

                var rectangle = new Rectangle(x, y, width, heigth);
                graphics.DrawRectangle(pen, rectangle);

                var fontFamily = new FontFamily("Times New Roman");
                var font = new Font(fontFamily, 24, FontStyle.Regular, GraphicsUnit.Pixel);
                var solidBrush = new SolidBrush(Color.Black);

                graphics.DrawString(this.LocationVinyl.Name, font, solidBrush, new PointF(x, y));

                graphics.DrawString(this.LocationVinyl.Title, font, solidBrush, new PointF(x, y + 30));

                graphics.DrawString(this.LocationVinyl.Genre, font, solidBrush, new PointF(x, y + 120));
            }
        }        
    }
}
