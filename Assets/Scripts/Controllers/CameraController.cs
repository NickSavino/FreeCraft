using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 camera_position;

    [Header("Camera Settings")]
    [SerializeField] private float _cameraSpeed;
    public Camera _camera;
    [SerializeField] private float _zoomSpeed = 20f;
    [SerializeField] private float _maxFov = 100f;
    [SerializeField] private float _minFov = 1.0f;
    [SerializeField] private float _dragSpeed = 1.0f;
    [SerializeField] private Vector3 _dragOrigin;


    void Start()
    {
        camera_position = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ArrowKeyHandler();
        MiddleMouseButtonHandler();
        ZoomHandler();
        this.transform.position = camera_position;
    }


    public void ZoomHandler()
    {
        //Zoom in
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            ZoomIn();
        }
        //Zoom out
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            ZoomOut();
        }
    }
    public void ZoomIn()
    {
        if (_camera.fieldOfView >= _minFov)
        {
            _camera.fieldOfView -= _zoomSpeed;
        }
    }
    public void ZoomOut()
    {
        if (_camera.fieldOfView <= _maxFov)
        {
            _camera.fieldOfView += _zoomSpeed;
        }
    }


    public void ArrowKeyHandler()
    {
        //up
        if (Input.GetKey(KeyCode.UpArrow) /*|| Input.mousePosition.y >= Camera.main.pixelHeight*/)
        {
            camera_position.y += _cameraSpeed / 50;
        }
        //left
        if (Input.GetKey(KeyCode.LeftArrow) /*|| Input.mousePosition.x <= 0 */)
        {
            camera_position.x -= _cameraSpeed / 50;
        }
        //down
        if (Input.GetKey(KeyCode.DownArrow) /*|| Input.mousePosition.y <= 0 */)
        {
            camera_position.y -= _cameraSpeed / 50;
        }
        //right
        if (Input.GetKey(KeyCode.RightArrow) /*|| Input.mousePosition.x >= Camera.main.pixelWidth */)
        {
            camera_position.x += _cameraSpeed / 50;
        }
    }
    public void MiddleMouseButtonHandler()
    {
        if (Input.GetMouseButtonDown(2))
        {
            //records mouse origin
            _dragOrigin = Input.mousePosition;
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - _dragOrigin);
            Vector3 move = new Vector3(pos.x * _dragSpeed, pos.y * _dragSpeed);
            camera_position.x += move.x;
            camera_position.y += move.y;
        }

    }
}