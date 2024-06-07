using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Algowizardry.Utility
{

    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        private void Awake()
        {

        }

        public void StartMinigame()
        {

        }

        public void PauseMinigame()
        {

        }

        public void QuitMinigame()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}