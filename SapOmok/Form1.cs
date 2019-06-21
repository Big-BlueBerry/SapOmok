using System.Drawing;
using System.Windows.Forms;

namespace SapOmok
{
    public partial class Form1 : Form
    {
        private int dsize = 31;
        private int stoneSize = 28;
        private Graphics graphics;
        private Pen pen;
        public Color color;
        private Omok omok = new Omok();

        public Form1()
        {
            InitializeComponent();
            color = Color.Black;
            pen = new Pen(color);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            OnDraw();
        }

        private void OnDraw()
        {
            graphics = CreateGraphics();
            for (int i = 0; i < 16; i++)
            {
                graphics.DrawLine(pen, i * dsize, 0, i * dsize, 470);
                graphics.DrawLine(pen, 0, i * dsize, 470, i * dsize);
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            int xSpot = e.X / dsize;
            int ySpot = e.Y / dsize;

            omok.CanPut(xSpot, ySpot, out bool c);
            if (c == false)
                DrawStone(xSpot, ySpot);
        }

        private void DrawStone(int xSpot, int ySpot)
        {
            if (Omok.color == Omok.Status.Black)
                color = Color.Black;
            else color = Color.White;

            graphics = CreateGraphics();
            graphics.FillEllipse(new SolidBrush(color), new Rectangle(15 + dsize * xSpot - stoneSize / 2, 15 + dsize * ySpot - stoneSize / 2, stoneSize, stoneSize));

            Omok.ChangeColor();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}