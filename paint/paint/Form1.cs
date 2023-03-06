using System;
using System.Drawing;
using System.Windows.Forms;

namespace zad1
{
    public partial class Form1 : Form
    {
        private Point startPoint;
        private Point endPoint;
        private Pen pen;
        private Brush brush;
        private ShapeType shapeType;


        private enum ShapeType
        {
            None,
            Rectangle,
            Ellipse,
            Line
        }

        public Form1()
        {
            InitializeComponent();
            pen = new Pen(Color.Black, 1);
            brush = Brushes.Transparent;
            shapeType = ShapeType.None;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnRectangle_Click(object sender, EventArgs e)
        {
            shapeType = ShapeType.Rectangle;
        }

        private void btnEllipse_Click(object sender, EventArgs e)
        {
            shapeType = ShapeType.Ellipse;
        }

        private void btnLine_Click(object sender, EventArgs e)
        {
            shapeType = ShapeType.Line;
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                pen.Color = colorDialog1.Color;
            }
        }

        private void btnFillColor_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                brush = new SolidBrush(colorDialog1.Color);
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            startPoint = e.Location;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            endPoint = e.Location;
            DrawShape();
        }

        private void DrawShape()
        {
            if (shapeType != ShapeType.None)
            {
                using (Graphics graphics = this.CreateGraphics())
                {
                    Rectangle rect = GetRectangle(startPoint, endPoint);
                    switch (shapeType)
                    {
                        case ShapeType.Rectangle:
                            graphics.FillRectangle(brush, rect);
                            graphics.DrawRectangle(pen, rect);
                            break;
                        case ShapeType.Ellipse:
                            graphics.FillEllipse(brush, rect);
                            graphics.DrawEllipse(pen, rect);
                            break;
                        case ShapeType.Line:
                            graphics.DrawLine(pen, startPoint, endPoint);
                            break;
                    }
                }
            }
        }

        private Rectangle GetRectangle(Point startPoint, Point endPoint)
        {
            int x = Math.Min(startPoint.X, endPoint.X);
            int y = Math.Min(startPoint.Y, endPoint.Y);
            int width = Math.Abs(startPoint.X - endPoint.X);
            int height = Math.Abs(startPoint.Y - endPoint.Y);
            return new Rectangle(x, y, width, height);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.Invalidate();
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            using (Bitmap bmp = new Bitmap(this.ClientSize.Width, this.ClientSize.Height))
            {
                this.DrawToBitmap(bmp, this.ClientRectangle);
                bmp.Save("shape.png");
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "PNG Files|*.png";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                using (Bitmap bitmap = new Bitmap(openFileDialog1.FileName))
                {
                    this.ClientSize = bitmap.Size;
                    this.BackgroundImage = new Bitmap(bitmap);
                }
            }
        }
    }
}