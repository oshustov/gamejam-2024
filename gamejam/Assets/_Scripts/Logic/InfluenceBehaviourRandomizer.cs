using System;
using Assets._Scripts.Entities;

// ReSharper disable once CheckNamespace
namespace Assets._Scripts.Logic
{
  public class InfluenceBehaviourRandomizer
  {
    private readonly Board _board;
    private readonly Random _random;

        private float timeForCurse = 10;

    public InfluenceBehaviourRandomizer(Board board)
    {
      _board = board;
      _random = new Random();
    }

    public InfluenceBehaviour Get(Cell cell, int clicks, float totalTimeInSeconds, bool canBeCurse)
    {
      var up = GetForUp(cell);
      var down = GetForDown(cell);
      var left = GetForLeft(cell);
      var right = GetForRight(cell);

            var curse = GetCurse(clicks, totalTimeInSeconds);

            if (!canBeCurse)
                curse = false;

            if (curse)
            {
                up = false;
                down = false;
                left = false;
                right = false;
            }

      return new InfluenceBehaviour(up, down, left, right, curse);
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

        private bool GetCurse(int clicks, float timeInSeconds)
        {
            if(timeInSeconds < timeForCurse)
                return false;

           if(clicks % GameOptions.ClickCountForCurse == 0)
                return true;
           else return false;
        }
    }
}
