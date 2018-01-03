using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OOmok;

namespace SapOmok
{
    public partial class Form1 : Form
    {
        private int stoneSize = 25;
        private int DSize = 10;
        private int hwa = 10;
        private Pen pen;
        private Brush Bbrush, Wbrush;
        public Omok omok = new Omok();

        public Form1()
        {
            InitializeComponent();
            pen = new Pen(Color.Black);
            Wbrush = new SolidBrush(Color.White);
            Bbrush = new SolidBrush(Color.Black);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            OnDraw();
        }

        //선 그리기
        private void OnDraw()
        {
            Graphics g = panel1.CreateGraphics();
            for (int i = 0; i < 50; i++)//세로
                g.DrawLine(pen, 0, 10 * i * DSize / 3, 1000, 10 * i * DSize / 3);
            for (int i = 0; i < 50; i++)//가로
                g.DrawLine(pen, 10 * i * DSize / 3, 0, 10 * i * DSize / 3, 1000 + 10 * DSize);
            OnDrawHwa(g);
        }

        //점찍기
        private void OnDrawHwa(Graphics g)
        {
            for (int i = 3; i <= 50; i += 10)
            {
                for (int j = 3; j <= 50; j += 10)
                {
                    Rectangle r = new Rectangle(1 + DSize * i - hwa / 3, 2 + DSize * j - hwa / 3, hwa, hwa);
                    g.FillEllipse(Bbrush, r);
                }
            }
        }

        //마우스 입력받기
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Graphics g = panel1.CreateGraphics();
            int x, y;
            x = e.X / DSize;
            y = e.Y / DSize;

            omok.SetStone(x, y, out bool c);
            DrawStone(g,x,y);
        }

        private void DrawStone(Graphics g, int x, int y)
        {
            Rectangle r = new Rectangle(10 + DSize * x - stoneSize / 2, 10 + DSize * y - stoneSize / 2, stoneSize, stoneSize);

            if (omok.color == Stone.Black)
                g.FillEllipse(Bbrush, r);
            else
                g.FillEllipse(Wbrush, r);

        }
    }
}