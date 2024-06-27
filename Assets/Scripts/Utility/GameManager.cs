using UnityEngine;
using UnityEngine.SceneManagement;

namespace Algowizardry.Utility
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public void QuitMinigame()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}