namespace Assets._Scripts.Entities
{
  public class GameCube
  {
    public int Index { get; set; }
    public Relations Relations { get; set; }
  }

  public class Relations
  {
    public int[] NeighboursIndicies { get; set; }
  }
}