using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PandyIT.VinylOrganizer.DAL.Model.Entities;
using System.Drawing;

namespace PandyIt.VinylOrganizer.Labels.Entities
{
    public class LabelContent
    {
        private LocationVinyl LocationVinyl { get; set; }
        private LabelSlot LabelSlot { get; set; }

        public LabelContent(LabelSlot labelSlot, LocationVinyl locationVinyl)
        {
            this.LabelSlot = labelSlot;
            this.LocationVinyl = locationVinyl;
        }

        public void Draw(Graphics graphics)
        {
            //using (var pen = new Pen(Color.Black))
            //{
            //    graphics.DrawRectangle(
            //        pen, 
            //        this.GetAvailableX(), 
            //        this.GetAvailableY(), 
            //        this.GetAvailableWidth(),
            //        this.GetAvailableHeigth());
            //}

            var fontFamily = new FontFamily("Times New Roman");
            var font = new Font(fontFamily, 24, FontStyle.Regular, GraphicsUnit.Pixel);
            var solidBrush = new SolidBrush(Color.Black);

            var textRectangle = new RectangleF(new PointF(this.GetAvailableX(), this.GetAvailableY() + 30), new SizeF() { Height = 90, Width = this.GetAvailableWidth()});

            graphics.DrawString(this.LocationVinyl.Name, font, solidBrush, new PointF(this.GetAvailableX(), this.GetAvailableY()));
            graphics.DrawString(this.LocationVinyl.Title, font, solidBrush, textRectangle);
            graphics.DrawString(this.LocationVinyl.Genre, font, solidBrush, new PointF(this.GetAvailableX(), this.GetAvailableY() + 120));
        }

        public int GetAvailableX()
        {
            return this.LabelSlot.GetAvailableX() + 10;
        }

        public int GetAvailableWidth()
        {
            return this.LabelSlot.GetAvailableWidth() - 20;
        }

        public int GetAvailableHeigth()
        {
            return this.LabelSlot.GetAvailableHeigth() - 20;
        }

        public int GetAvailableY()
        {
            return this.LabelSlot.GetAvailableY() + 10;
        }
    }
}
