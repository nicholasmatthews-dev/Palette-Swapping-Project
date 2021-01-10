using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformControls : MonoBehaviour
{
    public float _Sensitivity = 1f;
    public float _ZoomSensitivity = 1f;
    public float _MaxZoom = 2.4f;
    public float _MinZoom = 0.4f;

    private Transform myTransform;
    private float currentZoom = 1f;
    private float currentX = 0f;
    private float currentY = 0f;
    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<Transform>();
        myTransform.position = new Vector3(currentX, currentY);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Pan"))
        {
            currentX += Input.GetAxisRaw("Mouse X") * _Sensitivity;
            currentY += Input.GetAxisRaw("Mouse Y") * _Sensitivity;
        }
        currentZoom *= 1 + (Input.GetAxisRaw("Mouse ScrollWheel") * _ZoomSensitivity);
        if (currentZoom > _MaxZoom)
        {
            currentZoom = _MaxZoom;
        }
        else if (currentZoom < _MinZoom)
        {
            currentZoom = _MinZoom;
        }
        myTransform.position = new Vector3(currentX, currentY);
        myTransform.localScale = new Vector3(currentZoom, currentZoom, 1f);
    }

    private float getZoomCoefficient()
    {
        return (currentZoom - _MinZoom) / (_MaxZoom - _MinZoom) + 0.5f;
    }
}
