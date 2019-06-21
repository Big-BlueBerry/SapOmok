namespace SapOmok {
    public interface IGame {
        Stone CurrentRole { get; }
        bool SetStone(int x, int y, out bool cannot);
    }
}