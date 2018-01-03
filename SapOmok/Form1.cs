using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SapOmok
{
    public partial class Form1 : Form
    {
        private int stoneSize = 10;
        private int DSize = 10;
        private int hwa = 10;
        private Pen pen;
        private Brush Bbrush, Wbrush;
        public Stone color = Stone.White;
        public Stone[,] board = new Stone[100, 100];

        public Form1()
        {
            InitializeComponent();
            pen = new Pen(Color.Black);
            Wbrush = new SolidBrush(Color.White);
            Bbrush = new SolidBrush(Color.Black);
        }

        public enum Stone
        {
            None,
            Black,
            White
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
            int x, y;
            x = e.X / (10 * DSize / 3);
            y = e.Y / (10 * DSize / 3);

            IsOutside(x, y);
        }

        //확인
        public bool IsOutside(int x, int y)
        {
            SetStone(x, y);
            return x < 0 || y < 0 || x >= board.Length || y >= board.GetLength(1);
        }

        //돌 놓기
        public bool SetStone(int x, int y)
        {
            int width = 0, height = 0, diagl = 0, diagr = 0;

            Graphics d = panel1.CreateGraphics();
            Rectangle r = new Rectangle(10 + DSize * (x/10) - stoneSize / 2, 10 + DSize * (y/10) - stoneSize / 2, stoneSize, stoneSize);

            if (board[x, y] != Stone.None)
            {
                return false;
            }

            board[x, y] = color;

            // 왼쪽
            for (int i = 1; true; i++)
            {
                if (IsOutside(x, y - i) || board[x, y - i] != color)
                {
                    width += i - 1;
                    break;
                }
            }

            // 오른쪽
            for (int i = 1; true; i++)
            {
                if (IsOutside(x, y + i) || board[x, y + i] != color)
                {
                    width += i - 1;
                    break;
                }
            }

            //위
            for (int i = 1; true; i++)
            {
                if (IsOutside(x + i, y) || board[x + i, y] != color)
                {
                    height += i - 1;
                    break;
                }
            }

            //아래
            for (int i = 1; true; i++)
            {
                if (IsOutside(x - i, y) || board[x - i, y] != color)
                {
                    height += i - 1;
                    break;
                }
            }

            //제2사분면 대각선
            for (int i = 1; true; i++)
            {
                if (IsOutside(x + i, y - i) || board[x + i, y - i] != color)
                {
                    diagr += i - 1;
                    break;
                }
            }

            //제3사분면 대각선
            for (int i = 1; true; i++)
            {
                if (IsOutside(x - i, y - i) || board[x - i, y - i] != color)
                {
                    diagl += i - 1;
                    break;
                }
            }

            //제1사분면 대각선
            for (int i = 1; true; i++)
            {
                if (IsOutside(x + i, y + i) || board[x + i, y + i] != color)
                {
                    diagr += i - 1;
                    break;
                }
            }

            //제4사분면 대각선
            for (int i = 1; true; i++)
            {
                if (IsOutside(x - i, y + i) || board[x - i, y + i] != color)
                {
                    diagl += i - 1;
                    break;
                }
            }

            if (color == Stone.Black)
            {                
                color = Stone.White;
            }

            else
            {
                color = Stone.Black;
            }

            if (color == Stone.Black)
            {
                d.FillEllipse(Bbrush, r);
            }
            else
                d.FillEllipse(Wbrush, r);

            return width >= 4 || height >= 4 || diagl >= 4 || diagr >= 4;
        }
    }
}
