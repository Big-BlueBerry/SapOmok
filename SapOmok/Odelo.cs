using System.Drawing;
using System.Windows.Forms;

namespace SapOmok {
    public class Odelo : IGame{
        public Stone CurrentTurn { get; private set; } = Stone.Black;
        private Stone[,] board = new Stone[15, 15];

        private bool IsOutside(int x, int y) {
            return x < 0 || y < 0 || x >= board.Length || y >= board.GetLength(1);
        }

        /// <summary>
        /// 하스스톤 말고 셋스톤~~
        /// </summary>
        /// <returns>이기면 true 아니면 false</returns>
        public bool SetStone(int x, int y, out bool cannot) {
            if (board[x, y] != Stone.None) {
                cannot = true;
                return false;
            }
            cannot = false;

            board[x, y] = CurrentTurn;

            int width = 0, height = 0, diagl = 0, diagr = 0;

            // 왼쪽
            for (int i = 1; true; i++) {
                if (IsOutside(x, y - i) || board[x, y - i] != CurrentTurn) {
                    width += i - 1;
                    break;
                }
            }

            // 오른쪽
            for (int i = 1; true; i++) {
                if (IsOutside(x, y + i) || board[x, y + i] != CurrentTurn) {
                    width += i - 1;
                    break;
                }
            }

            //위
            for (int i = 1; true; i++) {
                if (IsOutside(x + i, y) || board[x + i, y] != CurrentTurn) {
                    height += i - 1;
                    break;
                }
            }

            //아래
            for (int i = 1; true; i++) {
                if (IsOutside(x - i, y) || board[x - i, y] != CurrentTurn) {
                    height += i - 1;
                    break;
                }
            }

            //제2사분면 대각선
            for (int i = 1; true; i++) {
                if (IsOutside(x + i, y - i) || board[x + i, y - i] != CurrentTurn) {
                    diagr += i - 1;
                    break;
                }
            }

            //제3사분면 대각선
            for (int i = 1; true; i++) {
                if (IsOutside(x - i, y - i) || board[x - i, y - i] != CurrentTurn) {
                    diagl += i - 1;
                    break;
                }
            }

            //제1사분면 대각선
            for (int i = 1; true; i++) {
                if (IsOutside(x + i, y + i) || board[x + i, y + i] != CurrentTurn) {
                    diagr += i - 1;
                    break;
                }
            }

            //제4사분면 대각선
            for (int i = 1; true; i++) {
                if (IsOutside(x - i, y + i) || board[x - i, y + i] != CurrentTurn) {
                    diagl += i - 1;
                    break;
                }
            }

            CurrentTurn = CurrentTurn == Stone.Black ? Stone.White : Stone.Black;

            return width >= 4 || height >= 4 || diagl >= 4 || diagr >= 4;
        }

        private void ShowFristStone() {
            Form1 form = new Form1();
            Graphics g = form.panel1.CreateGraphics();

            CurrentTurn = Stone.Black;
            SetStone(7, 4, out bool a);
            form.DrawStone(g, 7, 4);
            CurrentTurn = Stone.White;
            SetStone(8, 4, out bool d);
            form.DrawStone(g, 8, 4);
            CurrentTurn = Stone.Black;
            SetStone(8, 5, out bool b);
            form.DrawStone(g, 8, 5);
            CurrentTurn = Stone.White;
            SetStone(7, 5, out bool c);
            form.DrawStone(g, 7, 5);
            CurrentTurn = Stone.Black;
        }
    }
}
