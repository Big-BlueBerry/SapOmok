using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SapOmok
{
    class Omok
    {
        public static Status color = Status.Black;
        public Status[,] board = new Status[15, 15];
        public enum Status
        {
            None,
            Black,
            White
        }

        private bool IsOutside(int x, int y)
        {
            return x < 0 || y < 0 || x >= board.Length || y >= board.GetLength(1);
        }

        public bool CanPut(int x, int y, out bool cantput)
        {
            if (board[x, y] != Status.None)
            {
                cantput = true;
                return cantput;
            }
            else cantput = false;

            board[x, y] = color;

            SetStone(x, y);

            return cantput;
        }

        struct Vector2
        {
            public readonly int X, Y;

            public Vector2(int X, int Y)
            {
                this.X = X;
                this.Y = Y;
            }

            public static Vector2 Up = new Vector2(0, 1);
            public static Vector2 Down = new Vector2(0, -1);
            public static Vector2 Left = new Vector2(-1, 0);
            public static Vector2 Right = new Vector2(1, 0);
            public static Vector2 UpAndRight = new Vector2(1, 1);
            public static Vector2 UpAndLeft = new Vector2(-1, 1);
            public static Vector2 DownAndRight = new Vector2(1, -1);
            public static Vector2 DownAndLeft = new Vector2(-1, -1);
        }

        private int CountStone(ref Vector2 now, ref Vector2 direction)
        {
            for (int i = 1; true; i++)
            {
                if (IsOutside(now.X + direction.X * i, now.Y - direction.Y * i)
                    || board[now.X + direction.X * i, now.Y - direction.Y * i] != color)
                {
                    return i - 1;
                }
            }
        }

        public void SetStone(int x, int y)
        {
            Vector2 now = new Vector2(x, y);
            
            int width = CountStone(ref now, ref Vector2.Left) + CountStone(ref now, ref Vector2.Right),
                height = CountStone(ref now, ref Vector2.Up) + CountStone(ref now, ref Vector2.Down),
                diagl = CountStone(ref now, ref Vector2.UpAndLeft) + CountStone(ref now, ref Vector2.DownAndRight),
                diagr = CountStone(ref now, ref Vector2.UpAndRight) + CountStone(ref now, ref Vector2.DownAndLeft);

            if (width >= 4 || height >= 4 || diagl >= 4 || diagr >= 4)
                IsWin();
        }

        public static void ChangeColor()
        {
            if (color == Status.Black)
                color = Status.White;
            else
                color = Status.Black;
        }

        private void IsWin()
        {
            MessageBox.Show("추카추카~!!~!\"" + color + "\"WIN~!~!!", "호고고곡 게임이 끝나 부럿눼~!!");
            Application.Restart();
        }

    }
}

