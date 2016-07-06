using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using PandyIT.VinylOrganizer.DAL.Model.Entities;

namespace PandyIt.VinylOrganizer.Labels.Entities
{
    public class LabelSlot
    {
        public int Row { get; set; }

        public int Column { get; set; }

        LabelContent labelContent { get; set; }

        public LabelPage Page { get; set; }                

        public LabelSlot(LabelPage page, LocationVinyl locationVinyl, int row, int column)
        {
            this.Row = row;
            this.Column = column;
            this.Page = page;
            this.labelContent = new LabelContent(this, locationVinyl);
        }

        public void Draw(Graphics graphics)
        {
            //using (var pen = new Pen(Color.Black))
            //{
            //    var rectangle = new Rectangle(GetAvailableX(), GetAvailableY(), GetAvailableWidth(), GetAvailableHeigth());
            //    graphics.DrawRectangle(pen, rectangle);
            //}

            labelContent.Draw(graphics);
        }

        public int GetAvailableX()
        {
            return GetX() + 2;
        }

        public int GetAvailableY()
        {
            return GetY() + 2;
        }


        public int GetAvailableWidth()
        {
            return GetWidth() - 3;
        }

        public int GetAvailableHeigth()
        {
            return GetHeigth();
        }

        
        private int GetX()
        {
            return this.Column * GetWidth() + 10;
        }

        private int GetY()
        {
            return this.Row * GetHeigth();
        }

        private int GetWidth()
        {
            return this.Page.Width / Page.Columns;
        }

        private int GetHeigth()
        {
            return this.Page.Heigth / Page.Rows - 1;
        }
    }
}
