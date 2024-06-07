using Assets._Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets._Scripts.Managers
{
    public class GameOverManager : StaticInstance<GameOverManager>
    {
        public GameObject GameOverMenu;

        public Transform Parent;
        private GameObject _gameOver;

        public Transform WhereToSpawn;

        public void Show(bool isWin, float seconds, int clicks)
        {
            if (_gameOver != null)
            {
                return;
            }

            _gameOver = Instantiate(GameOverMenu, Vector3.zero, Quaternion.identity, Parent);
            _gameOver.transform.localPosition = new Vector3(WhereToSpawn.position.x, _gameOver.transform.localPosition.y, WhereToSpawn.position.z);

            var popup = _gameOver.GetComponent<GameOverPopup>();

            if (isWin )
                popup.YouWinObj.SetActive(true);

            if (!isWin)
                popup.YouLoseObj.SetActive(true);

            popup.TimeScoreText.text = $"{seconds.ToString()} seconds!";
            popup.ClickScoreText.text = $"{clicks.ToString()} clicks!";
        }

        public void Restart()
        {
            SceneManager.LoadScene("maingameplay");
        }

        public void MainMenu()
        {
            SceneManager.LoadScene("menu");
        }
    }
}
