namespace SapOmok {
    public interface IGame {
        Stone CurrentTurn { get; }
        bool SetStone(int x, int y, out bool cannot);
    }
}