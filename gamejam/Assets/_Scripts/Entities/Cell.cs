﻿// ReSharper disable once CheckNamespace
using System;

namespace Assets._Scripts.Entities
{
  public class Cell
  {
    public readonly int X;
    public readonly int Y;

    public InfluenceBehaviour Behaviour { get; private set; }
    public CellState State { get; private set; } = CellState.Hidden;

    private readonly Board _board;

    public Cell(int x, int y, Board board)
    {
      X = x;
      Y = y;
      _board = board;
      Behaviour = _board.InfluenceRandomizer.Get(this);
    }

    public void Influence(int influenceLevel, Action<Cell> rotateCube)
    {
      if (State == CellState.Hidden)
      {
        State = CellState.Success;
      }
      else if (State == CellState.Success)
      {
        State = CellState.Hidden;
      }

      _board.UpdateState(this, influenceLevel);
      Behaviour = _board.InfluenceRandomizer.Get(this);

            if (rotateCube != null)
                rotateCube(this);
    }
  }
}
