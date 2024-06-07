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

    void Start() => ChangeState(GameState.Play);

    public void ChangeState(GameState newState)
    {
        if (newState == State)
            return;

        OnBeforeStateChanged?.Invoke(newState);

        State = newState;
        switch (newState)
        {
            case GameState.Play:
                _board = BoardManager.Instance.MakeBoard();
                AudioSystem.Instance.PlayMainTheme();
                break;
            case GameState.Finish:
                BoardManager.Instance.Reset();
                GameOverManager.Instance.SpawnButtons();
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
  Play = 1,
  Finish = 2,
  Lose = 3
}
