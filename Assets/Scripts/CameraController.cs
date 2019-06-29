using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float xBounds, yBounds;
    GameObject player;
    [SerializeField]
    float camXBuffer, camYBuffer;
    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        player = GameObject.Find("Player");
        if (!cam.orthographic)
        {
            Debug.LogError("Camera is not set to orthographic");
        }
        camYBuffer = cam.orthographicSize;
        camXBuffer = camYBuffer * cam.aspect;
    }


    void Update()
    {
        float setX = transform.position.x;
        float setY = transform.position.y;
        if (!((Mathf.Abs(player.transform.position.x) + camXBuffer) > xBounds))
            setX = player.transform.position.x;
        if (!((Mathf.Abs(player.transform.position.y) + camYBuffer) > yBounds))
            setY = player.transform.position.y;
        transform.position = new Vector3(setX, setY, transform.position.z);
    }
}
