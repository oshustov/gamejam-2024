using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Scripts.Entities
{
  public class Board
  {
    public readonly Cell[,] Cells;

    public Board(int sizeX, int sizeY)
    {
      Cells = new Cell[sizeX, sizeY];

      if (sizeX < 1)
        throw new Exception("Size by x can`t be less than 1");

      if (sizeY < 1)
        throw new Exception("Size by Y can`t be less than 1");

      for (var x = 0; x < sizeX; x++)
      {
        for (var y = 0; y < sizeY; y++)
        {
          Cells[x, y] = new Cell(x, y, this, new InfluenceBehaviour(false, false, false, false));
        }
      }
    }

    public bool IsFinished()
    {
      var rows = Cells.GetLength(0);
      var cols = Cells.GetLength(1);

      for (var x = 0; x < rows; x++)
      {
        for (var y = 0; y < cols; y++)
        {
          var cell = Cells[x, y];

          if (cell.State != CellState.Success)
            return false;
        }
      }

      return true;
    }

    public void UpdateState(Cell cell)
    {

    }
  }
}
