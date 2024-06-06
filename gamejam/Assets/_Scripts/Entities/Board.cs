

using System;
// ReSharper disable once CheckNamespace
using Assets._Scripts.Logic;

namespace Assets._Scripts.Entities
{
  public class Board
  {
    public readonly int MaxX;
    public readonly int MaxY;
    public readonly Cell[,] Cells;
    public readonly int TotalCellsCount;
    public readonly InfluenceBehaviourRandomizer InfluenceRandomizer;

        public Action<Cell> RotateCube;

    public int ClicksCount { get; private set; }

    public Board(int sizeX, int sizeY, Action<Cell> rotateCube)
    {
            RotateCube = rotateCube;
      MaxX = sizeX - 1;
      MaxY = sizeY - 1;
      TotalCellsCount = sizeX * sizeY;
      InfluenceRandomizer = new InfluenceBehaviourRandomizer(this);

      Cells = new Cell[sizeX, sizeY];

      if (sizeX < 1)
        throw new Exception("Size by x can`t be less than 1");

      if (sizeY < 1)
        throw new Exception("Size by Y can`t be less than 1");

      for (var x = 0; x < sizeX; x++)
      {
        for (var y = 0; y < sizeY; y++)
        {
          Cells[x, y] = new Cell(x, y, this);
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

    public void UpdateState(Cell influencer, int influenceLevel)
    {
      ClicksCount++;

            if (influenceLevel == 0)
                return;

      var behaviour = influencer.Behaviour;

      if (behaviour.Up)
        InfluenceToUpCell(influencer, influenceLevel - 1);

      if (behaviour.Down)
        InfluenceToDownCell(influencer, influenceLevel - 1);

      if (behaviour.Left)
        InfluenceToLestCell(influencer, influenceLevel - 1);

      if (behaviour.Right)
        InfluenceToRightCell(influencer, influenceLevel - 1);
    }

    private void InfluenceToUpCell(Cell influencer, int influenceLevel)
    {
      var upCell = Cells[influencer.X, influencer.Y + 1];
      upCell.Influence(influenceLevel, RotateCube);
    }

    private void InfluenceToDownCell(Cell influencer, int influenceLevel)
    {
      var upCell = Cells[influencer.X, influencer.Y - 1];
      upCell.Influence(influenceLevel, RotateCube);
    }

    private void InfluenceToLestCell(Cell influencer, int influenceLevel)
    {
      var upCell = Cells[influencer.X - 1, influencer.Y];
      upCell.Influence(influenceLevel, RotateCube);
    }

    private void InfluenceToRightCell(Cell influencer, int influenceLevel)
    {
      var upCell = Cells[influencer.X + 1, influencer.Y];
      upCell.Influence(influenceLevel, RotateCube);
    }

    public int GetSuccessCellsCount()
    {
      var result = 0;

      var rows = Cells.GetLength(0);
      var cols = Cells.GetLength(1);

      for (var x = 0; x < rows; x++)
      {
        for (var y = 0; y < cols; y++)
        {
          var cell = Cells[x, y];

          if (cell.State == CellState.Success)
            result++;
        }
      }

      return result;
    }
  }
}
