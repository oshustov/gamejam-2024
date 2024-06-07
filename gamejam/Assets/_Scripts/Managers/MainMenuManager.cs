
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets._Scripts.Managers
{
    public class MainMenuManager : StaticInstance<MainMenuManager>
    {
        void Start()
        {
            AudioSystem.Instance.PlayMainMenuTheme();
        }

        public void OnStartClick()
        {
            SceneManager.LoadScene("maingameplay");
        }

        public void OnExitClick()
        {
            Application.Quit();
        }
    }
}
