﻿using OOmok;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SapOmok;

namespace SapOmok
{
    public class Odelo
    {
        public Stone color = Stone.White;

        public Stone[,] board = new Stone[15, 15];

        public bool IsOutside(int x, int y)
        {
            return x < 0 || y < 0 || x >= board.Length || y >= board.GetLength(1);
        }

        public bool SetStone(int x, int y, out bool cannot)
        {
            if (board[x, y] != Stone.None)
            {
                cannot = true; return false;
            }
            cannot = false;

            board[x, y] = color;

            int width = 0, height = 0, diagl = 0, diagr = 0;

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

            if (width >= 4 || height >= 4 || diagl >= 4 || diagr >= 4)
            {
                IsWin();
            }

            if (color == Stone.Black)
                color = Stone.White;
            else
                color = Stone.Black;

            return true;
        }

        public void IsWin()
        {
            MessageBox.Show("추카추카~!!~!\"" + color + "\"WIN~!~!!", "호고고곡 게임이 끝나 부럿눼~!!");
            Application.Restart();
        }
    }
}