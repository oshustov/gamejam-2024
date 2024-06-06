using System.Collections;
using Assets._Scripts.Entities;
using Assets._Scripts.Managers;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Assets._Scripts.Gameplay
{
    public class GameCubeComponent : MonoBehaviour
    {
        private Cell _cell;
        private CellState _currentState;
        private TweenerCore<Quaternion, Vector3, QuaternionOptions> _tweener;

        private bool _isRotatedForward = false;
        private Vector3 _forwardRotation = new Vector3(180, 0, 0);
        private Vector3 _backwardRotation = new Vector3(0, 0, 0);

        public GameObject LeftArrow;
        public GameObject RightArrow;
        public GameObject UpArrow;
        public GameObject DownArrow;

        public GameObject FlippedLeftArrow;
        public GameObject FlippedRightArrow;
        public GameObject FlippedUpArrow;
        public GameObject FlippedDownArrow;


        public GameObject Curse;
        public GameObject FlippedCurse;

        public float ShakeDuration = 5f;
        public float ShakeStrength = 0.05f;
        public int ShakeVibrato = 1;
        public float ShakeRandomness = 90f;

        private Vector3 _initialPos;
        private Vector3 _minBounds;
        private Vector3 _maxBounds;

        private Tweener _shakingTween;
        private bool _doShaking = true;

        void Start()
        {
            _initialPos = transform.parent.position;
            _minBounds = new Vector3(0, 0, -0.1f);
            _maxBounds = new Vector3(0.01f, 0.01f, 0.1f);
        }

        void Update()
        {
            if (_doShaking)
            {
                _shakingTween = this.transform.parent.transform
                    .DOShakePosition(ShakeDuration, ShakeStrength, ShakeVibrato, ShakeRandomness)
                    .SetLoops(-1, LoopType.Restart)
                    .OnUpdate(EnsureInBounds);
            }
        }

        private void EnsureInBounds()
        {
            Vector3 clampedPosition = transform.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, _initialPos.x + _minBounds.x, _initialPos.x + _maxBounds.x);
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, _initialPos.y + _minBounds.y, _initialPos.y + _maxBounds.y);
            clampedPosition.z = Mathf.Clamp(clampedPosition.z, _initialPos.z + _minBounds.z, _initialPos.z + _maxBounds.z);
            transform.parent.position = clampedPosition;
        }

        public void Click()
        {
            if (_cell != null)
            {
                BoardManager.Instance.HandleCellClick(this, _cell);
                Debug.Log($"[{_cell.X.ToString()}][{_cell.Y.ToString()}] is clicked");
            }
        }

        public void SetCell(Cell cell)
        {
            _cell = cell;
            if (_cell.State != _currentState)
                RotateCube();

            _currentState = _cell.State;
        }

        public bool CanBeRotated()
        {
            if (_tweener != null && _tweener.active)
                return false;

            return true;
        }


        public void RotateCube()
        {
            AudioSystem.Instance.PlayRandomStone();
            _tweener = transform.parent.transform.DORotate(_isRotatedForward ? _backwardRotation : _forwardRotation, 0.5f, RotateMode.FastBeyond360);
            _isRotatedForward = !_isRotatedForward;
            DrawArrows();
        }

        public void DrawArrows()
        {
            if (!_isRotatedForward)
            {
                LeftArrow.SetActive(_cell.Behaviour.Left);
                RightArrow.SetActive(_cell.Behaviour.Right);
                UpArrow.SetActive(_cell.Behaviour.Up);
                DownArrow.SetActive(_cell.Behaviour.Down);

                Curse.SetActive(_cell.Behaviour.Curse);
            }
            else
            {
                FlippedLeftArrow.SetActive(_cell.Behaviour.Left);
                FlippedRightArrow.SetActive(_cell.Behaviour.Right);
                FlippedUpArrow.SetActive(_cell.Behaviour.Down);
                FlippedDownArrow.SetActive(_cell.Behaviour.Up);

                FlippedCurse.SetActive(_cell.Behaviour.Curse);
            }
        }

        public Sequence PlayDying()
        {
            _shakingTween?.Complete(false);
            _doShaking = false;

            var sequence = DOTween.Sequence();

            var randSecs = Random.Range(1, 5);

            sequence.Join(transform.parent.transform.DOMoveZ(100f, randSecs, false));

            return sequence;
        }
    }
}
