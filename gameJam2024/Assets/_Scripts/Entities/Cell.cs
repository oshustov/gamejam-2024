namespace Assets._Scripts.Entities
{
  public class Cell
  {
    public readonly int X;
    public readonly int Y;

    public InfluenceBehaviour Behaviour { get; set; }
    public CellState State { get; private set; } = CellState.Hidden;

    private readonly Board _board;

    public Cell(int x, int y, Board board, InfluenceBehaviour behaviour)
    {
      X = x;
      Y = y;
      _board = board;
      Behaviour = behaviour;
    }

    public void Influence()
    {
      if (State == CellState.Hidden)
      {
        State = CellState.Success;
        return;
      }

      if (State == CellState.Success)
      {
        State = CellState.Hidden;
      }

      _board.UpdateState(this);
    }
  }
}
