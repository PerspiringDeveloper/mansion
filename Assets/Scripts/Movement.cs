using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float playerSpeed, floatSpeed, lookSpeed;
    float camVertical, camHorizontal;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        camVertical = 0f;
        camHorizontal = 0f;
    }

    void Update()
    {
        MoveTransform();

        // Camera
        camHorizontal += Input.GetAxis("Mouse X") * lookSpeed;
        camVertical += -Input.GetAxis("Mouse Y") * lookSpeed;
        camVertical = Mathf.Clamp(camVertical, -60, 60);
        transform.rotation = Quaternion.Euler(camVertical, camHorizontal, 0);
    }

    private void MoveTransform()
    {
        float curSpeedX = playerSpeed * Input.GetAxis("Vertical");
        float curSpeedY = playerSpeed * Input.GetAxis("Horizontal");

        Vector3 moveDirection = (transform.forward * curSpeedX) + (transform.right * curSpeedY);

        if (Input.GetKey("space")) {
            moveDirection += floatSpeed * Vector3.up;
        }
        if (Input.GetKey("left shift")) {
            moveDirection += floatSpeed * Vector3.down;
        }

        transform.position = transform.position + (moveDirection * Time.deltaTime);
    }
}
