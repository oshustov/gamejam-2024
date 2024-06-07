
using Assets._Scripts.Systems;
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
            FindObjectOfType<PersistDataSystem>().IsNormal = true;
            SceneManager.LoadScene("maingameplay");
        }

        public void OnStartHardClick()
        {
            FindObjectOfType<PersistDataSystem>().IsHard = true;
            SceneManager.LoadScene("maingameplay");
        }

        public void OnExitClick()
        {
            Application.Quit();
        }
    }
}
