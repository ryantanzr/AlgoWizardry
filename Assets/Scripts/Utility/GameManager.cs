using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Algowizardry.Utility
{

    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public Camera mainCamera;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                mainCamera = Camera.main;
            }
            else
            {
                Destroy(this);
            }
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