using System.Collections;
using Assets._Scripts.Entities;
using Assets._Scripts.Gameplay;
using DG.Tweening;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    public class BoardManager : StaticInstance<BoardManager>
    {
        [SerializeField] public int FieldWidth;

        [SerializeField] public int FieldHeight;

        [SerializeField] public GameObject GameCubePrefab;

        [SerializeField] public Transform CubesParent;

        [SerializeField] public float CubeWidth;

        [SerializeField] public float CubeHeight;

        private GameObject[,] _field;
        private Board _board;

        private bool _countTime = false;
        public float _timeToEnd;

        public Board MakeBoard()
        {
            _timeToEnd = GameOptions.RoundTimeInSeconds;
            _board = new Board(FieldWidth, FieldHeight, RotateCube);
            _field ??= new GameObject[FieldWidth, FieldHeight];

            for (int i = 0; i < _board.Cells.GetLength(0); i++)
            {
                for (int j = 0; j < _board.Cells.GetLength(1); j++)
                {
                    var pos = new Vector3(i * CubeWidth, j * CubeHeight, 1);

                    var newCube = Instantiate(GameCubePrefab, pos, Quaternion.identity, CubesParent);
                    newCube.GetComponentInChildren<GameCubeComponent>().SetCell(_board.Cells[i, j]);
                    newCube.GetComponentInChildren<GameCubeComponent>().DrawArrows();
                    newCube.gameObject.name = $"[{i}][{j}]";

                    _field[i, j] = newCube;
                }
            }

            return _board;
        }

        public void Update()
        {
            _timeToEnd -= Time.deltaTime;

            if( _timeToEnd <= 0)
                GameManager.Instance.ChangeState(GameState.Lose);
        }

        public void HandleCellClick(GameCubeComponent gameCubeComponent, Cell cell)
        {
            if (_timeToEnd <= 0)
                return;

            if (gameCubeComponent.CanBeRotated())
            {
                _board.ClicksCount++;

                if (!_countTime)
                {
                    _countTime = true;
                    _board.time.Start();
                }

                cell.Influence(GameOptions.InfluenceLevel, RotateCube, _board.ClicksCount, (_board.time.ElapsedMilliseconds / 1000), true);
            }

            if (_board.IsFinished())
                GameManager.Instance.ChangeState(GameState.Finish);
        }

        public void RotateCube(Cell cell)
        {
            _field[cell.X, cell.Y].GetComponentInChildren<GameCubeComponent>().RotateCube();
        }

        public void Reset()
        {
            _board = null;

            foreach (var cell in _field)
            {
                StartCoroutine("Die", cell);
            }
        }

        IEnumerator Die(object value)
        {
            (value as GameObject).transform.DOMoveZ(-4, 1f, false);
            yield return new WaitForSeconds(1f);
            DestroyImmediate(value as GameObject);
        }
    }
}
