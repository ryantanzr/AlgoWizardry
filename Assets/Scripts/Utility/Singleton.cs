using UnityEngine;

/**********************************************************
* Author: Ryan Tan
* This file contains abstractions for the Singleton
* pattern
**********************************************************/

namespace Algowizardry.Utility
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindFirstObjectByType<T>();
                    if (instance == null)
                    {
                        Debug.LogError("No instance of " + typeof(T) + " found in the scene");
                    }
                    else
                    {
                        DontDestroyOnLoad(instance.gameObject);
                    }
                }

                return instance;
            }
        }
    }

}