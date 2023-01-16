using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Calculadora 
{
    public class CustomButton : Button 
    {
        private int borderSize = 0;
        private int borderRadius = 40;
        private Color borderColor = Color.DarkSlateBlue;

        [Category("Custom Button")]
        public int BorderSize 
        {
            get 
            {
                return borderSize;
            }
            set 
            {
                borderSize = value;
                this.Invalidate();
            }
        }

        [Category("Custom Button")]
        public int BorderRadius 
        {
            get 
            {
                return borderRadius;              
            }
            set 
            {
                if (value <= this.Height)
                    borderRadius = value;
                else borderRadius = this.Height;
                this.Invalidate();    
            }
        }

        [Category("Custom Button")]
        public Color BorderColor 
        {
            get 
            { 
                return borderColor;
            }
            set 
            {
                borderColor = value;
                this.Invalidate();
            }
        }

        [Category("Custom Button")]
        public Color BackgroundColor 
        {
            get { return this.BackColor; }
            set { this.BackColor = value; }
        }

        [Category("Custom Button")]
        public Color TextColor 
        {
            get { return this.ForeColor; }
            set { this.ForeColor = value; }
        }

        public CustomButton() 
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Size = new Size(150, 40);
            this.BackColor = Color.MediumSlateBlue;
            this.ForeColor = Color.White;
            this.Resize += new EventHandler(Button_Resize);
        }

        private GraphicsPath GetFigurePath(RectangleF rectangle, int radius) 
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.StartFigure();
            graphicsPath.AddArc(rectangle.X, rectangle.Y, radius, radius, 180, 90);
            graphicsPath.AddArc(rectangle.Width - radius, rectangle.Y, radius, radius, 270, 90);
            graphicsPath.AddArc(rectangle.Width - radius, rectangle.Height - radius, radius, radius, 0, 90);
            graphicsPath.AddArc(rectangle.X, rectangle.Height - radius, radius, radius, 90, 90);
            graphicsPath.CloseFigure();

            return graphicsPath;
        }

        protected override void OnPaint(PaintEventArgs pevent) 
        {
            base.OnPaint(pevent);
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rectangleSurface = this.ClientRectangle;
            Rectangle rectangleBorder = Rectangle.Inflate(rectangleSurface, -borderSize, -borderSize);

            if (borderRadius > 2) 
            {
                using (GraphicsPath graphicsPathSurface = GetFigurePath(rectangleSurface, borderRadius))
                using (GraphicsPath graphicsPathBorder = GetFigurePath(rectangleBorder, borderRadius - borderSize))
                using (Pen penSurface = new Pen(this.Parent.BackColor, 2))
                using (Pen penBorder = new Pen(borderColor, borderSize)) 
                {
                    penBorder.Alignment = PenAlignment.Inset;
                    this.Region = new Region(graphicsPathSurface);
                    pevent.Graphics.DrawPath(penBorder, graphicsPathSurface);

                    if (borderSize >= 1)

                    pevent.Graphics.DrawPath(penBorder, graphicsPathBorder);
                }
            } 
             else 
             {
                this.Region = new Region(rectangleSurface);
                if (borderSize >= 1)
                {
                   using (Pen penBorder = new Pen(borderColor, borderSize)) 
                   {
                    penBorder.Alignment = PenAlignment.Inset;
                    pevent.Graphics.DrawRectangle(penBorder, 0, 0, Width - 1, Height - 1);
                   }

                }
             }
        }
        protected override void OnHandleCreated(EventArgs e) 
        {
            base.OnHandleCreated(e);
            this.Parent.BackColorChanged += new EventHandler(Container_BackColorChanged);

        }
        private void Container_BackColorChanged(object sender, EventArgs e) 
        {
            if (this.DesignMode)
                this.Invalidate();
        }

        private void Button_Resize(object sender, EventArgs e) 
        {
            if (borderRadius > this.Height)
                borderRadius = this.Height;
        }
    }
}
