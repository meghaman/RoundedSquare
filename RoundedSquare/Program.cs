using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace RoundedSquare
{
    public class Program
    {
        const double PixelToFeetRatio = 10;
        const int CornerRadius = 10;
        const string FilePath = "C:\\Users\\Murali\\Documents\\Rainmeter\\Skins\\Damage Report.v.2.1\\@Resources";

        public class Blueprint
        {
            public Blueprint()
            {
                BackgroundColor = Color.FromArgb(255, 255, 255);
                TopMargin = 0;
                SideMargin = 0;
            }
            public double Width
            {
                get; set;
            }
            public double Length
            {
                get; set;
            }
            public string FileName
            {
                get; set;
            }
            public Color BackgroundColor
            {
                get; set;
            }
            public int TopMargin
            {
                get; set;
            }
            public int SideMargin
            {
                get; set;
            }

            public void DrawRoundedRectangle(Graphics gfx, Pen DrawPen, Color FillColor)
            {
                int strokeOffset = Convert.ToInt32(Math.Ceiling(DrawPen.Width));
                Rectangle bounds = new Rectangle(0, 0, Convert.ToInt32(PixelToFeetRatio * Width) + SideMargin, Convert.ToInt32(PixelToFeetRatio * Length) + TopMargin);
                bounds = Rectangle.Inflate(bounds, -strokeOffset, -strokeOffset);

                GraphicsPath gfxPath = new GraphicsPath();
                gfxPath.AddArc(bounds.X, bounds.Y, CornerRadius, CornerRadius, 180, 90);
                gfxPath.AddArc(bounds.X + bounds.Width - CornerRadius, bounds.Y, CornerRadius, CornerRadius, 270, 90);
                gfxPath.AddArc(bounds.X + bounds.Width - CornerRadius, bounds.Y + bounds.Height - CornerRadius, CornerRadius, CornerRadius, 0, 90);
                gfxPath.AddArc(bounds.X, bounds.Y + bounds.Height - CornerRadius, CornerRadius, CornerRadius, 90, 90);
                gfxPath.CloseAllFigures();

                gfx.FillPath(new SolidBrush(FillColor), gfxPath);
                gfx.DrawPath(DrawPen, gfxPath);
            }
        }

        public static void Main(string[] args)
        {

            List<Blueprint> bluePrints = new List<Blueprint>()
            {
                new Blueprint { Width = 2, Length = 7, FileName = "LobbyCloset", BackgroundColor = Color.FromArgb(140, 0, 0, 0) },
                new Blueprint { Width = 17, Length = 7, FileName = "Lobby" },
                new Blueprint { Width = 5, Length = 4, FileName = "Desk" },
                new Blueprint { Width = 3, Length = 4, FileName = "LobbyHallway" },
                new Blueprint { Width = 8, Length = 9, FileName = "Kitchen" },
                new Blueprint { Width = 2, Length = 1.5, FileName = "KitchenSink" },
                new Blueprint { Width = 2, Length = 4, FileName = "KitchenTable" },
                new Blueprint { Width = 7, Length = 5, FileName = "Bathroom" },
                new Blueprint { Width = 2, Length = 5, FileName = "Tub" },
                new Blueprint { Width = 10, Length = 12.5, FileName = "LivingRoom" },
                new Blueprint { Width = 2, Length = 4, FileName = "CoffeeTable" },
                new Blueprint { Width = 9, Length = 11, FileName = "Bedroom" },
                new Blueprint { Width = 3, Length = 5.5, FileName = "BedroomEntrance" },
                new Blueprint { Width = 6, Length = 5.5, FileName = "Closet" },
                new Blueprint { Width = 2, Length = 8, FileName = "Counter" },
                new Blueprint { Width = 2, Length = 2, FileName = "Toilet" },
                new Blueprint { Width = 2, Length = 1.5, FileName = "BathroomSink" },
                new Blueprint { Width = 1.5, Length = 1.5, FileName = "Nighttable" },
                new Blueprint { Width = 1, Length = 4, FileName = "TV" },
                //new Blueprint { Width = 20.5, Height = 34, FileName = "DamageMeterBackground", BackgroundColor = Color.FromArgb(140, 0, 0, 0) }
            };

            Bitmap image;
            Graphics graphicsObject;

            foreach (Blueprint bp in bluePrints)
            {
                image = new Bitmap(Convert.ToInt32(PixelToFeetRatio * bp.Width) + bp.SideMargin, Convert.ToInt32(PixelToFeetRatio * bp.Length) + bp.TopMargin);
                graphicsObject = Graphics.FromImage(image);

                bp.DrawRoundedRectangle(graphicsObject, Pens.Black, bp.BackgroundColor);

                image.Save(FilePath + "\\" + bp.FileName + ".png", System.Drawing.Imaging.ImageFormat.Png);
                image.Dispose();
                graphicsObject.Dispose();
            }
        }
    }
}
