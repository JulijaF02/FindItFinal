using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera[] cameras;
    private int currentCameraIndex = 0;

    void Start()
    {
        // Ensure that one camera is active at the start
        if (cameras.Length > 0)
        {
            // Activate the first camera and deactivate the others
            for (int i = 0; i < cameras.Length; i++)
            {
                cameras[i].gameObject.SetActive(i == currentCameraIndex);
                cameras[i].GetComponent<AudioListener>().enabled = (i == currentCameraIndex); // Enable Audio Listener on active camera
            }
        }
    }

    void Update()
    {
        // Switch to the next camera on right arrow key press
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SwitchToNextCamera();
        }

        // Switch to the previous camera on left arrow key press
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SwitchToPreviousCamera();
        }
    }

    void SwitchToNextCamera()
    {
        // Disable the current camera and its Audio Listener
        cameras[currentCameraIndex].gameObject.SetActive(false);
        cameras[currentCameraIndex].GetComponent<AudioListener>().enabled = false;

        // Increment the camera index
        currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

        // Activate the next camera and its Audio Listener
        cameras[currentCameraIndex].gameObject.SetActive(true);
        cameras[currentCameraIndex].GetComponent<AudioListener>().enabled = true;
    }

    void SwitchToPreviousCamera()
    {
        // Disable the current camera and its Audio Listener
        cameras[currentCameraIndex].gameObject.SetActive(false);
        cameras[currentCameraIndex].GetComponent<AudioListener>().enabled = false;

        // Decrement the camera index
        currentCameraIndex = (currentCameraIndex - 1 + cameras.Length) % cameras.Length;

        // Activate the previous camera and its Audio Listener
        cameras[currentCameraIndex].gameObject.SetActive(true);
        cameras[currentCameraIndex].GetComponent<AudioListener>().enabled = true;
    }
}
