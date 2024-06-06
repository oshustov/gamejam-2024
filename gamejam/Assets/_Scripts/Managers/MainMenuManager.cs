
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets._Scripts.Managers
{
    public class MainMenuManager : StaticInstance<MainMenuManager>
    {
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
