using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Utility
{
    public class QuitGame : MonoBehaviour
    {
        // Start is called before the first frame update
        public void QuitApplication()
        {
            Application.Quit();
        }
    }
}
