using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Utility
{
    public class PauseController : MonoBehaviour
    {
        //UI to be update
        public GameObject pauseMenu;
        private bool isPaused = false;

        // Update is called once per frame
        private void Start()
        {
            pauseMenu.SetActive(false);
        }
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                isPaused = !isPaused;
                SetUpdatePause(isPaused);
            }
        }
        public void Pause()
        {
            isPaused = true;
            SetUpdatePause(isPaused);
        }
        public void Unpause()
        {
            isPaused = false;
            SetUpdatePause(isPaused);
        }
        private void SetUpdatePause(bool isPaused)
        {
            //Pause the "clock"
            Time.timeScale = isPaused ? 0.0f : 1.0f;
            //Stop the music
            AudioListener.pause = isPaused;
            //Disable / Enable pause menu UI
            pauseMenu.SetActive(isPaused);
        }
    }

}
