using System.Collections;
using Assets._Scripts.Entities;
using Assets._Scripts.Gameplay;
using Assets._Scripts.Systems;
using DG.Tweening;
using Oculus.Interaction.Input;
using OculusSampleFramework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

        public TextMeshProUGUI TextTime;

        private int InfluenceLevel = GameOptions.InfluenceLevelNormal;

        private GameObject[,] _field;
        private Board _board;

        private bool _countTime = false;
        public float _timeToEnd;

        void Start()
        {
            var isHard = FindObjectOfType<PersistDataSystem>()?.IsHard ?? false;
            InfluenceLevel = isHard
                ? GameOptions.InfluenceLevelHard
                : GameOptions.InfluenceLevelNormal;

            _timeToEnd = isHard
                ? GameOptions.RoundTimeInSecondsHard
                : GameOptions.RoundTimeInSeconds;

            Debug.Log($"InfluenceLevel is {InfluenceLevel}");
        }

        public float GetCompletionTime()
        {
            var isHard = FindObjectOfType<PersistDataSystem>()?.IsHard;

            var givenTime = isHard ?? false
                ? GameOptions.RoundTimeInSeconds
                : GameOptions.RoundTimeInSecondsHard;

            return Mathf.Round(givenTime - _timeToEnd);
        }

        public Board MakeBoard()
        {
            var isHard = FindObjectOfType<PersistDataSystem>()?.IsHard ?? false;
            _board = new Board(FieldWidth, FieldHeight, RotateCube, isHard);
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

            TextTime.text = $"Time: {(int)_timeToEnd}";

            if (_timeToEnd < 20)
            {
                TextTime.color = Color.red;
            }

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

                if (cell.Behaviour.Curse)
                    AudioSystem.Instance.PlayRandomBombSound();

                cell.Influence(InfluenceLevel, RotateCube, _board.ClicksCount, (_board.time.ElapsedMilliseconds / 1000), true);
            }

            if (_board.IsFinished())
            {
                GameManager.Instance.ChangeState(GameState.Finish);
            }
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
            var go = value as GameObject;
            if (go == null)
            {
                Debug.Log("Can't animate destroy because value is not a game object");
                yield break;
            }

            go.GetComponent<ButtonController>().ForceResetButton();
            var gameCubeComponent = go.GetComponentInChildren<GameCubeComponent>();
            yield return gameCubeComponent.PlayDying().WaitForCompletion();
            go.SetActive(false);
            //Destroy(go);
        }
    }
}
