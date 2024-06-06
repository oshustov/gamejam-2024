using Assets._Scripts.Gameplay;
using UnityEngine;

namespace Assets._Scripts.Utilities
{
  public class CameraClickCaster : MonoBehaviour
  {
    void Update()
    {
      if (Input.GetMouseButtonDown(0))
      {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
          var gameCubeComponent = hit.collider.gameObject.GetComponentInChildren<GameCubeComponent>();
          if (gameCubeComponent != null)
          {
            gameCubeComponent.Click();
          }
        }
      }
    }
  }
}
