using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{

    public float sensity = 1f;
    public Transform cameraFollow;

    private float rotX = 0;

    private void Start()
    {
       // Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Slerp(transform.position, cameraFollow.position, 0.05f);
        // transform.position = cameraFollow.position;
        Rotate();
    }

    void Rotate()
    {
        float x = (Input.GetKey(KeyCode.E) ? 1.0f : 0) - (Input.GetKey(KeyCode.Q) ? 1.0f : 0);
        rotX += x;
        transform.eulerAngles = new Vector3(0, rotX * sensity, 0);
    }

}



