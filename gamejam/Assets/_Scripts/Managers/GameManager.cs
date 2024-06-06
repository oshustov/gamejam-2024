using System;
using Assets._Scripts.Entities;
using Assets._Scripts.Managers;
using UnityEngine;

public class GameManager : StaticInstance<GameManager>
{
  public static event Action<GameState> OnBeforeStateChanged;
  public static event Action<GameState> OnAfterStateChanged;

  public GameState State { get; private set; }

  private Board _board;

  void Start() => ChangeState(GameState.SpawningField);

  public void ChangeState(GameState newState)
  {
    OnBeforeStateChanged?.Invoke(newState);

    State = newState;
    switch (newState)
    {
      case GameState.SpawningField:
        _board = BoardManager.Instance.MakeBoard();
        break;
      default:
        throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
    }

    OnAfterStateChanged?.Invoke(newState);

    Debug.Log($"New state: {newState}");
  }
}

[Serializable]
public enum GameState
{
  Starting = 0,
  SpawningField = 1
}