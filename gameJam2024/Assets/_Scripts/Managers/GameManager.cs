using System;
using Assets._Scripts.Entities;
using Assets._Scripts.Managers;
using UnityEngine;

public class GameManager : StaticInstance<GameManager> {
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;

    public GameState State { get; private set; }

    void Start() => ChangeState(GameState.SpawningField);

    public void ChangeState(GameState newState)
    {
      OnBeforeStateChanged?.Invoke(newState);

      State = newState;
      switch (newState)
      {
        case GameState.SpawningField:
          GameCubesSpawnManager.Instance.SetArray(new GameCube[]
          {
            new GameCube() { Index = 0, Relations = null },
            new GameCube() { Index = 1, Relations = null },
            new GameCube() { Index = 2, Relations = null },
            new GameCube() { Index = 3, Relations = null },
            new GameCube() { Index = 4, Relations = null },
            new GameCube() { Index = 5, Relations = null },
            new GameCube() { Index = 6, Relations = null },
            new GameCube() { Index = 7, Relations = null },
            new GameCube() { Index = 8, Relations = null },
            new GameCube() { Index = 9, Relations = null },
            new GameCube() { Index = 10, Relations = null },
          });
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
      }

      OnAfterStateChanged?.Invoke(newState);

      Debug.Log($"New state: {newState}");
    }
}

[Serializable]
public enum GameState {
    Starting = 0,
    SpawningField = 1
}