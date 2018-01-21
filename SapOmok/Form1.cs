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
        //public Omok omok = new Omok();
        public Odelo omok = new Odelo();

        public Form1()
        {
            InitializeComponent();
            pen = new Pen(Color.Black);
            Wbrush = new SolidBrush(Color.White);
            Bbrush = new SolidBrush(Color.Black);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            ShowWhoTurn();
            OnDraw();
            FirstShow();
        }      

        //선 그리기
        private void OnDraw()
        {
            Graphics g = panel1.CreateGraphics();
            for (int i = 0; i < 50; i++)//가로
                g.DrawLine(pen, 0,  i * DSize, 420,  i * DSize);
            for (int i = 0; i < 15; i++)//세로
                g.DrawLine(pen, i * DSize, 0, i * DSize, 1000 + 10 * DSize);
            ShowWhoTurn();
        }

        //마우스 입력받기
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Graphics g = panel1.CreateGraphics();   
            
            int x, y;
            x = e.X / DSize;
            y = e.Y / DSize;


            
            DrawStone(g, x, y);
            omok.SetStone(x, y,out bool c);           
            ShowWhoTurn();
        }

        //돌그리기
        private void DrawStone(Graphics g, int x, int y)
        {
            Rectangle r = new Rectangle(15 + DSize * x - stoneSize / 2, 15 + DSize * y - stoneSize / 2, stoneSize, stoneSize);

            if (omok.color == Stone.White)
                g.FillEllipse(Wbrush, r);
            else
                g.FillEllipse(Bbrush, r);
        }

        //오델로용 첨뜨는 돌 4개
        public void FirstShow()
        {
            Graphics g = panel1.CreateGraphics();

            DrawStone(g, 5, 6);
            omok.color = Stone.Black;
            DrawStone(g, 6, 6);
            omok.color = Stone.White;
            DrawStone(g, 5, 5);
            omok.color = Stone.Black;
            DrawStone(g, 6, 5);
        }

        //현재 차례 표시
        private void ShowWhoTurn()
        {
            label1.Text = ("현재 차례\n" + omok.color);
        }

        
    }
}