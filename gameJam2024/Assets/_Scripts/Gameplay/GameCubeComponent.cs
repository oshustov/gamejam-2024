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
    private Vector3 _forwardRotation = new Vector3(90, 0, 0);
    private Vector3 _backwardRotation = new Vector3(0, 0, 0);

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

    public void RotateCube()
    {
      if (_tweener != null && _tweener.active)
        return;

      _tweener = transform.DORotate(_isRotatedForward ? _backwardRotation : _forwardRotation, 0.5f, RotateMode.FastBeyond360);
      _isRotatedForward = !_isRotatedForward;
    }
  }
}