using Assets._Scripts.Entities;
using UnityEngine;

namespace Assets._Scripts.Managers
{
  public class GameCubesSpawnManager : StaticInstance<GameCubesSpawnManager>
  {
    [SerializeField] public int FieldWidth;

    [SerializeField] public int FieldHeight;

    [SerializeField] public GameObject GameCubePrefab;

    [SerializeField] public Transform CubesParent;

    [SerializeField] public int CubeWidth;

    [SerializeField] public int CubeHeight;

    [SerializeField] public Material[] Materials;

    private GameObject[,] _field;

    public void SetArray(GameCube[] cubes)
    {
      _field ??= new GameObject[FieldWidth, FieldHeight];
      for (int i = 0; i < FieldWidth; i++)
      {
        for (int j = 0; j < FieldHeight; j++)
        {
          var rand = Random.Range(0, Materials.Length);

          var pos = new Vector3(i * CubeWidth, j * CubeHeight, 1);
          _field[i, j] = Instantiate(GameCubePrefab, pos, Quaternion.identity, CubesParent);
          _field[i, j].GetComponent<MeshRenderer>().material = Materials[rand];
        }
      }
    }
  }
}