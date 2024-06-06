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
            }
            else
            {
                FlippedLeftArrow.SetActive(_cell.Behaviour.Left);
                FlippedRightArrow.SetActive(_cell.Behaviour.Right);
                FlippedUpArrow.SetActive(_cell.Behaviour.Down);
                FlippedDownArrow.SetActive(_cell.Behaviour.Up);
            }
        }
    }
}
