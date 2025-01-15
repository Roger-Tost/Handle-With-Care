using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_PlayerCam : MonoBehaviour
{
    public float seX;
    public float seY;
    public Transform orientation;
    public Transform cameraPosition;
    float roX;
    float roY;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraPosition.position;

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * seX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * seY;

        roX -= mouseY;
        roY += mouseX;

        roX = Mathf.Clamp(roX, -90f, 90f);

        transform.rotation = Quaternion.Euler(roX, roY, 0);
        orientation.rotation = Quaternion.Euler(0, roY, 0);
    }
}
