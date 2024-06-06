using System;
using Assets._Scripts.Entities;

// ReSharper disable once CheckNamespace
namespace Assets._Scripts.Logic
{
  public class InfluenceBehaviourRandomizer
  {
    private readonly Board _board;
    private readonly Random _random;

    public InfluenceBehaviourRandomizer(Board board)
    {
      _board = board;
      _random = new Random();
    }

    public InfluenceBehaviour Get(Cell cell)
    {
      var up = GetForUp(cell);
      var down = GetForDown(cell);
      var left = GetForLeft(cell);
      var right = GetForRight(cell);

      return new InfluenceBehaviour(up, down, left, right);
    }

    private bool GetForUp(Cell cell)
    {
      if (cell.Y == _board.MaxY)
        return false;
      return GetRandomly();
    }

    private bool GetForDown(Cell cell)
    {
      if (cell.Y == 0)
        return false;

      return GetRandomly();
    }

    private bool GetForLeft(Cell cell)
    {
      if (cell.X == 0)
        return false;

      return GetRandomly();
    }

    private bool GetForRight(Cell cell)
    {
      if (cell.X == _board.MaxX)
        return false;

      return GetRandomly();
    }

    private bool GetRandomly()
    {
      var randomNumber = _random.Next(0, 100);
      return randomNumber < 25;
    }
  }
}