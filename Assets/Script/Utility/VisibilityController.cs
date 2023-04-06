using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Cameras
{
    public class VisibilityController : MonoBehaviour
    {
        private Renderer objectRenderer;

        private void Start()
        {
            Debug.Log("Starting");
            objectRenderer = GetComponent<Renderer>();
        }

        private void OnBecameVisible()
        {
            Debug.Log("Visible");

            // Enable the renderer when it's within the camera's view
            objectRenderer.enabled = true;
        }

        private void OnBecameInvisible()
        {
            Debug.Log("Invisible");

            // Disable the renderer when it's outside the camera's view
            objectRenderer.enabled = false;
        }
    }

}
