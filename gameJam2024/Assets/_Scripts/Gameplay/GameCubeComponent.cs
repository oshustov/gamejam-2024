using System.Collections;
using Assets._Scripts.Entities;
using Assets._Scripts.Managers;
using UnityEngine;

namespace Assets._Scripts.Gameplay
{
  public class GameCubeComponent : MonoBehaviour
  {
    private Cell _cell;

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
    }

    public void RotateCube(Vector3 axis)
    {
      // Calculate the new rotation
      Quaternion newRotation = Quaternion.Euler(axis * 90) * transform.rotation;

      // Apply the new rotation
      transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime);
    }
  }
}