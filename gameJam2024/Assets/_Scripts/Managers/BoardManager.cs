using Assets._Scripts.Entities;
using Assets._Scripts.Gameplay;
using UnityEngine;

namespace Assets._Scripts.Managers
{
  public class BoardManager : StaticInstance<BoardManager>
  {
    [SerializeField] public int FieldWidth;

    [SerializeField] public int FieldHeight;

    [SerializeField] public GameObject GameCubePrefab;

    [SerializeField] public Transform CubesParent;

    [SerializeField] public int CubeWidth;

    [SerializeField] public int CubeHeight;

    private GameObject[,] _field;

    public Board MakeBoard()
    {
      var board = new Board(FieldWidth, FieldHeight);
      _field ??= new GameObject[FieldWidth, FieldHeight];

      for (int i = 0; i < board.Cells.GetLength(0); i++)
      {
        for (int j = 0; j < board.Cells.GetLength(1); j++)
        {
          var pos = new Vector3(i * CubeWidth, j * CubeHeight, 1);

          var newCube = Instantiate(GameCubePrefab, pos, Quaternion.identity, CubesParent);
          newCube.GetComponent<GameCubeComponent>().SetCell(board.Cells[i, j]);
          newCube.gameObject.name = $"[{i}][{j}]";
        }
      }

      return board;
    }

    public void HandleCellClick(GameCubeComponent gameCubeComponent, Cell cell)
    {
      cell.State
      gameCubeComponent.RotateCube(Vector3.down);
    }
  }
}