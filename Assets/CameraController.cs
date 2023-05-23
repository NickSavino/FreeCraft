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

    void Start()
    {
        camera_position = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        //up
        if (Input.GetKey(KeyCode.W))
        {

            camera_position.y += _cameraSpeed / 50;
        }


        //left
        if (Input.GetKey(KeyCode.A))
        {

            camera_position.x -= _cameraSpeed / 50;
        }


        //down
        if (Input.GetKey(KeyCode.S))
        {

            camera_position.y -= _cameraSpeed / 50;
        }

        //right
        if (Input.GetKey(KeyCode.D))
        {

            camera_position.x += _cameraSpeed / 50;
        }


        //Zoom in
        if (Input.GetAxis("Mouse ScrollWheel") > 0) 
        {
            zoomIn();
        }


        //Zoom out
        if (Input.GetAxis("Mouse ScrollWheel") < 0) 
        {
            zoomOut();
        }

        this.transform.position = camera_position;
    }

    public void zoomIn()
    {
        if (_camera.fieldOfView >= _minFov)
        {
            _camera.fieldOfView -= _zoomSpeed;
        }
    }
    public void zoomOut()
    {
        if (_camera.fieldOfView <= _maxFov)
        {
            _camera.fieldOfView += _zoomSpeed;
        }
    }
}