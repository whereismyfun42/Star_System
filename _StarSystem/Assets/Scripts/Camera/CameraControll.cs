using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    public static CameraControll instance;


    public float CameraSpeed = 20f;
    public float Border = 10f;
    public float zoomSpeed = 5f;
    public float minY = 20f;
    public float maxY = 100f;
    public float height = 25;
    public float gravity = -18;
    

    public bool Pause = false;

    public Vector2 Limit;

    public Transform CameraCentrePoint;


    public void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 CameraPosition = transform.position;

      
            if ((Input.GetKey("w") || Input.mousePosition.y >= Screen.height - Border) && Pause == false)
            {
                CameraPosition.z += CameraSpeed * Time.deltaTime;
            }
            if ((Input.GetKey("s") || Input.mousePosition.y >= Screen.height - Border) && Pause == false)
            {
                CameraPosition.z -= CameraSpeed * Time.deltaTime;
            }
            if ((Input.GetKey("d") || Input.mousePosition.y >= Screen.height - Border) && Pause == false)
            {
                CameraPosition.x += CameraSpeed * Time.deltaTime;
            }
            if ((Input.GetKey("a") || Input.mousePosition.y >= Screen.height - Border) && Pause == false)
            {
                CameraPosition.x -= CameraSpeed * Time.deltaTime;
            }
            if (Input.GetKey("f") && Pause == false)
            {
                CameraPosition.x = CameraCentrePoint.position.x;
                CameraPosition.y = CameraCentrePoint.position.y;
                CameraPosition.z = CameraCentrePoint.position.z;               
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause = !Pause;
            }

            if (Pause)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }


            CameraPosition.x = Mathf.Clamp(CameraPosition.x, -Limit.x, Limit.x);
            CameraPosition.y = Mathf.Clamp(CameraPosition.y, minY, maxY);
            CameraPosition.z = Mathf.Clamp(CameraPosition.z, -Limit.y, Limit.y);

            float scrollMouse = Input.GetAxis("Mouse ScrollWheel");

            CameraPosition.y += -scrollMouse * zoomSpeed * 1000f * Time.deltaTime;

            transform.position = CameraPosition;
     }
}

