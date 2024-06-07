using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Algowizardry.Utility
{
    public class Billboard : MonoBehaviour
    {
        Vector3 cameraDirection;

        void Start()
        {
            cameraDirection = Camera.main.transform.forward;
            cameraDirection.y = 0;

            transform.rotation = Quaternion.LookRotation(cameraDirection);
        }
    }
}
