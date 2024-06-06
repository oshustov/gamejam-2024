

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

    public Board(int sizeX, int sizeY)
    {
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

    public void UpdateState(Cell influencer)
    {
      var behaviour = influencer.Behaviour;

      if (behaviour.Up)
        InfluenceToUpCell(influencer);

      if (behaviour.Down)
        InfluenceToDownCell(influencer);

      if (behaviour.Left)
        InfluenceToLestCell(influencer);

      if (behaviour.Right)
        InfluenceToRightCell(influencer);
    }

    private void InfluenceToUpCell(Cell influencer)
    {
      var upCell = Cells[influencer.X, influencer.Y + 1];
      upCell.Influence();
      UpdateState(upCell);
    }

    private void InfluenceToDownCell(Cell influencer)
    {
      var upCell = Cells[influencer.X, influencer.Y - 1];
      upCell.Influence();
      UpdateState(upCell);
    }

    private void InfluenceToLestCell(Cell influencer)
    {
      var upCell = Cells[influencer.X - 1, influencer.Y];
      upCell.Influence();
      UpdateState(upCell);
    }

    private void InfluenceToRightCell(Cell influencer)
    {
      var upCell = Cells[influencer.X + 1, influencer.Y];
      upCell.Influence();
      UpdateState(upCell);
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
