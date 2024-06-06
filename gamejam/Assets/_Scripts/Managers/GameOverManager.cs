﻿using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets._Scripts.Managers
{
    public class GameOverManager : StaticInstance<GameOverManager>
    {
        public GameObject GameOverMenu;

        public Transform Parent;
        private GameObject _gameOver;

        public void SpawnButtons()
        {
            _gameOver = Instantiate(GameOverMenu, Vector3.zero, Quaternion.identity, Parent);
            _gameOver.transform.localPosition = new Vector3(0f, _gameOver.transform.localPosition.y, 0f);
        }

        public void Restart()
        {
            DestroyImmediate(_gameOver);
            GameManager.Instance.ChangeState(GameState.Play);
        }

        public void MainMenu()
        {
            SceneManager.LoadScene("menu");
        }
    }
}