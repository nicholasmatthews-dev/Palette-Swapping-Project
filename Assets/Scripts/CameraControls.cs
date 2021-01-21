using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public float _Sensitivity = 1f;
    public float _ZoomSensitivity = 1f;
    public float _MaxZoom = 10f;
    public float _MinZoom = 1.5f;

    private Transform myTransform;
    private Camera myCamera;
    private float currentZoom = 1f;
    private float currentX = 0f;
    private float currentY = 0f;
    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<Transform>();
        myTransform.position = new Vector3(currentX, currentY, -1f);
        myCamera = GetComponent<Camera>();
        myCamera.orthographicSize = currentZoom;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Pan"))
        {
            currentX += Input.GetAxisRaw("Mouse X") * _Sensitivity * -1f * getZoomCoefficient();
            currentY += Input.GetAxisRaw("Mouse Y") * _Sensitivity * -1f * getZoomCoefficient();
        }
        currentZoom *= 1 + -1 * (Input.GetAxisRaw("Mouse ScrollWheel") * _ZoomSensitivity);
        if (currentZoom > _MaxZoom)
        {
            currentZoom = _MaxZoom;
        }
        else if (currentZoom < _MinZoom)
        {
            currentZoom = _MinZoom;
        }
        myTransform.position = new Vector3(currentX, currentY, -1f);
        myCamera.orthographicSize = currentZoom;
    }

    private float getZoomCoefficient()
    {
        return (currentZoom - _MinZoom) / (_MaxZoom - _MinZoom) + 0.5f;
    }

    private void centerView()
    {

    }
}
