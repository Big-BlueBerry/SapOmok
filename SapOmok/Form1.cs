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
using SapOmok;

namespace SapOmok
{
    public partial class Form1 : Form
    {
        private int stoneSize = 28;
        private int DSize = 30;
        private Pen pen;
        private Brush Bbrush, Wbrush;
        public Omok omok = new Omok();
        //public Odelo omok = new Odelo();

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
            ShowWhoTurn();
            Graphics g = panel1.CreateGraphics();
            for (int i = 0; i < 50; i++)//가로
                g.DrawLine(pen, 0,  i * DSize, 420,  i * DSize);
            for (int i = 0; i < 15; i++)//세로
                g.DrawLine(pen, i * DSize, 0, i * DSize, 1000 + 10 * DSize);
        }

        //마우스 입력받기
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Graphics g = panel1.CreateGraphics();   
            
            int x, y;
            x = e.X / DSize;
            y = e.Y / DSize;


            ShowWhoTurn();
            omok.SetStone(x, y,out bool c);
            if (c == false)
                DrawStone(g, x, y); 
        }

        //돌그리기
        public void DrawStone(Graphics g, int x, int y)
        {
            Rectangle r = new Rectangle(15 + DSize * x - stoneSize / 2, 15 + DSize * y - stoneSize / 2, stoneSize, stoneSize);

            if (omok.color == Stone.White)
                g.FillEllipse(Wbrush, r);
            else
                g.FillEllipse(Bbrush, r);

            omok.ChangeColor();
            ShowWhoTurn();
        }

        //현재 차례 표시
        private void ShowWhoTurn()
        {
            label1.Text = ("현재 차례\n" + omok.color);
        }
    }
}