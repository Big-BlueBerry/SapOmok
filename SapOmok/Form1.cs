using System.Drawing;
using System.Windows.Forms;

namespace SapOmok {
    public partial class Form1 : Form {
        private int stoneSize = 28;
        private int DSize = 30;
        private Graphics g;
        private Pen pen;
        private Brush Bbrush, Wbrush;

        private IGame game = new Omok();

        public Form1() {
            InitializeComponent();
            pen = new Pen(Color.Black);
            Wbrush = new SolidBrush(Color.White);
            Bbrush = new SolidBrush(Color.Black);
            g = panel1.CreateGraphics();
        }

        protected override void OnPaint(PaintEventArgs e) {
            OnDraw();
        }

        private void OnDraw() {
            ShowWhoseTurn();
            for (int i = 0; i < 50; i++) //가로
                g.DrawLine(pen, 0, i * DSize, 420, i * DSize);
            for (int i = 0; i < 15; i++) //세로
                g.DrawLine(pen, i * DSize, 0, i * DSize, 1000 + 10 * DSize);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e) {
            int x, y;
            x = e.X / DSize;
            y = e.Y / DSize;

            var turn = game.CurrentTurn;
            ShowWhoseTurn();
            var isEnded = game.SetStone(x, y, out bool cannot);
            if (cannot == false) {
                DrawStone(x, y);
            }
            if (isEnded) {
                MessageBox.Show($"추카추카~!!~!\"{turn}\"WIN~!~!!", "호고고곡 게임이 끝나 부럿눼~!!");
                Application.Restart();
            }
        }

        public void DrawStone(int x, int y) {
            Rectangle r = new Rectangle(15 + DSize * x - stoneSize / 2, 15 + DSize * y - stoneSize / 2, stoneSize,
                stoneSize);

            if (game.CurrentTurn == Stone.White)
                g.FillEllipse(Wbrush, r);
            else
                g.FillEllipse(Bbrush, r);
            ShowWhoseTurn();
        }

        private void ShowWhoseTurn() {
            label1.Text = $"현재 차례\n{game.CurrentTurn}";
        }
    }
}