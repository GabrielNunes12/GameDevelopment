using UnityEngine;
namespace RPG.Cameras
{
    public class CameraController : MonoBehaviour
    {
        public float mouseSensitivity = 100f;
        float xRotation = 0f;
        public Transform target;
        public float yRotation = 0f;
        // Start is called before the first frame update
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Update is called once per frame
        void Update()
        {
            // Get the mouse movement delta
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // Rotate the camera around the player based on the mouse movement
            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            // Update the camera position and rotation
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0.0f);
            transform.position = target.position - transform.forward;
        }
    }

}
