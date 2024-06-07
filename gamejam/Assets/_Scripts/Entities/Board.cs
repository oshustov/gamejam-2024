

using System;
using System.Collections.Generic;
using System.Diagnostics;

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
        private readonly Random _random;

        public Action<Cell> RotateCube;

        public int ClicksCount { get; set; }
        public Stopwatch time = new Stopwatch();

        public Board(int sizeX, int sizeY, Action<Cell> rotateCube)
        {
            _random = new Random();
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

        public bool UpdateState(Cell influencer, int influenceLevel, bool canBeBomb)
        {
            if (influenceLevel == 0)
                return false;

            var behaviour = influencer.Behaviour;
            var isBomb = false;
            var hasBomb = false;
            if (behaviour.Up)
                isBomb = InfluenceToUpCell(influencer, influenceLevel - 1, canBeBomb);

            if (isBomb)
            {
                hasBomb = true;
                canBeBomb = false;
            }

            if (behaviour.Down)
                isBomb = InfluenceToDownCell(influencer, influenceLevel - 1, canBeBomb);

            if (isBomb)
            {
                hasBomb = true;
                canBeBomb = false;
            }

            if (behaviour.Left)
                isBomb = InfluenceToLestCell(influencer, influenceLevel - 1, canBeBomb);

            if (isBomb)
            {
                hasBomb = true;
                canBeBomb = false;
            }

            if (behaviour.Right)
                isBomb = InfluenceToRightCell(influencer, influenceLevel - 1, canBeBomb);

            if (isBomb)
            {
                hasBomb = true;
                canBeBomb = false;
            }

            if (behaviour.Curse)
                InfluenceCurseCell(influencer, influenceLevel - 1);

            return hasBomb;
        }

        private bool InfluenceToUpCell(Cell influencer, int influenceLevel, bool canBeBomb)
        {
            var upCell = Cells[influencer.X, influencer.Y + 1];
            return upCell.Influence(influenceLevel, RotateCube, ClicksCount, (time.ElapsedMilliseconds / 1000), canBeBomb);
        }

        private bool InfluenceToDownCell(Cell influencer, int influenceLevel, bool canBeBomb)
        {
            var upCell = Cells[influencer.X, influencer.Y - 1];
            return upCell.Influence(influenceLevel, RotateCube, ClicksCount, (time.ElapsedMilliseconds / 1000), canBeBomb);
        }

        private bool InfluenceToLestCell(Cell influencer, int influenceLevel, bool canBeBomb)
        {
            var upCell = Cells[influencer.X - 1, influencer.Y];
            return upCell.Influence(influenceLevel, RotateCube, ClicksCount, (time.ElapsedMilliseconds / 1000), canBeBomb);
        }

        private bool InfluenceToRightCell(Cell influencer, int influenceLevel, bool canBeBomb)
        {
            var upCell = Cells[influencer.X + 1, influencer.Y];
            return upCell.Influence(influenceLevel, RotateCube, ClicksCount, (time.ElapsedMilliseconds / 1000), canBeBomb);
        }

        private void InfluenceCurseCell(Cell influencer, int influenceLevel)
        {
            var hashSet = new HashSet<(int, int)>();

            int tries = 0;
            while(hashSet.Count < GameOptions.CurseFlipCount)
            {
                var i = _random.Next(0, MaxX);
                var j = _random.Next(0, MaxY);
                hashSet.Add((i, j));

                tries++;
                if(tries == 30)
                    break;
            }

            foreach(var cellIndex  in hashSet)
            {
                var cell = Cells[cellIndex.Item1, cellIndex.Item2];
                cell.Influence(influenceLevel, RotateCube, ClicksCount, (time.ElapsedMilliseconds / 1000), false);
            }
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
