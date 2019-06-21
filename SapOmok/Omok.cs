using System.Windows.Forms;

namespace SapOmok {
    public class Omok : IGame {
        public Stone CurrentRole { get; private set; }
        public Stone[,] board = new Stone[15, 15];

        public bool IsOutside(int x, int y) {
            return x < 0 || y < 0 || x >= board.Length || y >= board.GetLength(1);
        }

        /// <summary>
        /// 하스스톤 말고 셋스톤~~
        /// </summary>
        /// <returns>이기면 true 아니면 false</returns>
        public bool SetStone(int x, int y, out bool cannot) {
            if (board[x, y] != Stone.None) {
                cannot = true;
                return true;
            }
            else
                cannot = false;

            board[x, y] = CurrentRole;

            int width = 0, height = 0, diagl = 0, diagr = 0;

            // 왼쪽
            for (int i = 1; true; i++) {
                if (IsOutside(x, y - i) || board[x, y - i] != CurrentRole) {
                    width += i - 1;
                    break;
                }
            }

            // 오른쪽
            for (int i = 1; true; i++) {
                if (IsOutside(x, y + i) || board[x, y + i] != CurrentRole) {
                    width += i - 1;
                    break;
                }
            }

            //위
            for (int i = 1; true; i++) {
                if (IsOutside(x + i, y) || board[x + i, y] != CurrentRole) {
                    height += i - 1;
                    break;
                }
            }

            //아래
            for (int i = 1; true; i++) {
                if (IsOutside(x - i, y) || board[x - i, y] != CurrentRole) {
                    height += i - 1;
                    break;
                }
            }

            //제2사분면 대각선
            for (int i = 1; true; i++) {
                if (IsOutside(x + i, y - i) || board[x + i, y - i] != CurrentRole) {
                    diagr += i - 1;
                    break;
                }
            }

            //제3사분면 대각선
            for (int i = 1; true; i++) {
                if (IsOutside(x - i, y - i) || board[x - i, y - i] != CurrentRole) {
                    diagl += i - 1;
                    break;
                }
            }

            //제1사분면 대각선
            for (int i = 1; true; i++) {
                if (IsOutside(x + i, y + i) || board[x + i, y + i] != CurrentRole) {
                    diagr += i - 1;
                    break;
                }
            }

            //제4사분면 대각선
            for (int i = 1; true; i++) {
                if (IsOutside(x - i, y + i) || board[x - i, y + i] != CurrentRole) {
                    diagl += i - 1;
                    break;
                }
            }

            if (width >= 4 || height >= 4 || diagl >= 4 || diagr >= 4) {
                IsWin();
            }
            ChangeColor();

            return true;
        }

        public void ChangeColor() {
            if (CurrentRole == Stone.Black)
                CurrentRole = Stone.White;
            else
                CurrentRole = Stone.Black;
        }

        public void IsWin() {
            MessageBox.Show("추카추카~!!~!\"" + CurrentRole + "\"WIN~!~!!", "호고고곡 게임이 끝나 부럿눼~!!");
            Application.Restart();
        }
    }
}
